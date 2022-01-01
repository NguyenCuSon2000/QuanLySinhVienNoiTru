using DAL;
using Microsoft.Extensions.Configuration;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public partial class NoiTruBusiness : INoiTruBusiness
    {
        private INoiTruRepository _res;
        private string Secret;
        public NoiTruBusiness(INoiTruRepository res, IConfiguration configuration)
        {
            Secret = configuration["AppSettings:Secret"];
            _res = res;
        }

        public bool Create(NoiTruModel model)
        {
            return _res.Create(model);
        }

        public bool Delete(string ma_noi_tru)
        {
            return _res.Delete(ma_noi_tru);
        }

        public NoiTruModel GetDatabyID(string ma_noi_tru)
        {
            return _res.GetDatabyID(ma_noi_tru);
        }

        public List<NoiTruModel> Search(int pageIndex, int pageSize, out long total, string ho_ten, string ngay_dang_ky, string nam_hoc)
        {
            return _res.Search(pageIndex, pageSize, out total, ho_ten, ngay_dang_ky, nam_hoc);

        }

        public bool Update(NoiTruModel model)
        {
            return _res.Update(model);
        }
    }
}
