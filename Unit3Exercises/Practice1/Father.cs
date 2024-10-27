namespace Practice1
{
	internal class Father : GrandFather
	{
		public string Field1F;
		protected string Field2F;
		private string Field3F;

		public string GetField3F()
		{
			return Field3F;
		}

		public void SetField3F(string value)
		{
			Field3F = value;
		}
	}
}
