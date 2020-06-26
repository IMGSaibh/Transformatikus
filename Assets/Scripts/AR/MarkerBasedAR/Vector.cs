using System;
using System.Text;
using UnityEngine;

namespace MarkerBasedARExample.MarkerBasedAR
{
    public class Vector : Transformation
    {
        public Vector3 vector;
        
        public Vector()
        {
            this.vector = new Vector3(0,0,0);
        }
        
        public Vector(String[] content) : base(content)
        {
            this.vector = new Vector3();
        }
        
        public Vector(String[] content, Vector3 vectorTransformation) : base(content)
        {
            this.vector = vectorTransformation;
        }

        public override String GetTransformation()
        {
            StringBuilder sb = new StringBuilder();
            
            for (int i = 0; i < this.content.Length; i++)
            {
                sb.Append(this.content[i]);
                sb.Append(" \n");
            }

            return sb.ToString();
        }
    }
}