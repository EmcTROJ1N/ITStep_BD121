#pragma once
#include <string>
#include <vector>
#include <iostream>
#include <algorithm>

using namespace std;

class Ticket
{
protected:
    vector<int> num;
public:
    Ticket() {}
    Ticket(int tic)
    {
        int k = 0;
        while (tic != 0)
        {
            num.push_back(tic % 10);
            tic /= 10;
            k++;
        }
        reverse(num.begin(), num.end());
        if (k < 6)
        {
            for (int i = k; i < 6; i++)
                num.push_back(0);
        }
        if (k > 6)
            num.erase(num.begin() + 6, num.end());
    }
    void Print()
    {
        for (auto elem : num)
            cout << elem;
        cout << endl;
    }

    vector<int> GetNum() { return num; }
};