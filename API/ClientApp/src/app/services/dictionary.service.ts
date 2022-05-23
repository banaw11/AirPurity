import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, throwError } from 'rxjs';
import { catchError, map } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { DictionaryModel } from '../models/dictionaryModel';
import { ResponseModel } from '../models/responseModel';

@Injectable({
  providedIn: 'root'
})
export class DictionaryService {

  apiUrl = environment.apiUrl
  constructor(private http: HttpClient) { }

  getProvinces(){
    return this.http.get(this.apiUrl + 'dictionary/address/get-provinces').pipe(
      map((response : ResponseModel) => {
        console.log(response);
          if(response.success){
            return <DictionaryModel[]>response.data
          }
          else{
            //handling errors
            return [];
          }
      })
      ,catchError(err => throwError(err))
    )
  }

  getDistricts(provinceId: number){
    return this.http.get(this.apiUrl + 'dictionary/address/get-districts', {params: {provinceId : provinceId.toString()}}).pipe(
      map((response : ResponseModel) => {
          if(response.success){
            return <DictionaryModel[]>response.data
          }
          else{
            //handling errors
            return [];
          }
      })
      ,catchError(err => throwError(err))
    )
  }

  getCommunes(districtId: number){
    return this.http.get(this.apiUrl + 'dictionary/address/get-communes', {params: {districtsId : districtId.toString()}}).pipe(
      map((response : ResponseModel) => {
          if(response.success){
            return <DictionaryModel[]>response.data
          }
          else{
            //handling errors
            return [];
          }
      })
      ,catchError(err => throwError(err))
    )
  }

  getCities(communeId: number){
    return this.http.get(this.apiUrl + 'dictionary/address/get-cities', {params: {communeId : communeId.toString()}}).pipe(
      map((response : ResponseModel) => {
          if(response.success){
            return <DictionaryModel[]>response.data
          }
          else{
            //handling errors
            return [];
          }
      })
      ,catchError(err => throwError(err))
    )
  }

}
