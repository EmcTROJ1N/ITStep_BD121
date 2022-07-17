#include <iostream>
#include <fstream>
#include <map>
#include <string>
#include <set>

using namespace std;

void task1()
{
    map<string, int> mp;
    multimap<int, string> res;

    string filename("test.txt");
    // cin >> filename;
    ifstream is(filename);
    string str;
    for (is >> str; !is.eof(); is >> str)
        mp[str] += 1;
    is.close();

    for(auto it = mp.begin(); it != mp.end(); it++)
        res.insert(pair<int, string>(it->second, it->first));

    ofstream os("res.txt");
    for(auto it = res.rbegin(); it != res.rend(); it++)
        os << it->first << " " << it->second << endl;
    os.close();
}

int main()
{
    task1();
    return 0;
}