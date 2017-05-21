#include <wiringPi.h>
#include <stdio.h>

int main (void)
{
	printf ("Wysylanie danych\n") ;
	int randomBit;
  wiringPiSetup () ;
  pinMode (2, OUTPUT) ;
  pinMode (3, OUTPUT) ;
  for (;;)
  {
	randomBit = rand() % 2;
	digitalWrite(2,randomBit);
	printf("wyslano %d \n",randomBit);
	digitalWrite(3,LOW);
	delay (500) ;
	digitalWrite(3,HIGH);
	delay (2) ;
  }
  return 0 ;
}
