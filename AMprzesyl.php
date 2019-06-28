<?php
	$zadanie = $_GET["z"];
	$par1 = $_GET["p"];
	$wynik=exec("./AMuruchom.sh $zadanie $par1");
	
	$json=json_encode($wynik);
	echo "$json";
	
	//echo "$wynik";	
?>
