import { DistrictDTO } from "./districtDTO";

export interface ProvinceDTO{
    provinceName: string,
    districts: DistrictDTO[]
}