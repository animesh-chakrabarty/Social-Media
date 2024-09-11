// shared.service.ts
import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { User } from '../models/user';

@Injectable({
  providedIn: 'root',
})
export class SharedService {
  private selectedUserSubject = new BehaviorSubject<User | null>(null);

  // Method to set the selected user
  setSelectedUser(user: User): void {
    this.selectedUserSubject.next(user);
  }

  // Method to get the selected user as an observable
  getSelectedUser(): Observable<User | null> {
    return this.selectedUserSubject.asObservable();
  }
}
