import { Injectable } from '@angular/core';
//import { environment } from 'environments/environment';

import { Http, Response } from '@angular/http';
import { User } from './models/user';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/operator/map';
import 'rxjs/add/operator/catch';
import 'rxjs/add/observable/throw';

const API_URL = "http://localhost:3000/api/register";

@Injectable()
export class ApiService {

  constructor(
    private http: Http
  ) {
  }

  public createUser(user: User): Observable<User> {
    return this.http
      .post(API_URL + '/user', user)
      .map(response => {
        return new User(response.json());
      })
      .catch(this.handleError);
  }

  private handleError (error: Response | any) {
    console.error('ApiService::handleError', error);
    return Observable.throw(error);
  }
}