import { Injectable } from '@angular/core';
import 'rxjs/add/operator/map';
//import { Storage } from '@ionic/storage';
import { SQLite, SQLiteObject } from '@ionic-native/sqlite';
import { Platform } from 'ionic-angular';

/*
  Generated class for the Database provider.

  See https://angular.io/docs/ts/latest/guide/dependency-injection.html
  for more info on providers and Angular 2 DI.
*/
@Injectable()
export class SqlStorage {

    DB_NAME: string = 'ssddb';

    constructor( public platform: Platform, public sqlite: SQLite) {
        this.platform.ready().then(() => {
            this.configureDatabase();
        });
    }

    configureDatabase() {
        this.query('CREATE TABLE IF NOT EXISTS EMP (key text primary key, value text)')
            .catch(err => {
                console.error('Unable to create initial storage tables', err.tx, err.err);
            });
    }

    query(query: string, params: any[] = []): Promise<any> {
        return new Promise((resolve, reject) => {
            try {
                this.sqlite.create({
                    name: this.DB_NAME,
                    location: 'default'
                })
                    .then((db: SQLiteObject) => {
                        db.transaction((tx: any) => {
                            tx.executeSql(query, params,
                                (tx: any, res: any) => resolve({ tx: tx, res: res }),
                                (tx: any, err: any) => reject({ tx: tx, err: err }));
                        })
                    })
                    .catch(e => console.log(e));
            } catch (err) {
                reject({ err: err });
            }
        });
    }

    get(key: string): Promise<any> {
        return this.query('select key, value from EMP where key = ? limit 1', [key])
            .then(data => {
                if (data.res.rows.length > 0) {
                    return data.res.rows.item(0).value;
                }
            });
    }

    set(key: string, value: string): Promise<any> {
        return this.query('insert into EMP(key, value) values (?, ?)', [key, value]);
    }



}
