using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public partial interface ISinhVienRepository
    {
        SinhVienModel GetSinhVien(string ma_sinh_vien, string mat_khau);
        List<SinhVienModel> GetAll();
        SinhVienModel GetDatabyID(string ma_sinh_vien);
        bool Create(SinhVienModel model);
        bool Update(SinhVienModel model);
        bool Delete(string ma_sinh_vien);
        List<SinhVienModel> Search(int pageIndex, int pageSize, out long total, string ho_ten, string lop_hoc, string dia_chi_thuong_tru);
    }
}
