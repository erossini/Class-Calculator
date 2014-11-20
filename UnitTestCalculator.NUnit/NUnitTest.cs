using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using EnricoRossini.Calculator; 

namespace UnitTestCalculator.NUnit
{
    [TestFixture]
    public class NUnitTest
    {
        const string NegativeNumber = "Negative numbers don't accept -1;-2";
        Calculator calculator = new Calculator();

        [Test]
        public void DecimalNumbers_MustReturnDouble()
        {
            double expect = 2.3;
            Assert.AreEqual(expect, calculator.Add("//;\n1.1;1.2"));
        }

        [Test]
        public void NewLineSeparators()
        {
            int expect = 39;
            Assert.AreEqual(expect, calculator.Add("1\n2\n31\n5"));
        }

        [Test]
        public void NewLineSeparatorsCommasCombined()
        {
            int expect = 39;
            Assert.AreEqual(expect, calculator.Add("1\n2,31,5"));
        }

        [Test]
        public void NonNumericStringPassed_RaiseThrowException()
        {
            Assert.Throws<ValidationException>(() => calculator.Add("XT"));
        }

        [Test]
        public void NullString_ReturnZero()
        {
            int expect = 0;
            Assert.AreEqual(expect, calculator.Add(null));
        }

        [Test]
        public void NumbersGreaterThan1000_BeSkip()
        {
            double expect = 1.1;
            Assert.AreEqual(expect, calculator.Add("//[***]\n1.1***1000.2"));
        }

        [Test]
        public void OneString_ReturnTheString()
        {
            int expect = 3;
            Assert.AreEqual(expect, calculator.Add("3"));
        } 

        [Test]
        public void PassEmptyString_ReturnZero()
        {
            int expect = 0;
            Assert.AreEqual(expect, calculator.Add(string.Empty));
        }

        [Test]
        public void SpacesBetweenDelimiters_Handled()
        {
            double expect = 4.3;
            Assert.AreEqual(expect, calculator.Add("//[***][kkk]\n1.1   ***  1.2kkk   2"));
        } 
 
        [Test]
        public void SpecialDelimiterWithMultipleNegativeNumber_HaveThrows()
        {
            var ce = Assert.Throws<ValidationException>(() => calculator.Add("//[***]\n-1***-2"));
            Assert.AreEqual(ce.Message, NegativeNumber);
        } 

        [Test]
        public void ThreeString_Pass()
        {
            int expect = 39;
            Assert.AreEqual(expect, calculator.Add("3,31,5"));
        } 

        [Test]
        public void TwoNegativeString_HaveAnException()
        {
            var ce = Assert.Throws<ValidationException>(() => calculator.Add("-1,-2"));
            Assert.AreEqual(ce.Message, NegativeNumber);
        } 

        [Test]
        public void TwoString_Pass()
        {
            int expect = 39;
            Assert.AreEqual(expect, calculator.Add("6,33"));
        } 

        [Test]
        public void WithDelimiterAndColon_Pass()
        {
            int expect = 3;
            Assert.AreEqual(expect, calculator.Add("//;\n1;2"));
        } 

        [Test]
        public void WithMultipleNegativeNumber_HaveAnException()
        {
            var exception = Assert.Throws<ValidationException>(() => calculator.Add("//;\n-1;-2"));
            Assert.AreEqual(exception.Message, NegativeNumber);
        } 

        [Test]
        public void WithNegativeNumber_HaveAnException()
        {
            Assert.Throws<ValidationException>(() => calculator.Add("//;\n1;-2"));
        } 

        [Test]
        public void WithOneSpecialDelimiters_Pass()
        {
            double expect = 2.3;
            Assert.AreEqual(expect, calculator.Add("//[***]\n1.1***1.2"));
        } 

        [Test]
        public void WithSpecialDelimitersOnlyOneChar_Pass()
        {
            double expect = 2.3;
            Assert.AreEqual(expect, calculator.Add("//[*]\n1.1*1.2"));
        } 

        [Test]
        public void WithSwithAndAnySpecialDelimiters_Pass()
        {
            double expect = 2.3;
            Assert.AreEqual(expect, calculator.Add("//***\n1.1***1.2"));
        } 

        [Test]
        public void WithThreeSpecialDelimiters_Pass()
        {
            double expect = 9.0;
            Assert.AreEqual(expect, calculator.Add("//[***][kkk][GGG]\n1.1***1.2kkk2GGG4.7"));
        } 

        [Test]
        public void WithTwoSpecialDelimiters_Pass()
        {
            double expect = 4.3;
            Assert.AreEqual(expect, calculator.Add("//[***][kkk]\n1.1***1.2kkk2"));
        } 

        [Test]
        public void WithTwoSpecialDelimitersOnlyOneChar_Pass()
        {
            int expect = 6;
            Assert.AreEqual(expect, calculator.Add("//[*][%]\n1*2%3"));
        }
    }
}