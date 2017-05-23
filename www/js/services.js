

angular.module('starter.services', [])

.service('receivedData', function() {
  // INITIAL VALUES
  var temperatureReceived;
  var fotoReceived;
  var distanceReceived
  var isThereData;

  return {
    getTemperature: function () {
      return temperatureReceived;
    },
    getFoto: function () {
      return fotoReceived;
    },
    getDistance: function () {
      return distanceReceived;
    },
    getIsThereData: function () {
      return isThereData;
    },
    getData: function () {
      bluetoothSerial.readUntil("/n",function (data) {
        console.log(data);
        if(data.includes("t"))
        {
          if(data.includes("n"))
          {
            isThereData=1;

            temperatureReceived=data.substring(data.indexOf('t')+1, data.indexOf('s'));
            fotoReceived=data.substring(data.indexOf('s')+1, data.indexOf('p'));
            distanceReceived=data.substring(data.indexOf('p')+1, data.indexOf('/'));

            bluetoothSerial.clear(console.log(),console.log());
          }
          else{
            isThereData=0;
          }
        }
        else{
          isThereData=0;
          bluetoothSerial.clear(console.log(),console.log())
        }
      },function () {console.log("failure")});
    }
  }
})

.service('bluetoothInformation', function($timeout) {
  // INITIAL VALUES
  var bluetoothOnOffStatus;
  var bluetoothConnectionStatus;

  return {
    isBluetoothON: function () {
      bluetoothSerial.isEnabled(function (){
        console.log("Bluetooth is ON"), bluetoothOnOffStatus=1;},function (){
          console.log("Bluetooth is OFF. Please ENABLE");
          bluetoothOnOffStatus=0;
          bluetoothSerial.enable(function (){
            console.log("Bluetooth was ENABLED"); bluetoothOnOffStatus=1},function (){
              console.log("Bluetooth wasn't ENABLED");
              console.log("Please enable bluetooth");
              alert("Application is not going to work correctly without Bluetooth turned ON")
              bluetoothOnOffStatus=0;
              //navigator.app.exitApp();
            })
        })
      return bluetoothOnOffStatus;
    },
    checkConnectionState: function () {
      bluetoothSerial.isConnected(function(){bluetoothConnectionStatus=1},function(){bluetoothConnectionStatus=0});
    },
    getConnectionState: function () {
      return bluetoothConnectionStatus;
    }
  }
});
