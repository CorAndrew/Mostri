namespace CentroAddestramento;

using System;
using static System.Console;

// Definiti gli Enum
enum TipoMostro 
{ 
    Fuoco, 
    Acqua, 
    Erba, 
    Elettrico, 
    Roccia 
}
enum LivelloEnergia 
{ 
    Basso, 
    Medio, 
    Alto 
}

// Defnite le struct
struct MostroDigitale
{
    public int codice;
    public TipoMostro tipo;
    public LivelloEnergia energia;
    public int eta;
    public int forza;
    public string allenatore;

    public MostroDigitale(int c, TipoMostro t, LivelloEnergia e, int et, int f, string a)
    {
        codice = c;
        tipo = t;
        energia = e;
        eta = et;
        forza = f;
        allenatore = a;
    }

    // Metodi richiesti
    public bool EAllenabile()
    {
        if (energia != LivelloEnergia.Basso)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void CambiaAllenatore(string nuovoAllenatore)
    {
        allenatore = nuovoAllenatore;
    }

    public void Allenati(int punti)
    {
        forza += punti;
        if (forza > 100) 
            forza = 100;
    }

    public string Descrizione()
    {
        return $"[Cod: {codice}] {tipo} - Forza: {forza} - Energia: {energia} - All: {allenatore}";
    }

    // Estensione facoltativa
    public bool EStanco()
    {
        if (energia == LivelloEnergia.Basso)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}

class Program
{
    static void Main()
    {
        MostroDigitale[] centro = new MostroDigitale[5];
        bool inizializzato = false;
        string scelta;

        do
        {
            WriteLine("\n--- CENTRO ADDESTRAMENTO MOSTRI ---");
            WriteLine("1: Inizializza centro | 2: Allena mostro | 3: Cambia allenatore");
            WriteLine("4: Scambia allenatori | 5: Visualizza tutti | 6: Filtra per Tipo");
            WriteLine("7: Filtra per Energia | 8: Mostra Stanchi | 9: Esci");
            Write("Scegli un'opzione: ");
            scelta = ReadLine();

            switch (scelta)
            {
                case "1":
                    InizializzaCentro(centro);
                    inizializzato = true;
                    break;
                case "2":
                    if (inizializzato) AllenaMostro(centro);
                    else WriteLine("Centro non inizializzato!");
                    break;
                case "3":
                    if (inizializzato) CambiaAllenatoreMenu(centro);
                    break;
                case "4":
                    if (inizializzato) ScambiaTraDue(centro);
                    break;
                case "5":
                    if (inizializzato) VisualizzaTutti(centro);
                    break;
                case "6":
                    if (inizializzato) FiltraPerTipo(centro);
                    break;
                case "7":
                    if (inizializzato) FiltraPerEnergia(centro);
                    break;
                case "8":
                    if (inizializzato) VisualizzaStanchi(centro);
                    break;
            }
        } while (scelta != "9");
    }

    // Logia esercizo

    static void InizializzaCentro(MostroDigitale[] centro)
    {
        for (int i = 0; i < centro.Length; i++)
        {
            Write($"\nCodice mostro {i + 1}: ");
            int c = int.Parse(ReadLine());
            Write("Tipo (0:Fuoco, 1:Acqua, 2:Erba, 3:Elettr, 4:Roccia): ");
            TipoMostro t = (TipoMostro)int.Parse(ReadLine());
            Write("Energia (0:Basso, 1:Medio, 2:Alto): ");
            LivelloEnergia e = (LivelloEnergia)int.Parse(ReadLine());
            Write("Forza (1-100): ");
            int f = int.Parse(ReadLine());
            Write("Nome Allenatore: ");
            string a = ReadLine();

            centro[i] = new MostroDigitale(c, t, e, 1, f, a);
        }
    }

    static void AllenaMostro(MostroDigitale[] centro)
    {
        Write("Inserisci codice mostro da allenare: ");
        int cod = int.Parse(ReadLine());
        for (int i = 0; i < centro.Length; i++)
        {
            if (centro[i].codice == cod)
            {
                if (centro[i].EAllenabile())
                {
                    centro[i].Allenati(10);
                    WriteLine("Allenamento completato! Forza aumentata.");
                }
                else WriteLine("Il mostro è troppo stanco per allenarsi!");
                return;
            }
        }
        WriteLine("Mostro non trovato.");
    }

    static void CambiaAllenatoreMenu(MostroDigitale[] centro)
    {
        Write("Codice mostro: ");
        int cod = int.Parse(ReadLine());
        Write("Nuovo allenatore: ");
        string nuovo = ReadLine();

        for (int i = 0; i < centro.Length; i++)
        {
            if (centro[i].codice == cod)
            {
                centro[i].CambiaAllenatore(nuovo);
                return;
            }
        }
    }

    static void ScambiaTraDue(MostroDigitale[] centro)
    {
        Write("Codice Mostro A: ");
        int c1 = int.Parse(ReadLine());
        Write("Codice Mostro B: ");
        int c2 = int.Parse(ReadLine());

        int idx1 = -1, idx2 = -1;
        for (int i = 0; i < centro.Length; i++)
        {
            if (centro[i].codice == c1) idx1 = i;
            if (centro[i].codice == c2) idx2 = i;
        }

        if (idx1 != -1 && idx2 != -1)
        {
            string temp = centro[idx1].allenatore;
            centro[idx1].CambiaAllenatore(centro[idx2].allenatore);
            centro[idx2].CambiaAllenatore(temp);
            WriteLine("Scambio effettuato!");
        }
    }

    static void VisualizzaTutti(MostroDigitale[] centro)
    {
        foreach (var m in centro) WriteLine(m.Descrizione());
    }

    static void FiltraPerTipo(MostroDigitale[] centro)
    {
        Write("Tipo da cercare (0-4): ");
        TipoMostro t = (TipoMostro)int.Parse(ReadLine());
        foreach (var m in centro) if (m.tipo == t) WriteLine(m.Descrizione());
    }

    static void FiltraPerEnergia(MostroDigitale[] centro)
    {
        Write("Energia da cercare (0-2): ");
        LivelloEnergia e = (LivelloEnergia)int.Parse(ReadLine());
        foreach (var m in centro) if (m.energia == e) WriteLine(m.Descrizione());
    }

    static void VisualizzaStanchi(MostroDigitale[] centro)
    {
        WriteLine("Mostri stanchi:");
        foreach (var m in centro) if (m.EStanco()) WriteLine(m.Descrizione());
    }
}