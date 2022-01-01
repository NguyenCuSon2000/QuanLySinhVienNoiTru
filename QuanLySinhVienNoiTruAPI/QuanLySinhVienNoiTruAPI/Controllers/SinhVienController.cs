using BLL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace QuanLySinhVienNoiTruAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SinhVienController : ControllerBase
    {
        private ISinhVienBusiness _sinhvienBusiness;
        private string _path;
        public SinhVienController(ISinhVienBusiness sinhvienBusiness, IConfiguration configuration)
        {
            _sinhvienBusiness = sinhvienBusiness;
            _path = configuration["AppSettings:PATH"];
        }

        [NonAction]
        public string SaveFileFromBase64String(string RelativePathFileName, string dataFromBase64String)
        {
            if (dataFromBase64String.Contains("base64,"))
            {
                dataFromBase64String = dataFromBase64String.Substring(dataFromBase64String.IndexOf("base64,", 0) + 7);
            }
            return WriteFileToAuthAccessFolder(RelativePathFileName, dataFromBase64String);
        }

        [NonAction]
        public string WriteFileToAuthAccessFolder(string RelativePathFileName, string base64StringData)
        {
            try
            {
                string result = "";
                string serverRootPathFolder = _path;
                string fullPathFile = $@"{serverRootPathFolder}\{RelativePathFileName}";
                string fullPathFolder = System.IO.Path.GetDirectoryName(fullPathFile);
                if (!Directory.Exists(fullPathFolder))
                    Directory.CreateDirectory(fullPathFolder);
                System.IO.File.WriteAllBytes(fullPathFile, Convert.FromBase64String(base64StringData));
                return result;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        [Route("upload")]
        [HttpPost, DisableRequestSizeLimit]
        public async Task<IActionResult> Upload(IFormFile file)
        {
            try
            {
                if (file.Length > 0)
                {
                    string filePath = $"{file.FileName}";
                    var fullPath = CreatePathFile(filePath, _path);
                    using (var fileStream = new FileStream(fullPath, FileMode.Create))
                    {
                        await file.CopyToAsync(fileStream);
                    }
                    return Ok(new { filePath });
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception)
            {
                return StatusCode(500, "Không tìm thây");
            }
        }

        [NonAction]
        private string CreatePathFile(string RelativePathFileName, string path)
        {
            try
            {
                string serverRootPathFolder = path;
                string fullPathFile = $@"{serverRootPathFolder}\{RelativePathFileName}";
                string fullPathFolder = System.IO.Path.GetDirectoryName(fullPathFile);
                if (!Directory.Exists(fullPathFolder))
                    Directory.CreateDirectory(fullPathFolder);
                return fullPathFile;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

      
        [HttpPost("authenticate")]
        public IActionResult Login([FromBody] AuthenticateModel model)
        {
            var user = _sinhvienBusiness.Authenticate(model.ma_sinh_vien, model.mat_khau);

            if (user == null)
                return BadRequest(new { message = "Username or password is incorrect" });
            return Ok(user);
        }
        [Route("get-all")]
        [HttpGet]
        public IEnumerable<SinhVienModel> GetAll()
        {
            return _sinhvienBusiness.GetAll();

        }

        [Route("create")]
        [HttpPost]
        public SinhVienModel Create([FromBody] SinhVienModel model)
        {
            _sinhvienBusiness.Create(model);
            return model;
        }

        [Route("update")]
        [HttpPost]
        public SinhVienModel Update([FromBody] SinhVienModel model)
        {
            _sinhvienBusiness.Update(model);
            return model;
        }


        [Route("get-by-id/{id}")]
        [HttpGet]
        public SinhVienModel GetDatabyID(string id)
        {
            return _sinhvienBusiness.GetDatabyID(id);
        }

        [Route("delete")]
        [HttpPost]
        public IActionResult DeleteProduct([FromBody] Dictionary<string, object> formData)
        {
            string ma_sinh_vien = "";
            if (formData.Keys.Contains("ma_sinh_vien") && !string.IsNullOrEmpty(Convert.ToString(formData["ma_sinh_vien"]))) { ma_sinh_vien = Convert.ToString(formData["ma_sinh_vien"]); }
            _sinhvienBusiness.Delete(ma_sinh_vien);
            return Ok();
        }

        [Route("search")]
        [HttpPost]
        public ResponseModel Search([FromBody] Dictionary<string, object> formData)
        {
            var response = new ResponseModel();
            try
            {
                var page = int.Parse(formData["page"].ToString());
                var pageSize = int.Parse(formData["pageSize"].ToString());
                string ho_ten = "";
                if (formData.Keys.Contains("ho_ten") && !string.IsNullOrEmpty(Convert.ToString(formData["ho_ten"]))) { ho_ten = Convert.ToString(formData["ho_ten"]); }
                string lop_hoc = "";
                if (formData.Keys.Contains("lop_hoc") && !string.IsNullOrEmpty(Convert.ToString(formData["lop_hoc"]))) { lop_hoc = Convert.ToString(formData["lop_hoc"]); }
                string dia_chi_thuong_tru = "";
                if (formData.Keys.Contains("dia_chi_thuong_tru") && !string.IsNullOrEmpty(Convert.ToString(formData["dia_chi_thuong_tru"]))) { dia_chi_thuong_tru = Convert.ToString(formData["dia_chi_thuong_tru"]); }
                long total = 0;
                var data = _sinhvienBusiness.Search(page, pageSize, out total, ho_ten,lop_hoc, dia_chi_thuong_tru);
                response.TotalItems = total;
                response.Data = data;
                response.Page = page;
                response.PageSize = pageSize;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return response;
        }
    }
}
