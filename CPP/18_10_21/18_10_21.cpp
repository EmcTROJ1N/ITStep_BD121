#include <iostream>
#include <boost/filesystem.hpp>
#include <string>

using namespace std;
using namespace boost::filesystem;

void recursion(path folderPath, FILE* fileWrite)
{
    for (directory_entry& item : directory_iterator(folderPath))
    {
        if (is_directory(item))
        {
            string newPath;
            newPath = folderPath.string();
            newPath += "/";
            newPath += item.path().filename().string();
            cout << newPath;
            path _newPath = newPath;

            recursion(_newPath, fileWrite);
        }
        else
        {
            if (item.path().extension() == ".txt")
            {
                char buf[666];
                FILE* fileRead;
                strcpy(buf, item.path().string().c_str());
                fileRead = fopen(buf, "r");
                char str[666];
                while (!feof(fileRead))
                {
                    strcpy(str, "");
                    fgets(str, 666, fileRead);
                    fputs(str, fileWrite);
                    
                }
                fclose(fileRead);
            }
        }
    }
}

int main()
{
    char _path[666];
    cin >> _path;
    path p = _path;
    FILE* fileWrite = fopen("res.txt", "w");
    recursion(p, fileWrite);
    fclose(fileWrite);
}