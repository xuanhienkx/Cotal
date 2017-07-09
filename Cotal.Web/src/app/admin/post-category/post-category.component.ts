import { Component, OnInit, ViewChild } from '@angular/core';
import { DataService } from '../../core/services/data.service';
import { NgForm } from '@angular/forms';
import { NotificationService } from '../../core/services/notification.service';
import { UtilityService } from '../../core/services/utility.service';
import { MessageContstants } from '../../core/common/message.constants';
import { ModalDirective } from 'ngx-bootstrap/modal';
import { TreeComponent } from 'angular-tree-component';
import { UploadService } from "app/core/services/upload.service";
import { SystemConstants } from "app/core/common/system.constants";
@Component({
  selector: 'app-post-category',
  templateUrl: './post-category.component.html',
  styleUrls: ['./post-category.component.css']
})
export class PostCategoryComponent implements OnInit {
  @ViewChild('addEditModal') public addEditModal: ModalDirective;
  @ViewChild('image') image;
  @ViewChild(TreeComponent)
  public baseFolder: string = SystemConstants.BASE_API;
  private treeCategory: TreeComponent;
  public filter: string = '';
  public entity: any;
  public functionId: string;
  public _CategoryHierachy: any[];
  public _Categories: any[];
  public editFlg: boolean;
  constructor(
    private _dataService: DataService,
    private notificationService: NotificationService,
    private utilityService: UtilityService, private _uploadService: UploadService) {
  }

  ngOnInit() {
    this.search();
    this.getListForDropdown();
  }
  public onSelectedChange($event) {
    console.log($event);
  }
  public createAlias() {
    this.entity.Alias = this.utilityService.MakeSeoTitle(this.entity.Name);
  }
  //Load data
  public search() {
    this._dataService.get('/api/PostCategory/GetAll?filter =' + this.filter)
      .subscribe((response: any[]) => {
        this._Categories = response;
        this._CategoryHierachy = this.utilityService.Unflatten2(response);
      }, error => this._dataService.handleError(error));
  }
  public getListForDropdown() {
    this._dataService.get('/api/PostCategory/GetAllHierachy')
      .subscribe((response: any[]) => {
        this._Categories = response;
      }, error => this._dataService.handleError(error));
  }
  //Show add form
  public showAdd() {
    this.entity = {};
    this.addEditModal.show();
    this.editFlg = false;
  }
  //Show edit form
  public showEdit(id: string) {
    this._dataService.get('/api/PostCategory/Detail/' + id).subscribe((response: any) => {
      this.entity = response;
      this.editFlg = true;
      this.addEditModal.show();
    }, error => this._dataService.handleError(error));
  }

  //Action delete
  public deleteConfirm(id: string): void {
    this._dataService.delete('/api/PostCategory/Delete/', 'id', id).subscribe((response: any) => {
      this.notificationService.printSuccessMessage(MessageContstants.DELETED_OK_MSG);
      this.search();
    }, error => this._dataService.handleError(error));
  }
  //Click button delete turn on confirm
  public delete(id: string) {
    this.notificationService.printConfirmationDialog(MessageContstants.CONFIRM_DELETE_MSG, () => this.deleteConfirm(id));
  }
  saveChanges(form: NgForm) {
    if (form.valid) {
      let fi = this.image.nativeElement;
      if (fi.files.length > 0) {
        this._uploadService.postWithFile('/api/Upload/SaveImage?type=News', null, fi.files)
          .then((imageUrl: string) => {
            this.entity.Image = imageUrl;
          }).then(() => {
            this.saveData(form.valid);
          });
      }
      else {
        this.saveData(form.valid);
      }
    }
  }
  //Save change for modal popup
  public saveData(valid: boolean) {
    if (valid) {
      if (this.editFlg == false) {
        this._dataService.post('/api/PostCategory/Created', JSON.stringify(this.entity)).subscribe((response: any) => {
          this.search();
          this.addEditModal.hide();
          this.notificationService.printSuccessMessage(MessageContstants.CREATED_OK_MSG);
        }, error => this._dataService.handleError(error));
      }
      else {
        this._dataService.put('/api/PostCategory/Update', JSON.stringify(this.entity)).subscribe((response: any) => {
          this.search();
          this.addEditModal.hide();
          this.notificationService.printSuccessMessage(MessageContstants.UPDATED_OK_MSG);
        }, error => this._dataService.handleError(error));
      }
    }

  }
}
