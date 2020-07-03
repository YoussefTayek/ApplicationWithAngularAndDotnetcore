import { MemberMessagesComponent } from './members/member-messages/member-messages.component';
import { MessagesResolver } from './resolvers/messages.resolver';
import { ListsResolver } from './resolvers/lists.resolver';
import { PhotoEditorComponent } from './members/photo-editor/photo-editor.component';
import { PreventUnsavedChanges } from './_guards/prevent-unsaved-changes.guard';
import { MemberEditResolver } from './resolvers/member-edit-resolver';
import { MemberEditComponent } from './members/member-edit/member-edit.component';
import { MemberDetailResolver } from './resolvers/member-detail-resolver';
import { AlertifyService } from './_services/alertify.service';

import { UserService } from './_services/user.service';
import { appRoutes } from './routes';
import { AuthService } from './_services/auth.service';
import { BrowserModule, HAMMER_GESTURE_CONFIG, HammerGestureConfig } from '@angular/platform-browser';
import { BrowserAnimationsModule} from '@angular/platform-browser/animations';
import { RouterModule } from '@angular/router';
import { JwtModule } from '@auth0/angular-jwt';
import { NgxGalleryModule } from 'ngx-gallery';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { ErrorInteceptorProvider } from './_services/error.interceptor';
import { BsDropdownModule, TabsModule, BsDatepickerModule, PaginationModule, ButtonsModule } from 'ngx-bootstrap';
import { HttpClientModule} from '@angular/common/http';
import { FileUploadModule } from 'ng2-file-upload';
import { MemberDetailComponent } from './members/member-detail/member-detail.component';
import { AppComponent } from './app.component';
import { NavComponent } from './nav/nav.component';
import { HomeComponent } from './home/home.component';
import { RegisterComponent } from './register/register.component';
import { ListsComponent } from './lists/lists.component';
import { MessagesComponent } from './messages/messages.component';
import { MemberListComponent } from './members/member-list/member-list.component';
import { MemberCardComponent } from './members/member-card/member-card.component';
import { AuthGuard } from './_guards/auth.guard';
import { MemberListResolver } from './resolvers/member-list-resolver';
import { TimeAgoPipe} from 'time-ago-pipe';

export class CustomHammerConfig extends HammerGestureConfig  {
   overrides = {
       pinch: { enable: false },
       rotate: { enable: false }
   };
}
export function tokenGetter() {
   return localStorage.getItem('token');
}
@NgModule({
   declarations: [
      AppComponent,
      NavComponent,
      HomeComponent,
      RegisterComponent,
      ListsComponent,
      MessagesComponent,
      MemberListComponent,
      MemberCardComponent,
      MemberDetailComponent,
      MemberEditComponent,
      PhotoEditorComponent,
      MemberMessagesComponent,
      TimeAgoPipe
   ],
   imports: [
      BrowserModule,
      BrowserAnimationsModule,
      HttpClientModule,
      FormsModule,
      ReactiveFormsModule,
      BsDropdownModule.forRoot(),
      BsDatepickerModule.forRoot(),
      ButtonsModule.forRoot(),
      PaginationModule.forRoot(),
      RouterModule.forRoot(appRoutes),
      NgxGalleryModule,
      FileUploadModule,
      TabsModule.forRoot(),
      JwtModule.forRoot ({
         config: {

          tokenGetter,
          whitelistedDomains: ['localhost:5000'],
          blacklistedRoutes: ['localhost:5000/api/auth']
         }
       })
   ],
   providers: [
      AuthService,
      ErrorInteceptorProvider,
      AlertifyService,
      AuthGuard,
      PreventUnsavedChanges,
      UserService,
      MemberDetailResolver,
      MemberEditResolver,
      MemberListResolver,
      MessagesResolver,
      ListsResolver,
      { provide: HAMMER_GESTURE_CONFIG, useClass: CustomHammerConfig }
   ],
   bootstrap: [
      AppComponent
   ]
})
export class AppModule { }
