#include <wiringPi.h>
#include <stdio.h>
#include <string.h>

int fromBinary(char *s);

int main (void)
{
	printf ("Pobieranie danych\n") ;

	char password[7]="";//result of password generation
	char singleChar;
	int singleCharinInt;//singleBit char in password
	char znak[7]="";//binary data collected to creata singleBit CHAR
	char singleBit[1];//from reading(INT) to char
	int reading;

	int clocks;//clock from external source-FPGA
	int previousState=0;//previous state of clock

	int i=0;//counter for loop


  wiringPiSetup () ;
  pinMode (0, INPUT) ; //clock
  pinMode (7, INPUT) ; //data
  for (;;)
  {
		i=0;
		memset(znak, 0, 255);
		for(i=0;i<8;)
		{
			clocks=digitalRead(0);

			if(clocks==1&&previousState!=clocks){
				reading=digitalRead(7);
				sprintf(singleBit,"%d",reading);
				printf("%s ",singleBit );
				const char *strFrom=singleBit;
				strcat(znak,strFrom);
				printf("Odebrano %s \n",znak);
				if(i==7)
				{
					singleCharinInt=fromBinary(znak);
					printf("Wyliczona liczba %d \n",singleCharinInt);
					singleChar=singleCharinInt+'0';
					printf("Odebrany znak %c \n",singleChar);
				}
				i++;
			}
			previousState=clocks;
  	}

	}
  return 0 ;
}

int fromBinary(char *s)
{
  return (int) strtol(s, NULL, 2);
}
