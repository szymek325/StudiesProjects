angular.module('starter.controllers', [])

.controller('TempCtrl', function($scope, $interval, $ionicModal, receivedData, bluetoothInformation, $ionicLoading) {
	// INITIAL VALUES
	$scope.currentFoto="wartosc";
	$scope.currentTemperature="stopnie";
	$scope.currentDistance="value";

	var interval1=$interval(receiveData, 2000);
	 var timeX=new Date().toTimeString().split(" ")[0];
	// var counter=0;

	// $scope.labels1 = [timeX];
	// $scope.series1 = [['Actual Temperature'],['Setpoint']];
	// $scope.data1=[[22],[22]];
	// $scope.colors = ['#ff6384','#ff6384'];
	//
	// $scope.labels2 = [timeX];
	// $scope.series2 = ['PWM'];
	// $scope.data2=[0];
	// $scope.color2 = ['#ff6384'];

	function receiveData(){
		//counter=counter+1;
		var timeX=new Date().toTimeString().split(" ")[0];
		receivedData.getData();
		if(receivedData.getIsThereData())
		{
			$scope.currentTemperature = receivedData.getTemperature();
			$scope.currentFoto = receivedData.getFoto();
			$scope.currentDistance=receivedData.getDistance();

			// if(counter>=3){
			// 	updateCharts();
			// 	counter=0;
			// }
		}
	}

	// function updateCharts(){
	// 	timeX=new Date().toTimeString().split(" ")[0];
	// 	if($scope.labels1.length>=150){
	// 		console.log($scope.labels1.length);
	// 		$scope.labels1=$scope.labels1.slice(1,150);
	// 		$scope.data1[0]=$scope.data1[0].slice(1,150);
	// 		$scope.data1[1]=$scope.data1[1].slice(1,150);
	//
	// 		$scope.labels2=$scope.labels2.slice(1,150);
	// 		$scope.data2=$scope.data2.slice(1,150);
	// 	}
	//
	// 	$scope.labels1.push(timeX);
	// 	$scope.data1[0].push($scope.currentTemperature);
	// 	$scope.data1[1].push($scope.currentSetpoint);
	//
	// 	$scope.labels2.push(timeX);
	// 	$scope.data2.push($scope.pwmSignal);
	// }

	// $scope.resetChart= function(){
	// 	$scope.data1=[[receivedData.getTemperature()],[receivedData.getSetpoint()]];
	// 	$scope.labels1=[timeX];
	// 	$scope.data2=[receivedData.getPwm()];
	// 	$scope.labels2=[timeX];
	// }


//
// 	$scope.options1 = {
// 		animation:false,
// 		scales: {
// 			yAxes: [
// 			{
// 				id: 'y-axis-1',
// 				type: 'linear',
// 				display: true,
// 				position: 'left',
// 			}],
// 			xAxes: [{
// 				ticks: {
// 					autoSkip:true,
// 					maxTicksLimit:4,
// 				}
// 			}],
// 		},
// 	}
//
// 	$scope.options2 = {
// 		animation:false,
// 		scales: {
// 			yAxes: [
// 			{
// 				id: 'y-axis-1',
// 				type: 'linear',
// 				display: true,
// 				position: 'left',
// 			}],
// 			xAxes: [{
// 				ticks: {
// 					autoSkip:true,
// 					maxTicksLimit:4,
// 				}
// 			}],
// 		},
// 	}
//
//
 })

.controller('ControlCtrl', function($scope, $ionicModal, bluetoothInformation) {
	// INITIAL VALUES


	$scope.sendSetpoint= function(argument){
		if(bluetoothInformation.getConnectionState()){
			console.log(argument);
			bluetoothSerial.write(argument, function (data){
				console.log("Data sent")}, function (data){
					console.log("Data not sent")});
		}
		else{
			alert('You should connect to a device first');
		}
	}
})

.controller('BlueCtrl', function($scope, $timeout, $interval, $ionicLoading, bluetoothInformation) {
	// INITIAL VALUES
	$scope.addresses = [];
	$scope.textConnect='Connect';
	$scope.typeConnect='button button-block button-calm';


	$timeout(function() {
		bluetoothInformation.isBluetoothON();
	}, 500);



	$scope.checkConnection=function(){
		bluetoothInformation.checkConnectionState();
		var bluetoothState=bluetoothInformation.getConnectionState();
		console.log("connectionstate "+bluetoothState);
		if(bluetoothState){
			$scope.$apply(function () {
				console.log("You are not connected");
				$scope.textConnect = 'Connect';
				$scope.typeConnect='button button-block button-calm';

			})
		}
		else{
			$scope.$apply(function () {
				console.log("You are connected to device");
				$scope.textConnect = 'Disconnect';
				$scope.typeConnect='button button-block button-assertive';
			})

		}
	}

	$scope.checkConnection1=function(){
		$scope.$apply(function () {
			console.log("You are not connected");
			$scope.textConnect = 'Connect';
			$scope.typeConnect='button button-block button-calm';
		})
	}

	$scope.findDevices = function(){
		if(bluetoothInformation.isBluetoothON())
		{
			$ionicLoading.show();
			bluetoothSerial.list(function (data) {
				console.log("List: "+data);
				$scope.$apply(function () {
					$scope.pairedDevices=data})},function () {
					console.log("No devices found");
				});
			bluetoothSerial.discoverUnpaired(function (data) {
				console.log("Discover Unpaired: "+data);
				$scope.$apply(function () {
					$scope.discoveredDevices=data});$ionicLoading.hide();},function () {
					console.log("No devices found");
					$ionicLoading.hide();
				});
		}
	}

	$scope.connectMac = function(data){
		if(bluetoothInformation.isBluetoothON()){
			$ionicLoading.show();
			bluetoothInformation.checkConnectionState();
			if(bluetoothInformation.getConnectionState()){
				bluetoothSerial.disconnect(function (){
					console.log("You have been disconnected"); alert("You have been disconnected"); $scope.checkConnection1();$ionicLoading.hide()}, function (){
						console.log("Disconnection wasn't possible");alert("Disconnection wasn't possible");$scope.checkConnection();$ionicLoading.hide()
					});
			}
			else{
				bluetoothSerial.connect(data.address, function (){
					console.log("You have been connected to device:"); alert("You have been connected");$scope.checkConnection();$ionicLoading.hide()}, function (){
						console.log("Connection wasn't possible");alert("Connection wasn't possible");$scope.checkConnection1();bluetoothInformation.checkConnectionState();$ionicLoading.hide()
					});

			};
			bluetoothInformation.checkConnectionState();
		}
		else{

		}
	}

})
