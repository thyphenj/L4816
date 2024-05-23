namespace L4816
{
	public class Letter :IEquatable<Letter>
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

        public override bool Equals(object other)
        {
            if (other == null)
                return false;
            else
            {
                Letter otherAsLetter = other as Letter; ;
                return  Equals(otherAsLetter);
            }
        }

        public bool Equals ( Letter other)
        {
            return this.Name == other.Name;
        }
    }
}

