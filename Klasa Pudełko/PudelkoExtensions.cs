using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KlasaPudelko;

namespace PudelkoExtensions
{
    public static class PudelkoExtensions
    {
        public static Pudelko Kompresuj(this Pudelko pudelko)
        {
            if (pudelko == null)
            {
                throw new ArgumentNullException(nameof(pudelko));
            }

            double objetosc = pudelko.A * pudelko.B * pudelko.C;
            double bokSzesccianu = Math.Pow(objetosc, 1.0 / 3.0);

            return new Pudelko(bokSzesccianu, bokSzesccianu, bokSzesccianu, UnitOfMeasure.Meter);
        }
    }
}