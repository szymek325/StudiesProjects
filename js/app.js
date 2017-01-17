var app=angular.module('myApp',['ngAnimate', 'ngSanitize', 'ui.bootstrap','chart.js'])


app.controller('myCtrl', function($scope, $interval, receiveData) {
	//data
	$scope.suwak={'amplituda':10};
	//$scope.suwak1={'czas':100};
	$scope.ampPila=0;
	// chart view
	$scope.chartVisibility="hidden";
	var chartState=false;
	$scope.chartText="";
	$scope.chartStyle="btn btn-primary";
	//all data view
	$scope.dataLogVisibility="hidden";
	var dataLogState=false;
	$scope.dataLogText="";
	$scope.dataLogStyle="btn btn-primary";
	//download button
	$scope.downloadButtonText="Włącz";
	$scope.downloadButtonStyle="btn btn-primary";
	var downloadButtonState=false;
	$scope.singleDownloadState=1;
	//panel view
	$scope.controlPanelVisibility="hidden"
	$scope.controlPanelText="";
	var controlPanelState=false;
	$scope.controlPanelStyle="btn btn-primary";
		//MENU pobieranie
	$scope.menuPobieranieVisibility="hidden";
	var menuPobieranieState=false;
	$scope.menuPobieranieText="";
	$scope.menuPobieranieStyle="btn btn-primary";
	//chart data
	var timeX=new Date().toTimeString().split(" ")[0];
	$scope.labels1 = [timeX];
	$scope.series1 = ['Actual Temperature'];
	$scope.data1=[$scope.ampPila];
	var oneTimeOnly=0;
	//intervals
	var interval1;
	///
	var table = document.getElementById("dataLog");
	var pila=0;
	var ampReceived;
	var counter=0;
	var statusDiody;
	////
	$scope.dataSent="value"

	////////////////////////////	FUNKCJA POBIERANIA DANYCH 	////////////////////////////////////
	function downloadData(){
		counter=counter+1;
		receiveData.downloadData();
		receiveData.downloadDioda();
		receiveData.downloadAmp();
		console.log($scope.ampPila);
		updateChart();
	}

	function updateChart(){
		if($scope.labels1.length>=60){
			console.log($scope.labels1.length);
			$scope.labels1=$scope.labels1.slice(1,60);
			$scope.data1=$scope.data1.slice(1,60);
		}
		console.log($scope.data1[59]);
		if(receiveData.getData()>20){
			
		}
		else if(receiveData.getData().length>2){
			
		}
		else if(receiveData.getData().length==0){
			
		}
		// else if($scope.ampPila-receiveData.getData()>2){
			// if(receiveData.getData()==0){
				// $scope.ampPila=receiveData.getData();
				// timeX=new Date().toTimeString().split(" ")[0];
				// $scope.labels1.push(timeX);
				// $scope.data1.push($scope.ampPila);
				// updateDataLog()
			// }
		// }
		// else if($scope.ampPila-receiveData.getData()<-2){
			// if(receiveData.getData()==0){
				// $scope.ampPila=receiveData.getData();
				// timeX=new Date().toTimeString().split(" ")[0];
				// $scope.labels1.push(timeX);
				// $scope.data1.push($scope.ampPila);
				// updateDataLog()
			// }
		// }
		else{
			if(receiveData.getAmpValue().length>2){
				
			}
			else{
				$scope.ampPila=receiveData.getData();
				timeX=new Date().toTimeString().split(" ")[0];
				$scope.labels1.push(timeX);
				$scope.data1.push($scope.ampPila);
				updateDataLog();
			}
		}
		
	}

	function updateDataLog(){
		if(table.rows.length>=61){
			table.deleteRow(60);
		}
		if(receiveData.getDiodaState()==1){
			statusDiody="Włączona"
		}
		else{
			statusDiody="Wyłączona"
		}
		var row = table.insertRow(1);
		var cell1 = row.insertCell(0);
		var cell2 = row.insertCell(1);
		var cell3 = row.insertCell(2);
		var cell4 = row.insertCell(3);
		cell1.innerHTML = timeX;
		cell2.innerHTML = $scope.ampPila;
		cell3.innerHTML = statusDiody;
		cell4.innerHTML = receiveData.getAmpValue();
	}
	////////////////////////////	PRZYCISKi	pobieranie i wysyłanie 	/	////////////////////////////////////
	$scope.downloadToggle=function(){
		if(downloadButtonState){
			$scope.downloadButtonText="Włącz";
			$scope.downloadButtonStyle="btn btn-primary";
			downloadButtonState=!downloadButtonState;
			$interval.cancel(interval1);
			$scope.singleDownloadState=1;
		}
		else{
			$scope.downloadButtonText="Wyłącz";
			$scope.downloadButtonStyle="btn btn-danger";
			downloadButtonState=!downloadButtonState;
			interval1=$interval(downloadData, 500);
			$scope.singleDownloadState=0;
		}
	}

	$scope.downloadSingleData=function(){
		ampReceived=receiveData.getData();
		$scope.ampPila=ampReceived;
		timeX=new Date().toTimeString().split(" ")[0];
		updateChart();
		updateDataLog();
	}

	$scope.sendAmp=function(){
		$scope.dataSent=receiveData.sendData($scope.suwak.amplituda);
	}
	
	$scope.sendDioda=function(){
		$scope.dataSent=receiveData.changeDiodeState();
	}
	////////////////////////////		INNE do wykresu	i data logu/////////////////////////////////////
	$scope.resetChart= function(){
		timeX=new Date().toTimeString().split(" ")[0];
		$scope.data1=[$scope.ampPila];
		$scope.labels1=[timeX];
		var tableHeaderRowCount = 1;
		var table = document.getElementById('dataLog');
		var rowCount = table.rows.length;
		for (var i = tableHeaderRowCount; i < rowCount; i++) {
			table.deleteRow(tableHeaderRowCount);
		}
	}
	////////////////////////////		FUNKCJE menu pboierania 	/	////////////////////////////////////
	$scope.menuPobieranieToggle=function(){
		//$scope.chartHide();
		//$scope.dataLogHide();
		if(menuPobieranieState){
			$scope.menuPobieranieHide();
		}
		else{
			$scope.menuPobieranieShow();
		}
	}
	$scope.menuPobieranieHide=function(){
		$scope.menuPobieranieVisibility="hidden";
		if(menuPobieranieState){
			menuPobieranieState=!menuPobieranieState;
		}
		$scope.menuPobieranieText="";
		$scope.menuPobieranieStyle="btn btn-primary";
	}
	$scope.menuPobieranieShow=function(){
		$scope.menuPobieranieVisibility="show";
		if(!menuPobieranieState){
			menuPobieranieState=!menuPobieranieState;
		}
		$scope.menuPobieranieText="Ukryj";
		$scope.menuPobieranieStyle="btn btn-danger";
	}
	////////////////////////////		FUNKCJE menu pboierania 	/	////////////////////////////////////
	$scope.controlPanelToggle=function(){
		//$scope.chartHide();
		//$scope.dataLogHide();
		if(controlPanelState){
			$scope.controlPanelHide();
		}
		else{
			$scope.controlPanelShow();
		}
	}
	$scope.controlPanelHide=function(){
		$scope.controlPanelVisibility="hidden";
		if(controlPanelState){
			controlPanelState=!controlPanelState;
		}
		$scope.controlPanelText="";
		$scope.controlPanelStyle="btn btn-primary";
	}
	$scope.controlPanelShow=function(){
		$scope.controlPanelVisibility="show";
		if(!controlPanelState){
			controlPanelState=!controlPanelState;
		}
		$scope.controlPanelText="Ukryj";
		$scope.controlPanelStyle="btn btn-danger";
	}

	////////////////////////////		FUNKCJE WYKRESY		/////////////////////////////////////
	$scope.chartToggle=function(){
		//$scope.dataLogHide();
		if(chartState){
			$scope.chartHide();
		}
		else{
			$scope.chartShow();
		}
	}
	$scope.chartHide=function(){
		$scope.chartVisibility="hidden";
		if(chartState){
			chartState=!chartState;
		}
		$scope.chartText="";
		$scope.chartStyle="btn btn-primary";
	}
	$scope.chartShow=function(){
		$scope.chartVisibility="show";
		if(!chartState){
			chartState=!chartState;
		}
		$scope.chartText="Ukryj";
		$scope.chartStyle="btn btn-danger";
	}

////////////////////////////		FUNKCJE DATA LOG		/////////////////////////////////////
$scope.dataLogToggle=function(){
	//$scope.chartHide();
	if(dataLogState){
		$scope.dataLogHide();
	}
	else{
		$scope.dataLogShow();
	}
}
$scope.dataLogHide=function(){
	$scope.dataLogVisibility="hidden";
	if(dataLogState){
		dataLogState=!dataLogState;
	}
	$scope.dataLogText="";
	$scope.dataLogStyle="btn btn-primary";
}
$scope.dataLogShow=function(){
	$scope.dataLogVisibility="show";
	if(!dataLogState){
		dataLogState=!dataLogState;
	}
	$scope.dataLogText="Ukryj";
	$scope.dataLogStyle="btn btn-danger";
}
////////////////////////////			/////////////////////////////////////
$scope.options1 = {
	animation:false,
	//legend: {display: true},
	scales: {
		yAxes: [
		{
			id: 'y-axis-1',
			type: 'linear',
			display: true,
			position: 'left',
			ticks: {
				min:0,
				max:20,
			}	
		}],
		xAxes: [{
				//type: 'time',
				ticks: {
					autoSkip:true,
					maxTicksLimit:5,
				} 
			}],	
		},
	}
})

