public interface IAIDrag
{
    // KRAV 6:
    // 1: Subtyppolymorfism
    // 2: Klasserna AISlump och AIHögsta implementerar båda gränssnittet IAIDrag, med olika implementeringar av VäljGrop.
    // 3: Gör det möjligt att använda olika AI-strategier genom samma gränssnitt, vilket möjliggör polymorfism.
    int VäljGrop(string[] namn, bool ärSpelare1sTur, Spelplan spelplan);
}
