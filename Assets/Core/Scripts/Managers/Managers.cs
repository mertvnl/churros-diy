using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Managers : MonoBehaviour
{
#if UNITY_EDITOR
    /// <summary>
    /// In editor when we stop playing the game editor throws some errors.
    /// This is a temprorary solution for those errors.
    /// </summary>
    private void OnApplicationQuit()
    {
        foreach (var script in FindObjectsOfType<MonoBehaviour>())
        {
            script.enabled = false;
        }
    }
#endif
}
