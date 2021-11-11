#include <iostream>
#include <string.h>

using namespace std;

int main()
{
    char filename[666];
    cin >> filename;
    FILE* file = fopen(filename, "r");
    
    char str[666];
    int k = 0;
    while (!feof(file))
    {
        fgets(str, 66, file);
        k++;
    }
    rewind(file);

    char** arr = new char*[k];
    int x = 0;

    while (!feof(file))
    {
        strcpy(str, "");
        fgets(str, 666, file);
        arr[x] = strdup(str);
        x++;
    }
    fclose(file);

	for (int i = 0; i < k; i++)
	{
		for (int j = 0; j < k - 1; j++)
		{
            if (strcmp(arr[j + 1], arr[j]) > 0)
			{
                char tmp[99];
                strcpy(tmp, arr[j + 1]);
                strcpy(arr[j + 1], arr[j]);
                strcpy(arr[j], tmp);
			}
		}
	}

    file = fopen("res.txt", "w");

    for (int i = 0; i < k; i++) fputs(arr[i], file);
    
    fclose(file);
    delete[] arr;
    return 0;
}