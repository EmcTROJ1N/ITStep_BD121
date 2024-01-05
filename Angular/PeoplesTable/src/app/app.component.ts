import {Component, Input, Output} from '@angular/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {

  data: string[] = []
  title: string = 'PeoplesTable';

  onInputDataUpdate($event: any)
  {
      this.data.push($event);
  }
}
