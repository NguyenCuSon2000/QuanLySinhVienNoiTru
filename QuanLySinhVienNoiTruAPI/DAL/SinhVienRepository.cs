using DAL.Helper;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public partial class SinhVienRepository : ISinhVienRepository
    {
        private IDatabaseHelper _dbHelper;
        public SinhVienRepository(IDatabaseHelper dbHelper)
        {
            _dbHelper = dbHelper;
        }
        public SinhVienModel GetSinhVien(string ma_sinh_vien, string mat_khau)
        {
            string msgError = "";
            try
            {
                var dt = _dbHelper.ExecuteSProcedureReturnDataTable(out msgError, "sp_sinh_vien_get_by_masv_matkhau",
                     "@ma_sinh_vien", ma_sinh_vien,
                     "@mat_khau", mat_khau);
                if (!string.IsNullOrEmpty(msgError))
                    throw new Exception(msgError);
                return dt.ConvertTo<SinhVienModel>().FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool Create(SinhVienModel model)
        {
            string msgError = "";
            try
            {
                var result = _dbHelper.ExecuteScalarSProcedureWithTransaction(out msgError, "sp_sinh_vien_create",
                "@ma_sinh_vien", model.ma_sinh_vien,
                "@ho_ten", model.ho_ten,
                "@ngay_sinh", model.ngay_sinh,
                "@gioi_tinh", model.gioi_tinh,
                "@noi_sinh", model.noi_sinh,
                "@so_cmnd", model.so_cmnd,
                "@so_dien_thoai", model.so_dien_thoai,
                "@email", model.email,
                "@khoa", model.khoa,
                "@lop_hoc", model.lop_hoc,
                "@dia_chi_thuong_tru", model.dia_chi_thuong_tru,
                "@anh", model.anh,
                "@mat_khau", model.mat_khau,
                "@quyen", model.quyen);
                if ((result != null && !string.IsNullOrEmpty(result.ToString())) || !string.IsNullOrEmpty(msgError))
                {
                    throw new Exception(Convert.ToString(result) + msgError);
                }
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Delete(string ma_sinh_vien)
        {
            string msgError = "";
            try
            {
                var result = _dbHelper.ExecuteScalarSProcedureWithTransaction(out msgError, "sp_sinh_vien_delete",
                "@ma_sinh_vien", ma_sinh_vien);
                if ((result != null && !string.IsNullOrEmpty(result.ToString())) || !string.IsNullOrEmpty(msgError))
                {
                    throw new Exception(Convert.ToString(result) + msgError);
                }
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<SinhVienModel> GetAll()
        {
            string msgError = "";
            try
            {
                var dt = _dbHelper.ExecuteSProcedureReturnDataTable(out msgError, "sp_sinh_vien_all");
                if (!string.IsNullOrEmpty(msgError))
                    throw new Exception(msgError);
                return dt.ConvertTo<SinhVienModel>().ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public SinhVienModel GetDatabyID(string ma_sinh_vien)
        {
            string msgError = "";
            try
            {
                var dt = _dbHelper.ExecuteSProcedureReturnDataTable(out msgError, "sp_sinh_vien_get_by_id",
                     "@ma_sinh_vien", ma_sinh_vien);
                if (!string.IsNullOrEmpty(msgError))
                    throw new Exception(msgError);
                return dt.ConvertTo<SinhVienModel>().FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<SinhVienModel> Search(int pageIndex, int pageSize, out long total, string ho_ten, string lop_hoc, string dia_chi_thuong_tru)
        {
            string msgError = "";
            total = 0;
            try
            {
                var dt = _dbHelper.ExecuteSProcedureReturnDataTable(out msgError, "sp_sinh_vien_search",
                    "@page_index", pageIndex,
                    "@page_size", pageSize,
                    "@ho_ten", ho_ten,
                    "@lop_hoc", lop_hoc,
                    "@dia_chi_thuong_tru", dia_chi_thuong_tru
                    );
                if (!string.IsNullOrEmpty(msgError))
                    throw new Exception(msgError);
                if (dt.Rows.Count > 0) total = (long)dt.Rows[0]["RecordCount"];
                return dt.ConvertTo<SinhVienModel>().ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Update(SinhVienModel model)
        {
            string msgError = "";
            try
            {
                var result = _dbHelper.ExecuteScalarSProcedureWithTransaction(out msgError, "sp_sinh_vien_update",
                "@ma_sinh_vien", model.ma_sinh_vien,
                "@ho_ten", model.ho_ten,
                "@ngay_sinh", model.ngay_sinh,
                "@gioi_tinh", model.gioi_tinh,
                "@noi_sinh", model.noi_sinh,
                "@so_cmnd", model.so_cmnd,
                "@so_dien_thoai", model.so_dien_thoai,
                "@email", model.email,
                "@khoa", model.khoa,
                "@lop_hoc", model.lop_hoc,
                "@dia_chi_thuong_tru", model.dia_chi_thuong_tru,
                "@anh", model.anh,
                "@mat_khau", model.mat_khau,
                "@quyen", model.quyen);
                if ((result != null && !string.IsNullOrEmpty(result.ToString())) || !string.IsNullOrEmpty(msgError))
                {
                    throw new Exception(Convert.ToString(result) + msgError);
                }
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
