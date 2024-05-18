namespace L4816
{
	public class Letter
	{
		public char Name { get; set; }
		public long Value { get; set; }

		public Letter(char n, long val)
		{
			Name = n;
			Value = val;
		}
        public override string ToString()
        {
            return $"[{Name},{Value}]";
        }
    }
}

