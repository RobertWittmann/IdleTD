using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Debugger : MonoBehaviour
{
    [SerializeField] string message = "";

    public void DebugMessage()
    {
        Debug.Log(message);
    }
}
