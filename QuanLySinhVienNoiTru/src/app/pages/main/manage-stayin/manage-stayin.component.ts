import { HttpClient } from '@angular/common/http';
import { Component, ElementRef, Injector, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { Observable } from 'rxjs';
import 'rxjs/add/observable/combineLatest';
import 'rxjs/add/operator/takeUntil';
import { BaseComponent } from 'src/app/core/base-component';

interface SinhVien {
  ma_sinh_vien: string,
  ho_ten: string,
  noi_sinh: string
}

@Component({
  selector: 'app-manage-stayin',
  templateUrl: './manage-stayin.component.html',
  styleUrls: ['./manage-stayin.component.css']
})


export class ManageStayinComponent extends BaseComponent implements OnInit {
  
  public list_student_stayin: any;
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
  public students: SinhVien[] | any;
  public selectedSinhVien: SinhVien ;
  ngOnInit(): void {
    this.formsearch = this.fb.group({
      'ho_ten': [''],
      'ngay_dang_ky': [''],
      'nam_hoc': ['']
    });
    this.search();
  }
  
  loadPage(page) { 
    this._route.params.subscribe(params => {
      this._api.post('/api/NoiTru/search', { page: page, pageSize: this.pageSize}).takeUntil(this.unsubscribe).subscribe(res => {
        this.list_student_stayin = res.data;
        this.totalItems = res.totalItems;
      }, err => { });       
    });   
  }
  search() {
    this.page = 1;
    this.pageSize = 5;
    this._api.post('/api/NoiTru/search', { page: this.page, pageSize: this.pageSize, ho_ten: this.formsearch.get('ho_ten').value, ngay_dang_ky: this.formsearch.get('ngay_dang_ky').value, nam_hoc: this.formsearch.get('nam_hoc').value }).takeUntil(this.unsubscribe).subscribe(res => {
      this.list_student_stayin = res.data;
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
        'ngay_dang_ky': ['', Validators.required],
        'nam_hoc': ['', Validators.required],
        'hoc_ky': ['', Validators.required],
        'bat_dau_noi_tru': ['', Validators.required],
        'ket_thuc_noi_tru': ['', Validators.required],
      });
      this._api.get('/api/SinhVien/get-all').takeUntil(this.unsubscribe).subscribe((res) => {
        this.students = res;
      });
      this.doneSetupForm = true;
    });
  }
  
  public openUpdateModal(row) {
    this.doneSetupForm = false;
    this.displayAdd = true; 
    this.isCreate = false;
    setTimeout(() => {
      this._api.get('/api/NoiTru/get-by-id/'+ row.ma_noi_tru).takeUntil(this.unsubscribe).subscribe((res:any) => {
        this.student = res; 
        this.formdata = this.fb.group({
          'ma_sinh_vien': [this.student.ma_sinh_vien, Validators.required],
          'ngay_dang_ky': [this.student.ngay_dang_ky, Validators.required],
          'nam_hoc': [this.student.nam_hoc, Validators.required],
          'hoc_ky': [this.student.hoc_ky, Validators.required],
          'bat_dau_noi_tru': [this.student.bat_dau_noi_tru, Validators.required],
          'ket_thuc_noi_tru': [this.student.ket_thuc_noi_tru, Validators.required],
        }); 
        this._api.get('/api/SinhVien/get-all').takeUntil(this.unsubscribe).subscribe((res) => {
          this.students = res;
        });
        this.doneSetupForm = true;
      }); 
    }, 700);
  }
  
  onDelete(ma_noi_tru: string) { 
    this._api.post('/api/NoiTru/delete', { ma_noi_tru: ma_noi_tru }).takeUntil(this.unsubscribe).subscribe(res => {
      alert("Xóa thành công");
      this.search();
    }, err => { console.log(err) });
  }
  
  
  get f() { return this.formdata.controls; }
  onSubmit(value){
    this.submitted = true;
    if (this.formdata.invalid) {
      return;
    } 
    if(this.isCreate) { 
      
      let tmp = {
        ma_sinh_vien:this.selectedSinhVien.ma_sinh_vien,
        ngay_dang_ky:value.ngay_dang_ky,
        nam_hoc:value.nam_hoc,
        hoc_ky:value.hoc_ky,
        bat_dau_noi_tru:value.bat_dau_noi_tru,
        ket_thuc_noi_tru:value.ket_thuc_noi_tru,
      };
      debugger;
      this._api.post('/api/NoiTru/create',tmp).takeUntil(this.unsubscribe).subscribe(res => {
        alert('Thêm thành công');
        this.search();
        this.displayAdd = false;
      });
      
    } else { 
      
      let tmp = {
        ma_sinh_vien:this.selectedSinhVien.ma_sinh_vien,
        ngay_dang_ky:value.ngay_dang_ky,
        nam_hoc:value.nam_hoc,
        hoc_ky:value.hoc_ky,
        bat_dau_noi_tru:value.bat_dau_noi_tru,
        ket_thuc_noi_tru:value.ket_thuc_noi_tru,
        ma_noi_tru:this.student.ma_noi_tru,          
      };
      this._api.post('/api/Noitru/update',tmp).takeUntil(this.unsubscribe).subscribe(res => {
        alert('Cập nhật thành công');
        this.search();
        this.displayAdd = false;
      });
    }
  }
}
