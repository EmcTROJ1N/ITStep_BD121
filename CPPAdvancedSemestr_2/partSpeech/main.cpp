#include <iostream>
#include <cstring>
#include <boost/filesystem/operations.hpp>

using namespace std;
using namespace boost::filesystem;

// void recFunc(path _path)
// {   
//     directory_iterator begin(_path);
//     directory_iterator end;
//     path p;

//     while (begin != end)
//     {
//         file_status stat = status(_path);
//         switch (stat.type())
//         {
//             case regular_file:
//                 p = *begin;
                // cout << p.filename();
//                 break;

//             case directory_file:
//                 recFunc(p);
//                 break;
        
//         default:
//             break;
//         }
//     }
// }

int main()
{
    char* path = new char[100];
    strcpy(path, "/home/omon/dir");



    return 0;
}