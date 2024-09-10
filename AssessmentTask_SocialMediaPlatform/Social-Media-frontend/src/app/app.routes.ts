import { Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { PostComponent } from './post/post.component';

export const routes: Routes = [
    { path: '', component: HomeComponent },
    { path: "Post", component: PostComponent}
];
