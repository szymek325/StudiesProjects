angular.module('starter.controllers', [])

.controller('TempCtrl', function($scope, $interval, $ionicModal, receivedData, bluetoothInformation, $ionicLoading, $state) {
	// INITIAL VALUES
	$scope.currentFoto="value";
	$scope.currentTemperature="value";
	$scope.currentDistance="value";
	$scope.currentMovement="value";
	$scope.currentHumidity="value";
	$scope.currentFish="value";
	$scope.currentTime="value"

	$scope.foto=[{time:"time",value:"fotoresistor"}]
	$scope.temperature=[{time:"time",value:"temperature"}]
	$scope.distance=[{time:"time",value:"distance"}]
	$scope.movement=[{time:"time",value:"movement"}]
	$scope.humidity=[{time:"time",value:"humidity"}]
	$scope.fish=[{time:"time",value:"fish sensor"}]
	//$scope.time=["start"];
	var letitGo=1;

	var interval1=$interval(receiveData, 2000);
	var timeX=new Date().toTimeString().split(" ")[0];

	$state.go('tab.bluetooth');

	function receiveData(){
		//counter=counter+1;
		$scope.currentTime=new Date().toTimeString().split(" ")[0];
		receivedData.getData();
		if(receivedData.getIsThereData())
		{
			$scope.currentTemperature = receivedData.getTemperature();
			$scope.currentFoto = receivedData.getFoto();
			$scope.currentDistance=receivedData.getDistance();
			$scope.currentFish=receivedData.getFish();
			$scope.currentMovement=receivedData.getMovement();
			$scope.currentHumidity=receivedData.getHumidity();
			updateArrays();
		}
	}

	function updateArrays(){
		if($scope.temperature.length>=60){
			$scope.temperature=$scope.temperature.slice(1,60);
			$scope.humidity=$scope.humidity.slice(1,60);
			$scope.foto=$scope.foto.slice(1,60);
			$scope.distance=$scope.distance.slice(1,60);
			$scope.movement=$scope.movement.slice(1,60);
			$scope.fish=$scope.fish.slice(1,60);
		}
	$scope.temperature.push({time:$scope.currentTime,value:$scope.currentTemperature});
	$scope.humidity.push({time:$scope.currentTime,value:$scope.currentHumidity});
	$scope.foto.push({time:$scope.currentTime,value:$scope.currentFoto});
	$scope.distance.push({time:$scope.currentTime,value:$scope.currentDistance});
	$scope.movement.push({time:$scope.currentTime,value:$scope.currentMovement});
	$scope.fish.push({time:$scope.currentTime,value:$scope.currentFish});
	if(letitGo){
		letitGo=0;
		$scope.temperature=$scope.temperature.slice(1,2);
		$scope.humidity=$scope.humidity.slice(1,2);
		$scope.foto=$scope.foto.slice(1,2);
		$scope.distance=$scope.distance.slice(1,2);
		$scope.movement=$scope.movement.slice(1,2);
		$scope.fish=$scope.fish.slice(1,2);
	}
}


	$scope.openModal = function(index) {
	if (index == 1) $scope.oModal1.show();
	else if(index==2) $scope.oModal2.show();
	else if(index==3) $scope.oModal3.show();
	else if(index==4) $scope.oModal4.show();
	else if(index==5) $scope.oModal5.show();
	else $scope.oModal6.show();
	}

	$scope.closeModal = function(index) {
		if (index == 1) $scope.oModal1.hide();
		else if(index==2) $scope.oModal2.hide();
		else if(index==3) $scope.oModal3.hide();
		else if(index==4) $scope.oModal4.hide();
		else if(index==5) $scope.oModal5.hide();
		else $scope.oModal6.hide();
	}

	$ionicModal.fromTemplateUrl('templates/temperature.html', {
	id: '1',
	scope: $scope}).then(function(modal) {
		$scope.oModal1 = modal;
	})

	$ionicModal.fromTemplateUrl('templates/humidity.html', {
	id: '2',
	scope: $scope}).then(function(modal) {
		$scope.oModal2 = modal;
	})

	$ionicModal.fromTemplateUrl('templates/distance.html', {
	id: '3',
	scope: $scope}).then(function(modal) {
		$scope.oModal3 = modal;
	})

	$ionicModal.fromTemplateUrl('templates/movement.html', {
	id: '4',
	scope: $scope}).then(function(modal) {
		$scope.oModal4 = modal;
	})

	$ionicModal.fromTemplateUrl('templates/fish.html', {
	id: '5',
	scope: $scope}).then(function(modal) {
		$scope.oModal5 = modal;
	})

	$ionicModal.fromTemplateUrl('templates/foto.html', {
	id: '6',
	scope: $scope}).then(function(modal) {
		$scope.oModal6 = modal;
	})

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
