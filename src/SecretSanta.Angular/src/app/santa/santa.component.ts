import { animate, state, style, transition, trigger } from '@angular/animations';
import { Component } from '@angular/core';
import { ParticipantService } from '@app/core/services/participant.service';
import { EnvService } from '@app/shared/env.service';
import { LoggerService } from '@app/shared/logger.service';
import { Participant } from '@app/shared/participant';
import { ConcreteSantaService } from '@app/shared/santa';

@Component({
  selector: 'app-santa',
  animations: [
    trigger('openClose', [
      // ...
      state('open', style({
        height: '200px',
        opacity: 1,
        backgroundColor: 'yellow'
      })),
      state('closed', style({
        height: '100px',
        opacity: 0.8,
        backgroundColor: 'blue'
      })),
      transition('open => closed', [
        animate('1s')
      ]),
      transition('closed => open', [
        animate('0.5s')
      ]),
    ]),
  ],
  templateUrl: './santa.component.html',
  styleUrls: ['./santa.component.scss']
})

export class SantaComponent {
  isOpen = true;
  public resultsOpened = false;
  matches = ['First', 'Second', 'Third'];
  participants: Participant[] = [];

  constructor(
    private logger: LoggerService, 
    private env: EnvService, 
    private santaService: ConcreteSantaService,
    private participantService: ParticipantService) {}

  generate(): void {
    this.isOpen = !this.isOpen;
    this.participantService.getParticipants()
    .subscribe(participants => this.participants = participants);;
    this.logger.info(this.participants[0].email);
    this.santaService.generate();
    this.logger.info("generating");
    this.logger.info(this.env.apiUrl);
    this.logger.error("Generating Failed, implement the service");
    setTimeout(() => {
      this.isOpen = !this.isOpen;
    }, 2000);
    this.resultsOpened = true;
  }
}
