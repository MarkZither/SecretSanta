import { SantaMockService } from "./santa.mock.service";

export function SantaFactory(){
    const service = new SantaMockService();

    return service;
}