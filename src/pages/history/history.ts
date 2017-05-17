import { Component } from '@angular/core';
import { NavController, NavParams } from 'ionic-angular';
import {SqlStorage} from '../../providers/sql-storage';
import { GeneratorPage } from '../generator/generator';

@Component({
  selector: 'page-history',
  templateUrl: 'history.html'
})
export class HistoryPage {
selectedItem:any
allData:any
  constructor(public navCtrl: NavController, public sqlStorage: SqlStorage, public navParams: NavParams) {
     //this.sqlStorage.set('nazwa','godzina')
     this.selectedItem = navParams.get('list')
     console.log(this.selectedItem)
     this.allData=this.selectedItem
     console.log(this.allData)
 }
 showData(event){
   //this.sqlStorage.get(1+"").then(data => {console.log(data);})

 }


}
