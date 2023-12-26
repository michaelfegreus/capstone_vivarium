using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Josh
{
    /// <summary>
    /// Helps Josh to debug the game and print specific errors and functions
    /// </summary>
    public class DebugHelper : MonoBehaviour
    {
        // use to print things to the console
        public void PrintDebug(string msg)
        {
            // print the message
            Debug.Log(msg);
        }
    }
}
