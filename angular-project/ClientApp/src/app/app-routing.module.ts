import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { LoginComponent } from './login/login.component';
import { RegisterComponent } from './register/register.component';
import { AuthGuard } from './guard/auth.guard';
import { ServerModule } from '@angular/platform-server';

const routes: Routes = [
  {component:LoginComponent,path:'login'},
 {component:RegisterComponent,path:'register'},
 {component:HomeComponent,path:'',canActivate:[AuthGuard]}
];

@NgModule({
  imports: [RouterModule.forRoot(routes),
    ServerModule],
  exports: [RouterModule]
})
export class AppRoutingModule { }
