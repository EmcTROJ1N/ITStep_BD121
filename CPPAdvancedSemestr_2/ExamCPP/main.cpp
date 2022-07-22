#include <iostream>
#include <string>
#include <algorithm>
#include "Word.h"
#include "Sentence.h"
#include "Text.h"

using namespace std;

void task1()
{
    Word word("hello");
    word.Print();
    cout << endl;

    Word word2 = word;
    word2.Print();
    cout << endl;

    Word word3;
    word3 = word;
    word3.Print();
    cout << endl;

    cout << "Length: " << word.Length() << endl;

    word.SetStr("shalom");
    word.Print();
    cout << endl << word.GetStr() << endl;
    cout << word[0] << endl;

    cout << endl;
}

void task2()
{
    Sentence sent
    (
" Хорошие пельмени - это  очень вкусно, на самом  деле ,рецепт простой: много мяса ,мало теста ."
    );
    Sentence test = sent;
    test.Print();
    Sentence tt;
    tt.Set
    (
"Хорошие пельмени - это очень вкусно! на самом деле ,рецепт простой: много мяса ,мало теста ."
    );
    cout << tt.Get() << endl;
    tt.Set("Tmp Hello");
    tt.Add("World");
    tt.Remove(0);
    cout << tt.Get() << endl;
    tt = sent;
    cout << tt.Get() << endl;
    cout << sent[0] << endl;
    test.Set("Hello world. C++ better than C.");
    test.Print();
}

void task3()
{
    Text text
    (
" Хорошие пельмени - это очень вкусно! На  самом деле ,рецепт простой: много мяса ,мало теста. "
    );
    cout << text << endl;
    
    Text tt = text;
    
    Sentence ss("Hello world.");
    tt += ss;
    tt.Remove(0);
    cout << tt << endl;
    Text t5;
    t5 = tt;
    cout << t5 << endl;

    text.Save();
    text.Load();
    cout << text << endl;

    Text t2("Hello. Hello World!");
    t2.SaveWords();
    system("cat saveWords.txt");
}

int main()
{
    task3();
    return 0;
}