import { Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { PostComponent } from './post/post.component';
import { LeaderBoardComponent } from './leader-board/leader-board.component';

export const routes: Routes = [
  { path: '', component: HomeComponent },
  { path: 'post', component: PostComponent },
  { path: 'leaderboard', component: LeaderBoardComponent },
];
