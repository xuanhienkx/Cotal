import { Component, OnInit, ViewChild } from '@angular/core';
import { ModalDirective } from "ngx-bootstrap";
import { SystemConstants } from "app/core/common/system.constants";
import { AuthenService } from "app/core/services/authen.service";
import { DataService } from "app/core/services/data.service";
import { NotificationService } from "app/core/services/notification.service";
import { UtilityService } from "app/core/services/utility.service";
import { MessageContstants } from "app/core/common/message.constants";
import { NgForm } from "@angular/forms";

@Component({
  selector: 'app-page',
  templateUrl: './page.component.html',
  styleUrls: ['./page.component.css']
})
export class PageComponent implements OnInit {
@ViewChild('addEditModal') public addEditModal: ModalDirective;
  //@ViewChild("image") image; 
  public baseFolder: string = SystemConstants.BASE_API;
  public entity: any;
  public totalRow: number;
  public pageIndex: number = 1;
  public pageSize: number = 20;
  public pageDisplay: number = 10;
  public filterKeyword: string = ''; 
  public pages: any[];  
  constructor(public _authenService: AuthenService,
    private _dataService: DataService,
    private notificationService: NotificationService,
    private utilityService: UtilityService) { }

  ngOnInit() {
    this.search();
  }
   public createAlias() {
    this.entity.Alias = this.utilityService.MakeSeoTitle(this.entity.Name);
  }//GET /api/Page/GetAll
 public search() {
    this._dataService.get('/api/Page/GetAll?page=' + this.pageIndex + '&pageSize=' + this.pageSize + '&keyword=' + this.filterKeyword)
      .subscribe((response: any) => {
        this.pages = response.Data;
        this.pageIndex = response.PageNumber;
        this.totalRow = response.TotalEntityCount;
      }, error => this._dataService.handleError(error));
  }
  public reset() {
    this.filterKeyword = ''; 
    this.search();
  }
   public pageChanged(event: any): void {
    this.pageIndex = event.page;
    this.search();
  }

  public keyupHandlerContentFunction(e: any) {
    this.entity.Content = e;
  }
   //Show add form
  public showAdd() {
    this.entity = { Content: '' };
    this.addEditModal.show();
  }
  //Show edit form
  public showEdit(id: string) {
    this._dataService.get('/api/Page/Detail/' + id).subscribe((response: any) => {
      this.entity = response;
      this.addEditModal.show();
    }, error => this._dataService.handleError(error));
  }
    public delete(id: string) {
    this.notificationService.printConfirmationDialog(MessageContstants.CONFIRM_DELETE_MSG, () => {
      this._dataService.delete('/api/Page', 'id', id).subscribe((response: any) => {
        this.notificationService.printSuccessMessage(MessageContstants.DELETED_OK_MSG);
        this.search();
      }, error => this._dataService.handleError(error));
    });
  }
  private saveChange(form: NgForm) {
    if (this.entity.Id == undefined) {
      this._dataService.post('/api/Page', JSON.stringify(this.entity))
        .subscribe((response: any) => {
          this.reset();
          this.addEditModal.hide(); 
          this.notificationService.printSuccessMessage(MessageContstants.CREATED_OK_MSG);
        }, error => this._dataService.handleError(error));
    }
    else {
      this._dataService.put('/api/Page', JSON.stringify(this.entity))
        .subscribe((response: any) => {
          this.reset();
          this.addEditModal.hide(); 
          this.notificationService.printSuccessMessage(MessageContstants.UPDATED_OK_MSG);
        }, error => this._dataService.handleError(error));
    }
  }
}
