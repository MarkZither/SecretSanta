import { Component } from '@angular/core';

@Component({
  selector: 'app-santa',
  templateUrl: './santa.component.html',
  styleUrls: ['./santa.component.scss']
})
export class SantaComponent {
  generate(): void {
    console.log("generating");
  }
}
