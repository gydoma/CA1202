using System;
using System.Collections.Generic;

namespace CA1202
{
    public class Program
    {
        public static void Main()
        {
            /*
             ✅ A könyv nyelve 80%al magyar, 20% angol.
             ✅ hozz létre egy listát, benne 15 "random" könyvvel:
             ✅ A szerzők nevei és a címek megadásához használhatsz random generáló weboldalakat, AI-t vagy tetszőleges faker módszereket. Próbálj meg ügyelni rá, hogy a cím nyelve reflektáljon a könyv nyelvére.
             ✅ az ISBN kód legyen teljesen véletlenszerű, 10 számjegyű, ügyelj rá, hogy a listában semmiképp ne legyen két azonos. (.Any LINQval a legegyszerűbb talán)
             ✅ A készlet 30% eséllyel 0, egyébként egy 5 és 10 közötti egész szám.
             ✅ A könyvben 70% eséllyel egyetlen szerzője van, egyébként azonos eséllyel 2 vagy 3
            */


            /*
             ✅ ISBN azonosító (egyedi azonosito, long típusú, 10 számjegyű számsor)
             ✅ szerzők listája ([1-3] életmű Author osztály példányait tartalmazó lista)
             ✅ cím (minimum 3, maximum 64 karakter hosszú karakterlánc)
             ✅ kiadás éve (2007 és a jelen év közti egész szám)
             ✅ nyelv (csak az angol, német és magyar elfogadott érték)
             ✅ készlet (egész szám, minimum 0)
             ✅ ár (1000 és 10000 közötti, kerek 100as szám)
            */

            //könyvgen
            List<Book> books = new List<Book>();
            for (int i = 0; i < 15; i++)
            {
                long isbn = randomISBN();
                while (books.Exists(b => b.ISBN == isbn))
                {
                    isbn = randomISBN();
                }

                List<string> szerzok = randomAuthors();
                string cim = randomTitle();
                int ev = randomYear();
                string nyelv = randomLanguage();
                int keszlet = randomStock();
                int ar = randomPrice();

                Book book = new Book(isbn, cim, ev, nyelv, keszlet, ar, szerzok.ToArray());
                books.Add(book);
            }

            //könyviratás
            foreach (var book in books)
            {
                Console.WriteLine(book);
            }

            /*
             emulacio: 100 ismétlést hajtás végre az alábbi folyamatból:
             egy vásárló keres egy bizonyos könyvet (kiválasztást egyet random a listaról)
             ha van készleten, akkor csökkentjük a készlet számát és elszamoljuk a bevételt (a könyv árát)
             ha nincs készleten, akkor megpróbáljuk beszerezni:
             50% eséllyel növeljük a készlet mennyiséget random [1-10] darabbal, 50% eséllyel elfogyott a 
                nagykerben is: eltávolítjuk a könyvet a listáról.
             
            az emulácio után írjuk ki az eladásokbó származó (bruttó) bevételt, hogy hány könyv fogyott ki a 
                nagykerben, illetve hogy mennyit változott a raktárkeszletunk szamossaga a kiindulási állapothoz 
                képest (hány db könyv volt kezdetben készleten, mennyi van most, és ennek az előjeles kulonbsége)
             */
            int totalSold = 0;
            int totalSoldPrice = 0;
            for (int i = 0; i < 100; i++)
            {
                Random random = new Random();
                Console.WriteLine("\n");
                Console.WriteLine($"{i + 1}. emuláció kör".PadLeft((Console.WindowWidth + $"{i + 1}. emuláció kör".Length) / 2));
                Console.WriteLine("\n");

                Book book = books[random.Next(0, books.Count)];
                if (book.Keszlet > 0)
                {
                    book.Keszlet--;
                    totalSold++;
                    totalSoldPrice += book.Ar;
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"1 darab {book.ISBN} - {book.Cim} sikeresen eladva {book.Ar} összegért. " +
                        $"\n\tEzzel {totalSold} lett az eddigi eladott könyvek száma" +
                        $"\n\tMelyeknek összértéke {totalSoldPrice}.-Ft");
                    Console.ResetColor();
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine($"Ebben a körben nem adtunk el könyvet! Várakozás a további emulációra...");
                    Console.ResetColor();
                    int chance = random.Next(1, 11);
                    if (chance <= 5)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                        Console.WriteLine("Sikeres berendelés!");
                        int stock = random.Next(1, 11);
                        book.Keszlet += stock;
                        Console.WriteLine($"A készletet {stock} darabbal növeltük. Új készlet: {book.Keszlet}");
                        Console.ResetColor();
                    }
                    else if (books.Count != 0)
                    {
                        books.Remove(book);
                        Console.BackgroundColor = ConsoleColor.White;
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.Write($"A {book.Cim} című könyvet már nem árusítják a nagykereskedelemben. Készleten lévő könyvek száma: {books.Count}");
                        Console.ResetColor();
                    }
                    else
                    {
                        NotImplementedException e = new NotImplementedException("Nincs több könyv a nagykerben!");
                    }
                }
            }
            Console.WriteLine("Az emuláció sikeresen befejeződött! Íme a részletes végeredmény:");
            Console.WriteLine($"\tAz eladásokból származó bevétel: {totalSoldPrice}.-Ft");
            Console.WriteLine($"\tA nagykerben maradt könyvek száma: {books.Count}");
            Console.WriteLine($"\tA raktárkészlet változása: {totalSold - books.Count}");
            Console.WriteLine($"\tA raktárkészlet jelenlegi állapota: {books.Count} db könyv");
            
            foreach (var book in books)
            {
                Console.WriteLine(book);
            }

        }
        public static long randomISBN()
        {
            Random random = new Random();
            int r1 = random.Next(100000000, 999999999);
            int r2 = random.Next(0, 9);
            return Convert.ToInt64($"{r1}{r2}");
        }
        public static List<string> randomAuthors()
        {
            Random random = new Random();

            //esely szamitas
            int numAuthors;
            int chance = random.Next(1, 11);
            if (chance <= 7) numAuthors = 1;
            else numAuthors = random.Next(2, 4);

            List<string> authors = new List<string>();

            for (int i = 0; i < numAuthors; i++)
            {
                string[] AList = { "Cserepes Banánvirág", "Pöck Ödön", "Bud Izsák", "Ceruza Elemér", "Bac Ilus", "Görk Orsolya", "Kukija Kitsi", "Kuki Nuku" };
                string author = AList[random.Next(0, AList.Length)];
                authors.Add(author);
            }

            return authors;
        }

        public static string randomTitle()
        {
            Random random = new Random();
            string[] titlek = { "The Silent Night", "The Lost Time", "The Blue Lake", "The Secret Garden", "A csendes éj", "Az elveszett idő", "A kék tó", "A titkos kert" };
            return titlek[random.Next(0, titlek.Length)];
            
        }

        public static int randomYear()
        {
            Random random = new Random();
            int currentYear = DateTime.Now.Year;
            return random.Next(1970, currentYear + 1);
        }

        public static string randomLanguage()
        {
            Random random = new Random();
            string[] languages = { "magyar", "angol" };
            return languages[random.Next(0, 5) < 4 ? 0 : 1]; // 80% magyar, 20% angol
        }

        public static int randomStock()
        {
            Random random = new Random();
            int stock = random.Next(1, 11); // 1-10
            int chance = random.Next(1, 11); // 1-10

            if (chance <= 3) // 30% chance of beszerzés alatt
            {
                stock = 0;
            }

            return stock;
        }

        public static int randomPrice()
        {
            Random random = new Random();
            int price = random.Next(10, 101) * 100;
            return price;
        }
    }

    
}