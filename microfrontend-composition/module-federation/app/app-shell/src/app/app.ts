import { Component, signal } from '@angular/core';
import { DomSanitizer, SafeResourceUrl } from '@angular/platform-browser';
import { RouterOutlet } from '@angular/router';

@Component({
  selector: 'app-root',
  imports: [RouterOutlet],
  templateUrl: './app.html',
  styleUrl: './app.css'
})
export class App {
  iframeSrc: SafeResourceUrl;

  constructor(private sanitizer: DomSanitizer) {
    // default iframe source
    this.iframeSrc = this.sanitizer.bypassSecurityTrustResourceUrl('http://localhost:5001');
  }

  loadPage(port: number) {
    const url = `http://localhost:${port}`;
    this.iframeSrc = this.sanitizer.bypassSecurityTrustResourceUrl(url);
  }

}
