import { enableProdMode } from '@angular/core';
import { platformBrowserDynamic } from '@angular/platform-browser-dynamic';

import { AppModule } from './app/app.module';
import { environment } from './environments/environment';

export function getApiUrl() {
  return 'http://localhost:5000/api/';
  //return 'http://123.30.110.97:5001/api/';
}

export function getBaseUrl() {
  return 'http://localhost:5000/';
  //return 'http://123.30.110.97:5001/';
}

const providers = [
  { provide: 'API_URL', useFactory: getApiUrl, deps: [] }
  , { provide: 'BASE_URL', useFactory: getBaseUrl, deps: [] }
];

if (environment.production) {
  enableProdMode();
}

platformBrowserDynamic(providers).bootstrapModule(AppModule)
  .catch(err => console.log(err));
