#include <wiringPi.h>
#include <stdio.h>
#include <string.h>

int fromBinary(char *s);

int main (int argc, char **argv)
{
	//printf ("Pobieranie danych\n") ;
	//int num = atoi(argv);
	int num;
	sscanf (argv[1],"%d",&num);
	//printf("%d",num);

	char password[50]="";//result of password generation
	char singleCharArray[1];//array that makes it possible to append char to string
	char singleChar;
	int singleCharinInt;//singleBit char in password
	char znak[7]="";//binary data collected to creata singleBit CHAR
	char singleBit[1];//from reading(INT) to char
	int reading;

	int clocks;//clock from external source-FPGA
	int previousState=0;//previous state of clock

	int i=0;//counter for loop
	int j=0;//counter for password loop

  wiringPiSetup () ;
  pinMode (0, INPUT) ; //clock
  pinMode (7, INPUT) ; //data
  //for (;;)
  //{
		//j=0;
		for(j=0;j<num;)
		{
			i=0;
			memset(znak, 0, 7);
			for(i=0;i<8;)
			{
				clocks=digitalRead(0);

				if(clocks==1&&previousState!=clocks){
					reading=digitalRead(7);
					sprintf(singleBit,"%d",reading);
					printf("%s ",singleBit );
					const char *strFrom1=singleBit;
					strcat(znak,strFrom1);
					printf("Odebrano %s \n",znak);
					if(i==7){
						singleCharinInt=fromBinary(znak);
					}
					i++;
				}
				previousState=clocks;
  		}//end of bits FOR
			singleCharinInt=singleCharinInt%78;
			if(0<=singleCharinInt&&singleCharinInt<79){
				singleChar=singleCharinInt+'0';
				sprintf(singleCharArray,"%c",singleChar);
				printf("Odebrany znak w CHAR %c \n",singleChar);
				printf("Odebrany znak w tablicy %s \n",singleCharArray);
				strcat(password,singleCharArray);
				j++;
			}
			if(j==num){
				//printf("Utworzone haslo %s \n",password);
				//printf("DOLICZONO DO 12!!!!!!!!! \n");
			}
		}//end of password FOR
		printf("\"%s\"\n",password);
		//printf("dupa \n");
		return 1;
	//}
}

int fromBinary(char *s)
{
  return (int) strtol(s, NULL, 2);
}
