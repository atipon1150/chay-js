import { Component, OnInit } from '@angular/core';
import { HttpClient, HttpClientModule, provideHttpClient } from '@angular/common/http';
import { CommonModule } from '@angular/common';

interface Department {
  id: number;
  name: string;
}

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [CommonModule, HttpClientModule],
  template: `
    <h1>แผนกทั้งหมด</h1>
    <table *ngIf="departments.length > 0; else loading">
      <thead>
        <tr>
          <th>รหัส</th>
          <th>ชื่อแผนก</th>
        </tr>
      </thead>
      <tbody>
        <tr *ngFor="let dept of departments">
          <td>{{ dept.id }}</td>
          <td>{{ dept.name }}</td>
        </tr>
      </tbody>
    </table>
    <ng-template #loading>
      <p>กำลังโหลดข้อมูล...</p>
    </ng-template>
  `
})
export class AppComponent implements OnInit {
  departments: Department[] = [];

  constructor(private http: HttpClient) { }

  ngOnInit(): void {
    this.http.get<Department[]>('https://localhost:5031/api/Departments')
      .subscribe({
        next: data => this.departments = data,
        error: err => console.error('Error loading departments', err)
      });
  }
}
