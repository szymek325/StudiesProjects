import { Component } from '@angular/core';
import { NavController } from 'ionic-angular';
import {SqlStorage} from '../../providers/sql-storage';
import { HistoryPage } from '../history/history';

@Component({
  selector: 'page-home',
  templateUrl: 'home.html'
})
export class HomePage {

  constructor(public navCtrl: NavController, public sqlStorage: SqlStorage) {
     //this.sqlStorage.set('baza','dupa');
     //this.sqlStorage.set('nazwa','godzina')

 }

 setData(event){
   //this.sqlStorage.get('baza').then(data => {console.log(data);})
   this.sqlStorage.set(1+"",'dupa');
   this.navCtrl.push(HistoryPage);
 }

 goHistory(event){
   //this.sqlStorage.get('baza').then(data => {console.log(data);})
   this.navCtrl.setRoot(HistoryPage);
 }

}
