namespace Practice1
{
    internal class Father : GrandFather
    {
        public string field1F;
        protected string field2F;
        private string field3F;

        public string GetField3F()
        {
            return field3F;
        }

        public void SetField3F(string value)
        {
            field3F = value;
        }
    }
}
