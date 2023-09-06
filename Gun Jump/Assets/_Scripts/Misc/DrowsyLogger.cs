using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace _Scripts.Misc
{
    public static class DrowsyLogger
    {
        private static void DoLog(Action<string, Object> LogFunction, string prefix, Object myObj, object msg)
        {
#if UNITY_EDITOR
            LogFunction($"{prefix}[<color=lightblue>{myObj.name}</color>]: {msg}", myObj);
#endif
        }

        public static void Log(this Object myObj, object msg)
        {
            DoLog(Debug.Log, "", myObj, msg);
        }

        public static void LogError(this Object myObj, object msg)
        {
            DoLog(Debug.LogError, "<color=red><@></color>", myObj, msg);
        }

        public static void LogWarning(this Object myObj, object msg)
        {
            DoLog(Debug.LogWarning, "<color=yellow><@></color>", myObj, msg);
        }

        public static void LogSuccess(this Object myObj, object msg)
        {
            DoLog(Debug.Log, "<color=green><@></color>", myObj, msg);
        }
    }
}
