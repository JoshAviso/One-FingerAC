
using UnityEngine;

public class LogUtils
{
    static public void Log(string msg) { Debug.Log($"[LOG]: {msg}"); }
    static public void LogWarning(string msg) { Debug.LogWarning($"[WARN]: {msg}"); }
    static public void LogError(string msg) { Debug.LogError($"[ERR]: {msg}"); }
    static public void Log(MonoBehaviour source, string msg) 
        { Debug.Log($"[LOG]({source.gameObject}, {source}): {msg}"); }
    static public void LogWarning(MonoBehaviour source, string msg) 
        { Debug.LogWarning($"[WARN]({source.gameObject}, {source}): {msg}"); }
    static public void LogError(MonoBehaviour source, string msg) 
        { Debug.LogError($"[ERR]({source.gameObject}, {source}): {msg}"); }
    static public void Log(Object source, string msg) 
        { Debug.Log($"[LOG]({source}): {msg}"); }
    static public void LogWarning(Object source, string msg) 
        { Debug.LogWarning($"[WARN]({source}): {msg}"); }
    static public void LogError(Object source, string msg) 
        { Debug.LogError($"[ERR]({source}): {msg}"); }

}
