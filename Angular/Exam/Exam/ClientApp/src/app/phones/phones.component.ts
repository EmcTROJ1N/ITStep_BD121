import {Component} from '@angular/core';
import FetchController from "../../FetchController";
import {CrudColumn, DataType} from "../crud-api/crud-api.component";

@Component({
  selector: 'app-phones',
  templateUrl: './phones.component.html',
  styleUrls: ['./phones.component.css']
})
export class PhonesComponent {
  ColDefs: CrudColumn[] = [
    {fieldName: "id", headerCell: "Id", type: DataType.Number},
    {fieldName: "phoneNumber", headerCell: "Phone number", type: DataType.Text},
    {fieldName: "fullName", headerCell: "Full name", type: DataType.Text},
    {fieldName: "creationDate", headerCell: "Creation date", type: DataType.Date},
  ]
  Phones: PhoneNumber[] = []
  Controller: FetchController = new FetchController("api/Phones");

  async ngOnInit() {
    let response = await this.Controller.OnGetAsync();
    this.Phones = <PhoneNumber[]>(response.map((phone: any) => {
      phone.creationDate = new Date(phone.creationDate);
      return phone;
    }));
  }

  onPhoneCreated(phone: any) {
    this.Controller.OnPostAsync(phone).then(response => {
      this.Phones.push(response);
    }).catch(err => alert(err));
  }

  onPhoneDeleted(id: number | string) {
    this.Controller.OnDeleteAsync(id).then(response => {
      if (!response.ok) {
        alert(response.statusText);
      }
    }).catch(err => alert(err));
  }

  onPhoneEdited(e: any) {
    console.log(e);
    if (e.cancelled)
      return;
    this.Controller.OnPutAsync(e.newValue.id, e.newValue).then(response => {
      if (!response.ok) {
        alert(response.statusText)
      }
    }).catch(err => alert(err));
  }

}

export class PhoneNumber {
  id: number | undefined;
  phoneNumber: string | undefined;
  fullName: string | undefined;
  creationDate: Date | undefined;
  editingNow: boolean | undefined;
}
