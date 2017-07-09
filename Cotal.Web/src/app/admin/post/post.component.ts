import { Component, OnInit, ViewChild } from '@angular/core';
import { ModalDirective } from "ngx-bootstrap";
import { SystemConstants } from "app/core/common/system.constants";
import { AuthenService } from "app/core/services/authen.service";
import { DataService } from "app/core/services/data.service";
import { NotificationService } from "app/core/services/notification.service";
import { UtilityService } from "app/core/services/utility.service";
import { UploadService } from "app/core/services/upload.service";
import { MessageContstants } from "app/core/common/message.constants"; 
import { NgForm } from "@angular/forms";
@Component({
  selector: 'app-post',
  templateUrl: './post.component.html',
  styleUrls: ['./post.component.css']
})
export class PostComponent implements OnInit {
 @ViewChild('addEditModal') public addEditModal: ModalDirective;
  //@ViewChild("image") image;
  @ViewChild('image') image;
  public baseFolder: string = SystemConstants.BASE_API;
  public entity: any;
  public totalRow: number;
  public pageIndex: number = 1;
  public pageSize: number = 20;
  public pageDisplay: number = 10;
  public filterKeyword: string = '';
  public filterCategoryId: number;
  public posts: any[];
  public postCategories: any[];
  public checkedItems: any[];

  /*Product manage */
  public imageEntity: any = {};
  public productImages: any = [];
  constructor(public _authenService: AuthenService,
    private _dataService: DataService,
    private notificationService: NotificationService,
    private utilityService: UtilityService, private _uploadService: UploadService) { }

  ngOnInit() {
    this.search();
    this.loadPostCategories();
  }
 public createAlias() {
    this.entity.Alias = this.utilityService.MakeSeoTitle(this.entity.Name);
  }
  public search() {
    this._dataService.get('/api/Post/GetAll?page=' + this.pageIndex + '&pageSize=' + this.pageSize + '&keyword=' + this.filterKeyword + '&categoryId=' + this.filterCategoryId)
      .subscribe((response: any) => {
        this.posts = response.Data;
        this.pageIndex = response.PageNumber;
        this.totalRow = response.TotalEntityCount;
      }, error => this._dataService.handleError(error));
  }
  public reset() {
    this.filterKeyword = '';
    this.filterCategoryId = null;
    this.search();
  }
  private loadPostCategories() {
    this._dataService.get('/api/PostCategory/GetAllHierachy').subscribe((response: any[]) => {
      this.postCategories = response;
    }, error => this._dataService.handleError(error));
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
    this._dataService.get('/api/Post/Detail/' + id).subscribe((response: any) => {
      this.entity = response;
      this.addEditModal.show();
    }, error => this._dataService.handleError(error));
  }
    public delete(id: string) {
    this.notificationService.printConfirmationDialog(MessageContstants.CONFIRM_DELETE_MSG, () => {
      this._dataService.delete('/api/Post/Delete', 'id', id).subscribe((response: any) => {
        this.notificationService.printSuccessMessage(MessageContstants.DELETED_OK_MSG);
        this.search();
      }, error => this._dataService.handleError(error));
    });
  }

  public deleteMulti() {
    this.checkedItems = this.posts.filter(x => x.Checked);
    var checkedIds = [];
    for (var i = 0; i < this.checkedItems.length; ++i)
      checkedIds.push(this.checkedItems[i]["Id"]);

    this.notificationService.printConfirmationDialog(MessageContstants.CONFIRM_DELETE_MSG, () => {
      this._dataService.delete('/api/POST/deletemulti', 'checkedProducts', JSON.stringify(checkedIds)).subscribe((response: any) => {
        this.notificationService.printSuccessMessage(MessageContstants.DELETED_OK_MSG);
        this.search();
      }, error => this._dataService.handleError(error));
    });
  }
  saveChange(form: NgForm) {
    if (form.valid) { 
      let fi = this.image.nativeElement;
      if (fi.files.length > 0) {
        this._uploadService.postWithFile('/api/Upload/SaveImage?type=new', null, fi.files)
          .then((imageUrl: string) => { 
            this.entity.Image = imageUrl;
          }).then(() => {
            this.saveData(form);
          });
      }
      else {
        this.saveData(form);
      }
    }
  }
  private saveData(form: NgForm) {
    if (this.entity.Id == undefined) {
      this._dataService.post('/api/Post/Created', JSON.stringify(this.entity))
        .subscribe((response: any) => {
          this.reset();
          this.addEditModal.hide(); 
          this.notificationService.printSuccessMessage(MessageContstants.CREATED_OK_MSG);
        }, error => this._dataService.handleError(error));
    }
    else {
      this._dataService.put('/api/Post/Update', JSON.stringify(this.entity))
        .subscribe((response: any) => {
          this.reset();
          this.addEditModal.hide(); 
          this.notificationService.printSuccessMessage(MessageContstants.UPDATED_OK_MSG);
        }, error => this._dataService.handleError(error));
    }
  }
}
