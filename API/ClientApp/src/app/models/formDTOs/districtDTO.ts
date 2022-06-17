import { CommuneDTO } from "./communeDTO";

export interface DistrictDTO{
    districtName: string,
    communes: CommuneDTO[]
}