//dioda C
//dioda D stan diody
//E amplituda
app.service('receiveData', function($http) {
	var receivedDataJSON;
	var receivedDataString
	
	var dataJSON;
	var dataString

	var ampJSON;
	var ampString;
	
	var diodaJSON;
	var	diodaString;
	
	
	var address="AMprzesyl.php?z=B&p=";
	var dataString;
	var dataToSend;
	return{
		downloadData: function(){	
			$http.get("AMprzesyl.php?z=A&p=50").then(function(response){
				receivedDataJSON=response.data;
				//console.log("Data JSON "+receivedDataJSON);
				receivedDataString=JSON.parse(receivedDataJSON);
				//console.log("Data String "+receivedDataString);
			})
		},
		downloadDioda: function(){
			$http.get("AMprzesyl.php?z=D&p=50").then(function(response){
				diodaJSON=response.data;
				//console.log("Dioda JSON "+diodaJSON);
				diodaString=JSON.parse(diodaJSON);
				//console.log("Dioda String "+diodaString);
			})
		},
		downloadAmp: function(){
			$http.get("AMprzesyl.php?z=E&p=50").then(function(response){
				ampJSON=response.data;
				//console.log("Amp JSON "+ampJSON);
				ampString=JSON.parse(ampJSON);
				//console.log("Amp String "+ampString);
			})
		},
		getData: function(){
			//console.log("JSON "+receivedDataJSON);
			//receivedDataString=JSON.parse(receivedDataJSON);
			//console.log("String "+receivedDataString);
			//receivedDataInt=parseInt(receivedDataString);
			//console.log("Int "+receivedDataInt);
			return receivedDataString;
		},
		getDiodaState: function(){
			return diodaString;
		},
		getAmpValue: function(){
			return ampString;
		},
		sendData: function(data){
			dataString= data.toString();
			dataToSend=address.concat(dataString);
			$http.get(dataToSend);
			console.log(dataToSend);
		},
		changeDiodeState: function(){
			$http.get("AMprzesyl.php?z=C&p=50")
		}
	}
})
