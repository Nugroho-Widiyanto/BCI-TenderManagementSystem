import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { ListTendersComponent } from './tendermanagement/list-tenders/list-tenders.component';
import { ManageTenderComponent } from './tendermanagement/manage-tender/manage-tender.component';
import { HttpClientModule } from '@angular/common/http';
import { CommonModule } from '@angular/common';
import { MaterialModule } from './material-module';

@NgModule({
  declarations: [
    AppComponent,
    ListTendersComponent,
    ManageTenderComponent
  ],
  imports: [
    CommonModule,
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    HttpClientModule,
    MaterialModule,
    FormsModule,   
    ReactiveFormsModule
  ],
  providers: [],
  entryComponents:[ManageTenderComponent],
  bootstrap: [AppComponent]
})
export class AppModule { }