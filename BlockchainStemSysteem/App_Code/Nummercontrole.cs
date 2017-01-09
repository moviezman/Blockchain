//Nummercontrole bevat een functie die checkt of een nummer een Nederlands nummer is
public class Nummercontrole
{
    public bool Nummercheck(string nummer)
    {
        //NLNummers beginnen met 061, 062, 063, 064, 065 of 068 en hebben 10 nummers
        //Daarop wordt hier gecheckt
        if (nummer.Length == 10)
        {
            string NummerStart = nummer.Substring(0, 3);
            if (NummerStart == "061" || NummerStart == "062" || NummerStart == "063" || NummerStart == "064" || NummerStart == "065" || NummerStart == "068")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }
    }
}