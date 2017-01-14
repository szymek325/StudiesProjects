<?php
	$zadanie = $_GET["z"];
	$par1 = $_GET["p"];
	//$wynik=exec("./AMuruchom.sh $zadanie $par1");
	//$wynik=$zadanie.$par1;
	$wynik=rand(10,20);
	$json=json_encode($wynik);
	echo $json;	
?>
