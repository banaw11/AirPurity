import { Measure } from "./measure";

export interface StationData{
    id: number,
    paramName: string,
    paramCode: string,
    values: Measure[],
    percents?: number
}