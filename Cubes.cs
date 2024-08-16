public class Cubes
{
    public List<long> All;
    public List<long> Used;
    public List<long> Unused;

    public Cubes()
    {
        All = new List<long>();
        Used = new List<long>();
        Unused = new List<long>();

        for (int i = 2; i * i * i < 100000; i++)
        {
            All.Add(i * i * i);
            Unused.Add(i * i * i);
        }
    }

    public void MarkAsUsed(long val)
    {
        if (All.Contains(val) && !Used.Contains(val))
        {
            Used.Add(val);
            Unused.Remove(val);
        }
    }

    public List<long> UnusedOfLength(int i)
    {

        int n = i switch
        {
            1 => 1,
            2 => 10,
            3 => 100,
            4 => 1000,
            5 => 10000,
            _ => throw new Exception("invalid number length")
        };

        return Unused.Where(z => z / n > 0 && z / n < 10).ToList();
    }
}
