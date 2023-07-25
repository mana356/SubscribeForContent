import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { MatTableDataSource } from '@angular/material/table';
import { ActivatedRoute, Params } from '@angular/router';
import { SubscriptionLevelDto } from 'src/app/models/UserProfile/subscription-level.model';
import { PostService } from 'src/app/shared/services/post.service';

@Component({
  selector: 'app-subscribe-to-creator',
  templateUrl: './subscribe-to-creator.component.html',
  styleUrls: ['./subscribe-to-creator.component.scss'],
})
export class SubscribeToCreatorComponent implements OnInit {
  creatorUsername = '';
  subscribed = false;
  subscriptionFormGroup: FormGroup;
  levels: SubscriptionLevelDto[] = [];
  selectedSubscriptionCost = 0;
  displayedColumns: string[] = ['name', 'description', 'price'];
  dataSource = new MatTableDataSource<SubscriptionLevelDto>();

  constructor(
    private postService: PostService,
    private activatedRoute: ActivatedRoute
  ) {
    this.subscriptionFormGroup = new FormGroup({
      creatorSbscriptionLevel: new FormControl('', [Validators.required]),
    });
  }
  ngOnInit(): void {
    this.creatorUsername =
      this.activatedRoute.snapshot.paramMap.get('username') ?? '';
    this.postService
      .GetCreatorSubscriptionLevels(this.creatorUsername)
      .subscribe((res) => {
        this.levels = res;
        this.dataSource = new MatTableDataSource<SubscriptionLevelDto>(
          this.levels
        );
      });
  }

  setSubscriptionPrice(): void {
    const id =
      this.subscriptionFormGroup.controls['creatorSbscriptionLevel'].value;
    const price = this.levels.filter((s) => s.id === Number(id))[0].levelPrice;
    this.selectedSubscriptionCost = price;
  }
}
