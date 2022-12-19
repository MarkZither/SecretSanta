export interface ISantaService {
    generate(): void;
}

export class ConcreteSantaService implements ISantaService {
    something: string | undefined;
    generate() {
        this.something = 'sdfsdf';
    }
}