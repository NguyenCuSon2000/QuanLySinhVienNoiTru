using BLL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuanLySinhVienNoiTruAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NoiTruController : ControllerBase
    {
        private INoiTruBusiness _noitruBusiness;
        private string _path;
        public NoiTruController(INoiTruBusiness noitruBusiness, IConfiguration configuration)
        {
            _noitruBusiness = noitruBusiness;
            _path = configuration["AppSettings:PATH"];
        }

        [Route("create")]
        [HttpPost]
        public NoiTruModel Create([FromBody] NoiTruModel model)
        {
            model.ma_noi_tru = Guid.NewGuid().ToString();
            _noitruBusiness.Create(model);
            return model;
        }

        [Route("update")]
        [HttpPost]
        public NoiTruModel Update([FromBody] NoiTruModel model)
        {
            _noitruBusiness.Update(model);
            return model;
        }


        [Route("get-by-id/{id}")]
        [HttpGet]
        public NoiTruModel GetDatabyID(string id)
        {
            return _noitruBusiness.GetDatabyID(id);
        }

        [Route("delete")]
        [HttpPost]
        public IActionResult DeleteProduct([FromBody] Dictionary<string, object> formData)
        {
            string ma_noi_tru = "";
            if (formData.Keys.Contains("ma_noi_tru") && !string.IsNullOrEmpty(Convert.ToString(formData["ma_noi_tru"]))) { ma_noi_tru = Convert.ToString(formData["ma_noi_tru"]); }
            _noitruBusiness.Delete(ma_noi_tru);
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
                string ngay_dang_ky = "";
                if (formData.Keys.Contains("ngay_dang_ky") && !string.IsNullOrEmpty(Convert.ToString(formData["ngay_dang_ky"]))) { ngay_dang_ky = Convert.ToString(formData["ngay_dang_ky"]); }
                string nam_hoc = "";
                if (formData.Keys.Contains("nam_hoc") && !string.IsNullOrEmpty(Convert.ToString(formData["nam_hoc"]))) { nam_hoc = Convert.ToString(formData["nam_hoc"]); }
                long total = 0;
                var data = _noitruBusiness.Search(page, pageSize, out total, ho_ten, ngay_dang_ky, nam_hoc);
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
