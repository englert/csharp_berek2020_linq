// berek2020.txt http://www.infojegyzet.hu/vizsgafeladatok/okj-programozas/rendszeruzemelteto-210511/
//Név;Neme;Részleg;Belépés;Bér
//Beri Dániel;férfi;beszerzés;1979;222943
using System;                     // Console
using System.IO;                  // StreamReader
using System.Text;                // Encoding
using System.Collections.Generic; // List<>, Dictionary<>
using System.Linq;                // from where select

class Dolgozo{ 
	public string nev       {get; set;}
	public string neme      {get; set;}
	public string reszleg   {get; set;}
	public string belepes   {get; set;}
	public int ber          {get; set;}

    public Dolgozo(string sor){
        var s = sor.Trim().Split(';');
        this.nev     = s[0]; 
        this.neme    = s[1]; 
        this.reszleg = s[2]; 
        this.belepes = s[3]; 
        this.ber     = int.Parse(s[4]); 
    }
}

class Program {
    public static void Main (string[] args) {
        var lista = new List<Dolgozo>();              // üres lista létrehozása
        
        var f =  new StreamReader("berek2020.txt", Encoding.UTF8); // a "berek2020.txt" fájl megnyitása f néven 
        var elsosor = f.ReadLine();                   // az elsosorba kerül a fájl első sora.
        
        while (!f.EndOfStream){              // amig a fájl végére nem érünk addig imétlünk
		  var dolgozo = new Dolgozo(f.ReadLine());              // létrehozunk egy dolgozo-t a Dolgozó osztály alapján
		  lista.Add(dolgozo);                // a lista-hoz adjuk a dolgozo példányt.
		}
        f.Close();                           // bezárjuk az f fájlt

        // 3. feladat: Dolgozók száma: ? 
        Console.WriteLine($"3. feladat: Dolgozók száma: {lista.Count} fő");

        // 4. feladat: Bérek átlaga: ####,# eFt
        var atlag = (
            from  sor in lista 
            select sor.ber
            ).Average() / 1000;
        Console.WriteLine($"4. feladat: Bérek átlaga: {atlag:0.#} eFt");

        // 5. feladat: Kérem egy részleg nevét:
        Console.Write("5. feladat: Kérem egy részleg nevét: ");
        var reszlegneve = Console.ReadLine();

// 6. feladat: A legtöbbet kereső dolgozó a megadott részlegen
        var res = (
            from sor in lista 
            where (sor.reszleg == reszlegneve) 
            orderby sor.ber 
            select sor
            );	
        if (res.Any()){
            var maxi = res.Last();
            Console.WriteLine($"6. feladat: A legtöbbet kereső dolgozó a megadott részlegen");
            Console.WriteLine($"        Név: {maxi.nev}");
            Console.WriteLine($"        Neme: {maxi.neme}");
            Console.WriteLine($"        Belépés: {maxi.belepes}");
            Console.WriteLine($"        Bér: {maxi.ber} Forint"); 
        }
        else {
            Console.WriteLine($"6. feladat: A megadott részleg nem létezik a cégnél!");
        }
        
        //7. feladat: Statisztika
        Console.WriteLine($"7. feladat: Statisztika"); 
        var statisztika = (
            from sor in lista 
            group sor by sor.reszleg
            );
        foreach( var q in statisztika ){
            Console.WriteLine($"        {q.Key}: {q.Count()} ");
        }
//  -----------------------------------------------------------------------
    }  
}