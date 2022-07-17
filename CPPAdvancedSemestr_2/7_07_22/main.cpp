#include <iostream>
#include <fstream>
#include <map>
#include <string>

using namespace std;

int main()
{
    map<string, string> dict;
    cout << "Welcome. What do you want to do?" << endl;
    while (true)
    {
        cout << "1 - Add pair" << endl
             << "2 - See translate" << endl
             << "3 - Edit pair" << endl
             << "4 - Remove pair" << endl
             << "5 - See all pairs" << endl
             << "6 - Save dict" << endl
             << "7 - Load dict" << endl
             << "8 - Exit." << endl;
        int key;
        cin >> key;
        switch (key)
        {
        case 1:
        {
            cout << "Enter pls word and translate: ";
            string word, translate;
            cin >> word >> translate;
            dict[word] = translate;
            break;
        }

        case 2:
        {
            string word;
            cout << "Enter word pls: ";
            cin >> word;
            cout << "Translate is " << dict[word] << endl;
            break;
        }

        case 3:
        {
            string oldWord, newWord, newTranslate;
            cout << "Enter old word: ";
            cin >> oldWord;
            cout << "Enter new pair: ";
            cin >> newWord >> newTranslate;
            dict.erase(oldWord);
            dict[newWord] = newTranslate;
            break;
        }
        
        case 4:
        {
            string word;
            cout << "Enter word for remove them: ";
            cin >> word;
            dict.erase(word);
            break;
        }

        case 5:
        {
            for (auto pair : dict)
                cout << pair.first << " " << pair.second << endl;
            break;
        }

        case 6:
        {
            ofstream os("data.log");
            for (auto pair : dict)
                os << pair.first << " " << pair.second << endl;
            os.close();
            break;
        }

        case 7:
        {
            ifstream is("data.log");

            while (!is.eof())
            {
                string key, val;
                is >> key >> val;
                dict[key] = val;
            }

            is.close();
            break;
        }

        case 8:
        {
            cout << "Good bye." << endl;
            return 0;
            break;
        }
        
        default:
            cout << "Invalid input" << endl;
            break;
        }
        cout << endl;
    }
    return 0;
}