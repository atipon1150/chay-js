// Import Module ที่จำเป็น
import { Component, OnInit } from '@angular/core'; // Component, Lifecycle hook OnInit
import { HttpClient, HttpClientModule } from '@angular/common/http'; // HttpClient สำหรับเรียก API
import { CommonModule } from '@angular/common'; // CommonModule สำหรับ directive เช่น *ngIf, *ngFor
import { FormsModule } from '@angular/forms'; // FormsModule สำหรับ ngModel (binding input)


// Interface สำหรับ Department
// ใช้ TypeScript เพื่อกำหนดโครงสร้างข้อมูลให้ชัดเจน
interface Department {
  id: number;   // รหัสแผนก (จากฐานข้อมูล)
  name: string; // ชื่อแผนก
}


// Component หลัก
@Component({
  selector: 'app-root', // ชื่อ tag ที่ใช้ใน HTML
  standalone: true, // เป็น standalone component ไม่ต้องประกาศใน NgModule
  imports: [CommonModule, HttpClientModule, FormsModule], // import module ที่ใช้ภายใน component
  template: `
    <div class="container mt-5">
      <h1 class="mb-4 text-center">แผนกทั้งหมด</h1>

      <!-- Search -->
      <div class="mb-3">
        <!-- two-way binding ด้วย [(ngModel)] กับ searchText -->
        <input [(ngModel)]="searchText" placeholder="ค้นหา..." class="form-control" />
      </div>

      <!-- Create / Edit Form -->
      <div class="mb-4 d-flex gap-2">
        <!-- input สำหรับชื่อแผนก -->
        <input [(ngModel)]="currentDept.name" placeholder="ชื่อแผนก" class="form-control" />

        <!-- ปุ่มบันทึก ใช้ ternary เพื่อเปลี่ยนข้อความระหว่าง สร้าง / แก้ไข -->
        <button (click)="saveDept()" class="btn btn-primary">
          {{ currentDept.id ? 'แก้ไข' : 'สร้าง' }}
        </button>

        <!-- ปุ่มยกเลิก สำหรับการแก้ไข -->
        <button *ngIf="currentDept.id" (click)="cancelEdit()" class="btn btn-secondary">
          ยกเลิก
        </button>
      </div>

      <!-- Table แสดงข้อมูลแผนก -->
      <table class="table table-bordered table-hover">
        <thead class="table-dark">
          <tr>
            <th>รหัส</th>
            <th>ชื่อแผนก</th>
            <th>Actions</th>
          </tr>
        </thead>
        <tbody>
          <!-- ngFor สำหรับวนลูปแสดงแผนก และใช้ filteredDepartments() สำหรับค้นหา -->
          <tr *ngFor="let dept of filteredDepartments(); let i = index">
            <!-- ใช้ i+1 สำหรับแสดงเลขรันต่อกัน (UI) -->
            <td>{{ i + 1 }}</td>
            <td>{{ dept.name }}</td>
            <td>
              <!-- ปุ่มแก้ไข -->
              <button (click)="editDept(dept)" class="btn btn-warning btn-sm me-2">แก้ไข</button>
              <!-- ปุ่มลบ -->
              <button (click)="deleteDept(dept.id)" class="btn btn-danger btn-sm">ลบ</button>
            </td>
          </tr>
        </tbody>
      </table>
    </div>
  `
})
export class AppComponent implements OnInit {

  // เก็บข้อมูลแผนกทั้งหมดจาก API
  departments: Department[] = [];

  // เก็บข้อมูลแผนกที่กำลังสร้างหรือแก้ไข
  currentDept: Department = { id: 0, name: '' };

  // เก็บข้อความค้นหา
  searchText: string = '';

  // Constructor inject HttpClient สำหรับเรียก API
  constructor(private http: HttpClient) {}

  // Lifecycle hook เรียกเมื่อ component ถูกสร้าง
  ngOnInit(): void {
    this.loadDepartments(); // โหลดข้อมูลแผนกจาก API
  }

  // ฟังก์ชันโหลดข้อมูลแผนกจาก backend API
  loadDepartments() {
    this.http.get<Department[]>('http://localhost:5030/api/departments')
      .subscribe(data => this.departments = data); // บันทึกลงตัวแปร departments
  }

  // ฟังก์ชันกรองข้อมูลสำหรับ search bar
  filteredDepartments() {
    return this.departments.filter(d =>
      d.name.toLowerCase().includes(this.searchText.toLowerCase()) // แปลงเป็น lowercase เพื่อค้นหา case-insensitive
    );
  }

  // ฟังก์ชันบันทึกข้อมูลแผนก
  saveDept() {
    if (this.currentDept.id) {
      // ถ้า currentDept.id มีค่า = แก้ไข
      this.http.put(`http://localhost:5030/api/departments/${this.currentDept.id}`, this.currentDept)
        .subscribe(() => {
          this.loadDepartments(); // โหลดข้อมูลใหม่หลังแก้ไข
          this.currentDept = { id: 0, name: '' }; // reset form
        });
    } else {
      // ถ้า currentDept.id = 0 = สร้างใหม่
      this.http.post('http://localhost:5030/api/departments', this.currentDept)
        .subscribe(() => {
          this.loadDepartments(); // โหลดข้อมูลใหม่หลังสร้าง
          this.currentDept = { id: 0, name: '' }; // reset form
        });
    }
  }

  // ฟังก์ชันเริ่มแก้ไขแผนก
  editDept(dept: Department) {
    this.currentDept = { ...dept }; // คัดลอกข้อมูลไป currentDept
  }

  // ฟังก์ชันยกเลิกการแก้ไข
  cancelEdit() {
    this.currentDept = { id: 0, name: '' }; // reset form
  }

  // ฟังก์ชันลบแผนก
  deleteDept(id: number) {
    this.http.delete(`http://localhost:5030/api/departments/${id}`)
      .subscribe(() => this.loadDepartments()); // โหลดข้อมูลใหม่หลังลบ
  }
}
