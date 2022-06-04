import { Sensor } from "./sensor";
import { StationState } from "./stationState";

export interface Station{
    id: number,
    stationName: string,
    gegrLat: number,
    gegrLon: number,
    addressStreat: string,
    sensors?: Sensor[],
    stationState?: StationState
}