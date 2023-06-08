import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Params, Router } from '@angular/router';

@Component({
  selector: 'app-error',
  templateUrl: './error.component.html',
  styleUrls: ['./error.component.scss'],
})
export class ErrorComponent implements OnInit {
  constructor(private router: Router, private route: ActivatedRoute) {}
  errorCode: number | undefined;
  errorMsg: string | undefined;
  errorTrace: string | undefined;
  errorTitle: string | undefined;
  title: string = 'Oops something went wrong...';
  ngOnInit() {
    this.route.queryParams.subscribe((params: Params) => {
      this.errorCode = params['code'];
      this.errorMsg = params['msg'];
      this.errorTrace = params['trace'];
      this.errorTitle = params['title'];
    });
  }
}
