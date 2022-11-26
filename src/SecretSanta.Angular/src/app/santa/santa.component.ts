import { Component } from '@angular/core';
import { LoggerService } from '@app/shared/logger.service'

@Component({
  selector: 'app-santa',
  templateUrl: './santa.component.html',
  styleUrls: ['./santa.component.scss']
})
export class SantaComponent {

  constructor(private logger: LoggerService) {}

  generate(): void {
    this.logger.info("generating");
    this.logger.error("Generating Failed, implement the service");
  }
}
