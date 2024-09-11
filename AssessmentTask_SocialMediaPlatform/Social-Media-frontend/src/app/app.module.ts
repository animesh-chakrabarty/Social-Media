import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { RouterModule } from '@angular/router';
import { CommonModule } from '@angular/common'; // Required for *ngIf and other directives

import { AppComponent } from './app.component';
import { HomeComponent } from './home/home.component';
import { PostComponent } from './post/post.component';
import { routes } from './app.routes'; // Import routes from your app.routes.ts
import { LeaderBoardComponent } from './leader-board/leader-board.component';

@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    PostComponent,
    LeaderBoardComponent
  ],
  imports: [
    BrowserModule,
    CommonModule,
    RouterModule.forRoot(routes) // Initialize routing with your defined routes
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
