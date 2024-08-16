public class Letter : IEquatable<Letter>
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

    public override bool Equals(object? other)
    {
        if (other is not null)
        {
            Letter? otherAsLetter = other as Letter; ;
            return Equals(otherAsLetter);
        }
        return false;
    }

    public bool Equals(Letter? other)
    {
        return this.Name == other?.Name;
    }

    public override int GetHashCode()
    {
        throw new NotImplementedException();
    }
}
