import { Component, OnInit } from '@angular/core';
import { feedService } from '../services/feed.service';
import { UserService } from '../services/user.service'; // Import UserService
import { Feed } from '../models/feed';
import { User } from '../models/user';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-home',
  standalone: true,
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css'],
  imports: [CommonModule, FormsModule],
})
export class HomeComponent implements OnInit {
  feedData: Feed | undefined;
  newComment: { [postId: number]: string } = {};
  errorMessages: { [postId: number]: string } = {};
  selectedUser: User | null = null;

  userNameCache: Map<number, string> = new Map();

  constructor(
    private feedService: feedService,
    private userService: UserService
  ) {}

  ngOnInit(): void {
    this.loadFeed();
    this.userService.selectedUser$.subscribe((user) => {
      this.selectedUser = user;
    });
  }

  // load user feed
  loadFeed(): void {
    this.feedService.getFeed().subscribe({
      next: (data) => (this.feedData = data),
      error: (err) => console.error('Error fetching feed', err),
    });
  }

  // get like count by postID
  getLikeCount(postID: number): number {
    return (
      this.feedData?.likes.filter((like) => like.postID === postID).length || 0
    );
  }

  // get comments by postID
  getCommentsForPost(postID: number): any[] {
    return (
      this.feedData?.comments.filter((comment) => comment.postID === postID) ||
      []
    );
  }

  // like the post by postID based on selected user
  likePost(postID: number): void {
    if (this.selectedUser) {
      this.feedService.addLike(postID, this.selectedUser.userID).subscribe({
        next: () => {
          this.loadFeed(); // Refresh the feed
          this.errorMessages[postID] = ''; // Clear any previous error message
        },
        error: (err) => {
          console.error('Error liking post', err);
          this.errorMessages[postID] =
            err.error.message || 'An error occurred while liking the post';
        },
      });
    }
  }

  // Add a comment by postID based on selected user
  addComment(postID: number): void {
    if (this.selectedUser) {
      const content = this.newComment[postID];
      this.feedService
        .addComment(postID, this.selectedUser.userID, content)
        .subscribe({
          next: () => {
            this.newComment[postID] = ''; // Clear the input field
            this.loadFeed(); // Refresh the feed
            this.errorMessages[postID] = ''; // Clear any previous error message
          },
          error: (err) => {
            console.error('Error adding comment', err);
            this.errorMessages[postID] =
              err.error.message || 'An error occurred while adding the comment';
          },
        });
    }
  }

  // get username by id
  getUserName(userID: number): string {
    if (this.userNameCache.has(userID)) {
      return this.userNameCache.get(userID)!; // Return from cache if available
    }

    // Call the API to get the user's details (userID and userName)
    this.userService.getUserName(userID).subscribe({
      next: (user) => {
        this.userNameCache.set(user.userID, user.userName); // Cache the userName with userID
      },
      error: (err) => {
        console.error('Error fetching username', err);
        this.userNameCache.set(userID, 'Unknown'); // Fallback in case of error
      },
    });

    return 'Loading...'; // Display a placeholder until the username is fetched
  }

  // check if the post is already liked by user
  isPostLikedByUser(postID: number): boolean {
    if (!this.selectedUser || !this.feedData) {
      return false;
    }

    // Check if the selected user has already liked this post
    return this.feedData.likes.some(
      (like) =>
        like.postID === postID && like.userID === this.selectedUser?.userID
    );
  }
}
