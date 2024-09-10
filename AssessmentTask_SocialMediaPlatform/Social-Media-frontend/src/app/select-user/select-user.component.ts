import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { User } from '../models/user';
import { UserService } from '../services/user.service';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatSelectModule } from '@angular/material/select';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-select-user',
  standalone: true,
  imports: [MatFormFieldModule, MatSelectModule, CommonModule],
  templateUrl: './select-user.component.html',
  styleUrl: './select-user.component.css'
})
export class SelectUserComponent implements OnInit {
  users: User[] = [];
  selectedUser: User | null = null;
  
  constructor(private userService: UserService) {}

  ngOnInit(): void {
    this.loadUsers();
  }

  loadUsers(): void {
    this.userService.getUsers().subscribe(users => {
      this.users = users;
      if (this.users.length > 0) {
        this.onSelectUser(this.users[0]);
      }
    });
  }

  onSelectUser(user: User): void {
    this.selectedUser = user;
    this.userService.setSelectedUser(user);
  }
}
