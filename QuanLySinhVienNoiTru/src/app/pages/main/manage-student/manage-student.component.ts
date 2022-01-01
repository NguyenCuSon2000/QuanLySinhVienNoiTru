import { HttpClient } from '@angular/common/http';
import { Component, ElementRef, Injector, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { Observable } from 'rxjs';
import 'rxjs/add/observable/combineLatest';
import 'rxjs/add/operator/takeUntil';
import { BaseComponent } from 'src/app/core/base-component';

@Component({
  selector: 'app-manage-student',
  templateUrl: './manage-student.component.html',
  styleUrls: ['./manage-student.component.css']
})
export class ManageStudentComponent extends BaseComponent implements OnInit {
  public students: any;
  public student: any;
  public page = 1;
  public pageSize = 3;
  public totalItems:any;
  
  public formsearch: any;
  public formdata: any;
  submitted = false;
  isCreate: boolean;
  isEdit: boolean = false;
  constructor(private fb: FormBuilder, private httpclient: HttpClient, injector: Injector, private route: ActivatedRoute, private router: Router) {
    super(injector);
  }
  
  ngOnInit(): void {
    this.formsearch = this.fb.group({
      'ho_ten': [''],
      'lop_hoc': [''],
      'dia_chi_thuong_tru': ['']
    });
    this.search();
  }
  
  loadPage(page) { 
    this._route.params.subscribe(params => {
      this._api.post('/api/SinhVien/search', { page: page, pageSize: this.pageSize}).takeUntil(this.unsubscribe).subscribe(res => {
        this.students = res.data;
        this.totalItems = res.totalItems;
      }, err => { });       
    });   
  }
  search() {
    this.page = 1;
    this.pageSize = 5;
    this._api.post('/api/SinhVien/search', { page: this.page, pageSize: this.pageSize, ho_ten: this.formsearch.get('ho_ten').value, lop_hoc: this.formsearch.get('lop_hoc').value, dia_chi_thuong_tru: this.formsearch.get('dia_chi_thuong_tru').value }).takeUntil(this.unsubscribe).subscribe(res => {
      this.students = res.data;
      this.totalItems = res.totalItems;
      this.pageSize = res.pageSize;
    });
  }
  
  public doneSetupForm: any; 
  displayAdd: boolean = false;
  displayUpdate: boolean = false;
  showAdd() {
    this.displayAdd = true
    this.doneSetupForm = false;
    this.displayUpdate = true;
    this.isCreate = true;
    this.student = null;
    setTimeout(() => {
      this.formdata = this.fb.group({
        'ma_sinh_vien': ['', Validators.required],
        'ho_ten': ['', Validators.required],
        'ngay_sinh': ['', Validators.required],
        'gioi_tinh': ['', Validators.required],
        'noi_sinh': ['', Validators.required],
        'so_cmnd': ['', Validators.required],
        'so_dien_thoai': ['', Validators.required],
        'email': [''],
        'khoa': ['', Validators.required],
        'lop_hoc': ['', Validators.required],
        'dia_chi_thuong_tru': ['', Validators.required],
        'anh': [''],
        'mat_khau': ['', Validators.required],
        'quyen': ['', Validators.required],
      });
      this.doneSetupForm = true;
    });
  }
  
  public openUpdateModal(row) {
    this.doneSetupForm = false;
    this.displayAdd = true; 
    this.isCreate = false;
    setTimeout(() => {
      this._api.get('/api/SinhVien/get-by-id/'+ row.ma_sinh_vien).takeUntil(this.unsubscribe).subscribe((res:any) => {
        this.student = res; 
        this.formdata = this.fb.group({
          'ma_sinh_vien': [this.student.ma_sinh_vien, Validators.required],
          'ho_ten': [this.student.ho_ten, Validators.required],
          'ngay_sinh': [this.student.ngay_sinh, Validators.required],
          'gioi_tinh': [this.student.gioi_tinh, Validators.required],
          'noi_sinh': [this.student.noi_sinh, Validators.required],
          'so_cmnd': [this.student.so_cmnd, Validators.required],
          'so_dien_thoai': [this.student.so_dien_thoai, Validators.required],
          'email': [this.student.email, Validators.required],
          'khoa': [this.student.khoa, Validators.required],
          'lop_hoc': [this.student.lop_hoc, Validators.required],
          'dia_chi_thuong_tru': [this.student.dia_chi_thuong_tru, Validators.required],
          'anh': [this.student.anh, Validators.required],
          'mat_khau': [this.student.mat_khau, Validators.required],
          'quyen': [this.student.quyen, Validators.required],
        }); 
        this.doneSetupForm = true;
      }); 
    }, 700);
  }
  
  onDelete(ma_sinh_vien: string) { 
    this._api.post('/api/SinhVien/delete', { ma_sinh_vien: ma_sinh_vien }).takeUntil(this.unsubscribe).subscribe(res => {
      alert("Xóa thành công");
      this.search();
    }, err => { console.log(err) });
  }
  
  anh: any = null;
  onChange(event: any) {
    this.anh = event.target.files[0];
  }
  upload(file?: any): Observable<any> {
    const apiURL = 'http://localhost:7383/api/SinhVien/upload';
    const formData = new FormData();
    formData.append("file", file, file.name);
    return this.httpclient.post(apiURL, formData).pipe();
  }
  
  get f() { return this.formdata.controls; }
  onSubmit(value){
    this.submitted = true;
    if (this.formdata.invalid) {
      return;
    } 
    if(this.isCreate) { 
      this.upload(this.anh).subscribe(res => { 
        let tmp = {
          ma_sinh_vien:value.ma_sinh_vien,
          ho_ten:value.ho_ten,
          ngay_sinh:value.ngay_sinh,
          gioi_tinh:value.gioi_tinh,
          noi_sinh:value.noi_sinh,
          so_cmnd:value.so_cmnd,
          so_dien_thoai:value.so_dien_thoai,
          email:value.email,
          khoa:value.khoa,
          lop_hoc:value.lop_hoc,
          dia_chi_thuong_tru:value.dia_chi_thuong_tru,
          anh:res.filePath,
          mat_khau:value.mat_khau,
          quyen:value.quyen,
        };
        this._api.post('/api/SinhVien/create',tmp).takeUntil(this.unsubscribe).subscribe(res => {
          alert('Thêm thành công');
          this.search();
          this.displayAdd = false;
        });
      });
    } else { 
      this.upload(this.anh).subscribe(res => { 
        let tmp = {
          ho_ten:value.ho_ten,
          ngay_sinh:value.ngay_sinh,
          gioi_tinh:value.gioi_tinh,
          noi_sinh:value.noi_sinh,
          so_cmnd:value.so_cmnd,
          so_dien_thoai:value.so_dien_thoai,
          email:value.email,
          khoa:value.khoa,
          lop_hoc:value.lop_hoc,
          dia_chi_thuong_tru:value.dia_chi_thuong_tru,
          anh:res.filePath,
          mat_khau:value.mat_khau,
          quyen:value.quyen,
          ma_sinh_vien:this.student.ma_sinh_vien,          
        };
        this._api.post('/api/SinhVien/update',tmp).takeUntil(this.unsubscribe).subscribe(res => {
          alert('Cập nhật thành công');
          this.search();
          this.displayAdd = false;
        });
      });
    }
  }
  
  
}
