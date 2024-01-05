import { Component } from '@angular/core';
import FetchController from "../../FetchController";
import {DataType} from "../crud-api/crud-api.component";
import {DialogComponent} from "../dialog/dialog.component";
import {MatDialog} from "@angular/material/dialog";
import {MatSnackBar} from "@angular/material/snack-bar";
import {CreateContactComponent} from "../create-contact/create-contact.component";

@Component({
  selector: 'app-contacts-list',
  templateUrl: './contacts-list.component.html',
  styleUrls: ['./contacts-list.component.css']
})
export class ContactsListComponent {
  contacts: Contact[] = [];
  OldVerContact: Contact | undefined;
  ContactForEditing: Contact | undefined;

  ContactsController: FetchController = new FetchController("api/Contacts");
  PhonesController: FetchController = new FetchController("api/Phones");
  CategoriesController: FetchController = new FetchController("api/Categories");
  Procedures = new FetchController("api/Procedures");

  Fields: ContactField[] = [
    {fieldName: "address", header: "Adress", type: DataType.Text},
    {fieldName: "firstName", header: "First name", type: DataType.Text},
    {fieldName: "lastName", header: "Last name", type: DataType.Text},
    {fieldName: "email", header: "Email", type: DataType.Email},
    {fieldName: "notes", header: "Notes", type: DataType.Text},
    {fieldName: "phoneNumber", header: "Phone", type: DataType.Text},
    {fieldName: "categoryName", header: "category", type: DataType.Text}
  ]

  constructor(public dialog: MatDialog,
              private _snackBar: MatSnackBar)
  {}

  async ngOnInit() {
    this.contacts = <Contact[]>await this.Procedures.OnGetAsync();
  }

  async ngOnChanges() {
    this.contacts = <Contact[]>await this.Procedures.OnGetAsync();
  }

  onShowContact($event: any, id: number) {
    $event.preventDefault();

    let contact = {};
    let oldContact = {};
    Object.assign(contact, this.contacts.filter(contact => contact.id == id)[0]);
    Object.assign(oldContact, contact);
    this.ContactForEditing = <Contact>contact;
    this.OldVerContact = <Contact>oldContact;

    if (this.ContactForEditing != null)
      this.ContactForEditing.editableFields = [];
  }

  onEditFieldBegin(fieldName: keyof Contact) {
    this.ContactForEditing?.editableFields.push(fieldName.toString());
  }

  onEditFieldEnd(cancelled: boolean, fieldName: string) {

    let contact: any = this.contacts.filter(contact => contact.id == this.ContactForEditing?.id)[0];
    this.ContactForEditing?.editableFields.splice(this.ContactForEditing?.editableFields.indexOf(fieldName), 1);
    let anyContact: any = this.ContactForEditing;
    let anyOldContact: any = this.OldVerContact;
    if (!cancelled) {
      contact[fieldName] = anyContact[fieldName];
      //fetch
    }
    else if (this.ContactForEditing != null) {
      Object.assign(this.ContactForEditing, contact);
    }

  }

  onDelete(id: number) {

    this.ContactsController.OnDeleteAsync(id).then(response => {
      if (response.ok)
        this.contacts = this.contacts.filter(contact => contact.id != id);
      else
        alert(response.statusText);
    }).catch(err => alert(err));
    // fetch
  }

  onCloseContact() {
    this.ContactForEditing = undefined;
  }

  onCreateContact() {
    const dialogRef = this.dialog.open(CreateContactComponent);

    dialogRef.afterClosed().subscribe(result => {
      if (result == null)
        return;

      // fetch here
      console.log(result);
      result.firstName = result.name.split(" ")[0];
      result.lastName = result.name.split(" ")[1];

      this.ContactsController.OnPostAsync(result).then(response => {
        this.contacts.push(response);
        this._snackBar.open("Contact created", "Success!", {duration: 3000})
      }).catch(err => alert(err));

    });

  }
}
class Contact {
  id: number;

  firstName: string;
  lastName: string;
  email: string;
  address: string;
  birthday: Date;
  notes: string;

  phoneNumber: string;
  categoryName: string;
  editableFields: string[] = []

  constructor(id: number, firstName: string, lastName: string,
              email: string, address: string, birthday: Date,
              notes: string, phoneNumber: string, category: string) {
    this.id = id;
    this.firstName = firstName;
    this.lastName = lastName;
    this.email = email;
    this.address = address;
    this.birthday = birthday;
    this.notes = notes;
    this.phoneNumber = phoneNumber;
    this.categoryName = category;
  }

}

class ContactField {
  fieldName: keyof Contact;
  header: string;
  type: DataType;

  constructor(fieldName: keyof Contact, header: string, type: DataType) {
    this.fieldName = fieldName;
    this.header = header;
    this.type = type;
  }
}
