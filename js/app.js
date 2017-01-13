var app=angular.module('myApp',['ngAnimate', 'ngSanitize', 'ui.bootstrap','chart.js'])


app.controller('myCtrl', function($scope, $interval) {
	//data
	$scope.data1={'amplituda':50};
	$scope.ampPila=0;
	// chart view
	$scope.chartVisibility="hidden";
	var chartState=false;
	$scope.chartText="Show";
	//panel view
	$scope.controlPanelVisibility="hidden";
	var controlPanelState=false;
	$scope.controlPanelText="Show";
	//chart data
	var timeX=new Date().toTimeString().split(" ")[0];
	$scope.labels1 = [timeX];
	$scope.series1 = ['Actual Temperature'];
	$scope.data1=[$scope.ampPila];
	$scope.colors = ['#ff6384'];
	var oneTimeOnly=0;
	//intervals
	var interval1;

	$scope.panelToggle=function(){
		if(controlPanelState){
			$scope.controlPanelVisibility="hidden";
			controlPanelState=!controlPanelState;
			$scope.controlPanelText="Show";
		}
		else{
			$scope.controlPanelVisibility="show";
			controlPanelState=!controlPanelState;
			$scope.controlPanelText="Hide";
		}
	}

	$scope.chartToggle=function(){
		if(chartState){
			$scope.chartVisibility="hidden";
			chartState=!chartState;
			$scope.chartText="Show";
		}
		else{
			$scope.chartVisibility="show";
			chartState=!chartState;
			$scope.chartText="Hide";
		}
	}

	$scope.resetChart= function(){
		timeX=new Date().toTimeString().split(" ")[0];
		$scope.data1=[$scope.ampPila];
		$scope.labels1=[timeX];
	}

	function updateChart(){
		timeX=new Date().toTimeString().split(" ")[0];
		$scope.ampPila=$scope.ampPila+1;
		$scope.labels1.push(timeX);
		$scope.data1.push($scope.ampPila);
	}



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
