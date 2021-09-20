#pragma once
#ifdef MAX4MINCORE_EXPORTS
#define MAX4MINCORE_API __declspec(dllexport)
#else
#define MAX4MINCORE_API __declspec(dllimport)
#endif

#include <Windows.h>

#ifdef _DEBUG
#  define DEBUG(x) x
#else
#  define DEBUG(x) do {} while (0)
#endif

#ifdef __cplusplus
extern "C"
{
#endif

MAX4MINCORE_API bool InstallCBTHook();
MAX4MINCORE_API bool UninstallCBTHook();

#ifdef __cplusplus
}
#endif
