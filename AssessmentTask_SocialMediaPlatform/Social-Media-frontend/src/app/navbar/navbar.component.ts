import { Component } from '@angular/core';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatButtonModule } from '@angular/material/button';
import { RouterLink, RouterLinkActive, RouterOutlet } from '@angular/router';
import { SelectUserComponent } from '../select-user/select-user.component';
@Component({
    selector: 'app-navbar',
    standalone: true,
    templateUrl: './navbar.component.html',
    styleUrl: './navbar.component.css',
    imports: [
        RouterOutlet, RouterLink, RouterLinkActive,
        MatToolbarModule,
        MatButtonModule,
        SelectUserComponent
    ]
})
export class NavbarComponent {

}
