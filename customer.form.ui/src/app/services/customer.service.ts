import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
    
import {  Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { Customer } from '../models/Customer';

@Injectable({
  providedIn: 'root'
})
export class CustomerService {
  
 
private apiURL = "https://localhost:44329/v1/";
    
httpOptions = {
  headers: new HttpHeaders({
    'Content-Type': 'application/json'
  })
}
 
constructor(private httpClient: HttpClient) { }

saveCustomer(customer : Customer): Observable<Customer> {
    return this.httpClient.post<Customer>(this.apiURL + 'Customer/', JSON.stringify(customer), this.httpOptions)
    .pipe(
      catchError(this.errorHandler)
    )
  }   

errorHandler(error: { error: { message: string; }; status: any; message: any; }) {
    let errorMessage = '';
    if(error.error instanceof ErrorEvent) {
      errorMessage = error.error.message;
    } else {
      errorMessage = `Error Code: ${error.status}\nMessage: ${error.message}`;
    }
    return throwError(errorMessage);
 }
}