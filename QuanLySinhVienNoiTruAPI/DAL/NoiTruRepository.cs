using DAL.Helper;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public partial class NoiTruRepository : INoiTruRepository
    {
        private IDatabaseHelper _dbHelper;
        public NoiTruRepository(IDatabaseHelper dbHelper)
        {
            _dbHelper = dbHelper;
        }
        public bool Create(NoiTruModel model)
        {
            string msgError = "";
            try
            {
                var result = _dbHelper.ExecuteScalarSProcedureWithTransaction(out msgError, "sp_noi_tru_create",
                "@ma_noi_tru", model.ma_noi_tru,
                "@ma_sinh_vien", model.ma_sinh_vien,
                "@ngay_dang_ky", model.ngay_dang_ky,
                "@nam_hoc", model.nam_hoc,
                "@hoc_ky", model.hoc_ky,
                "@bat_dau_noi_tru", model.bat_dau_noi_tru,
                "@ket_thuc_noi_tru", model.ket_thuc_noi_tru);
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

        public bool Delete(string ma_noi_tru)
        {
            string msgError = "";
            try
            {
                var result = _dbHelper.ExecuteScalarSProcedureWithTransaction(out msgError, "sp_noi_tru_delete",
                "@ma_noi_tru", ma_noi_tru);
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

        public NoiTruModel GetDatabyID(string ma_noi_tru)
        {
            string msgError = "";
            try
            {
                var dt = _dbHelper.ExecuteSProcedureReturnDataTable(out msgError, "sp_noi_tru_get_by_id",
                     "@ma_noi_tru", ma_noi_tru);
                if (!string.IsNullOrEmpty(msgError))
                    throw new Exception(msgError);
                return dt.ConvertTo<NoiTruModel>().FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<NoiTruModel> Search(int pageIndex, int pageSize, out long total, string ho_ten, string ngay_dang_ky, string nam_hoc)
        {
            string msgError = "";
            total = 0;
            try
            {
                var dt = _dbHelper.ExecuteSProcedureReturnDataTable(out msgError, "sp_noi_tru_search",
                    "@page_index", pageIndex,
                    "@page_size", pageSize,
                    "@ho_ten", ho_ten,
                    "@ngay_dang_ky", ngay_dang_ky,
                    "@nam_hoc", nam_hoc
                    );
                if (!string.IsNullOrEmpty(msgError))
                    throw new Exception(msgError);
                if (dt.Rows.Count > 0) total = (long)dt.Rows[0]["RecordCount"];
                return dt.ConvertTo<NoiTruModel>().ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Update(NoiTruModel model)
        {
            string msgError = "";
            try
            {
                var result = _dbHelper.ExecuteScalarSProcedureWithTransaction(out msgError, "sp_noi_tru_update",
               "@ma_noi_tru", model.ma_noi_tru,
                "@ma_sinh_vien", model.ma_sinh_vien,
                "@ngay_dang_ky", model.ngay_dang_ky,
                "@nam_hoc", model.nam_hoc,
                "@hoc_ky", model.hoc_ky,
                "@bat_dau_noi_tru", model.bat_dau_noi_tru,
                "@ket_thuc_noi_tru", model.ket_thuc_noi_tru);
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
