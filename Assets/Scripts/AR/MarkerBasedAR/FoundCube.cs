using System;
using UnityEngine;

namespace MarkerBasedARExample.MarkerBasedAR
{
    public class FoundCube
    {
        private String cubeName;
        public TransformationDebug transformationClass;
        public Vector3 cubePosition;

        public FoundCube(string cubeName, TransformationDebug transformationClass, Vector3 cubePosition)
        {
            this.cubeName = cubeName;
            this.transformationClass = transformationClass;
            this.cubePosition = cubePosition;
        }

        public String GetCubeName()
        {
            return this.cubeName;
        }

    }
}