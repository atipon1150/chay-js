// Import Module ที่จำเป็นจาก Angular
import { NgModule } from '@angular/core'; // สำหรับสร้าง Angular Module
import { RouterModule, Routes } from '@angular/router'; // สำหรับตั้งค่า routing
import { AppComponent } from './app.component'; // นำ Component หลักมาทำ routing


// กำหนดเส้นทาง (Routes)
const routes: Routes = [
  { path: '', component: AppComponent } // path '' = หน้าแรกของเว็บ
  // ถ้า URL เป็น http://localhost:4200/ จะโหลด AppComponent
];


// สร้าง Module สำหรับ routing
@NgModule({
  imports: [RouterModule.forRoot(routes)], // ใช้ RouterModule.forRoot() เพื่อกำหนด routing หลัก
  exports: [RouterModule] // export RouterModule เพื่อให้ AppModule สามารถใช้ routing ได้
})
export class AppRoutingModule { } // ประกาศ class เป็น AppRoutingModule
