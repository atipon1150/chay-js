import {
  AngularNodeAppEngine,
  createNodeRequestHandler,
  isMainModule,
  writeResponseToNodeResponse,
} from '@angular/ssr/node';
import express, { Request, Response, NextFunction } from 'express';
import { fileURLToPath } from 'url';
import { dirname, join } from 'path';

// แปลง import.meta.url เป็น path
const __filename = fileURLToPath(import.meta.url);
const __dirname = dirname(__filename);

// Path ของไฟล์ build ของ Angular
const browserDistFolder = join(__dirname, '../browser');

const app = express();
const angularApp = new AngularNodeAppEngine();

// Serve static files
app.use(
  express.static(browserDistFolder, {
    maxAge: '1y',
    index: false,
    redirect: false,
  }),
);

// ตัวอย่าง API endpoint
app.get('/api/departments', (req: Request, res: Response) => {
  // ส่งข้อมูลตัวอย่าง
  res.json([{ id: 1, name: 'IT' }, { id: 2, name: 'HR' }]);
});

// Handle all other requests by Angular Universal
app.use((req: Request, res: Response, next: NextFunction) => {
  angularApp
    .handle(req)
    .then((response) =>
      response ? writeResponseToNodeResponse(response, res) : next(),
    )
    .catch(next);
});

// Start server
if (isMainModule(import.meta.url)) {
  const port = process.env['PORT'] || 4000;
  app.listen(port, (error?: any) => {
    if (error) throw error;
    console.log(`Node Express server listening on http://localhost:${port}`);
  });
}

// สำหรับ Angular CLI / Firebase
export const reqHandler = createNodeRequestHandler(app);
