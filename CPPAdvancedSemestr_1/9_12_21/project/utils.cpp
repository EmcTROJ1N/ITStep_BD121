#include "pch.h"

void init(char titleWindow[666], char header[666])
{
    system("clear");
    
    printf("%c]0;%s%c", '\033', titleWindow, '\007');
    cout << header;
}
