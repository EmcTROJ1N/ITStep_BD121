import {Component, Inject} from '@angular/core';
import {FormControl, FormsModule} from "@angular/forms";
import {
  MAT_DIALOG_DATA,
  MatDialogRef,
  MatDialogModule,
} from '@angular/material/dialog';
import {MatButtonModule} from '@angular/material/button';
import {MatInputModule} from '@angular/material/input';
import {MatFormFieldModule} from '@angular/material/form-field';
import {NgForOf, NgIf, NgStyle, NgSwitch, NgSwitchCase} from "@angular/common";
import {MatDividerModule} from "@angular/material/divider";
import {DataType} from "../crud-api/crud-api.component";


export class DialogField {
  description: string;
  type: DataType;
  id: string | number;

  constructor(id: string | number, type: DataType, description: string) {
    this.id = id;
    this.type = type;
    this.description = description;
  }

}
export class DialogData {
    header: string;
    fields: DialogField[];

    constructor(header: string, fields: DialogField[]) {
      this.header = header;
      this.fields = fields;
    }
}

@Component({
  selector: 'app-dialog',
  templateUrl: './dialog.component.html',
  styleUrls: ['./dialog.component.css'],
  standalone: true,
  imports: [
    MatDialogModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
    FormsModule,
    NgSwitch,
    NgSwitchCase,
    NgForOf,
    NgStyle,
    NgIf,
    MatDividerModule,
  ],
})
export class DialogComponent {
  newData: any = {};
  constructor(
    public dialogRef: MatDialogRef<DialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: DialogData,
  ) {}

  onNoClick(): void {
    this.dialogRef.close();
  }

  protected readonly DataType = DataType;
}
