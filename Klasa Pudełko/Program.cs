using KlasaPudelko;
using System;
using PudelkoExtensions;
using System.Collections.Generic;
namespace MyApp 
{
    public class Program
    {
        static void Main(string[] args)
        {
            //Pudelko pudelko = new Pudelko(200, 300, 400, UnitOfMeasure.Millimeter);
            //double[] dimensionsArray = (double[])pudelko;
            //Console.WriteLine($"Dimensions array: {dimensionsArray[0]}, {dimensionsArray[1]}, {dimensionsArray[2]}");

            //(int, int, int) dimensionsTuple = (200, 300, 400);
            //Pudelko pudelkoFromTuple = dimensionsTuple;
            //Console.WriteLine($"Pudelko from tuple: {pudelkoFromTuple}");
            //Pudelko pudelko2 = new Pudelko(1, 2, 3);
            //Console.WriteLine($"A: {pudelko2[0]}, B: {pudelko2[1]}, C: {pudelko2[2]}");
            //Pudelko pudelko3 = new Pudelko(1, 2, 3);
            //foreach (double dimension in pudelko3)
            //{
            //    Console.WriteLine(dimension);
            //}
            //Pudelko pudelko4 = Pudelko.Parse("2.500 m × 9.321 m × 0.100 m");
            //Console.WriteLine(pudelko4);


            Pudelko p = new Pudelko(2, 3, 4, UnitOfMeasure.Meter);
            Pudelko sześcian = p.Kompresuj();

            Console.WriteLine($"Oryginalne pudełko: {p}");
            Console.WriteLine($"Sześcian o takiej samej objętości: {sześcian}");

            List<Pudelko> pudelka = new List<Pudelko>
            {
                new Pudelko(2, 3, 4, UnitOfMeasure.Meter),
                new Pudelko(1, 2, 1, UnitOfMeasure.Meter),
                new Pudelko(3, 3, 3, UnitOfMeasure.Meter),
                new Pudelko(250, 300, 150, UnitOfMeasure.Centimeter),
                new Pudelko(1000, 2000, 1000, UnitOfMeasure.Millimeter)
            };

            Console.WriteLine("Lista pudełek:");
            foreach (Pudelko p5 in pudelka)
            {
                Console.WriteLine(p5);
            }

            pudelka.Sort(ComparePudelka);

            Console.WriteLine("\nPosortowana lista pudełek:");
            foreach (Pudelko p5 in pudelka)
            {
                Console.WriteLine(p5);
            }




        }
        private static int ComparePudelka(Pudelko p1, Pudelko p2)
        {
            int result = p1.Objetosc.CompareTo(p2.Objetosc);

            if (result == 0)
            {
                result = p1.Pole.CompareTo(p2.Pole);

                if (result == 0)
                {
                    double p1Sum = p1.A + p1.B + p1.C;
                    double p2Sum = p2.A + p2.B + p2.C;
                    result = p1Sum.CompareTo(p2Sum);
                }
            }

            return result;
        }
    }
}
    
