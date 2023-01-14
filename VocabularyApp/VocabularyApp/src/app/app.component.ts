import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { _countGroupLabelsBeforeOption } from '@angular/material/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {

  title = 'VocabularyApp';

  ngOnInit(): void {
    window.onscroll = function() {scrollFunction()}
  }

  scrollToTop()
  {
    document.documentElement.scrollTop = 0;
  }
}

function scrollFunction() {
  //Gets scroll button
  var scrlBtn = document.getElementById("scrollButton");

  //If the window is scrolled more than 20 pixels, than show the "To top" button.
  //Otherwise hide it
  if (document.documentElement.scrollTop > 20) {
    scrlBtn!.style.display = "block";
  }
  else {
    scrlBtn!.style.display = "none"; 
  }
}