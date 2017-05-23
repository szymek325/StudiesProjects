

angular.module('starter.services', [])

.service('receivedData', function() {
  // INITIAL VALUES
  var temperatureReceived;
  var fotoReceived;
  var distanceReceived;
  var fishReceived;
  var movementReceived;
  var humidityReceived;
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
    getHumidity: function () {
      return humidityReceived;
    },
    getFish: function () {
      return fishReceived;
    },
    getMovement: function () {
      return movementReceived;
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

            fotoReceived=data.substring(data.indexOf('r')+1, data.indexOf('f'));
            fishReceived=data.substring(data.indexOf('f')+1, data.indexOf('c'));
            movementReceived=data.substring(data.indexOf('c')+1, data.indexOf('o'));
            distanceReceived=data.substring(data.indexOf('o')+1, data.indexOf('t'));
            temperatureReceived=data.substring(data.indexOf('t')+1, data.indexOf('h'));
            humidityReceived=data.substring(data.indexOf('h')+1, data.indexOf('/'));
            console.log()
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
