using System;
using System.Text;

namespace MarkerBasedARExample.MarkerBasedAR
{
    public class Objekt : Transformation
    {
        public Objekt()
        {
            this.content = new[]
            {
                "Objekt"
            };
        }
        
        public Objekt(String[] content) : base(content)
        {
        }
        
        public override String GetTransformation()
        {
            StringBuilder sb = new StringBuilder();
            
            for (int i = 0; i < this.content.Length; i++)
            {
                sb.Append(this.content[i]);
            }

            return sb.ToString();
        }
    }
}