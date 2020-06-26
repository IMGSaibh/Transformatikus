using System;
using System.Text;

namespace MarkerBasedARExample.MarkerBasedAR
{
    public class Skalar : Transformation
    {
        public float skalar;
        
        public Skalar()
        {
            this.content = new[]
            {
                "  1.0  "
            };
            
            skalar = 1.0f;
        }
        
        public Skalar(String[] content) : base(content)
        {
            this.skalar = 1.0f;
        }
    
        public Skalar(String[] content, float skalar) : base(content)
        {
            this.skalar = skalar;
        }

        public override string GetTransformation()
        {
            StringBuilder sb = new StringBuilder();
            
            sb.Append(this.content[0]);

            return sb.ToString();
        }
    }
}