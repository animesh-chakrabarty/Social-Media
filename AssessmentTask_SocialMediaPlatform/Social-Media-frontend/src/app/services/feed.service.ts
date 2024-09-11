import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Feed } from '../models/feed';

@Injectable({
  providedIn: 'root'
})
export class FeedService {
  private apiUrl = 'http://localhost:5182/post'; 

  constructor(private http: HttpClient) { }

  getFeed(): Observable<Feed> {
    return this.http.get<Feed>(`${this.apiUrl}/feed`);
  }
}
