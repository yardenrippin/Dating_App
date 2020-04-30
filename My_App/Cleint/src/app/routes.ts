
import {Routes} from '@angular/router';
import { HomeComponent } from './home/home.component';
import { MemberListComponent } from './Members/member-list/member-list.component';

import { ListsComponent } from './Lists/Lists.component';
import { MessagesComponent } from './messages/messages.component';
import { AuthGuard } from './_guards/auth.guard';
import { MemberDetailComponent } from './Members/member-detail/member-detail.component';
import { MemberDetailresolver } from './_resolver/member-detail.resolver';
import { Memberlistresolver } from './_resolver/member-list.resolver';
import { MemberEditComponent } from './Members/member-edit/member-edit.component';
import { MemberEditresolver } from './_resolver/member-edit.resolver';
import { PreventUnsavedChanges } from './_guards/prevent-unsavde-changes.guard';
import { Listlistresolver } from './_resolver/list.resolver';
import { Messagesresolver } from './_resolver/message.resolver';


export const appRoutes: Routes = [

{path: '', component: HomeComponent},
{path: 'members', component: MemberListComponent , canActivate: [AuthGuard], resolve: {users: Memberlistresolver }},
{path: 'members/:id', component: MemberDetailComponent, canActivate: [AuthGuard] , resolve: { user: MemberDetailresolver}},
{path: 'messages', component: MessagesComponent, canActivate: [AuthGuard], resolve:{messages: Messagesresolver}},
{path: 'lists', component: ListsComponent, canActivate: [AuthGuard], resolve: { user: Listlistresolver}},
{path: 'member/edit', component: MemberEditComponent, canActivate: [AuthGuard],
canDeactivate: [PreventUnsavedChanges] , resolve: { user: MemberEditresolver}},
{path: '**', redirectTo: '', pathMatch: 'full'}


];


