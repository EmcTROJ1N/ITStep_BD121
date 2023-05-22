#include <iostream>
#include <windows.h>

using namespace std;

HKEY GetRootKey(WCHAR* key)
{
    if (wcscmp(key, L"HKEY_CLASSES_ROOT") == 0) return HKEY_CLASSES_ROOT;
    if (wcscmp(key, L"HKEY_CURRENT_USER") == 0) return HKEY_CURRENT_USER;
    if (wcscmp(key, L"HKEY_LOCAL_MACHINE") == 0) return HKEY_LOCAL_MACHINE;
    if (wcscmp(key, L"HKEY_USERS") == 0) return HKEY_USERS;
    if (wcscmp(key, L"HKEY_CURRENT_CONFIG") == 0) return HKEY_CURRENT_CONFIG;
}

void splitPath(WCHAR* path, HKEY& branch, WCHAR* folder, WCHAR* file)
{
    for (int i = 0; path[i] != '\0'; i++)
    {
        if (path[i] == '/')
            path[i] = '\\';
    }
    WCHAR* firstSlash = wcschr(path, '\\');
    WCHAR* lastSlash = wcsrchr(path, '\\');
    WCHAR* key = new WCHAR[100];

    if (firstSlash == nullptr)
    {
        folder[0] = NULL;
        branch = NULL;
        wcscpy(file, path);
    }
    else if (firstSlash == lastSlash)
    {
        folder[0] = NULL;
        wcscpy(file, lastSlash + 1);
        WCHAR* key = new WCHAR[100];
        wcsncpy(key, path, firstSlash - path);
        *(key + (firstSlash - path)) = '\0';
        branch = GetRootKey(key);
    }
    else
    {
        //memmove(folder, firstSlash + 1, wcslen(firstSlash));
        wcscpy(folder, firstSlash + 1);
        WCHAR* lastSlash = wcsrchr(folder, '\\');
        *(lastSlash) = '\0';
        wcscpy(file, lastSlash + 1);

        wcsncpy(key, path, firstSlash - path);
        key[firstSlash - path] = '\0';
        branch = GetRootKey(key);
    }
    delete[] key;
}

UINT RenameRegistryKey(WCHAR* keyPath, WCHAR* newName)
{
    HKEY key;
    WCHAR* keyName = new WCHAR[100];
    WCHAR* parentKeyPath = new WCHAR[100];
    HKEY branch;
    splitPath(keyPath, branch, parentKeyPath, keyName);
    LONG lResult;
    
    if (parentKeyPath[0] == NULL)
        key = branch;
    else
    {
        lResult = RegOpenKeyEx(branch, parentKeyPath, NULL, KEY_WRITE, &key);
        if (lResult != ERROR_SUCCESS) return 1;
    }
    
    lResult = RegRenameKey(key, keyName, newName);
    
    if (lResult != ERROR_SUCCESS) return 1;
    
    delete[] keyName;
    delete[] parentKeyPath;
    RegCloseKey(key);
    return 0;
}

int main()
{
    WCHAR* path = new WCHAR[300];
    wcscpy(path, L"HKEY_CURRENT_USER\\SOFTWARE\\aaa");
    WCHAR* newName = new WCHAR[100];
    wcscpy(newName, L"bbb");

    RenameRegistryKey(path, newName);
    
    delete[] path;
    delete[] newName;
    return 0;
}