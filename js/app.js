var app=angular.module('myApp',['ngAnimate', 'ngSanitize', 'ui.bootstrap','chart.js'])


app.controller('myCtrl', function($scope, $interval, receiveData) {
	//data
	$scope.data1={'amplituda':50};
	$scope.ampPila=0;
	// chart view
	$scope.chartVisibility="hidden";
	var chartState=false;
	$scope.chartText="";
	$scope.chartStyle="btn btn-primary";
	//panel view
	$scope.controlPanelVisibility="hidden";
	var controlPanelState=false;
	$scope.controlPanelText="";
	$scope.controlPanelStyle="btn btn-primary";
	//all data view
	$scope.dataLogVisibility="hidden";
	var dataLogState=false;
	$scope.dataLogText="";
	$scope.dataLogStyle="btn btn-primary";
	//download button
	$scope.downloadButtonText="OFF";
	$scope.downloadButtonStyle="btn btn-primary";
	var downloadButtonState=false;
	$scope.singleDownloadState=1;
	//chart data
	var timeX=new Date().toTimeString().split(" ")[0];
	$scope.labels1 = [timeX];
	$scope.series1 = ['Actual Temperature'];
	$scope.data1=[$scope.ampPila];
	$scope.colors = ['#ff6384'];
	var oneTimeOnly=0;
	//intervals
	var interval1;
	var interval2;
	///
	var table = document.getElementById("dataLog");
	////

	////////////////////////////	FUNKCJA POBIERANIA DANYCH 	////////////////////////////////////
	function downloadData(){
		$scope.ampPila=receiveData.getData();
		timeX=new Date().toTimeString().split(" ")[0];
		updateDataLog();
	}
	////////////////////////////	PRZYCISKi	pobieranie 	/	////////////////////////////////////
	$scope.downloadToggle=function(){
		if(downloadButtonState){
			$scope.downloadButtonText="OFF";
			$scope.downloadButtonStyle="btn btn-primary";
			downloadButtonState=!downloadButtonState;
			$interval.cancel(interval1);
			$interval.cancel(interval2);
			$scope.singleDownloadState=1;
		}
		else{
			$scope.downloadButtonText="ON";
			$scope.downloadButtonStyle="btn btn-danger";
			downloadButtonState=!downloadButtonState;
			interval1=$interval(downloadData, 10);
			interval2=$interval(updateChart,2000);
			$scope.singleDownloadState=0;
		}
	}

	$scope.downloadSingleData=function(){
		$scope.ampPila=receiveData.getData();
		updateChart();
		updateDataLog();
	}
	////////////////////////////		INNE do wykresu	i data logu/////////////////////////////////////
	$scope.resetChart= function(){
		timeX=new Date().toTimeString().split(" ")[0];
		$scope.data1=[$scope.ampPila];
		$scope.labels1=[timeX];
	}
	function updateChart(){
		//timeX=new Date().toTimeString().split(" ")[0];
		$scope.labels1.push(timeX);
		$scope.data1.push($scope.ampPila);
	}
	function updateDataLog(){
		var row = table.insertRow(1);
		var cell1 = row.insertCell(0);
		var cell2 = row.insertCell(1);
		cell1.innerHTML = timeX;
		cell2.innerHTML = $scope.ampPila;
	}
	////////////////////////////		FUNKCJE PANEL 	/	////////////////////////////////////
	$scope.controlPanelToggle=function(){
		$scope.chartHide();
		$scope.dataLogHide();
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
		$scope.controlPanelText="Hide";
		$scope.controlPanelStyle="btn btn-danger";
	}
	////////////////////////////		FUNKCJE WYKRESY		/////////////////////////////////////
	$scope.chartToggle=function(){
		$scope.controlPanelHide();
		$scope.dataLogHide();
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
		$scope.chartText="Hide";
		$scope.chartStyle="btn btn-danger";
	}

////////////////////////////		FUNKCJE DATA LOG		/////////////////////////////////////
$scope.dataLogToggle=function(){
	$scope.chartHide();
	$scope.controlPanelHide();
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
	$scope.dataLogText="Hide";
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
					//min:0,
					//max:500,
				}	
			}],
			xAxes: [{
				//type: 'time',
				ticks: {
					autoSkip:true,
					maxTicksLimit:6,
				} 
			}],	
		},
	}

})


app.service('receiveData', function() {

	var receivedData=5;

	return{
		getData: function(){

			//receivedData=$http.get("//localhost/funkcja.php?z=A&p=50");
			receivedData=receivedData+1;
			return receivedData;
		},
		sendData: function(){
			$http.get("//localhost/funkcja.php?z=B&p="+value);
			
		}


	}


})



	// $scope.chartOnOff= function(){
	// 	if(chartPermission){
	// 		chartPermission=0;
	// 		$scope.chartStartStop='Start';
	// 		$scope.chartButtonStyle='button button-block button-positive';
	// 		$interval.cancel(interval2);
	// 		$scope.showHideChart="hidden"
	// 	}
	// 	else{
	// 		if(oneTimeOnly==0){
	// 			$scope.resetChart();
	// 			oneTimeOnly=1;
	// 		}
	// 		chartPermission=1;
	// 		$scope.chartStartStop='Stop';
	// 		$scope.chartButtonStyle='button button-block button-assertive';
	// 		interval2=$interval(updateCharts, 1000);
	// 		$scope.showHideChart="show"
	// 	}
	// }