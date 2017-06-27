import { Routes ,RouterModule} from '@angular/router';
import { MainComponent } from './main.component';

export const mainRouting: Routes = [
    {
        path: '', component: MainComponent, children: [
            //localhost:4200/main
            { path: '', redirectTo: 'home', pathMatch: 'full' },
            //localhost:4200/main/home
            { path: 'home', loadChildren: './home/home.module#HomeModule' }
        ]
    }] 
    export const MainRouting = RouterModule.forChild(mainRouting);