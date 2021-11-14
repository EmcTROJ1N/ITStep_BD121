#include <iostream>
#include <cstring>
#include <vector>
#include <string>

using namespace std;

struct Worker
{
    int number;

    char name[666];
    char surname[666];
    char patronymic[666];

    char dateBith[10];
    char adress[666];
    char phoneNumber[666];
    char salary[666];
};


Worker getWorker()
{
    Worker worker;
    char number[666];
    
    do
    {
        cout << "Enter workers number: ";
        cin >> number;
    } while (atoi(number) == 0);
    worker.number = atoi(number);   

    string str;
    int k = 0;

    cout << "Enter worker`s FIO: ";
    
    while (k != 2)
    {
        getline(cin, str);
        for (int i = 0; i < str.size(); i++)
        {
            if (str[i] == ' ') k++;
        }
    }

    char buf[666];
    strcpy(buf, str.c_str());
    sscanf(buf, "%s %s %s", &worker.surname, &worker.name, &worker.patronymic);

    cout << "Enter workers date birth: ";
    cin >> worker.dateBith;

    cout << "Enter workers phone number: ";
    cin >> worker.phoneNumber;

    cout << "Enter worker salary: ";
    cin >> worker.salary;
    cout << endl;

    return worker;
}

void printWorker(Worker worker)
{
    cout << "Worker`s number: " << worker.number << endl;
    cout << "Worker`s FIO: " << worker.surname << " " << worker.name << " " << worker.patronymic << endl;
    cout << "Worker`s date birth: " << worker.dateBith << endl;
    cout << "Worker`s phone number: " << worker.phoneNumber << endl;
    cout << "Worker`s salary: " << worker.salary << endl;
}

void printWorkers(vector<Worker> workers)
{
    for (int i = 0; i < workers.size(); i++)
    {
        printWorker(workers[i]);
        cout << endl;
        if (i < workers.size() - 1)
            cout << "-------------------" << endl;
    } 
}

void enterWorkers(vector<Worker> &workers, int size)
{
    for (int i = 0; i < size; i++) workers[i] = getWorker();
}

void printMenu()
{
	cout << "1. Add worker" << endl;
	cout << "2. Print workers" << endl;
	cout << "3. Save DB to file" << endl;
	cout << "4. Load DB from file" << endl;
    cout << "5. Delete one worker" << endl;
    cout << "6. Find worker (name)" << endl;
    cout << "7. Change workers`s data" << endl;
	cout << "8. Exit" << endl << endl;
}

bool saveData(vector<Worker> workers)
{
    FILE* file = fopen("log.dat", "wb");
    if (file == NULL) return false;
    
    Worker* _workers = new Worker[workers.size()];
    for (int i = 0; i < workers.size(); i++)
        _workers[i] = workers[i];
    fwrite(_workers, sizeof(Worker), workers.size(), file);

    delete[] _workers;
    fclose(file);

    file = fopen("count.dat", "wb");
    int count = workers.size();
    fwrite(&count, sizeof(int), 1, file);
    fclose(file);
    
    return true;
}

bool loadData(vector<Worker> &workers)
{
    FILE* file = fopen("count.dat", "rb");
    if (file == NULL) return false;

    int count;
    fread(&count, sizeof(int), 1, file);
    fclose(file);

    file = fopen("log.dat", "rb");
    if (file == NULL) return false;

    Worker* _workers = new Worker[count];
    fread(_workers, sizeof(Worker), count, file);

    for (int i = 0; i < count; i++)
        workers.push_back(_workers[i]);

    delete[] _workers;

    return true;
}

