using System;
using System.Text;

namespace MarkerBasedARExample.MarkerBasedAR
{
    public abstract class Transformation
    {
        protected String[] content;

        protected Transformation()
        {
            this.content = new []{""};
        }
        
        protected Transformation(String[] content)
        {
            this.content = content;
        }

        public abstract String GetTransformation();
    }
}