import { Component } from '@angular/core';
import { NavController } from 'ionic-angular';
import {SqlStorage} from '../../providers/sql-storage';
import { HomePage } from '../home/home';


@Component({
  selector: 'page-history',
  templateUrl: 'history.html'
})
export class HistoryPage {

  constructor(public navCtrl: NavController, public sqlStorage: SqlStorage) {
     //this.sqlStorage.set('nazwa','godzina')

 }
 showData(event){
   this.sqlStorage.get(1+"").then(data => {console.log(data);})
 }

 goHome(event){
   //this.sqlStorage.get('baza').then(data => {console.log(data);})
   this.navCtrl.push(HomePage);
 }

}
