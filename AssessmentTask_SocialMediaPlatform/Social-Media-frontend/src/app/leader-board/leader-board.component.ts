// src/app/leaderboard/leaderboard.component.ts
import { Component, OnInit } from '@angular/core';
import { LeaderBoardService } from './leader-board.service';
import { LeaderBoard } from '../models/leaderboard';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-leaderboard',
  standalone: true,
  templateUrl: './leader-board.component.html',
  styleUrls: ['./leader-board.component.css'],
  imports: [CommonModule],
})
export class LeaderBoardComponent implements OnInit {
  leaderBoard: LeaderBoard[] = [];

  constructor(private leaderBoardService: LeaderBoardService) {}

  ngOnInit(): void {
    this.leaderBoardService.getLeaderBoard().subscribe((data) => {
      this.leaderBoard = data;
    });
  }
}
