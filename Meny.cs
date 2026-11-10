public class Meny
{
    public void Val()
    {
        Console.WriteLine("Välkommen till Kalaha! Vill du starta direkt, eller läsa introduktionen? Skriv in 1 för direkt, 2 för introduktion och tryck enter.");
        string input = Console.ReadLine();

        if (int.TryParse(input, out int valet))
        {
            if (valet == 2)
            {
                Console.WriteLine("Du valde att läsa introduktionen, här kommer den:");
                PrintaRegler();

                Console.WriteLine();
                Console.WriteLine("Tryck enter när du är klar!");
                string klar = Console.ReadLine();
                while (!string.IsNullOrEmpty(klar))
                {
                    Console.WriteLine("Ogiltig inmatning, tryck bara Enter för att gå vidare.");
                    klar = Console.ReadLine();
                }

                VäljMotståndare();
            }
            else if (valet == 1)
            {
                Console.WriteLine("Du valde att starta direkt, lycka till!");
                VäljMotståndare();
            }
            else
            {
                Console.WriteLine("Ogiltig inmatning, försök igen.");
                Val(); 
            }
        }
        else
        {
            Console.WriteLine("Ogiltig inmatning, försök igen.");
            Val(); 
        }
    }


    public void VäljMotståndare()
    {
        Console.WriteLine("Vill du spela mot en kompis eller mot AI? Skriv in 1 för kompis, 2 för AI och tryck enter.");
        string valspelare = Console.ReadLine();
        
        if (int.TryParse(valspelare, out int val2))
        {
            string[] namn = new string[2];

            if (val2 == 1)
            {
                MänskligSpelare spelare = new MänskligSpelare(new string[] { "", "" });
                namn = spelare.NySpelare();

                SpeletsGång spelet = new SpeletsGång();
                VemsTur vemsTur = new VemsTur(true);
                spelet.Starta(namn);
            }
            else if (val2 == 2)
            {
                VäljAI();
            }
            else
            {
                Console.WriteLine("Ogiltigt val. Försök igen.");
                VäljMotståndare(); 
            }
        }
        else
        {
            Console.WriteLine("Ogiltig inmatning, försök igen.");
            VäljMotståndare();  
        }
    }

    public void PrintaRegler()
    {
        Console.WriteLine("Spelet spelas på ett bräde med 12 gropar med 4 kulor i vardera grop.");
        Console.WriteLine("6 gropar för varje spelare, samt två större gropar, så kallade bon. Varje spelare har ett bo.");
        Console.WriteLine("Efter att ha tagit kulorna från en valfri grop, lägger spelaren en kula i varje följande grop, i motsols riktning.");
        Console.WriteLine("Om sista kulan hamnar i spelarens eget bo, får spelaren fortsätta spela.");
        Console.WriteLine("Om sista kulan hamnar i en tom grop på egen sida,");
        Console.WriteLine("kan spelaren ta kulorna från motståndarens grop mittemot och lägga dem i sitt bo.");
        Console.WriteLine("Spelet är slut när alla gropar på en spelares sida är tomma.");
        Console.WriteLine("De resterande kulorna går automatiskt till den andra spelarens bo.");
        Console.WriteLine("Spelaren med flest kulor i sitt bo vinner.");
    }
    
    public void VäljAI()
    {
        Console.WriteLine("Skriv in ditt namn: ");
        string mänskligSpelareNamn = Console.ReadLine(); 
        string aiNamn = "AI";

        Console.WriteLine("Välj AI typ:");
        Console.WriteLine("AI som alltid väljer slumpmässigt, skriv 1");
        Console.WriteLine("AI som alltid väljer den största antalet kulor, skriv 2");

        string valAI = Console.ReadLine();

        if(int.TryParse(valAI, out int AIvald))
        {
            IAIDrag aiDrag = null; 

            string[] namn = new string[] { mänskligSpelareNamn, aiNamn };

            if (AIvald == 1)
            {
                aiDrag = new AISlump();
                SpeletsGång speletSlump = new SpeletsGång(aiDrag);
                VemsTur vemsTur1 = new VemsTur(true);
                speletSlump.StartaAI(namn, aiDrag);

            }
            else if (AIvald == 2)
            {
                aiDrag = new AIHögsta();
                SpeletsGång speletHögsta = new SpeletsGång(aiDrag);
                VemsTur vemsTur2 = new VemsTur(true);
                speletHögsta.StartaAI(namn, aiDrag);
            }
            else
            {
                Console.WriteLine("Ogiltigt val, försök igen");
                VäljAI();
            }
        }

        else
        {
            Console.WriteLine("Ogiltigt val, försök igen");
            VäljAI();
        }        
    }

    public void SpelaIgen()
    {
        Console.WriteLine("Vill du spela igen? (ja/nej)");
        string svar = Console.ReadLine().Trim().ToLower();
        
        if (svar == "ja" || svar == "Ja" || svar == "JA")
        {
            Val(); 
        }
        else if (svar == "nej" || svar == "Nej" || svar == "NEJ")
        {
            Console.WriteLine("Tack för att du spelade! Ha en bra dag!");
            return;
        }
        else
        {
            Console.WriteLine("Ogiltig inmatning. Skriv 'ja' eller 'nej'.");
            SpelaIgen(); 
        }
    }
    
}
