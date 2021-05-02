using System;
using System.Threading;

namespace OOP
{
  public class Programm //Fertig
  {
    private const string Schließen= "q";

    private static void Main (string[] args)
    {
      //Funktionsweise
      Anfang:
        Console.WriteLine("Herzlich willkommen");
        Console.WriteLine("Wie viele Stockwerke gibt es");

        int flur; string flurInput; Aufzug aufzug;

        flurInput = Console.ReadLine();

        if(Int32.TryParse(flurInput, out flur))
          aufzug = new Aufzug (flur);
          else{
            Console.WriteLine("Tippfehler!!");
            Console.Clear();
            goto Anfang;
          }
          string input = string.Empty;

          while(input!= Schließen)
          {
            Console.WriteLine("Bitte geben Sie die gewünschte Etage ein: ");

            input = Console.ReadLine();
            if(Int32.TryParse (input, out flur))
              aufzug.FlurPress(flur);
            else if (input == Schließen)
              Console.WriteLine("Tschüss ;D");
            else
              Console.WriteLine("Ungültige Eingabe. Versuche es erneut !!");
          }
    }
  }

  public class Aufzug //Offen !! + MaxAnzahl Personen
  {

    private bool[] flurFertig;
    public int aktuelleEtage = 1;
    private int maxEtage;
    public AufzugStatus Status = AufzugStatus.HÄLT;

    public Aufzug(int anzahlEtage = 10)
    {
      flurFertig = new bool[anzahlEtage +1];
      maxEtage = anzahlEtage;
    }

    private void Hält(int flur)
    {
      Status = AufzugStatus.HÄLT;
      aktuelleEtage = flur;
      flurFertig[flur] = false;
      Console.WriteLine("Aufzug hält auf {0}", flur);
    }

    private void Runterfahren(int flur)
    {
      for( int i = aktuelleEtage; i >= 1;i-- )
      {
        if(flurFertig[i])
          Hält(flur);
          else
            continue;
      }

      Status= AufzugStatus.HÄLT;
      Console.WriteLine("Warten...");
    }

    private void Hochfahren(int flur)
    {
      for(int i = aktuelleEtage; i <= maxEtage; i++)
      {
        if(flurFertig[i])
          Hält(flur);
        else
          continue;
      }
      Status = AufzugStatus.HÄLT;
      Console.WriteLine("Warten");
    }


    void StehenBleiben()
    {
      Console.WriteLine("Aktuelle Etage..");
    }

    public void FlurPress(int flur)
    {
      if(flur > maxEtage){
        Console.WriteLine("Fehler Es gibt nur {0} Etagen",maxEtage);
        return;
      }
      flurFertig[flur]=true;
      switch(Status){
        case AufzugStatus.UNTEN:
        Runterfahren(flur);
        break;

        case AufzugStatus.HÄLT:
        if(aktuelleEtage< flur)
        Hochfahren(flur);
        else if (aktuelleEtage == flur)
          StehenBleiben();
        else
          Runterfahren(flur);
        break;

        case AufzugStatus.OBEN:
          Hochfahren(flur);
          break;

        default:
          break;
      }

    }
    public enum AufzugStatus
    {
      OBEN,
      HÄLT,
      UNTEN,
    }
  }


}