import { Component, OnInit } from '@angular/core';
import { FeedService } from '../services/feed.service'; // Import FeedService
import { Feed } from '../models/feed'; // Import Feed interface
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-home',
  standalone: true,
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css'],
  imports: [CommonModule],
})
export class HomeComponent implements OnInit {
  feedData: Feed | undefined; // To store the feed data

  constructor(private feedService: FeedService) {} // Inject FeedService

  // This lifecycle hook will fetch the feed when the component is initialized
  ngOnInit(): void {
    this.loadFeed();
  }

  // Fetch the feed using FeedService
  loadFeed(): void {
    this.feedService.getFeed().subscribe({
      next: (data) => (this.feedData = data), // Assign the data to feedData
      error: (err) => console.error('Error fetching feed', err), // Handle errors
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
}
