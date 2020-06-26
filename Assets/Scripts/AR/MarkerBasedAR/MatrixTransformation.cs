using System;
using System.Text;
using UnityEngine;

namespace OpenCVMarkerBasedAR
{
    public class MatrixTransformation
    {
        private String[] stringMatrix;
        public Matrix4x4 numericMatrix;

        public MatrixTransformation()
        {
            this.stringMatrix = new []
            {
                "1", "0", "0",
                "0", "1", "0",
                "0", "0", "1"
            };
            
            this.numericMatrix = Matrix4x4.identity;
        }
        
        public MatrixTransformation(String[] stringMatrix, Matrix4x4 numericMatrix)
        {
            this.stringMatrix = stringMatrix;
            this.numericMatrix = numericMatrix;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < stringMatrix.Length; i++)
            {
                if (i == 2 || i == 5)
                    sb.Append(stringMatrix[i]+"\n");
                else sb.Append(stringMatrix[i] +" ");
            }

            return sb.ToString();
        }
    }
}