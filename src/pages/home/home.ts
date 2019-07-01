import { Component } from '@angular/core';
import { NavController } from 'ionic-angular';
import {SqlStorage} from '../../providers/sql-storage';
import { GeneratorPage } from '../generator/generator';

@Component({
  selector: 'page-home',
  templateUrl: 'home.html'
})
export class HomePage {

  constructor(public navCtrl: NavController, public sqlStorage: SqlStorage) {

 }

 goGenerator(event){
   this.navCtrl.setRoot(GeneratorPage)
 }

}
