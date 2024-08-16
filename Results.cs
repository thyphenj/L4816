static public class Results
{
    static public void Display()
    {
        Dictionary<char, char> map = new Dictionary<char, char>()
            {
                {'1','o' },
                {'2','t' },
                {'3','t' },
                {'4','f' },
                {'5','f' },
                {'6','s' },
                {'7','s' },
                {'8','e' },
                {'9','n' }
            };

        string digits
            = "61987316"
            + "61192198"
            + "87238962"
            + "62889789"
            + "28563838"
            + "19836387"
            + "98989857"
            + "72678968";

        int charno = 0;

        Console.WriteLine("---------------------------------");
        foreach (char ch in digits)
        {
            Console.Write(map[ch]);
            if (++charno % 8 == 0)
                Console.WriteLine();
        }
        Console.WriteLine();

    }
}
