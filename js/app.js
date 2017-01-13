var app=angular.module('myApp',['ngAnimate', 'ngSanitize', 'ui.bootstrap','chart.js'])


app.controller('myCtrl', function($scope, $interval) {

	$scope.data1={'amplituda':50};
	$scope.chartStartStop='Start';
	chartPermission=0;
	$scope.ampPila=0;
	$scope.showHideChart="hidden"

	var oneTimeOnly=0;
	var timeX=new Date().toTimeString().split(" ")[0];
	var interval2;

	$scope.labels1 = [timeX];
	$scope.series1 = ['Actual Temperature'];
	$scope.data1=[$scope.ampPila];
	$scope.colors = ['#ff6384'];

	function updateCharts(){
		timeX=new Date().toTimeString().split(" ")[0];
		$scope.ampPila=$scope.ampPila+1;
		$scope.labels1.push(timeX);
		$scope.data1.push($scope.ampPila);
	}

	$scope.resetChart= function(){
		timeX=new Date().toTimeString().split(" ")[0];
		$scope.data1=[$scope.ampPila];
		$scope.labels1=[timeX];
	}

	$scope.chartOnOff= function(){
		if(chartPermission){
			chartPermission=0;
			$scope.chartStartStop='Start';
			$scope.chartButtonStyle='button button-block button-positive';
			$interval.cancel(interval2);
			$scope.showHideChart="hidden"
		}
		else{
			if(oneTimeOnly==0){
				$scope.resetChart();
				oneTimeOnly=1;
			}
			chartPermission=1;
			$scope.chartStartStop='Stop';
			$scope.chartButtonStyle='button button-block button-assertive';
			interval2=$interval(updateCharts, 1000);
			$scope.showHideChart="show"
		}
	}

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
					maxTicksLimit:6,
				} 
			}],	
		},
	}

})
