#include <wiringPi.h>
#include <stdio.h>
#include <string.h>

int fromBinary(char *s);

int main (void)
{
	printf ("znaki Ascii\n") ;


	int i=0;//counter for loop
	char znak;

	//wiringPiSetup () ;

  for (i=0;i<79;i++)
  {
		znak=i+'0';
		printf("%d  Odebrany znak:  %c \n",i,znak);
	}
}
