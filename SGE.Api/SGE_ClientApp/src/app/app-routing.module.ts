import { NgModule, createComponent } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import{LoginComponent} from './login/login.component';
import { CreateComponent } from './create/create.component';
import { ShowAdminComponent } from './show-admin/show-admin.component';


const routes: Routes = [
  {path:'login', component:LoginComponent},
  {path:'create', component:CreateComponent},
  {path:'admin', component:ShowAdminComponent},
  
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
