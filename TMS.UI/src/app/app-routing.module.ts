import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AddUserComponent } from './pages/add-user/add-user.component';
import { HomeComponent } from './pages/home/home.component';
import { UserListPageComponent } from './pages/user-list-page/user-list-page.component';
import { UserViewComponent } from './pages/user-view/user-view.component';

const routes: Routes = [{ path: '', component: AddUserComponent }];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
