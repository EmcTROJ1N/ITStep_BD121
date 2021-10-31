#include <iostream>
#include <io.h>

using namespace std;

void main()
{
    // _A_NORMAL 0x00 // Normal file - No read/write restrictions        00000000
    // _A_RDONLY 0x01 // Read only file                                    00000001
    // _A_HIDDEN 0x02 // Hidden file                                    00000010
    // _A_SYSTEM 0x04 // System file                                    00000100
    // _A_SUBDIR 0x10 // Subdirectory                                    00001000
    // _A_ARCH   0x20 // Archive file                                    00010000
    //                                                                    00010001
    //                                                                    &
    //                                                                    00010000
    // переменная, хранящая информацию об одном файле
    _finddata_t c_file;

    // адрес списка найденных файлов
    long hFile;

    // найти файлы по маске и вернуть адрес списка найденных файлов и информацию о первом файле
    hFile = _findfirst("/home/anton/ITStep_BD121/CPP/*.*", &c_file );
    //
    //    10101
    //    &
    //    10000
    //    10000
    //
    // если файл является папкой
    if(c_file.attrib & _A_SUBDIR)
        cout << c_file.name << "   " << "<DIR>" << endl;
    else
        cout << c_file.name << "   " << c_file.size << endl;

    // цикл для получения информации об остальных найденных файлах
    while( _findnext( hFile, &c_file ) == 0 )
    {
        if(c_file.attrib & _A_SUBDIR)
            cout << c_file.name << "   " << "<DIR>" << endl;
        else
            cout << c_file.name << "   " << c_file.size << endl;
    }

    // освободить память от списка нденных файлов
    _findclose(hFile);
}