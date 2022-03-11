using UnityEngine;

public abstract class Utils {
    public static void Log(string s) {
        #if UNITY_EDITOR
        Debug.Log(s);
        #endif
    }
}