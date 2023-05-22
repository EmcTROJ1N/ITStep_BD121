// dllmain.cpp : Определяет точку входа для приложения DLL.
#include "pch.h"

HKEY GetRootKey(WCHAR* key)
{
    if (wcscmp(key, L"HKEY_CLASSES_ROOT") == 0) return HKEY_CLASSES_ROOT;
    if (wcscmp(key, L"HKEY_CURRENT_USER") == 0) return HKEY_CURRENT_USER;
    if (wcscmp(key, L"HKEY_LOCAL_MACHINE") == 0) return HKEY_LOCAL_MACHINE;
    if (wcscmp(key, L"HKEY_USERS") == 0) return HKEY_USERS;
    if (wcscmp(key, L"HKEY_CURRENT_CONFIG") == 0) return HKEY_CURRENT_CONFIG;
}

extern "C" __declspec(dllexport) UINT Test()
{
    return 1;
}

extern "C" __declspec(dllexport) UINT RegCreateSubKey(WCHAR * path)
{
    for (int i = 0; path[i] != '\0'; i++)
    {
        if (path[i] == '/')
            path[i] = '\\';
    }

    WCHAR* wKey = new WCHAR[100];
    WCHAR* firstSlash = wcschr(path, '\\');
    wcsncpy_s(wKey, 100, path, firstSlash - path);
    *(wKey + (firstSlash - path)) = '\0';
    HKEY branch = GetRootKey(wKey);

    WCHAR* keyPath = new WCHAR[100];
    wcscpy_s(keyPath - 1, 100, firstSlash);

    LONG lResult;

    HKEY hKey;
    DWORD dwDisposition;
    if (RegCreateKeyExW(branch, keyPath, 0, NULL, REG_OPTION_NON_VOLATILE, KEY_WRITE, NULL, &hKey, &dwDisposition) == ERROR_SUCCESS)
    {
        RegCloseKey(hKey);
        return 0;
    }
    else
        return 1;
}


BOOL APIENTRY DllMain( HMODULE hModule,
                       DWORD  ul_reason_for_call,
                       LPVOID lpReserved
                     )
{
    switch (ul_reason_for_call)
    {
    case DLL_PROCESS_ATTACH:
    case DLL_THREAD_ATTACH:
    case DLL_THREAD_DETACH:
    case DLL_PROCESS_DETACH:
        break;
    }
    return TRUE;
}

