using System.Diagnostics.Metrics;

namespace L4816;

class Program
{
    static private Cubes TheCubes = new Cubes();
    static private Dictionary<char, long> Lett = new Dictionary<char, long>();
    static private Dictionary<string, long> Answ = new Dictionary<string, long>();

    static void Main(string[] args)
    {
        string template = "";

        // -------------------------------------------------------------------------------------
        // -- These are all easily determined - they need solving in this order
        // -------------------------------------------------------------------------------------

        var dn07 = new Clue("dn07", 5, Y => (Y * Y * Y));
        CalcOneLetter(dn07, 'Y');

        var ac07 = new Clue("ac07", 2, b => b + b);
        CalcOneLetter(ac07, 'b', $"{CharAtPos("dn07", 0)}");

        var ac24 = new Clue("ac24", 2, J => Lett['b'] + J);
        CalcOneLetter(ac24, 'J');

        var dn23 = new Clue("dn23", 2, U => U - Lett['Y']);
        CalcOneLetter(dn23, 'U');

        Answ.Add("ac15", Lett['b'] + Lett['U'] + Lett['U'] + Lett['Y']);
        Answ.Add("dn02", Lett['U'] - Lett['b']);

        var dn16 = new Clue("dn16", 4, i => i + Lett['J']);
        CalcOneLetter(dn16, 'i', $"{CharAtPos("ac15", 2)}");

        var ac12 = new Clue("ac12", 4, E => Lett['i'] + Lett['J'] - E - E);
        CalcOneLetter(ac12, 'E', $"...{CharAtPos("dn07", 2)}");

        // -------------------------------------------------------------------------------------
        // -- Let's have a look at dn06 and dn09 which will give us L and t

        {
            var dn06 = new Clue("dn06", 4, L => Lett['E'] + L);
            var dn06a = CalcOneLetter(dn06, 'L', $"..{CharAtPos("ac12", 2)}-");

            template = $"...{CharAtPos("ac15", 0)}..{CharAtPos("ac24", 0)}";
            foreach (var L in dn06a)
            {
                var dn09 = new Clue("dn09", 7, t => Lett['E'] * t + Lett['E'] + L.Item1.Value);
                var dn09a = CalcOneLetter(dn09, 't', template);
                if (dn09a.Count == 1)
                {
                    Answ.Add("dn06", L.Item2);
                    Lett.Add(L.Item1.Name, L.Item1.Value);
                    TheCubes.MarkAsUsed(L.Item1.Value);

                    break;
                }
            }
        }

        // -- now that we have dn09 we can go for ac22
        {
            template = $"{CharAtPos("dn09", 5)}.{CharAtPos("dn16", 2)}";
            var ac22 = new Clue("ac22", 4, y => Lett['U'] + Lett['U'] + y - Lett['E']);
            CalcOneLetter(ac22, 'y', template);
        }

        // -------------------------------------------------------------------------------------
        // -- These two need cross-referencing
        // -------------------------------------------------------------------------------------
        {
            template = $"{CharAtPos("dn09", 0)}{CharAtPos("dn02", 1)}";
            var ac09 = new Clue("ac09", 4, (I, N) => I + N);
            var (ac09a, ac09b) = CalcTwoLetters(ac09, 'I', 'N', template);

            template = $"..{CharAtPos("ac12", 1)}";
            var dn05 = new Clue("dn05", 6, (N, s) => N * s - Lett['J']);
            var (dn05a, dn05b) = CalcTwoLetters(dn05, 'N', 's', template);

            // -- N appears in both of these - cross check to find a matching value
            // -- NOTE - I'm only checking ac09[1,1] against [dn05[1,1]

            char name = ' ';
            long value = 0;
            (Letter, long) ff = (new Letter('0', 0), 0), gg = (new Letter('0', 0), 0);

            bool found = false;
            foreach (var a in ac09b)
            {
                foreach (var b in dn05a)
                {
                    if (a.Item1.Name == b.Item1.Name && a.Item1.Value == b.Item1.Value)
                    {
                        found = true;
                        name = a.Item1.Name;
                        value = a.Item1.Value;
                        ff = a;
                        gg = b;
                        break;
                    }
                }
                if (found) break;
            }
            if (found)
            {
                Lett.Add(name, value);
                TheCubes.MarkAsUsed(value);

                ac09a.RemoveAll(z => z.Item1.Name == name || z.Item1.Value == value);
                ac09b.RemoveAll(z => z.Item1.Name == name || z.Item1.Value == value);
                dn05a.RemoveAll(z => z.Item1.Name == name || z.Item1.Value == value);
                dn05b.RemoveAll(z => z.Item1.Name == name || z.Item1.Value == value);

                Lett.Add(ac09a[0].Item1.Name, ac09a[0].Item1.Value);
                Lett.Add(dn05b[0].Item1.Name, dn05b[0].Item1.Value);
                TheCubes.MarkAsUsed(ac09a[0].Item1.Value);
                TheCubes.MarkAsUsed(dn05b[0].Item1.Value);

            }
        }

        // -------------------------------------------------------------------------------------

        Answ.Add("ac13", Lett['i'] + Lett['s'] + Lett['s'] - Lett['N']);

        {
            template = $".{CharAtPos("ac09", 3)}";
            var dn04 = new Clue("dn04", 2, T => Lett['s'] + Lett['s'] - T);
            var dn04a = CalcOneLetter(dn04, 'T', template);
        }
        {
            template = $".{CharAtPos("dn02", 0)}.{CharAtPos("dn04", 0)}";
            var ac01 = new Clue("ac01", 5, W => Lett['T'] + Lett['T'] + W + Lett['y']);
            var ac01a = CalcOneLetter(ac01, 'W', template);
        }
        {
            template = $"{CharAtPos("dn05", 1)}{CharAtPos("dn06", 1)}{CharAtPos("dn07", 1)}";
            var ac10 = new Clue("ac10", 4, S => Lett['E'] + S + Lett['T'] + Lett['T']);
            var ac10a = CalcOneLetter(ac10, 'S', template);
        }

        Answ.Add("ac20", Lett['S'] - Lett['U']);

        {
            template = $"{CharAtPos("ac01", 2)}{CharAtPos("ac09", 2)}.{CharAtPos("ac13", 1)}";
            var dn03 = new Clue("dn03", 4, (R, O) => R - O);
            var (dn03a, dn03b) = CalcTwoLetters(dn03, 'R', 'O', template);
        }
        {
            template = $"{CharAtPos("dn09", 1)}{CharAtPos("dn02", 2)}{CharAtPos("dn03", 2)}";
            var ac11 = new Clue("ac11", 3, (D, o) => D - o - o - Lett['Y']);
            var (ac11a, ac11b) = CalcTwoLetters(ac11, 'D', 'o', template);
        }

        Answ.Add("ac25", Lett['D'] - Lett['S'] - Lett['Y']);

        {
            template = $"{CharAtPos("ac13", 0)}{CharAtPos("ac15", 1)}.{CharAtPos("ac22", 1)}" +
                $"{CharAtPos("ac24", 1)}";
            var dn13 = new Clue("dn13", 5, (e, B) => Lett['J'] * Lett['J'] + e + B);
            var (dn13a, dn13b) = CalcTwoLetters(dn13, 'e', 'B', template);

            template = $".{CharAtPos("ac20", 0)}";
            var dn18 = new Clue("dn18", 4, B => B + Lett['J'] + Lett['J'] - Lett['E']);
            var dn18a = CalcOneLetter(dn18, 'B', template);
        }
        {
            template = $".{CharAtPos("dn05", 4)}.{CharAtPos("dn07", 4)}";
            var ac17 = new Clue("ac17", 4, l => Lett['B'] - l - l - Lett['s']);
            var ac17a = CalcOneLetter(ac17, 'l', template);
        }
        {
            template = $"{CharAtPos("dn13", 2)}{CharAtPos("dn16", 1)}.{CharAtPos("dn05", 5)}";
            var ac19 = new Clue("ac19", 4, H => H - Lett['E'] - Lett['l']);
            var ac19a = CalcOneLetter(ac19, 'H', template);
        }
        {
            template = $"{CharAtPos("ac07", 1)}{CharAtPos("ac10", 3)}...{CharAtPos("ac20", 2)}";
            var dn08 = new Clue("dn08", 7, P => Lett['B'] * P + Lett['o'] + Lett['o']);
            var dn08a = CalcOneLetter(dn08, 'P', template);
        }

        Answ.Add("dn21", Lett['P'] + Lett['T'] - Lett['s']);

        {
            template = $"{CharAtPos("dn06", 3)}{CharAtPos("dn07", 3)}";
            var ac14 = new Clue("ac14", 3, (d, e) => d - e - e - Lett['P']);
            var (ac14a, ac14b) = CalcTwoLetters(ac14, 'd', 'e', template);
        }
        {
            template = $"{CharAtPos("dn23", 0)}{CharAtPos("dn18", 2)}{CharAtPos("dn21", 1)}"
                + $"{CharAtPos("dn08", 6)}";
            var ac23 = new Clue("ac23", 4, (A, G) => A - G - G);
            var (ac23a, ac23b) = CalcTwoLetters(ac23, 'A', 'G', template);
            Dump(ac23, ac23a, ac23b);
        }
        {
            template = $"{CharAtPos("ac12", 0)}{CharAtPos("ac13", 2)}{CharAtPos("ac17", 0)}"
                + $"{CharAtPos("ac19", 2)}{CharAtPos("ac22", 3)}{CharAtPos("ac25", 0)}";
            var dn12 = new Clue("dn12", 6, F => Lett['b'] * F + F + Lett['S']);
            var dn12a = CalcOneLetter(dn12, 'F', template);
            Dump(dn12, dn12a);
        }
        // -------------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------------

        // -------------------------------------------------------------------------------------
        Console.WriteLine();
        foreach (var a in Answ.OrderBy(z => z.Key))
        {
            Console.WriteLine($"{a.Key} - {a.Value,7}");
        }
        Console.WriteLine();
        foreach (var l in Lett.OrderBy(x => x.Key))
        {
            Console.WriteLine($"{l.Key} - {l.Value,5}");
        }
        Console.WriteLine();
        foreach (var l in Lett.OrderBy(x => x.Value))
        {
            Console.WriteLine($"{l.Key} - {l.Value,5}");
        }
        Console.WriteLine();
    }

