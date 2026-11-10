using System.Xml;

public class Spelplan
{

    // KRAV 4: 
    // 1: Objektkomposition 
    // 2: Spelplan använder en array av Grop-objekt. 
    // 3: Gör det möjligt att hantera spelplanens tillstånd genom komponerade Grop-objekt.
    public Grop[] Gropar { get; private set; }

    public Spelplan()
    {
        Gropar = new Grop[14];
        for (int i = 0; i < 6; i++)
        {
            Gropar[i] = new Grop(4);
        }
        for(int i = 7; i < 13; i++)
        {
            Gropar[i] = new Grop(4);
        }

        Gropar[6] = new Grop(0);  
        Gropar[13] = new Grop(0);  
    }

    public void VisaSpelplan(string[]namn, bool ÄrSpelare1sTur)
    {
        Console.WriteLine();
        Console.WriteLine();
        Console.WriteLine();
        Console.WriteLine();
        Console.WriteLine("                 Gropar: 13<-8");

        Console.ForegroundColor = ConsoleColor.DarkBlue;
        Console.Write($"{namn[1]}:s bo({Gropar[13].AntalKulor}) | ");
        Console.ForegroundColor = ConsoleColor.White;
        for (int i = 12; i >= 7; i--)
        {
            Console.Write($"({Gropar[i].AntalKulor}) ");
        }
        Console.WriteLine();
        
        Console.Write("             ");
        for (int i = 0; i <= 5; i++)
        {
            Console.Write($"({Gropar[i].AntalKulor}) ");
        }
        Console.ForegroundColor = ConsoleColor.DarkBlue;
        
        Console.Write($"| {namn[0]}:s bo({Gropar[6].AntalKulor})");
        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine();
        Console.WriteLine("                 Gropar: 1->6");
    }
}
