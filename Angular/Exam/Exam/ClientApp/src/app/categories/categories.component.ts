import {Component} from '@angular/core';
import FetchController from "../../FetchController";
import {DialogComponent} from "../dialog/dialog.component";
import {MatDialog} from "@angular/material/dialog";
import {MatSnackBar} from "@angular/material/snack-bar";
import {DataType} from "../crud-api/crud-api.component";


@Component({
    selector: 'app-categories',
    templateUrl: './categories.component.html',
    styleUrls: ['./categories.component.css']
})

export class CategoriesComponent {

  categories: Category[] = [];
  Controller: FetchController = new FetchController("api/Categories");

  constructor(public dialog: MatDialog,
              private _snackBar: MatSnackBar)
  {}

  async ngOnInit() {
    let response = await this.Controller.OnGetAsync();
    this.categories = <Category[]>response;
  }
  onDeleteCategory($event: any, id: number)
  {
    if (id == null)
        return;

    this.Controller.OnDeleteAsync(id).then(response => {
      if (response.ok) {
        this.categories = this.categories.filter(category => category.id != id)
        this._snackBar.open("Category deleted", "Success!", {duration: 3000})
      }
      else
        this._snackBar.open(response.statusText, "Error", {duration: 3000})
    }).catch(err =>
      this._snackBar.open(err.toString(), "Error", {duration: 3000})
    );

    // fetch here
  }

  onOpenDialog($event: any)
  {
    const dialogRef = this.dialog.open(DialogComponent, {
      data: {
        header: "Category creating",
        fields: [
          { description: "Category name", type: DataType.Text, id: "categoryName" },
        ]
      }
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result == null)
        return;


      let category: Category = new Category();
      category.categoryName = result.categoryName;

      this.Controller.OnPostAsync(category).then(response => {
        this.categories.push(response);
      }).catch(err =>
        this._snackBar.open(err.toString(), "Error", {duration: 3000})
      );

      this._snackBar.open("Category created", "Success!", {duration: 3000})
    });

  }

}

export class Category
{
  id: number = 0;
  categoryName: string = "";
}
