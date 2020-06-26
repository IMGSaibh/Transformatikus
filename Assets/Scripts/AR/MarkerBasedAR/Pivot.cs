using System;
using System.Text;

namespace MarkerBasedARExample.MarkerBasedAR
{
    public class Pivot : Transformation
    {
        public Pivot()
        {
            this.content = new[]
            {
                "Pivot"
            };
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