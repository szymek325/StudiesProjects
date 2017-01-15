var app=angular.module('myApp',['ngAnimate', 'ngSanitize', 'ui.bootstrap','chart.js'])


app.controller('myCtrl', function($scope, $interval, receiveData) {
	//data
	$scope.suwak={'amplituda':250};
	$scope.suwak1={'czas':100};
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
	$scope.controlPanelVisibility="hidden";
	var controlPanelState=false;
	$scope.controlPanelText="";
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
	$scope.colors = ['#ff6384'];
	var oneTimeOnly=0;
	//intervals
	var interval1;
	///
	var table = document.getElementById("dataLog");
	var pila=0;
	var ampReceived;
	var counter;
	////
	$scope.dataSent="value"

	////////////////////////////	FUNKCJA POBIERANIA DANYCH 	////////////////////////////////////
	function downloadData(){
		counter=counter+1;
		ampReceived=receiveData.getData();
		$scope.ampPila=ampReceived;
		timeX=new Date().toTimeString().split(" ")[0];
		if(!(counter%1)){
			updateChart();
		}
		if(!(counter%200)){
			updateDataLog();
		}
	}

	function updateChart(){
		$scope.labels1.push(timeX);
		$scope.data1.push(ampReceived);
	}

	function updateDataLog(){
		var row = table.insertRow(1);
		var cell1 = row.insertCell(0);
		var cell2 = row.insertCell(1);
		cell1.innerHTML = timeX;
		cell2.innerHTML = $scope.ampPila;
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
			interval1=$interval(downloadData, 10);
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
	legend: {display: true},
	scales: {
		yAxes: [
		{
			id: 'y-axis-1',
			type: 'linear',
			display: true,
			position: 'left',
			ticks: {
				min:0,
				max:500,
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


app.service('receiveData', function($http) {
	var receivedDataJSON;
	var receivedData;
	return{
		getData: function(){

			$http.get("AMprzesyl.php?z=A&p=50").then(function(response){
				receivedDataJSON=response.data;
			})
			return receivedDataJSON;
		},
		sendData: function(data){
			$http.get("AMprzesyl.php?z=B&p="+data);
			console.log(data);
		}
	}
})
