#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

public static class FindMissingScriptsRecursivelyAndRemove 
{
    [MenuItem("Tools/Remove All Missing Scripts")]
    public static void RemoveMissingMonoScripts()
    {
        var allTransforms = GameObject.FindObjectsOfType<Transform>();
        
        foreach (var t in allTransforms)
        {
            GameObjectUtility.RemoveMonoBehavioursWithMissingScript(t.gameObject);
        }
    }
}
#endif