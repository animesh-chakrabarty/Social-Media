// user.service.ts

import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable, map } from 'rxjs';
import { User } from '../models/user';

@Injectable({
  providedIn: 'root'
})
export class UserService {
  private selectedUserSubject: BehaviorSubject<User | null> = new BehaviorSubject<User | null>(null);
  selectedUser$: Observable<User | null> = this.selectedUserSubject.asObservable();

  setSelectedUser(user: User): void {
    this.selectedUserSubject.next(user);
  }
  
  private apiUrl = 'http://localhost:5182/User';

  constructor(private http: HttpClient) {}

  getUsers(): Observable<User[]> {
    return this.http.get<User[]>(this.apiUrl);
  }

  getFirstUser(): Observable<User> {
    return this.getUsers().pipe(
      map((users: any[]) => users[0])
    );
  }
}
