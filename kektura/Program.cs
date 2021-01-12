using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;

namespace kektura
{
    class Program
    {

        struct Turaszakasz
        {
            public string Nev;
            public string VegPont;
            public int Hossz;
            public int Emelkedesek;
            public int Lejtesek;
            public bool Pecsetelohely;

            public Turaszakasz(string sor)
            {
                string[] t = sor.Split(';');

                Nev = t[0];
                VegPont = t[1];
                Hossz = int.Parse(t[2].Replace(",",""));
                Emelkedesek = int.Parse(t[3]);
                Lejtesek = int.Parse(t[4]);
                if (t[5] == "i") Pecsetelohely = true;
                else Pecsetelohely = false;
            }
        }
        static List<Turaszakasz> turaszakaszok = new List<Turaszakasz>();
        static int magassag;

        static void Main()
        {
            {
                F2();
                F3();
                F4();
                F5();
                F6();
                F7();
                F8();
                F9();

                Console.ReadLine();
            }
        }
        private static void F9()
        {
            var sw = new StreamWriter(@"../../res/kektura2.csv");
            sw.WriteLine(magassag);

            for (int i = 0; i < turaszakaszok.Count; i++)
            {
                string ki = $"{turaszakaszok[i].Nev};{turaszakaszok[i].VegPont};{turaszakaszok[i].Hossz};{turaszakaszok[i].Emelkedesek};{turaszakaszok[i].Lejtesek};";

                if (turaszakaszok[i].Pecsetelohely) ki += "i";
                else ki += "h";

                if (HianyosNev(turaszakaszok[i])) ki.Replace(turaszakaszok[i].VegPont, turaszakaszok[i].VegPont + " pecsetelohely");

                sw.WriteLine(ki);
            }

            sw.Flush();
            sw.Close();
        }

        private static void F8()
        {
            int legmagasabbIndex = 0;

            for (int i = 1; i < turaszakaszok.Count; i++)
            {
                if (magassag + turaszakaszok[i].Emelkedesek - turaszakaszok[i].Lejtesek > magassag + turaszakaszok[legmagasabbIndex].Emelkedesek - turaszakaszok[legmagasabbIndex].Lejtesek) legmagasabbIndex = i;
            }

            Console.WriteLine("8. feladat:");
            Console.WriteLine($"\tA végpont neve: {turaszakaszok[legmagasabbIndex].VegPont}");
            Console.WriteLine($"\tA végpont tengerszint feletti magassága: {magassag + turaszakaszok[legmagasabbIndex].Emelkedesek - turaszakaszok[legmagasabbIndex].Lejtesek} m");
        }

        private static void F7()
        {
            Console.WriteLine("7. feladat: Hiányos állomásnevek:");
            bool vanHianyos = false;
            foreach (var tura in turaszakaszok)
            {
                if (HianyosNev(tura))
                {
                    Console.WriteLine($"\t{tura.VegPont}");
                    vanHianyos = true;
                }
            }
            if (!vanHianyos) Console.WriteLine("\tNincs hiányos állomásnév!");
        }

        private static bool HianyosNev(Turaszakasz turaszakasz)
        {
            if (turaszakasz.Pecsetelohely == true && !turaszakasz.VegPont.Contains("pecsetelohely")) return true;
            else return false;
        }

        private static void F6()
        {

        }

        private static void F5()
        {
            int index = 0;
            for (int i = 1; i < turaszakaszok.Count; i++)
            {
                if (turaszakaszok[i].Hossz < turaszakaszok[index].Hossz) index = i;
            }

            Console.WriteLine("5. feladat:");
            Console.WriteLine($"\tKezdete: {turaszakaszok[index].Nev}");
            Console.WriteLine($"\tVége: {turaszakaszok[index].VegPont}");
            Console.WriteLine($"\tTávolság: {turaszakaszok[index].Hossz} km");
        }

        private static void F4()
        {
            int teljesHossz = 0;

            foreach (var tura in turaszakaszok)
            {
                teljesHossz = teljesHossz + tura.Hossz;
            }

            Console.WriteLine($"4. feladat: A túra teljes hossza: {teljesHossz}");
        }

        private static void F3()
        {
            Console.WriteLine($"3. feladat: Szakaszok száma: {turaszakaszok.Count}");
        }

        private static void F2()
        {
            var sr = new StreamReader(@"../../res/kektura.csv");
            magassag = int.Parse(sr.ReadLine());

            while (!sr.EndOfStream)
            {
                turaszakaszok.Add(new Turaszakasz(sr.ReadLine()));
            }
            {
                sr.Close();
            }
        }
    }      
}
