import { Component } from '@angular/core';
import { EnvService } from '@app/shared/env.service';
import { LoggerService } from '@app/shared/logger.service'
import { ConcreteSantaService } from '@app/shared/santa';

@Component({
  selector: 'app-santa',
  templateUrl: './santa.component.html',
  styleUrls: ['./santa.component.scss']
})
export class SantaComponent {

  constructor(private logger: LoggerService, private env: EnvService, private santaService: ConcreteSantaService) {}

  generate(): void {
    this.santaService.generate();
    this.logger.info("generating");
    this.logger.info(this.env.apiUrl);
    this.logger.error("Generating Failed, implement the service");
  }
}
