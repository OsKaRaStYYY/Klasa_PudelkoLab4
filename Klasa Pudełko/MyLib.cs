using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using System.Collections;
using System.Text.RegularExpressions;

namespace KlasaPudelko
{
    public enum UnitOfMeasure
    {
        Millimeter,
        Centimeter,
        Meter
    }

    public sealed class Pudelko : IFormattable, IEnumerable<double>
    {
     
        private const double MaxSizeMeters = 10;
        private const double DefaultValueMeters = 0.1;

        public double A { get; }
        public double B { get; }
        public double C { get; }
        public IEnumerator<double> GetEnumerator()
        {
            yield return A;
            yield return B;
            yield return C;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public Pudelko(double a = DefaultValueMeters, double b = DefaultValueMeters, double c = DefaultValueMeters, UnitOfMeasure unit = UnitOfMeasure.Meter)
        {
            A = ConvertToMeters(a, unit);
            B = ConvertToMeters(b, unit);
            C = ConvertToMeters(c, unit);

            ValidateDimensions(A, B, C);
        }

        private static double ConvertToMeters(double value, UnitOfMeasure unit)
        {
            return unit switch
            {
                UnitOfMeasure.Millimeter => Math.Round(value / 1000, 3, MidpointRounding.ToZero),
                UnitOfMeasure.Centimeter => Math.Round(value / 100, 3, MidpointRounding.ToZero),
                UnitOfMeasure.Meter => Math.Round(value, 3, MidpointRounding.ToZero),
                _ => throw new ArgumentOutOfRangeException(nameof(unit), "Invalid unit of measure"),
            };
        }


        private static void ValidateDimensions(double a, double b, double c)
        {
            if (a <= 0 || b <= 0 || c <= 0)
            {
                throw new ArgumentOutOfRangeException("All dimensions must be positive.");
            }

            if (a > MaxSizeMeters || b > MaxSizeMeters || c > MaxSizeMeters)
            {
                throw new ArgumentOutOfRangeException("Dimensions cannot be larger than 10 meters.");
            }
        }

        public override string ToString()
        {
            return ToString("m", CultureInfo.InvariantCulture);
        }
        public string ToString(string format)
        {
            return ToString(format, CultureInfo.InvariantCulture);
        }

        public string ToString(string format, IFormatProvider formatProvider)
        {
            if (string.IsNullOrEmpty(format))
            {
                format = "m";
            }

            if (formatProvider == null)
            {
                formatProvider = CultureInfo.InvariantCulture;
            }

            double a, b, c;
            string unit;
            string formatSpecifier;

            switch (format.ToLowerInvariant())
            {
                case "m":
                    a = A; b = B; c = C; unit = "m"; formatSpecifier = "F3";
                    break;
                case "cm":
                    a = A * 100; b = B * 100; c = C * 100; unit = "cm"; formatSpecifier = "F1";
                    break;
                case "mm":
                    a = A * 1000; b = B * 1000; c = C * 1000; unit = "mm"; formatSpecifier = "F0";
                    break;
                default:
                    throw new FormatException($"The '{format}' format string is not supported.");
            }

            return $"{a.ToString(formatSpecifier, formatProvider)} {unit} × {b.ToString(formatSpecifier, formatProvider)} {unit} × {c.ToString(formatSpecifier, formatProvider)} {unit}";
        }
        

        public double Objetosc
        {
            get
            {
                double objetosc = A * B * C;
                return Math.Round(objetosc, 9);
            }
        }

        public double Pole
        {
            get
            {
                double pole = 2 * (A * B + A * C + B * C);
                return Math.Round(pole, 6);
            }
        }
      public override bool Equals(object obj)
        {
            return Equals(obj as Pudelko);
        }

        public bool Equals(Pudelko other)
        {
            if (other == null)
                return false;

            var dimensions1 = new List<double>() { A, B, C };
            var dimensions2 = new List<double>() { other.A, other.B, other.C };
            bool result = dimensions1.All(dimensions2.Contains) && dimensions2.All(dimensions1.Contains);
            if (result) return true;
            return false;

        }

        public override int GetHashCode()
        {
            double[] dimensions = new[] { A, B, C };
            Array.Sort(dimensions);
            return HashCode.Combine(dimensions[0], dimensions[1], dimensions[2]);
        }

        public static bool operator ==(Pudelko left, Pudelko right)
        {
            if (object.ReferenceEquals(left, null))
            {
                return object.ReferenceEquals(right, null);
            }
            return left.Equals(right);
        }

        public static bool operator !=(Pudelko left, Pudelko right)
        {
            return !(left == right);
        }

        public static Pudelko operator +(Pudelko p1, Pudelko p2)
        {
            if (p1 == null || p2 == null)
            {
                throw new ArgumentNullException("Both instances must be not null.");
            }

            
            double a = Math.Max(p1.A, p2.A);
            double b = Math.Max(p1.B, p2.B);
            double c = Math.Max(p1.C, p2.C);

            
            return new Pudelko(a, b, c, UnitOfMeasure.Meter);
        }

        public static explicit operator double[](Pudelko p)
        {
            if (p == null)
            {
                throw new ArgumentNullException("Pudelko instance must be non-null.");
            }

            return new double[] { p.A, p.B, p.C };
        }

        public static implicit operator Pudelko((int, int, int) dimensions)
        {
            (int a, int b, int c) = dimensions;
            return new Pudelko(a, b, c, UnitOfMeasure.Millimeter);
        }

        public double this[int index]
        {
            get
            {
                switch (index)
                {
                    case 0:
                        return A;
                    case 1:
                        return B;
                    case 2:
                        return C;
                    default:
                        throw new ArgumentOutOfRangeException("Index must be between 0 and 2.");
                }
            }
        }
        public static Pudelko Parse(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                throw new ArgumentNullException(nameof(input), "Input string cannot be null or empty.");
            }

            Regex regex = new Regex(@"([\d.]+)\s*(\w+)\s*×\s*([\d.]+)\s*(\w+)\s*×\s*([\d.]+)\s*(\w+)");
            Match match = regex.Match(input);

            if (!match.Success)
            {
                throw new FormatException("Invalid input string format.");
            }

            double a = double.Parse(match.Groups[1].Value, CultureInfo.InvariantCulture);
            string unitA = match.Groups[2].Value;
            double b = double.Parse(match.Groups[3].Value, CultureInfo.InvariantCulture);
            string unitB = match.Groups[4].Value;
            double c = double.Parse(match.Groups[5].Value, CultureInfo.InvariantCulture);
            string unitC = match.Groups[6].Value;

            if (unitA != unitB || unitA != unitC)
            {
                throw new FormatException("All units must be the same.");
            }

            UnitOfMeasure unit = unitA.ToLowerInvariant() switch
            {
                "mm" => UnitOfMeasure.Millimeter,
                "cm" => UnitOfMeasure.Centimeter,
                "m" => UnitOfMeasure.Meter,
                _ => throw new FormatException($"Invalid unit of measure: {unitA}"),
            };

            return new Pudelko(a, b, c, unit);
        }

      

    
    }
}
