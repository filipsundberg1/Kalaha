public class MänskligSpelare
{

    // KRAV 1: 
    // 1: Inkapsling / Informationsgömning 
    // 2: Gör Namn privat för att förhindra direkt åtkomst och modifiering utanför klassen. 
    // 3: Skyddar integriteten av data och förhindrar obehörig åtkomst eller förändring.
    private string[] Namn { get; set; }

    public MänskligSpelare(string[] namn)
    {
        Namn = new string[0] ;
    }

    public string[] NySpelare()
    {
        Console.WriteLine("Skriv in namnen på spelarna (förslagsvis initialer), skilj med mellanslag.");
        string input = Console.ReadLine();

        Namn = input.Split(new[] { ' ' });

        if (Namn.Length < 2 || Namn.Length != 2)
        {
            Console.WriteLine("Ange TVÅ spelarnamn. Försök igen.");
            NySpelare();
        }
        else
        {
            Console.WriteLine();
            Console.WriteLine("Spelare registrerade:");
            foreach (var namn in Namn)
            {
                Console.WriteLine($"- {namn}");
            }
        }
        return Namn;
    }
}
