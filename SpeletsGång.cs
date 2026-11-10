using System.Reflection.Metadata;
using System.Runtime.InteropServices;

public class SpeletsGång
{
    private readonly Spelplan spelplan;
    private readonly VemsTur vemsTur;
    private readonly IAIDrag? aiSpelare;
    private bool spelSlut = false;


    // KRAV 3: 
    // 1: Computed Properties 
    // 2: Egenskapen VemVinner beräknar dynamiskt om spelare 1 har fler kulor än spelare 2. 
    // 3: Effektiviserar kod genom att beräkna värdet vid varje anrop, istället för att lagra det.
    private bool VemVinner => spelplan.Gropar[6].AntalKulor > spelplan.Gropar[13].AntalKulor;
    private bool Oavgjort => spelplan.Gropar[6].AntalKulor == spelplan.Gropar[13].AntalKulor;
        
    
    //KRAV 2:
    // 1: Overloading av konstruktorer
    // 2: Två konstruktorer som skapar objekt av klassen SpeletsGång med olika parametrar, en för 2 mänskliga spelare och den andra för en AI-spelare.
    // 3: Ger flexibilitet vid skapandet av objekt, med eller utan AI-spelare.
    public SpeletsGång()
    {
        this.spelplan = new Spelplan();
        this.vemsTur = new VemsTur(true);
    }
    public SpeletsGång(IAIDrag aiDrag)
    {
        this.spelplan = new Spelplan();
        vemsTur = new VemsTur(true);
        this.aiSpelare = aiDrag;
    }

    public void Starta(string[] namn)
    {
        Console.WriteLine();
        Console.WriteLine("\nSpelet börjar!");
        spelplan.VisaSpelplan(namn, vemsTur.ÄrSpelare1sTur);

        while (!spelSlut)
        {

            bool ärSpelare1 = vemsTur.ÄrSpelare1sTur;

            Console.WriteLine();
            Console.WriteLine($"{(ärSpelare1 ? namn[0] : namn[1])}, välj en grop (1-6 för {namn[0]}, 8-13 för {namn[1]}):");
            string input = Console.ReadLine();
            

            if (int.TryParse(input, out int valdGrop))
            {
                valdGrop--;

                if ((ärSpelare1 && valdGrop >= 0 && valdGrop <= 5) || (!ärSpelare1 && valdGrop >= 7 && valdGrop <= 12))
                {
                    if (spelplan.Gropar[valdGrop].AntalKulor > 0)
                    {
                        SpelaRunda(valdGrop, ärSpelare1, namn);
                        if (!spelSlut) spelplan.VisaSpelplan(namn, ärSpelare1);
                    }
                    else
                    {
                        Console.WriteLine("Gropen är tom, välj en annan.");
                    }
                }
                else
                {
                    Console.WriteLine("Ogiltigt val för din tur, välj en grop som tillhör dig.");
                }
            }
            else
            {
                Console.WriteLine("Ogiltig inmatning, försök igen.");
            }
        }
    }

    // KRAV 5: 
    // 1: Beroendeinjektion 
    // 2: Ett objekt av typen IAIDrag injiceras in i metoden StartaAI för att tillhandahålla en AI-strategi. 
    // 3: Gör metoden flexibel genom att stödja olika AI-strategier utan att ändra metoden.
    public void StartaAI(string[] namn, IAIDrag aiDrag)
    {
        Console.WriteLine("\nSpelet börjar!");
        spelplan.VisaSpelplan(namn, vemsTur.ÄrSpelare1sTur);

        while (!spelSlut)
        {
            bool ärSpelare1 = vemsTur.ÄrSpelare1sTur;

            if (!ärSpelare1)
            {
                int valdGrop = aiDrag.VäljGrop(namn, ärSpelare1, spelplan);
                Console.WriteLine($"AI valde grop {valdGrop + 1}");
                int slutIndex = SpelaRundaAI(valdGrop, ärSpelare1, namn);
                if (spelSlut) return;
                spelplan.VisaSpelplan(namn, ärSpelare1);

                if (!(ärSpelare1 && slutIndex == 6) && !(slutIndex == 13 && !ärSpelare1))
                {
                    vemsTur.BytTur();
                }
            }
            else  
            {
                while (true)
                {
                    Console.WriteLine($"{namn[0]}, välj en grop (1-6):");
                    string input = Console.ReadLine();

                    if (int.TryParse(input, out int valdGrop))
                    {
                        valdGrop--; 

                        if (valdGrop < 0 || valdGrop > 5)
                        {
                            Console.WriteLine("Ogiltig grop. Välj en siffra mellan 1 och 6.");
                            continue;
                        }

                        if (spelplan.Gropar[valdGrop].AntalKulor == 0)
                        {
                            Console.WriteLine("Den valda gropen är tom. Välj en annan grop.");
                            continue;
                        }

                        int slutIndex = SpelaRundaAI(valdGrop, ärSpelare1, namn);
                        if (spelSlut) return;
                        spelplan.VisaSpelplan(namn, ärSpelare1);

                        if (!(ärSpelare1 && slutIndex == 6) && !(slutIndex == 13 && !ärSpelare1))
                        {
                            vemsTur.BytTur();
                        }
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Ogiltig inmatning. Vänligen ange en siffra mellan 1 och 6.");
                    }
                }
            }
        }
        
    }
    
