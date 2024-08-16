public class Clue
{
    public string ClueName { get; set; }
    public int ClueLength { get; set; }
    public Func<long, long> Formula1 { get; set; }
    public Func<long, long, long> Formula2 { get; set; }

    public Clue(string clue, int cluelength, Func<long, long> formula)
    {
        ClueName = clue;
        ClueLength = cluelength;
        Formula1 = formula;
        Formula2 = (aa, bb) => aa + bb; // this is a stub
    }
    public Clue(string clue, int cluelength, Func<long, long, long> formula)
    {
        ClueName = clue;
        ClueLength = cluelength;
        Formula1 = aa => aa;            // this is a stub
        Formula2 = formula;
    }
}

