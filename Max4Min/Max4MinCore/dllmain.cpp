// dllmain.cpp : 定义 DLL 应用程序的入口点。
#include "pch.h"
#include "HookHandlerProc.h"

#include <fstream>

//Initialized Data to be shared with all instance of the dll
#pragma data_seg("Shared")
HINSTANCE g_hInstance = NULL;
HHOOK g_hCBTHook = NULL;
#pragma data_seg()
// Initialised data End of data share
#pragma comment(linker,"/section:Shared,RWS")


BOOL APIENTRY DllMain( HMODULE hModule,
                       DWORD  ul_reason_for_call,
                       LPVOID lpReserved
                     )
{
    switch (ul_reason_for_call)
    {
    case DLL_PROCESS_ATTACH:
        {
            g_hInstance = hModule;
            break;
        }
    case DLL_THREAD_ATTACH:
    case DLL_THREAD_DETACH:
    case DLL_PROCESS_DETACH:
        break;
    }
    return TRUE;
}

#ifdef __cplusplus
extern "C"
{
#endif


static LRESULT CALLBACK CBTProc(int nCode, WPARAM wParam, LPARAM lParam)
{
    if (nCode == HCBT_MINMAX && 
        lParam == SW_MAXIMIZE && 
        (GetAsyncKeyState(VK_SHIFT) & 0x8000)) {
        HWND hwnd = (HWND)wParam;
        HMONITOR hMonitor = MonitorFromWindow(hwnd, MONITOR_DEFAULTTONEAREST);
        MONITORINFO monitorInfo;
        monitorInfo.cbSize = sizeof(MONITORINFO);
        GetMonitorInfo(hMonitor, (LPMONITORINFO)&monitorInfo);
        RECT area = monitorInfo.rcWork;
        SetWindowPos(hwnd, HWND_TOP, area.left, area.top, area.right - area.left, area.bottom - area.top, SWP_SHOWWINDOW);
        return 1;
    }
    return CallNextHookEx(NULL, nCode, wParam, lParam);
}


bool InstallCBTHook()
{
    if (!g_hCBTHook)
    {
        g_hCBTHook = SetWindowsHookEx(WH_CBT, (HOOKPROC)CBTProc, g_hInstance, 0);

        if (g_hCBTHook)
        {
            DEBUG(OutputDebugStringA("Hook CBT succeed\n"));
            return true;
        }
        else
        {
            DEBUG({
                DWORD dwError = GetLastError();
                char szError[MAX_PATH];
                _snprintf_s(szError, MAX_PATH, "Hook CBT failed, error = %u\n", dwError);
                OutputDebugStringA(szError);
            })
        }
    }

    return false;
}

bool UninstallCBTHook()
{
    if (g_hCBTHook)
    {
        UnhookWindowsHookEx(g_hCBTHook);
        g_hCBTHook = NULL;
        DEBUG(OutputDebugStringA("Uninstall CBT Hook\n"));
    }
    return true;
}


#ifdef __cplusplus
}
#endif
