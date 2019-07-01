#include <wiringPi.h>
#include <stdio.h>

int main (void)
{
	printf ("Raspberry Pi - Gertboard Blink\n") ;
	int input;
  wiringPiSetup () ;
  pinMode (0, INPUT) ;
  pinMode (7, OUTPUT) ;
  for (;;)
  {
	digitalWrite(7,LOW);
    input=digitalRead(0) ; 
	printf("%d powinno byc 0 \n",input);
	delay (500) ;
	
	digitalWrite(7,HIGH);
    input=digitalRead (0) ; 
	printf("%d powinno byc 1 \n",input);
	delay (500) ;
  }
  return 0 ;
}