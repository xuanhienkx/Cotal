import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { AppComponent } from './app.component';
import { routing } from "app/app.routing";
import { HttpModule, BaseRequestOptions } from "@angular/http";
// used to create fake backend
import { fakeBackendProvider } from 'app/core/helpers/fake-backend';
import { MockBackend, MockConnection } from '@angular/http/testing';
import { AuthGuard } from "app/core/guards/auth.guard";
import { AuthenService } from "app/core/services/authen.service";
@NgModule({
  declarations: [
    AppComponent
  ],
  imports: [
    BrowserModule,
    FormsModule,
    HttpModule,
    routing
  ],
  providers: [
    AuthGuard,
    AuthenService,
    fakeBackendProvider,
    MockBackend,
    BaseRequestOptions],
  bootstrap: [AppComponent]
})
export class AppModule { }
