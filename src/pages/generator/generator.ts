import { Component } from '@angular/core';
import { NavController } from 'ionic-angular';
import {SqlStorage} from '../../providers/sql-storage';
import { HistoryPage } from '../history/history';
import { Http} from '@angular/http';
import 'rxjs/add/operator/map';
import {AlertController} from 'ionic-angular';

@Component({
  selector: 'page-generator',
  templateUrl: 'generator.html'
})
export class GeneratorPage {
generatedPassword:string
//passwordList:any
passwordList: Array<{time: String, data: string}>;
myDate: String=new Date().toTimeString().split(" ")[0]
  constructor(public navCtrl: NavController, public sqlStorage: SqlStorage, private http:Http) {
     this.sqlStorage.set('baza','dupa');
     //this.sqlStorage.set('nazwa','godzina')
     this.generatedPassword="jeszcze nie uzyskano",
     this.passwordList=[]


 }

 setData(event){
   this.myDate=new Date().toTimeString().split(" ")[0]
   var url='http://192.168.0.29/skrypt.php';
    this.http.get(url).map(res => res.json()).subscribe(received=>{
      this.passwordList.push({time:this.myDate,data:received})
      this.generatedPassword=received;
    })

   //this.generatedPassword="nowe haslo"
   //this.passwordList.push({time:this.myDate,data:this.generatedPassword})
   //this.sqlStorage.get('baza').then(data => {console.log(data);})
   //this.sqlStorage.set(1+"",'dupa');
   //this.navCtrl.push(HistoryPage);
 }

 goHistory(event){
   //this.sqlStorage.get('baza').then(data => {console.log(data);})
   this.navCtrl.push(HistoryPage,{list:this.passwordList})
 }

 //countData(event){
   //this.sqlStorage.get('baza').then(data => {console.log(data);})
  // var ilosc=this.sqlStorage.count()
   //alert(ilosc)
 //}

}
