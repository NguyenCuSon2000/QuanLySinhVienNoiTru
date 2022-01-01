import { Role } from "./Role.enum";

export class SinhVien {
    ma_sinh_vien: string;
    ho_ten: string
    ngay_sinh: Date;
    gioi_tinh;
    noi_sinh: string;
    so_cmnd: string;
    so_dien_thoai: string;
    email: string;
    khoa: string;
    lop_hoc: string;
    dia_chi_thuong_tru: string;
    anh: string;
    mat_khau: string;
    quyen: Role;
    token?: string;
}
