namespace L4816;

class Program
{
    static private Cubes TheCubes = new Cubes();
    static private Dictionary<char, long> TheLetters = new Dictionary<char, long>();
    static private Dictionary<string, long> TheAnswers = new Dictionary<string, long>();

    private delegate long Calculate(long val);

    //static private long Ac24(long val) => TheLetters['b'] + val;
    //static private long Dn23(long val) => val - TheLetters['Y'];
    //static private long dn07(long val) => val * val * val;

    static void Main(string[] args)
    {

        // -------------------------------------------------------------------------------------
        // -- dn07 : YYY (5)

        var poss_Y2 = func("dn07", 5, 'Y', 2, val=>val*val*val);

        // -------------------------------------------------------------------------------------
        // -- ac07 : b + b (2)
        {
            var poss_b = new List<Letter>();

            foreach (var b in TheCubes.UnusedOfLength(1))
            {
                var val = b + b;

                if (val > 9 && val < 100 && DigitAtPos(val, 0) == DigitAtPos(TheAnswers["dn07"], 0))
                {
                    poss_b.Add(new Letter('b', b));
                }
            }
            if (poss_b.Count == 1)
            {
                foreach (var b in poss_b)
                {
                    TheAnswers.Add("ac07", b.Value + b.Value);
                    TheLetters.Add('b', b.Value);
                    TheCubes.MarkAsUsed(b.Value);
                }
            }
        }

        // -------------------------------------------------------------------------------------
        // -- ac24 : b + J (2)

        var poss_J2 = func("ac24", 2, 'J', 2, val => TheLetters['b'] + val);

        // -------------------------------------------------------------------------------------
        // -- dn23 : U - Y (2)

        var poss_U2 = func(clue: "dn23", 2, letter: 'U', cubeLength: 3, val => val - TheLetters['Y']);

        // -------------------------------------------------------------------------------------

        foreach (var l in TheLetters)
        {
            Console.WriteLine($"{l.Key} - {l.Value,5}");
        }
        Console.WriteLine();
        foreach (var a in TheAnswers)
        {
            Console.WriteLine($"{a.Key} - {a.Value,5}");
        }

    }
    static private List<Letter> func(string clue, int cluelength, char letter, int cubeLength, Func<long, long> calc)
    {
        var possibles = new List<Letter>();

        foreach (var U in TheCubes.UnusedOfLength(cubeLength))
        {
            var val = calc(U);
            if (
                (cluelength == 2 && val > 9 && val < 100) ||
                (cluelength == 3 && val > 99 && val < 1000) ||
                (cluelength == 4 && val > 999 && val < 10000) ||
                (cluelength == 5 && val > 9999 && val < 100000)
                )
                possibles.Add(new Letter(letter, U));
        }
        if (possibles.Count == 1)
        {
            foreach (var U in possibles)
            {
                TheAnswers.Add(clue, calc(U.Value));
                TheLetters.Add(letter, U.Value);
                TheCubes.MarkAsUsed(U.Value);
            }
        }
        return possibles;
    }

    static private long Root(long n)
    {
        return (long)(Math.Pow(n, 1.0 / 3.0) + 0.0001);
    }
    static private int DigitAtPos(long val, int pos)
    {
        return int.Parse(val.ToString()[pos].ToString());
    }
}
