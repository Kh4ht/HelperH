using UnityEngine;
using System.Runtime.CompilerServices;
using HelperH;

public static class HDebug
{
    public static void Log(string message, XMLC color = XMLC.White,
        [CallerMemberName] string caller = "", [CallerFilePath] string file = "", [CallerLineNumber] int line = 0)
    {
#if UNITY_EDITOR
        Debug.Log(Format(message, Extract(color), caller, file, line));
#endif
    }

    public static void LogWarning(string message, XMLC color = XMLC.Yellow,
        [CallerMemberName] string caller = "", [CallerFilePath] string file = "", [CallerLineNumber] int line = 0)
    {
#if UNITY_EDITOR
        Debug.LogWarning(Format(message, Extract(color), caller, file, line));
#endif
    }

    public static void LogError(string message, XMLC color = XMLC.Red,
        [CallerMemberName] string caller = "", [CallerFilePath] string file = "", [CallerLineNumber] int line = 0)
    {
#if UNITY_EDITOR
        Debug.LogError(Format(message, Extract(color), caller, file, line));
#endif
    }



    private static string Format(string message, string color, string caller, string file, int line)
    {
        string fileName = System.IO.Path.GetFileName(file);
        return $"<b><color={color}>{message}</color>\n" +
               $"[Caller: <color=white>{caller}()</color> in <color=white>{fileName}</color>:<color=white>{line}</color>]</b>";
    }

    private static string Extract(XMLC color)
    {
        switch (color)
        {
            case XMLC.Green:
                return "#00FF00";

            case XMLC.Red:
                return "#FF2626";

            case XMLC.Yellow:
                return "yellow";

            case XMLC.White:
                return "white";
        }

        return "white";
    }
}