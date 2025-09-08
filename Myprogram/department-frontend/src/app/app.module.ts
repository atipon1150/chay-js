import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http';
import { AppComponent } from './app.component';
import { AppRoutingModule } from './app-routing.module'; // ✅ ต้อง import ไว้ด้วย

@NgModule({
  imports: [
    BrowserModule,
    HttpClientModule,
    AppRoutingModule,
    AppComponent // ✅ standalone component import
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
