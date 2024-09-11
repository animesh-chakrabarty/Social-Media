import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { NavbarComponent } from "./navbar/navbar.component";
import { PostComponent } from './post/post.component';
import { CommonModule } from '@angular/common';

@Component({
    selector: 'app-root',
    standalone: true,
    templateUrl: './app.component.html',
    styleUrls: ['./app.component.css'], // Corrected from 'styleUrl'
    imports: [RouterOutlet, NavbarComponent] // Removed PostComponent as it might not be needed here
})
export class AppComponent {
  title = 'Social Media Platform';
}
