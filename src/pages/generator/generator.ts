import { Component } from '@angular/core';
import { NavController } from 'ionic-angular';
import {SqlStorage} from '../../providers/sql-storage';
import { HistoryPage } from '../history/history';

@Component({
  selector: 'page-generator',
  templateUrl: 'generator.html'
})
export class GeneratorPage {
generatedPassword:string
//passwordList:any
passwordList: Array<{time: String, data: string}>;
myDate: String=new Date().toTimeString().split(" ")[0]
  constructor(public navCtrl: NavController, public sqlStorage: SqlStorage) {
     //this.sqlStorage.set('baza','dupa');
     //this.sqlStorage.set('nazwa','godzina')
     this.generatedPassword="jeszcze nie uzyskano",
     this.passwordList=[]


 }

 setData(event){
   this.generatedPassword="nowe haslo"
   this.passwordList.push({time:this.myDate,data:this.generatedPassword})
   //this.sqlStorage.get('baza').then(data => {console.log(data);})
   //this.sqlStorage.set(1+"",'dupa');
   //this.navCtrl.push(HistoryPage);
 }

 goHistory(event){
   //this.sqlStorage.get('baza').then(data => {console.log(data);})
   this.navCtrl.push(HistoryPage,{list:this.passwordList})
 }

}
