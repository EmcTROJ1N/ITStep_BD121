#include <iostream>
#include <boost/filesystem/operations.hpp>

using namespace std;
using namespace boost::filesystem;

int main()
{
    char _path[666];
    cin >> _path;
    directory_iterator begin(_path);
    directory_iterator end;
    while (begin != end)
    {
        file_status stat = status(*begin);
        {
            switch (stat.type())
            {
            case regular_file: cout << "FILE"; break;
            case directory_file: cout << "DIR"; break;
            
            default: cout << "OTHER"; break;
            }
            cout << " ";
            if (stat.permissions() & owner_write) cout << "W";
            else
                if (stat.permissions() & owner_read) cout << "R";
                    else
                    {
                        if (stat.permissions() & owner_all) cout << "All";
                        else cout << "-";
                    }
            cout << " ";
            if (stat.type() != directory_file)
            {
                path p = *begin;
                cout << p.filename() << " " << file_size(p) << " Bytes ";
            }
            else
            {
                path p = *begin;
                cout << p.filename();
            }
        }
        begin++;
        cout << endl;
    }
    return 0;
}