import { Component } from '@angular/core';
import { NavController, NavParams } from 'ionic-angular';
import {SqlStorage} from '../../providers/sql-storage';
import { GeneratorPage } from '../generator/generator';
import {AlertController} from 'ionic-angular';
import { Clipboard } from '@ionic-native/clipboard';


@Component({
  selector: 'page-history',
  templateUrl: 'history.html'
})
export class HistoryPage {
selectedItem:any
allData:any
  constructor(private clipboard: Clipboard,private alertController: AlertController,public navCtrl: NavController, public sqlStorage: SqlStorage, public navParams: NavParams) {
     //this.sqlStorage.set('nazwa','godzina')
     this.selectedItem = navParams.get('list')
     console.log(this.selectedItem)
     this.allData=this.selectedItem
     console.log(this.allData)
 }

  copyToClipboard(password:string){
  this.clipboard.copy(password)
  console.log(password)
  this.clipboard.paste().then(
     (resolve: string) => {
        alert("Hasło zostało skopiowane");
      },
      (reject: string) => {
        alert('Error: ' + reject);
      }
    );
  }


}
