using UnityEngine;
using System.Collections.Generic;
using MarkerBasedARExample.MarkerBasedAR;

namespace DefaultNamespace
{ 
    public class SortedCubesListScript : MonoBehaviour
    {
        public static List<KeyValuePair<string, FoundCube>> sortedCubes;

        // Use this for initialization
        void Start ()
        {
            DontDestroyOnLoad (gameObject);
        }
    }
}