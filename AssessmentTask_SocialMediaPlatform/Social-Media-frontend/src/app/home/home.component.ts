import { Component, OnInit } from '@angular/core';
import { PostService } from '../services/post.service';  // Adjust the path based on your project structure
import { Post } from '../models/post';                  // Import the Post interface
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-home',
  standalone: true,
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css'],
  imports: [CommonModule]
})
export class HomeComponent implements OnInit {
  posts: Post[] = [];  // Array to store the posts

  constructor(private postService: PostService) {}

  // This lifecycle hook will fetch the posts when the component is initialized
  ngOnInit(): void {
    this.loadPosts();
  }

  // Fetch the posts using PostService
  loadPosts(): void {
    this.postService.getPosts().subscribe({
      next: (data) => this.posts = data,    // Assign the data to the posts array
      error: (err) => console.error('Error fetching posts', err)  // Handle errors
    });
  }
}
