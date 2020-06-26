
using System;
using System.Text;
using UnityEngine;

namespace MarkerBasedARExample.MarkerBasedAR
{
    public class Matrix : Transformation
    {
        public Matrix4x4 matrix;
        
        public Matrix()
        {
            this.content = new[]
            {
                "  1  ", "  0  ", "  0  ",
                "  0  ", "  1  ", "  0  ",
                "  0  ", "  0  ", "  1  ",
            };
            
            matrix = Matrix4x4.identity;
        }
        
        public Matrix(String[] content) : base(content)
        {
            this.matrix = new Matrix4x4();
        }
    
        public Matrix(string[] content, Matrix4x4 matrixTransformation) : base(content)
        {
            this.matrix = matrixTransformation;
        }

        public override string GetTransformation()
        {
            StringBuilder sb = new StringBuilder();
            
            for (int i = 0; i < this.content.Length; i++)
            {
                sb.Append(this.content[i]);

                if (i == 2 || i == 5)
                {
                    sb.Append(" \n");
                }
                else sb.Append(" ");
            }

            return sb.ToString();
        }
    }
}