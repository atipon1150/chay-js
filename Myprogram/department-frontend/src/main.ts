// นำเข้า Zone.js เพื่อให้ Angular ติดตามการเปลี่ยนแปลงของ DOM และ async tasks
import 'zone.js';

// นำฟังก์ชัน bootstrapApplication สำหรับเริ่ม Angular app แบบ Standalone component
import { bootstrapApplication } from '@angular/platform-browser';

// นำ Root Component ของแอปมาใช้
import { AppComponent } from './app/app.component';

// ฟังก์ชันช่วย import providers จาก Module ต่าง ๆ สำหรับ Standalone bootstrap
import { importProvidersFrom } from '@angular/core';

// นำ HttpClientModule เพื่อให้ component สามารถเรียก API ผ่าน HttpClient ได้
import { HttpClientModule } from '@angular/common/http';

// เริ่ม Angular app ด้วย AppComponent เป็น root component
bootstrapApplication(AppComponent, {
  // กำหนด providers ของ app
  // importProvidersFrom(HttpClientModule) -> ทำให้ HttpClient ใช้งานได้
  providers: [importProvidersFrom(HttpClientModule)]
})
// ถ้าเกิด error ระหว่าง bootstrap ให้แสดง error ใน console
.catch(err => console.error(err));
