using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public partial interface INoiTruRepository
    {
        NoiTruModel GetDatabyID(string ma_sinh_vien);
        bool Create(NoiTruModel model);
        bool Update(NoiTruModel model);
        bool Delete(string ma_sinh_vien);
        List<NoiTruModel> Search(int pageIndex, int pageSize, out long total, string ho_ten, string ngay_dang_ky, string nam_hoc);
    }
}