    // -------------------------------------------------------------------------------------
    // -------------------------------------------------------------------------------------

    static private List<(Letter, long)> CalcOneLetter(Clue clue, char letter, string template = "")
    {
        var possibles = new List<(Letter, long)>();

        foreach (var cube1 in TheCubes.Unused)
        {
            var entry = clue.Formula1(cube1);

            if ((clue.ClueLength == 2 && entry > 9 && entry < 100) ||
                (clue.ClueLength == 3 && entry > 99 && entry < 1000) ||
                (clue.ClueLength == 4 && entry > 999 && entry < 10000) ||
                (clue.ClueLength == 5 && entry > 9999 && entry < 100000) ||
                (clue.ClueLength == 6 && entry > 99999 && entry < 1000000) ||
                (clue.ClueLength == 7 && entry > 999999 && entry < 10000000))
            {
                string str = entry.ToString();
                bool success = true;
                for (int i = 0; i < template.Length; i++)
                    if (template[i] != '.')
                    {
                        if ((template[i] == '-' && str[i] == '0')
                            || (template[i] != '-' && str[i] != template[i]))
                        {
                            success = false;
                            break;
                        }
                    }

                if (success)
                    possibles.Add((new Letter(letter, cube1), entry));
            }
        }
        if (possibles.Count == 1)
        {
            foreach (var poss in possibles)
            {
                Answ.Add(clue.ClueName, poss.Item2);
                Lett.Add(letter, poss.Item1.Value);
                TheCubes.MarkAsUsed(poss.Item1.Value);
            }
        }
        return possibles;
    }
    // -------------------------------------------------------------------------------------
    static private (List<(Letter, long)>, List<(Letter, long)>) CalcTwoLetters(Clue clue, char letter1, char letter2, string template = "")
    {
        var poss1 = new List<(Letter, long)>();
        var poss2 = new List<(Letter, long)>();

        foreach (var cube1 in TheCubes.Unused)
            foreach (var cube2 in TheCubes.Unused.Where(z => z != cube1))
            {
                var entry = clue.Formula2(cube1, cube2);

                if ((clue.ClueLength == 2 && entry > 9 && entry < 100) ||
                    (clue.ClueLength == 3 && entry > 99 && entry < 1000) ||
                    (clue.ClueLength == 4 && entry > 999 && entry < 10000) ||
                    (clue.ClueLength == 5 && entry > 9999 && entry < 100000) ||
                    (clue.ClueLength == 6 && entry > 99999 && entry < 1000000) ||
                    (clue.ClueLength == 7 && entry > 999999 && entry < 10000000))
                {
                    string str = entry.ToString();
                    bool success = true;
                    for (int i = 0; i < template.Length; i++)
                        if (template[i] != '.')
                        {
                            if ((template[i] == '-' && str[i] == '0')
                                || (template[i] != '-' && str[i] != template[i]))
                            {
                                success = false;
                                break;
                            }
                        }

                    if (success)
                    {
                        //Console.WriteLine($"{letter1}={cube1} {letter2}={cube2} entry={entry} ");
                        poss1.Add((new Letter(letter1, cube1), entry));
                        poss2.Add((new Letter(letter2, cube2), entry));
                    }
                }
            }
        if (poss1.Count == 1)
        {
            Answ.Add(clue.ClueName, poss1[0].Item2);
            foreach (var poss in poss1)
            {
                Lett.Add(letter1, poss.Item1.Value);
                TheCubes.MarkAsUsed(poss.Item1.Value);
            }
            foreach (var poss in poss2)
            {
                Lett.Add(letter2, poss.Item1.Value);
                TheCubes.MarkAsUsed(poss.Item1.Value);
            }
        }
        else if (poss1.Count == 2 && poss1[0].Item2 == poss1[1].Item2)
        {
            Answ.Add(clue.ClueName, poss1[0].Item2);
        }
        return (poss1, poss2);
    }
    // -------------------------------------------------------------------------------------


    static private void Dump(Clue? clue, List<(Letter, long)> ans)
    {
        Console.Write(clue == null ? "     " : clue.ClueName + " ");
        if (ans.Count > 0)
        {
            Console.Write(ans[0].Item1.Name + " ");
            foreach (var a in ans)
                Console.Write(" [" + a.Item1.Value + " " + a.Item2 + "]");
        }
        Console.WriteLine();
    }
    static private void Dump(Clue clue, List<(Letter, long)> ansa, List<(Letter, long)> ansb)
    {
        Dump(clue, ansa);
        Dump(null, ansb);
    }
    static private int DigitAtPos(long val, int pos) => int.Parse(val.ToString()[pos].ToString());

    static private char CharAtPos(string ans, int pos) => Answ[ans].ToString()[pos];
}
