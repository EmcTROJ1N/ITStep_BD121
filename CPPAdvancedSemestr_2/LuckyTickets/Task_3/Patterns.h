#pragma once
#include "Ticket.h"
#include <algorithm>
#include <functional>
#include <list>

list<function<bool(Ticket &)>> patterns
{
    [](Ticket &tic)
    {
        vector<int> num = tic.GetNum();
        if (num[0] + num[1] + num[2] ==
            num[3] + num[4] + num[5])
            return true;
        return false;
    },

    [](Ticket &tic)
    {
        vector<int> num = tic.GetNum();

        for (int i = 1; i < num.size(); i++)
        {
            if (num[i - 1] > num[i])
                return false;
        }

        return true;
    },

    [](Ticket &tic)
    {
        vector<int> num = tic.GetNum();

        for (int i = 1; i < num.size(); i++)
        {
            if (num[i - 1] < num[i])
                return false;
        }
        return true;
    },

    [](Ticket &tic)
    {
        vector<int> num = tic.GetNum();
        if (count(num.begin(), num.end(), num[0]) == 6)
            return true;
        return false;
    },

    [](Ticket &tic)
    {
        vector<int> num = tic.GetNum();
        for (int i = 0, j = 1; i < num.size() - 2; i += 2, j += 2)
        {
            if ((num[j + 2] != num[j]) || (num[i + 2] != num[i]))
                return false;
        }
        return true;
    }
};