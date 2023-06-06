import {NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { RouterModule, Routes } from '@angular/router';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MaterialModule } from 'src/material.module';
import { ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import {ToastrModule} from 'ngx-toastr';

//components are the pages
import { RegisterComponent } from './register/register.component';
import { LoginComponent } from './login/login.component';
import { HomeComponent } from './home/home.component';
import { UpdatepopupComponent } from './updatepopup/updatepopup.component';
import { ErrorComponent } from './error/error.component';
import { SerialServiceService } from './serial-service.service';


//class with the routes types
const appRoute:Routes =[
  {path:"",component:HomeComponent},
  {path:"**",component:ErrorComponent}
]
//declares the components/pages of the app
@NgModule({
  declarations: [
    AppComponent,
    RegisterComponent,
    LoginComponent,
    HomeComponent,
    UpdatepopupComponent,
    ErrorComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    MaterialModule,
    ReactiveFormsModule,
    HttpClientModule,
    ToastrModule.forRoot(),
    RouterModule.forRoot(appRoute) //declares for the router the pages
  ],
  providers: [SerialServiceService],
  bootstrap: [AppComponent]
})
export class AppModule { }
