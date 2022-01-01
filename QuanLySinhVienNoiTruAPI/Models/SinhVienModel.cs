using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public partial class SinhVienModel
    {
		public string ma_sinh_vien { get; set; }
		public string ho_ten { get; set; }
		public DateTime? ngay_sinh { get; set; }
		public string gioi_tinh { get; set; }
		public string noi_sinh { get; set; }
		public string so_cmnd { get; set; }
		public string so_dien_thoai { get; set; }
		public string email { get; set; }
		public string khoa { get; set; }
		public string lop_hoc { get; set; }
		public string dia_chi_thuong_tru { get; set; }
		public string anh { get; set; }
		public string mat_khau { get; set; }
		public string quyen { get; set; }

		public string token { get; set; }
	}
}
