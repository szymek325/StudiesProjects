import { Component } from '@angular/core';
import { NavController } from 'ionic-angular';
//import {SqlStorage} from '../../providers/sql-storage';
import { HistoryPage } from '../history/history';
import { Http} from '@angular/http';
import 'rxjs/add/operator/map';
import {AlertController} from 'ionic-angular';
import {LoadingController} from 'ionic-angular';

@Component({
  selector: 'page-generator',
  templateUrl: 'generator.html'
})
export class GeneratorPage {
generatedPassword:string;
public dlugosc=10;
//passwordList:any
passwordList: Array<{time: String, data: string}>;
myDate: String=new Date().toTimeString().split(" ")[0]
  constructor(public navCtrl: NavController, private http:Http, public loadingCtrl: LoadingController) {// public sqlStorage: SqlStorage
     //this.sqlStorage.set('baza','dupa');
     //this.sqlStorage.set('nazwa','godzina')
     this.generatedPassword="jeszcze nie uzyskano",
     this.passwordList=[];
     //this.passwordLenght.value={'value':'10'};
   }
 setData(event){
   let loading = this.loadingCtrl.create({
   content: 'Downloading...'});
   loading.present();
   setTimeout(() => {
    loading.dismiss();
  }, 2000);
    var cos=Number(this.dlugosc)
    //var par=this.dlugosc.ToString();
   this.myDate=new Date().toTimeString().split(" ")[0];
   var url='http://172.24.1.1/index.php?z='+cos;
   console.log(url);
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
