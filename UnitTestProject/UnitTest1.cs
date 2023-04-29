using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyLib;

namespace PudelkoTests
{
    [TestClass]
    public class PudelkoTests
    {
        [TestMethod]
        public void DefaultConstructorTest()
        {
            Pudelko pudelko = new Pudelko();
            Assert.AreEqual(0.1, pudelko.A);
            Assert.AreEqual(0.1, pudelko.B);
            Assert.AreEqual(0.1, pudelko.C);
        }

        [TestMethod]
        public void CustomConstructorTest()
        {
            Pudelko pudelko = new Pudelko(2.5, 9.321, 0.1, UnitOfMeasure.Meter);
            Assert.AreEqual(2.5, pudelko.A);
            Assert.AreEqual(9.321, pudelko.B);
            Assert.AreEqual(0.1, pudelko.C);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void InvalidDimensionsTest()
        {
            Pudelko pudelko = new Pudelko(0, 1, 1);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TooLargeDimensionsTest()
        {
            Pudelko pudelko = new Pudelko(12, 1, 1, UnitOfMeasure.Meter);
        }

        [TestMethod]
        
        public void ToStringDefaultTest()
        {
            Pudelko pudelko = new Pudelko(2.5, 9.321, 0.1, UnitOfMeasure.Meter);
            string expectedResult = "2.500 m × 9.321 m × 0.100 m";
            Assert.AreEqual(expectedResult, pudelko.ToString());
        }

        [TestMethod]
        public void ToStringWithFormatTest()
        {
            Pudelko pudelko = new Pudelko(2.5, 9.321, 0.1, UnitOfMeasure.Meter);
            string expectedResultM = "2.500 m × 9.321 m × 0.100 m";
            string expectedResultCm = "250.0 cm × 932.1 cm × 10.0 cm";
            string expectedResultMm = "2500 mm × 9321 mm × 100 mm";
            Assert.AreEqual(expectedResultM, pudelko.ToString("m"));
            Assert.AreEqual(expectedResultCm, pudelko.ToString("cm"));
            Assert.AreEqual(expectedResultMm, pudelko.ToString("mm"));
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void ToStringWithInvalidFormatTest()
        {
            Pudelko pudelko = new Pudelko(2.5, 9.321, 0.1, UnitOfMeasure.Meter);
            pudelko.ToString("invalid");
        }
        [TestMethod]
        public void ObjetoscTest()
        {
            Pudelko pudelko = new Pudelko(2, 3, 4, UnitOfMeasure.Meter);
            double expectedObjetosc = 24.0;
            Assert.AreEqual(expectedObjetosc, pudelko.Objetosc);
        }

        [TestMethod]
        public void PoleTest()
        {
            Pudelko pudelko = new Pudelko(2, 3, 4, UnitOfMeasure.Meter);
            double expectedPole = 52.0;
            Assert.AreEqual(expectedPole, pudelko.Pole);
        }
        [TestMethod]
        public void Equals_WithIdenticalDimensions_ReturnsTrue()
        {
            
            var p1 = new Pudelko(1.0, 2.0, 3.0);
            var p2 = new Pudelko(1.0, 2.0, 3.0);

            
            bool result = p1.Equals(p2);

            
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void Equals_WithDifferentDimensions_ReturnsFalse()
        {
            
            var p1 = new Pudelko(1.0, 2.0, 3.0);
            var p2 = new Pudelko(2.0, 2.0, 3.0);

            bool result = p1.Equals(p2);

            
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void Equals_WithSwappedDimensions_ReturnsTrue()
        {
            
            var p1 = new Pudelko(1.0, 2.0, 3.0);
            var p2 = new Pudelko(1.0, 3.0, 2.0);

           
            bool result = p1.Equals(p2);

            
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void Equals_WithNull_ReturnsFalse()
        {
            
            var p1 = new Pudelko(1.0, 2.0, 3.0);
            Pudelko p2 = null;

           
            bool result = p1.Equals(p2);

           
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void GetHashCode_WithIdenticalDimensions_ReturnsSameHashCode()
        {
          
            var p1 = new Pudelko(1.0, 2.0, 3.0);
            var p2 = new Pudelko(1.0, 2.0, 3.0);

           
            int hashCode1 = p1.GetHashCode();
            int hashCode2 = p2.GetHashCode();

            
            Assert.AreEqual(hashCode1, hashCode2);
        }

        [TestMethod]
        public void GetHashCode_WithDifferentDimensions_ReturnsDifferentHashCode()
        {
           
            var p1 = new Pudelko(1.0, 2.0, 3.0);
            var p2 = new Pudelko(2.0, 2.0, 3.0);

            int hashCode1 = p1.GetHashCode();
            int hashCode2 = p2.GetHashCode();

            
            Assert.AreNotEqual(hashCode1, hashCode2);
        }
        [TestMethod]
        public void OperatorEquals_SameDimensions_ReturnsTrue()
        {
           
            var pudelko1 = new Pudelko(2, 3, 4, UnitOfMeasure.Meter);
            var pudelko2 = new Pudelko(2, 3, 4, UnitOfMeasure.Meter);

            
            bool areEqual = (pudelko1 == pudelko2);

           
            Assert.IsTrue(areEqual);
        }

        [TestMethod]
        public void OperatorNotEquals_DifferentDimensions_ReturnsTrue()
        {
            
            var pudelko1 = new Pudelko(2, 3, 4, UnitOfMeasure.Meter);
            var pudelko2 = new Pudelko(1, 3, 4, UnitOfMeasure.Meter);

            
            bool areNotEqual = (pudelko1 != pudelko2);

           
            Assert.IsTrue(areNotEqual);
        }
        [TestMethod]
        public void OperatorAddition_TwoBoxes_ReturnsNewBoxWithMinimalDimensions()
        {
            
            var pudelko1 = new Pudelko(2, 3, 4, UnitOfMeasure.Meter);
            var pudelko2 = new Pudelko(1, 4, 3, UnitOfMeasure.Meter);
            var expectedPudelko = new Pudelko(2, 4, 4, UnitOfMeasure.Meter);

            var resultPudelko = pudelko1 + pudelko2;

            Assert.AreEqual(expectedPudelko, resultPudelko);
        }
        [TestMethod]
        public void ExplicitConversion_PudelkoToDoubleArray_ReturnsCorrectDimensionsArray()
        {
            
            Pudelko pudelko = new Pudelko(200, 300, 400, UnitOfMeasure.Millimeter);
            double[] expectedDimensions = new double[] { 0.2, 0.3, 0.4 };

            
            double[] actualDimensions = (double[])pudelko;

            
            CollectionAssert.AreEqual(expectedDimensions, actualDimensions, "The conversion from Pudelko to double[] did not return the correct dimensions array.");
        }

        [TestMethod]
        public void ImplicitConversion_ValueTupleToPudelko_ReturnsCorrectPudelkoInstance()
        {
            
            (int, int, int) dimensionsTuple = (200, 300, 400);
            Pudelko expectedPudelko = new Pudelko(0.2, 0.3, 0.4, UnitOfMeasure.Meter);

          
            Pudelko actualPudelko = dimensionsTuple;

          
            Assert.AreEqual(expectedPudelko, actualPudelko, "The conversion from ValueTuple<int,int,int> to Pudelko did not return the correct Pudelko instance.");
        }
        [TestMethod]
        public void Indexer_GetDimensions_ReturnsCorrectValues()
        {
            // Arrange
            Pudelko pudelko = new Pudelko(1, 2, 3, UnitOfMeasure.Meter);
            double expectedA = 1;
            double expectedB = 2;
            double expectedC = 3;

            // Act
            double actualA = pudelko[0];
            double actualB = pudelko[1];
            double actualC = pudelko[2];

            // Assert
            Assert.AreEqual(expectedA, actualA, "The indexer did not return the correct value for dimension A.");
            Assert.AreEqual(expectedB, actualB, "The indexer did not return the correct value for dimension B.");
            Assert.AreEqual(expectedC, actualC, "The indexer did not return the correct value for dimension C.");
        }
        [TestMethod]
        public void PudelkoForeach_IteratesThroughDimensionsInOrder()
        {
            // Arrange
            Pudelko pudelko = new Pudelko(1, 2, 3);
            List<double> expectedDimensions = new List<double> { 0.01, 0.02, 0.03};
            List<double> actualDimensions = new List<double>();

            // Act
            foreach (double dimension in pudelko)
            {
                actualDimensions.Add(dimension);
            }

            // Assert
            CollectionAssert.AreEqual(expectedDimensions, actualDimensions, "The foreach loop did not iterate through dimensions in the correct order.");
        }



    }
}
