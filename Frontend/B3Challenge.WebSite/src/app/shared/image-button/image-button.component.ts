import { Component, OnInit, Input } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-image-button',
  templateUrl: './image-button.component.html',
  styleUrls: ['./image-button.component.css']
})
export class ImageButtonComponent implements OnInit {

  @Input() url!:string;
  @Input() icon!:string;
  @Input() label!:string;


  constructor() { }

  ngOnInit() {
  }

}
