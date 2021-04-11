import { DistrictDTO } from "./districtDTO";

export interface ProvinceDTO{
    name: string,
    districts: DistrictDTO[]
}