    private void SpelaRunda(int startIndex, bool ärSpelare1, string[] namn)
    {
        int kulor = spelplan.Gropar[startIndex].TaKulor();
        int index = startIndex;

        while (kulor > 0)
        {
            index = (index + 1) % 14;

            if (ärSpelare1 && index == 13)
                continue;

            if (!ärSpelare1 && index == 6)
                continue;

           
            if (index < 13)
            {
                spelplan.Gropar[index].LäggTillKula();
            }

            else
            {
                if (ärSpelare1)
                {
                    spelplan.Gropar[6].LäggTillKula(); 
                }
                else
                {
                    spelplan.Gropar[13].LäggTillKula();
                }
            }

            kulor--; 
        }

        bool spelare1sGroparTomma = true;
        for (int i = 0; i <= 5; i++)
        {
            if (spelplan.Gropar[i].AntalKulor > 0)
            {
                spelare1sGroparTomma = false;
                break;
            }
        }
        if ((ärSpelare1 && index >= 0 && index <= 5 && spelplan.Gropar[index].AntalKulor == 1) || (!ärSpelare1 && index >= 7 && index <= 12 && spelplan.Gropar[index].AntalKulor == 1)) 
        { 
            int motståendeIndex = 12 - index; 
            int motståndarensKulor = spelplan.Gropar[motståendeIndex].TaKulor(); 
            int spelarensBo = ärSpelare1 ? 6 : 13; 
            spelplan.Gropar[spelarensBo].LäggTillKulor(1 + motståndarensKulor); 
            spelplan.Gropar[index].TaKulor(); 
        }

        bool spelare2sGroparTomma = true;
        for (int i = 7; i <= 12; i++)
        {
            if (spelplan.Gropar[i].AntalKulor > 0)
            {
                spelare2sGroparTomma = false;
                break;
            }
        }

        if (spelare1sGroparTomma || spelare2sGroparTomma)
        {
            spelSlut = true;
            FlyttaKvarvarandeKulor();
            VemHarVunnit(namn);
            var meny = new Meny();
            meny.SpelaIgen();
            return;
        }

        if (!(ärSpelare1 && index == 6) && !(index == 13 && !ärSpelare1))
        {
            vemsTur.BytTur();  
        }
    }

    private int SpelaRundaAI(int startIndex, bool ärSpelare1, string[] namn)
    {
        int kulor = spelplan.Gropar[startIndex].TaKulor();
        int index = startIndex;

        while (kulor > 0)
        {
            index = (index + 1) % 14;

            if (ärSpelare1 && index == 13) 
                continue;

            if (!ärSpelare1 && index == 6)  
                continue;

            spelplan.Gropar[index].LäggTillKula();
            kulor--;
        }
        if ((ärSpelare1 && index >= 0 && index <= 5 && spelplan.Gropar[index].AntalKulor == 1) || (!ärSpelare1 && index >= 7 && index <= 12 && spelplan.Gropar[index].AntalKulor == 1)) 
        { 
            int motståendeIndex = 12 - index; 
            int motståndarensKulor = spelplan.Gropar[motståendeIndex].TaKulor(); 
            int spelarensBo = ärSpelare1 ? 6 : 13; 
            spelplan.Gropar[spelarensBo].LäggTillKulor(1 + motståndarensKulor);
            spelplan.Gropar[index].TaKulor(); 
        }

        bool spelare1sGroparTomma = true;
        for (int i = 0; i <= 5; i++)
        {
            if (spelplan.Gropar[i].AntalKulor > 0)
            {
                spelare1sGroparTomma = false;
                break; 
            }
        }

        

        bool spelare2sGroparTomma = true;
        for (int i = 7; i <= 12; i++)
        {
            if (spelplan.Gropar[i].AntalKulor > 0)
            {
                spelare2sGroparTomma = false;
                break; 
            }
        }

        if (spelare1sGroparTomma || spelare2sGroparTomma)
        {
            spelSlut = true;
            FlyttaKvarvarandeKulor();
            VemHarVunnit(namn);
            var meny = new Meny();
            meny.SpelaIgen();
            return index; 
        }

        return index; 
    }

    private void FlyttaKvarvarandeKulor()
    {
        for (int i = 0; i <= 5; i++)
        {
            spelplan.Gropar[6].LäggTillKulor(spelplan.Gropar[i].TaKulor());
        }

        for (int i = 7; i <= 12; i++)
        {
            spelplan.Gropar[13].LäggTillKulor(spelplan.Gropar[i].TaKulor());
        }
    }

    public void VemHarVunnit(string[]namn)
    {
        int spelare1sPoäng = spelplan.Gropar[6].AntalKulor;
        int spelare2sPoäng = spelplan.Gropar[13].AntalKulor;

        Console.WriteLine("Sista draget är gjort, resterande kulor flyttas till respektive gropar.");
        Console.WriteLine("\nSpelet är slut!");
        Console.WriteLine($"{namn[0]} har {spelare1sPoäng} kulor.");
        Console.WriteLine($"{namn[1]} har {spelare2sPoäng} kulor.");

        if (Oavgjort)
        {
            Console.WriteLine("Det är oavgjort!");
        }
        else if (VemVinner)
        {
            Console.WriteLine($"{namn[0]} vinner!");
        }
        else if (!VemVinner)
        {
            Console.WriteLine($"{namn[1]} vinner!");
        }

        

    }

}
