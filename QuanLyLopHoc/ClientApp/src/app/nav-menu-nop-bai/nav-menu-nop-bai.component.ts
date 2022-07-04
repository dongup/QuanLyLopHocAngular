import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { NopBaiDetailComponent } from '../nop-bai-detail/nop-bai-detail.component';

@Component({
  selector: 'app-nav-menu-nop-bai',
  templateUrl: './nav-menu-nop-bai.component.html',
  styleUrls: ['./nav-menu-nop-bai.component.css']
})
export class NavMenuNopBaiComponent implements OnInit {
  isExpanded = false;
  maSinhVien: string = '';

  @Output() nopBai = new EventEmitter();

  constructor() { }

  ngOnInit(): void {
  }

  nopBaiClick() {
    this.nopBai.emit()
  }

  collapse() {
    this.isExpanded = false;
  }

  toggle() {
    this.isExpanded = !this.isExpanded;
  }
}
