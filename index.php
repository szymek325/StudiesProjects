<?php
	$dlugosc = $_GET["z"];
	$wynik=exec("sudo ./read $dlugosc");
	echo "$wynik";
?>
