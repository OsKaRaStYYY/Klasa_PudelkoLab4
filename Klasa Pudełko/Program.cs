using MyLib;
using System;

namespace MyApp 
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Pudelko pudelko = new Pudelko(200, 300, 400, UnitOfMeasure.Millimeter);
            double[] dimensionsArray = (double[])pudelko;
            Console.WriteLine($"Dimensions array: {dimensionsArray[0]}, {dimensionsArray[1]}, {dimensionsArray[2]}");

            (int, int, int) dimensionsTuple = (200, 300, 400);
            Pudelko pudelkoFromTuple = dimensionsTuple;
            Console.WriteLine($"Pudelko from tuple: {pudelkoFromTuple}");
            Pudelko pudelko2 = new Pudelko(1, 2, 3);
            Console.WriteLine($"A: {pudelko2[0]}, B: {pudelko2[1]}, C: {pudelko2[2]}");
            Pudelko pudelko3 = new Pudelko(1, 2, 3);
            foreach (double dimension in pudelko3)
            {
                Console.WriteLine(dimension);
            }
            Pudelko pudelko4 = Pudelko.Parse("2.500 m × 9.321 m × 0.100 m");
            Console.WriteLine(pudelko4);
        }
    }
}