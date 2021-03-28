import { Station } from "./station";

export interface City{
    id: number,
    name: string,
    stations: Station[]
}