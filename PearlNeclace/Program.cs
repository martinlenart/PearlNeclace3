﻿// See https://aka.ms/new-console-template for more information

namespace PearlNecklace
{
    internal class Program
    {
        static int count = 0;
        static decimal MostExpensive = decimal.MinValue;

        static void Main(string[] args)
        {
            //Create a couple of Random pearls
            Console.WriteLine("Create a Random pearls");
            var p1 = Pearl.Factory.CreateRandomPearl();
            var p2 = Pearl.Factory.CreateRandomPearl();
            Console.WriteLine(p1);
            Console.WriteLine(p2);

            Console.WriteLine("Create a copy of the immutable pearl");
            var p3 = p2.SetColor(PearlColor.Pink).SetSize(25).SetShape(PearlShape.DropShaped);
            Console.WriteLine(p3);

            p3.Write("The Pink Pearl.txt");


            //Create a random Necklaces
            Console.WriteLine("\nCreate a random Necklaces");
            var necklace = Necklace.Factory.CreateRandomNecklace(35);
            Console.WriteLine(necklace);
            Console.WriteLine($"Nr of pearls: {necklace.Count()}");
            Console.WriteLine($"Nr of Freshwater pearls: {necklace.Count(PearlType.FreshWater)}");
            Console.WriteLine($"Nr of Saltwater pearls: {necklace.Count(PearlType.SaltWater)}");
            Console.WriteLine($"Price of the necklace: {necklace.Price}");

            necklace.ForEachPearl(FindMostExpensivePearl);
            Console.WriteLine($"Most expensive pearl {MostExpensive}");

            
            //Create a bag of 20 Necklaces
            Console.WriteLine("\nCreate a bag of 20 Necklaces");
            var bagOfNecklaces = NecklaceBag.Factory.CreateRandomNecklaceBag(20, NecklaceDelegate);
            //Console.WriteLine(bagOfNecklaces);

            //Necklace written to file using Stream Adapter Layer
            Console.WriteLine("\nNecklaceBag written to file using Stream Adapter Layer");

            var s = bagOfNecklaces.Write("NecklaceBag.zip");
            Console.WriteLine(s);

            MostExpensive = 0;
            bagOfNecklaces.ForEachPearl(FindMostExpensivePearl);
            Console.WriteLine($"Most expensive pearl {MostExpensive}");
   
        }
        
        #region Delegate action for new neclace
        static void NecklaceDelegate(INecklace necklace)
        {
            if (count++ % 100 == 0)
            {
                Console.Write(".");
                //Console.WriteLine(necklace);
            }
        }

        static void FindMostExpensivePearl(IPearl pearl)
        {
            if (pearl.Price > MostExpensive)
                MostExpensive = pearl.Price;
        }
        #endregion
    }
}



