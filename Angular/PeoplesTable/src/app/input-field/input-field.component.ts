import {Component, EventEmitter, Input, Output} from '@angular/core';

@Component({
    selector: 'app-input-field',
    templateUrl: './input-field.component.html',
    styleUrls: ['./input-field.component.css']
})
export class InputFieldComponent
{
    @Input() name: string = "";
    @Input() phone: number = 0;
    @Input() age: number = 0;
    @Input() address: string = "";

    @Output() onDataUpdate = new EventEmitter();
    onUpdate()
    {
      this.onDataUpdate.emit({
        name: this.name,
        phone: this.phone,
        age: this.age,
        address: this.address
      });
    }
}
