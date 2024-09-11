import { Component, OnInit } from '@angular/core';
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
  styleUrls: ['./select-user.component.css'],
})
export class SelectUserComponent implements OnInit {
  users: User[] = [];
  selectedUser: User | null = null;

  constructor(private userService: UserService) {}

  ngOnInit(): void {
    this.loadUsers();
  }

  // Load users from the UserService
  loadUsers(): void {
    this.userService.getUsers().subscribe((users) => {
      this.users = users;
      if (this.users.length > 0) {
        this.onSelectUser(this.users[0]); // Set the first user as the default selected user
      }
    });
  }

  // Set the selected user and update it via UserService
  onSelectUser(user: User): void {
    this.selectedUser = user;
    this.userService.setSelectedUser(user);
  }
}
