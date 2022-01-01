using DAL;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public partial class SinhVienBusiness : ISinhVienBusiness
    {
        private ISinhVienRepository _res;
        private string Secret;
        public SinhVienBusiness(ISinhVienRepository res, IConfiguration configuration)
        {
            Secret = configuration["AppSettings:Secret"];
            _res = res;
        }

        public SinhVienModel Authenticate(string ma_sinh_vien, string mat_khau)
        {
            var user = _res.GetSinhVien(ma_sinh_vien, mat_khau);
            // return null if user not found
            if (user == null)
                return null;

            // authentication successful so generate jwt token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.ho_ten.ToString(), user.quyen.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            user.token = tokenHandler.WriteToken(token);

            return user;

        }
        public bool Create(SinhVienModel model)
        {
            return _res.Create(model);
        }

        public bool Delete(string ma_sinh_vien)
        {
            return _res.Delete(ma_sinh_vien);
        }

        public List<SinhVienModel> GetAll()
        {
            return _res.GetAll();
        }

        public SinhVienModel GetDatabyID(string ma_sinh_vien)
        {
            return _res.GetDatabyID(ma_sinh_vien);
        }

        public List<SinhVienModel> Search(int pageIndex, int pageSize, out long total, string ho_ten, string lop_hoc, string dia_chi_thuong_tru)
        {
            return _res.Search(pageIndex, pageSize, out total, ho_ten, lop_hoc, dia_chi_thuong_tru);

        }

        public bool Update(SinhVienModel model)
        {
            return _res.Update(model);
        }
    }
}
