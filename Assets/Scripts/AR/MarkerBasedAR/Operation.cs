namespace MarkerBasedARExample.MarkerBasedAR
{
    public class Operation : Transformation
    {
        public Operation(string[] content) : base(content) {}

        public override string GetTransformation()
        {
            return this.content[0];
        }
    }
}