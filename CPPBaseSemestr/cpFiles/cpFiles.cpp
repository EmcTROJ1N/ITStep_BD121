#include <iostream>
#include <boost/filesystem/operations.hpp>
#include <string>
#include <vector>

using namespace std;
using namespace boost::filesystem;

string returnExtension(path p)
{
    string str = "";
    if (!(is_directory(p)))
        {
            str = p.filename().string();
            str = str.substr(str.find_last_of(".") + 1);
        }
    return str;
}

vector<string> split(const string& str, const string& delim)
{
    vector<string> tokens;
    size_t prev = 0, pos = 0;
    do
    {
        pos = str.find(delim, prev);
        if (pos == string::npos) pos = str.length();
        string token = str.substr(prev, pos - prev);
        if (!token.empty()) tokens.push_back(token);
        prev = pos + delim.length();
    } while (pos < str.length() && prev < str.length());
    return tokens;
}

void task_1()
{
    char _path[666];
    cin >> _path;
    path p = _path;
    directory_iterator begin(_path);
    directory_iterator end;
    remove_all(p);
}

void recursive_copy(path src, path dst, string extension)
{
    if (is_directory(src)) {
        create_directories(dst);
        for (directory_entry& item : directory_iterator(src))
        {
            if (returnExtension(item.path()) == extension)
                recursive_copy(item.path(), dst / item.path().filename(), extension);
        }
    }
    else if (is_regular_file(src)) {
        copy(src, dst);
    }
    else {
        throw runtime_error(dst.generic_string() + " not dir or file");
    }
}


void task_2()
{
    char path1[666];
    char path2[666];
    string extension;
    cin >> path1 >> path2 >> extension;
    recursive_copy(path1, path2, extension);
}



int main()
{
    task_2();
    return 0;
}