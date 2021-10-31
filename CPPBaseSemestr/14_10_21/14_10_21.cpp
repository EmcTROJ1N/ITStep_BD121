#include <iostream>
#include <boost/filesystem/operations.hpp>
#include <vector>
#include <string>
#include <inttypes.h>

using namespace std;
using namespace boost::filesystem;

void task_1()
{
    char _path[666];
    cin >> _path;
    directory_iterator it(_path);
    directory_iterator end;
    int countFiles = 0;
    int sizeFiles = 0;
    while (it != end)
    {
        file_status fs = status(*it);
        if (fs.type() != directory_file)
            sizeFiles += file_size(*it);
        it++;
        countFiles++;
    }
    cout << "Cout of files without mask: " << countFiles << endl;
    cout << "Size of files without mask: " << sizeFiles;
}

void task_2()
{
    char _path[666];
    char filename[666];
    cin >> _path;
    cin >> filename;
    directory_iterator it(_path);
    directory_iterator end;
    FILE* file;
    file = fopen(filename, "w");
    while (it != end)
    {
        path p = *it;
        char name[66];
        char size[60];
        strcpy(name, p.filename().string().c_str());
        fputs("File: ", file);
        fputs(name, file);
        
        file_status fs = status(*it);
        if (fs.type() != directory_file)
        {
            sprintf(size," Size is %" PRIuMAX "\n", file_size(p));
            fputs(size, file);
        }
        it++;
    }
    fclose(file);
}

int main()
{
    task_2();
    return 0;
}