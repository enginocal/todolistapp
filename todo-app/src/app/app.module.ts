import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';

import { AppComponent } from './app.component';
import { TodoDataService } from './todo-data.service';
import { TodoListComponent } from './todo-list/todo-list.component';
import { TodoListFooterComponent } from './todo-list-footer/todo-list-footer.component';
import { TodoListHeaderComponent } from './todo-list-header/todo-list-header.component';
import { TodoListItemComponent } from './todo-list-item/todo-list-item.component';
import { ApiService } from './api.service';
import { RegisterComponent } from './register/register.component';
import { UserServiceService } from './user-service.service';
import { RegisterServiceService } from './register-service.service';

@NgModule({
  declarations: [
    AppComponent,
    TodoListComponent,
    TodoListFooterComponent,
    TodoListHeaderComponent,
    TodoListItemComponent,
    RegisterComponent
  ],
  imports: [
    BrowserModule,
    FormsModule,
    HttpModule
  ],
  providers: [TodoDataService, ApiService, UserServiceService, RegisterServiceService],
  bootstrap: [AppComponent]
})
export class AppModule { }