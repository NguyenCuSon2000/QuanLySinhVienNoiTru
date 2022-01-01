import { HttpClient } from '@angular/common/http';
import { Component, ElementRef, Injector, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { Observable } from 'rxjs';
import 'rxjs/add/observable/combineLatest';
import 'rxjs/add/operator/takeUntil';
import { BaseComponent } from 'src/app/core/base-component';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css']
})
export class DashboardComponent extends BaseComponent implements OnInit {

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
  

}