bool changeData(vector<Worker> &workers, int what, int index)
{
    bool _flag = false;
    for (int i = 0; i < workers.size(); i++)
    {
        if (workers[i].number == index) _flag = true;
    }

    if (_flag == false) return false;


    switch (what)
    {
        case 1:
        {
            char name[666];
            cout << "Enter new param: ";
            cin >> name;

            for (int i = 0; i < workers.size(); i++)
            {
                if (workers[i].number == index)
                    strcpy(workers[i].name, name);
            }

            break;
        }

        case 2:
        {
            char surname[666];
            cout << "Enter new param: ";
            cin >> surname;

            for (int i = 0; i < workers.size(); i++)
            {
                if (workers[i].number == index)
                    strcpy(workers[i].surname, surname);
            }
            break;
        }

        case 3:
        {
            char patronymic[666];
            cout << "Enter new param: ";
            cin >> patronymic;

            for (int i = 0; i < workers.size(); i++)
            {
                if (workers[i].number == index)
                    strcpy(workers[i].patronymic, patronymic);
            }
 
            break;
        }

        case 4:
        {
            int numb;
            cout << "Enter new param: ";
            cin >> numb;
            for (int i = 0; i < workers.size(); i++)
            {
                if (workers[i].number == index)
                    workers[i].number = numb;
            }
            break;
        }

        case 5:
        {
            char phoneNumber[666];
            cout << "Enter new param: ";
            cin >> phoneNumber;

            for (int i = 0; i < workers.size(); i++)
            {
                if (workers[i].number == index)
                    strcpy(workers[i].phoneNumber, phoneNumber);
            }

            break;
        }

        case 6:
        {
            char dateBirth[666];
            cout << "Enter new param: ";
            cin >> dateBirth;

            for (int i = 0; i < workers.size(); i++)
            {
                if (workers[i].number == index)
                    strcpy(workers[i].dateBith, dateBirth);
            }

            break;
        }

        case 7:
        {
            char salary[666];
            cout << "Enter new param: ";
            cin >> salary;

            for (int i = 0; i < workers.size(); i++)
            {
                if (workers[i].number == index)
                    strcpy(workers[i].salary, salary);
            }

            break;
        }

        default:
            return false;
        break;
    }

    return true;
}


int main()
{
    vector<Worker> workers;

    while (true)
    {
        printMenu();

        int keyCode;
        cin >> keyCode;

        char filename[666];

        switch (keyCode)
        {
            case 1:
            {
                workers.push_back(getWorker());
                break;
            }

            case 2:
            {
                printWorkers(workers);
                break;
            }

            // Сохрнанить
            case 3:
            {
                if (saveData(workers)) cout << "Success." << endl;
                else cout << "Something went wrong" << endl;
            
                break;
            }
            
            // Загрузить
            case 4:
            {
                if (loadData(workers)) cout << "Success." << endl;
                else cout << "Something went wrong" << endl;
                break;
            }

            // Удаление одного работника
            case 5:
            {
                char _index[666];
                int index;
                do 
                {
                    cout << "Enter numb of worker for delete: ";
                    cin >> _index;
                } while(atoi(_index) == 0);

                index = atoi(_index);

                for (int i = 0; i < workers.size(); i++)
                {
                    if (workers[i].number == index)
                        workers.erase(workers.begin() + i);
                }
                cout << endl;
                break;
            }

            // Найти работника
            case 6:
            {
                char name[666];
                cout << "Enter worker`s name: ";
                cin >> name;

                bool flag = true;

                for (int i = 0; i < workers.size(); i++)
                {
                    if (strcmp(workers[i].name, name) == 0)
                    {
                        flag = false;
                        cout << "Worker finded: " << endl << "~~~~~~~~~" << endl;
                        printWorker(workers[i]);
                        cout << endl;
                    }
                }
            
                if (flag)
                    cout << "There no that worker" << endl;

                cout << endl;
                break;


            }
             // Изменить данные работника
            case 7:
            {
                int number;
                char _number[666];
                do
                {
                    cout << "Enter worker`s number: ";
                    cin >> _number;

                } while (atoi(_number) == 0);

                number = atoi(_number);
                
                int what;
                char _what[666];

                do
                {
                    cout << "What are you want to change?" << endl;
                    cout << "1 - Name" << endl;
                    cout << "2 - Surname" << endl;
                    cout << "3 - Patronymic" << endl; 
                    cout << "4 - Number" << endl;
                    cout << "5 - Phone number" << endl; 
                    cout << "6 - Date birth" << endl; 
                    cout << "7 - Salary" << endl; 
                    cin >> _what;

                } while (atoi(_what) == 0);

                what = atoi(_what);

                if (changeData(workers, what, number)) cout << "Succcess." << endl;
                else cout << "Something went wrong..." << endl;

                break;
            }

            case 8:
            {
                cout << "Good bye";
                return 0;
                break;
            }

            default:
            {
                cout << "Invalid input..." << endl;
                break;
            }
        }

    }
    return 0;
}