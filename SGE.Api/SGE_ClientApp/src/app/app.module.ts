import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { TabMenuModule } from 'primeng/tabmenu';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { ButtonModule } from 'primeng/button';
import { LoginComponent } from './login/login.component';
import { ReactiveFormsModule} from '@angular/forms';
import { DialogModule } from 'primeng/dialog';
import { ImageCropperModule } from 'ngx-image-cropper';
import { HttpClientModule } from '@angular/common/http';
import { CreateComponent } from './create/create.component';
import { ShowAdminComponent } from './show-admin/show-admin.component';


@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    CreateComponent,
    ShowAdminComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    TabMenuModule,
    ButtonModule,
    ReactiveFormsModule,
    DialogModule,
    ImageCropperModule,
    HttpClientModule,
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
