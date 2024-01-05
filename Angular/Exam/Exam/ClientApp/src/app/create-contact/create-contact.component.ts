import {Component, Inject} from '@angular/core';
import {MAT_DIALOG_DATA, MatDialogRef} from "@angular/material/dialog";
import {DialogData} from "../dialog/dialog.component";
import {FormBuilder, Validators} from "@angular/forms";
import FetchController from "../../FetchController";
import {PhoneNumber} from "../phones/phones.component";
import {Category} from "../categories/categories.component";

@Component({
  selector: 'app-create-contact',
  templateUrl: './create-contact.component.html',
  styleUrls: ['./create-contact.component.css']
})
export class CreateContactComponent {
  newData: any = {};
  isLinear: boolean = false;

  PhonesController: FetchController = new FetchController("api/Phones");
  CategoriesController: FetchController = new FetchController("api/Categories");

  phones: PhoneNumber[] = [];
  categories: Category[] = [];

  constructor(
    private _formBuilder: FormBuilder,
    public dialogRef: MatDialogRef<CreateContactComponent>,
    @Inject(MAT_DIALOG_DATA) public data: DialogData,
  ) {
    this.CategoriesController.OnGetAsync().then(response =>
      this.categories = <Category[]>response);
    this.PhonesController.OnGetAsync().then(response =>
      this.phones = <PhoneNumber[]>response);
  }

  firstFormGroup = this._formBuilder.group({
    firstCtrl: ['', Validators.required],
  });
  secondFormGroup = this._formBuilder.group({
    secondCtrl: ['', Validators.required],
  });
  thirdFormGroup = this._formBuilder.group({
    thirdCtrl: ['', Validators.required],
  });
  fourthFormGroup = this._formBuilder.group({
    fourthCtrl: ['', Validators.required],
  });
  fifthFormGroup = this._formBuilder.group({
    fifthCtrl: ['', Validators.required],
  });
  sixthFormGroup = this._formBuilder.group({
    sixthCtrl: ['', Validators.required],
  });
  seventhFormGroup = this._formBuilder.group({
    seventhCtrl: ['', Validators.required],
  });
  eighthFormGroup = this._formBuilder.group({
    eighthCtrl: ['', Validators.required],
  });

  onNoClick(): void {
    this.dialogRef.close();
  }
}
