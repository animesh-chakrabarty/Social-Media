import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Feed } from '../models/feed';

@Injectable({
  providedIn: 'root',
})
export class feedService {
  private apiUrl = 'http://localhost:5182/';

  constructor(private http: HttpClient) {}

  getFeed(): Observable<Feed> {
    return this.http.get<Feed>(`${this.apiUrl}post/feed`);
  }

  addLike(postID: number, userID: number): Observable<any> {
    return this.http.post(`${this.apiUrl}feed/postinteraction/${postID}/like`, {
      UserID: userID,
    });
  }

  addComment(postID: number, userID: number, content: string): Observable<any> {
    return this.http.post(`${this.apiUrl}feed/postinteraction/${postID}/comment`, {
      UserID: userID,
      Content: content,
    });
  }
}
