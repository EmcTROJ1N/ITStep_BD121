#include <iostream>
#include <boost/filesystem.hpp>
#include <string>

using namespace std;
using namespace boost::filesystem;

void showFolderInfo(path folderPath)
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
            cout << "<DIR>" << newPath << endl;

            showFolderInfo(_newPath);
        }
        else
            cout << item.path().filename() << "    " << item.path().size() << endl;
    }
}


int main()
{
    char _path[666];
    cin >> _path;
    path p = _path;
    showFolderInfo(p);
    return 0;
}