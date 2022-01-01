using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public partial class NoiTruModel
    {
		public string ma_noi_tru { get; set; }
		public string ma_sinh_vien { get; set; }
		public string ho_ten { get; set; }
		public string so_cmnd { get; set; }
		public string so_dien_thoai { get; set; }
		public string email { get; set; }
		public string lop_hoc { get; set; }
		public string dia_chi_thuong_tru { get; set; }
		public DateTime? ngay_dang_ky { get; set; }
		public string nam_hoc { get; set; }
		public string hoc_ky { get; set; }
		public DateTime? bat_dau_noi_tru { get; set; }
		public DateTime? ket_thuc_noi_tru { get; set; }
	}
}
