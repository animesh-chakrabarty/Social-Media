import { Component, OnInit } from '@angular/core';
import { feedService } from '../services/feed.service';
import { Feed } from '../models/feed';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms'; // Import FormsModule for NgModel

@Component({
  selector: 'app-home',
  standalone: true,
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css'],
  imports: [CommonModule, FormsModule], // Import FormsModule here
})
export class HomeComponent implements OnInit {
  feedData: Feed | undefined;
  newComment: { [postId: number]: string } = {};
  errorMessages: { [postId: number]: string } = {}; // To hold error messages

  constructor(private feedService: feedService) {}

  ngOnInit(): void {
    this.loadFeed();
  }

  loadFeed(): void {
    this.feedService.getFeed().subscribe({
      next: (data) => (this.feedData = data),
      error: (err) => console.error('Error fetching feed', err),
    });
  }

  getLikeCount(postID: number): number {
    return (
      this.feedData?.likes.filter((like) => like.postID === postID).length || 0
    );
  }

  getCommentsForPost(postID: number): any[] {
    return (
      this.feedData?.comments.filter((comment) => comment.postID === postID) ||
      []
    );
  }

  likePost(postID: number): void {
    const userId = 2; // Replace with actual user ID
    this.feedService.addLike(postID, userId).subscribe({
      next: () => {
        this.loadFeed(); // Refresh the feed
        this.errorMessages[postID] = ''; // Clear any previous error message
      },
      error: (err) => {
        console.error('Error liking post', err);
        this.errorMessages[postID] = err.error.message || 'An error occurred while liking the post';
      },
    });
  }

  addComment(postID: number): void {
    const userId = 2; // Replace with actual user ID
    const content = this.newComment[postID];
    this.feedService.addComment(postID, userId, content).subscribe({
      next: () => {
        this.newComment[postID] = ''; // Clear the input field
        this.loadFeed(); // Refresh the feed
        this.errorMessages[postID] = ''; // Clear any previous error message
      },
      error: (err) => {
        console.error('Error adding comment', err);
        this.errorMessages[postID] = err.error.message || 'An error occurred while adding the comment';
      },
    });
  }
}
