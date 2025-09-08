// Import Module ที่จำเป็นจาก Angular
import { NgModule } from '@angular/core'; // ใช้สำหรับสร้าง Angular Module
import { BrowserModule } from '@angular/platform-browser'; // สำหรับรัน Angular ใน browser
import { HttpClientModule } from '@angular/common/http'; // สำหรับเรียก HTTP API
import { AppComponent } from './app.component'; // นำ Component หลักเข้ามาใช้
import { AppRoutingModule } from './app-routing.module'; // นำ Routing Module เข้ามา


// สร้าง Angular Module หลักของแอป
@NgModule({
  // Modules ที่ต้อง import เพื่อให้ component ใช้งานได้
  imports: [
    BrowserModule, // ต้องมีในแอป Angular ที่รันบน browser
    HttpClientModule, // ให้ component ใช้ HttpClient ได้
    AppRoutingModule, // ให้ routing ของแอปทำงาน
    AppComponent // สำหรับ standalone component สามารถ import ได้โดยตรง
  ],
  
  // Services หรือ providers ใส่ตรงนี้ (เช่น API service, auth service)
  providers: [],

  // Component ที่จะ bootstrap เมื่อแอปเริ่มทำงาน
  bootstrap: [AppComponent] // โหลด AppComponent เป็น root ของแอป
})
export class AppModule { } // ประกาศ Module หลัก
