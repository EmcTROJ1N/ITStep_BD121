import {Component, EventEmitter, Input, Output} from '@angular/core';
import {Sort} from "@angular/material/sort";
import {MatDialog} from "@angular/material/dialog";
import {MatSnackBar} from "@angular/material/snack-bar";
import {DialogComponent, DialogData, DialogField} from "../dialog/dialog.component";
import {concatWith} from "rxjs";

@Component({
  selector: 'app-crud-api',
  templateUrl: './crud-api.component.html',
  styleUrls: ['./crud-api.component.css']
})
export class CrudApiComponent {
  @Input() ItemsSource: any[] = [];
  @Input() ColumnsDefinitions: CrudColumn[] = [];

  @Output() created = new EventEmitter<any>();
  @Output() deleted = new EventEmitter<number | string>();
  @Output() edited = new EventEmitter<any>();


  SortedItemsSource: any[];
  ItemForEdit: any = {};

  get DisplayedColumns(): string[] {
    let res =this.ColumnsDefinitions.map(col => col.fieldName);
    res.push("actions");
    return res;
  }

  constructor(public dialog: MatDialog,
              private _snackBar: MatSnackBar) {
    this.SortedItemsSource = this.ItemsSource.slice();
  }

  ngOnInit() { this.Update() }
  ngOnChanges() { this.Update() }
  Update() {
    this.ItemsSource = this.ItemsSource.map((item: any) => {
        item.editingNow = false;
        return item;
    });
    this.SortedItemsSource = this.ItemsSource.slice();
  }

  openDialog($event: any) {

    let data: DialogField[] = this.ColumnsDefinitions
      //.filter(item => item.fieldName != "id")
      .map(item => {
        return new DialogField(
        item.fieldName,
        item.type,
        item.headerCell
      );
    })

    const dialogRef = this.dialog.open(DialogComponent, {
      data: {
        header: "Add new item",
        fields: data
      }
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result == null)
        return;
      this.onCreate($event, result);
    });
  }


  onSort(sort: Sort) {
    const data = this.ItemsSource.slice();
    if (!sort.active || sort.direction === '') {
      this.SortedItemsSource = data;
      return;
    }

    this.SortedItemsSource = data.sort((a, b) => {
      const isAsc = sort.direction === 'asc';
      switch (sort.active) {
        case 'id':
          return compare(a.id, b.id, isAsc);
        case 'phoneNumber':
          return compare(a.phoneNumber, b.phoneNumber, isAsc);
        case 'fullName':
          return compare(a.fullName, b.fullName, isAsc);
        case 'creationDate':
          return compare(a.creationDate, b.creationDate, isAsc);
        default:
          return 0;
      }
    });

    function compare(a: number | string | Date, b: number | string | Date, isAsc: boolean) {
      return (a < b ? -1 : 1) * (isAsc ? 1 : -1);
    }

  }


  onCreate($event: any, item: any) {
    if (this.ItemsSource.map(item => item.id).includes(item.id)) {
      alert("This id already used");
      return;
    }
    item.creationDate = new Date(item.creationDate);
    this.created.emit(item);
    item.editingNow = false;
    this.ItemsSource.push(item);
    this.Update();
  }
  onDelete($event: any, id: number) {
    this.deleted.emit(id);
    this.ItemsSource = this.ItemsSource.filter(item => item.id != id);
    this.Update();
  }

  onEditBegin($event: any, item: any) {
    Object.assign(this.ItemForEdit, item);
    item.editingNow = true;
  }

  onEditComplete($event: any, item: any, confirm: boolean) {
    item.editingNow = false;

    this.edited.emit({
      cancelled: !confirm,
      oldValue: this.ItemForEdit,
      newValue: confirm ? item : this.ItemForEdit,
    });

    if (!confirm)
      Object.assign(item, this.ItemForEdit);
    this.ItemForEdit = {};
  }

  protected readonly ColumnTypes = DataType;
  protected readonly Date = Date;
}

export class CrudColumn {
  fieldName: string = "";
  headerCell: string = "";
  type: DataType = DataType.Text;
}

export enum DataType {
  Text,
  Number,
  Date,
  Boolean,
  Select,
  Email
}
