namespace L4816;

class Program
{
    static private Cubes TheCubes = new Cubes();
    static private Dictionary<char, long> TheLetters = new Dictionary<char, long>();
    static private Dictionary<string, long> TheAnswers = new Dictionary<string, long>();

    static void Main(string[] args)
    {
        // -------------------------------------------------------------------------------------
        // -- These are all easily determined - they need solving in this order
        // -------------------------------------------------------------------------------------

        func(clue: "dn07", cluelength: 5, letter: 'Y', calculate: Y => Y * Y * Y);
        func(clue: "ac07", cluelength: 2, letter: 'b', calculate: b => b + b, val => DigitAtPos(val, 0), DigitAtPos(TheAnswers["dn07"], 0));
        func(clue: "ac24", cluelength: 2, letter: 'J', calculate: J => TheLetters['b'] + J);
        func(clue: "dn23", cluelength: 2, letter: 'U', calculate: U => U - TheLetters['Y']);

        TheAnswers.Add("dn02", TheLetters['U'] - TheLetters['b']);
        TheAnswers.Add("ac15", TheLetters['b'] + 2 * TheLetters['U'] + TheLetters['Y']);

        func(clue: "dn16", cluelength: 4, letter: 'i', calculate: i => i + TheLetters['J'], val => DigitAtPos(val, 0), DigitAtPos(TheAnswers["ac15"], 2));
        func(clue: "ac12", cluelength: 4, letter: 'E', calculate: E => TheLetters['i'] + TheLetters['J'] - E * 2, val => DigitAtPos(val, 3), DigitAtPos(TheAnswers["dn07"], 2));
        var dn18 = func(clue: "dn18", cluelength: 4, letter: 'B', calculate: B => B + TheLetters['J'] * 2 - TheLetters['E']);

        var dn06 = func(clue: "dn06", cluelength: 4, letter: 'L', calculate: L => TheLetters['E'] + L, val => DigitAtPos(val, 2), DigitAtPos(TheAnswers["ac12"], 2));
        var ac22 = func(clue: "ac22", cluelength: 4, letter: 'y', calculate: y => TheLetters['U'] * 2 + y - TheLetters['E'], val=>DigitAtPos(val,2), DigitAtPos(TheAnswers["dn16"],2));

        // -------------------------------------------------------------------------------------
        //var ac09 = func(clue: "ac09", cluelength: 4, letter: 'N', calculate: v => TheLetters['I'] + v, val => DigitAtPos(val, 1), DigitAtPos(TheAnswers["dn02"], 2));
        // -------------------------------------------------------------------------------------

        foreach (var a in TheAnswers.OrderBy(z => z.Key))
        {
            Console.WriteLine($"{a.Key} - {a.Value,5}");
        }
        Console.WriteLine();
        foreach (var l in TheLetters.OrderBy(z => z.Key))
        {
            Console.WriteLine($"{l.Key} - {l.Value,5}");
        }
    }

    // -------------------------------------------------------------------------------------
    // -------------------------------------------------------------------------------------

    static private List<Letter> func(string clue, int cluelength, char letter, Func<long, long> calculate, Func<long, int> thisDigit, int otherDigit)
    {
        var possibles = new List<Letter>();

        foreach (var cube in TheCubes.Unused)
        {
            var val = calculate(cube);
            if ((cluelength == 2 && val > 9 && val < 100) ||
                (cluelength == 3 && val > 99 && val < 1000) ||
                (cluelength == 4 && val > 999 && val < 10000) ||
                (cluelength == 5 && val > 9999 && val < 100000))
            {
                if (thisDigit(val) == otherDigit)
                    possibles.Add(new Letter(letter, cube));
            }
        }
        if (possibles.Count == 1)
        {
            foreach (var U in possibles)
            {
                TheAnswers.Add(clue, calculate(U.Value));
                TheLetters.Add(letter, U.Value);
                TheCubes.MarkAsUsed(U.Value);
            }
        }
        return possibles;
    }

    // -------------------------------------------------------------------------------------

    static private List<Letter> func(string clue, int cluelength, char letter, Func<long, long> calculate)
    {
        var possibles = new List<Letter>();

        foreach (var cube in TheCubes.Unused)
        {
            var val = calculate(cube);
            if ((cluelength == 2 && val > 9 && val < 100) ||
                (cluelength == 3 && val > 99 && val < 1000) ||
                (cluelength == 4 && val > 999 && val < 10000) ||
                (cluelength == 5 && val > 9999 && val < 100000))
                possibles.Add(new Letter(letter, cube));
        }
        if (possibles.Count == 1)
        {
            foreach (var U in possibles)
            {
                TheAnswers.Add(clue, calculate(U.Value));
                TheLetters.Add(letter, U.Value);
                TheCubes.MarkAsUsed(U.Value);
            }
        }
        return possibles;
    }
    // -------------------------------------------------------------------------------------

    static private int DigitAtPos(long val, int pos) => int.Parse(val.ToString()[pos].ToString());
}
