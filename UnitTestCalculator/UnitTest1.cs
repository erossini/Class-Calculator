using EnricoRossini.Calculator;
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestCalculator
{
    [TestClass]
    public class UnitTest1
    {
        const string NegativeNumber = "Negative numbers don't accept -1;-2";
        Calculator calculator = new Calculator();

        [TestMethod]
        public void vst_DecimalNumbers_MustReturnDouble()
        {
            double expect = 2.3;
            Assert.AreEqual(expect, calculator.Add("//;\n1.1;1.2"));
        }

        [TestMethod]
        public void vst_NewLineSeparators()
        {
            int expect = 39;
            Assert.AreEqual(expect, calculator.Add("1\n2\n31\n5"));
        }

        [TestMethod]
        public void vst_NewLineSeparatorsCommasCombined()
        {
            int expect = 39;
            Assert.AreEqual(expect, calculator.Add("1\n2,31,5"));
        }

        [TestMethod]
        public void vst_NonNumericStringPassed_RaiseThrowException()
        {
            MyAssert.Throws<ValidationException>(() => calculator.Add("XT"));
        }

        [TestMethod]
        public void vst_NullString_ReturnZero()
        {
            int expect = 0;
            Assert.AreEqual(expect, calculator.Add(null));
        }

        [TestMethod]
        public void vst_NumbersGreaterThan1000_BeSkip()
        {
            double expect = 1.1;
            Assert.AreEqual(expect, calculator.Add("//[***]\n1.1***1000.2"));
        }

        [TestMethod]
        public void vst_OneString_ReturnTheString()
        {
            int expect = 3;
            Assert.AreEqual(expect, calculator.Add("3"));
        } 

        [TestMethod]
        public void vst_PassEmptyString_ReturnZero()
        {
            int expect = 0;
            Assert.AreEqual(expect, calculator.Add(string.Empty));
        }

        [TestMethod]
        public void vst_SpacesBetweenDelimiters_Handled()
        {
            double expect = 4.3;
            Assert.AreEqual(expect, calculator.Add("//[***][kkk]\n1.1   ***  1.2kkk   2"));
        }

        [TestMethod]
        public void vst_SpecialDelimiterWithMultipleNegativeNumber_HaveThrows()
        {
            try
            {
                MyAssert.Throws<CalculatorException>(() => calculator.Add("//[***]\n-1***-2"));
                Assert.Fail();
            }
            catch (Exception exception)
            {
                Assert.AreEqual(exception.Message, NegativeNumber);
            }
        }

        [TestMethod]
        public void vst_ThreeString_Pass()
        {
            int expect = 39;
            Assert.AreEqual(expect, calculator.Add("3,31,5"));
        }

        [TestMethod]
        public void vst_TwoNegativeString_HaveAnException()
        {
            try
            {
                MyAssert.Throws<CalculatorException>(() => calculator.Add("-1,-2"));
                Assert.Fail();
            }
            catch (Exception exception)
            {
                Assert.AreEqual(exception.Message, NegativeNumber);
            }
        }

        [TestMethod]
        public void vst_TwoString_Pass()
        {
            int expect = 39;
            Assert.AreEqual(expect, calculator.Add("6,33"));
        }

        [TestMethod]
        public void vst_WithDelimiterAndColon_Pass()
        {
            int expect = 3;
            Assert.AreEqual(expect, calculator.Add("//;\n1;2"));
        }

        [TestMethod]
        public void vst_WithMultipleNegativeNumber_HaveAnException()
        {
            try
            {
                MyAssert.Throws<CalculatorException>(() => calculator.Add("//;\n-1;-2"));
                Assert.Fail();
            }
            catch (Exception exception)
            {
                Assert.AreEqual(exception.Message, NegativeNumber);
            }
        }

        [TestMethod]
        public void vst_WithNegativeNumber_HaveAnException()
        {
            MyAssert.Throws<ValidationException>(() => calculator.Add("//;\n1;-2"));
        }

        [TestMethod]
        public void vst_WithOneSpecialDelimiters_Pass()
        {
            double expect = 2.3;
            Assert.AreEqual(expect, calculator.Add("//[***]\n1.1***1.2"));
        }

        [TestMethod]
        public void vst_WithSpecialDelimitersOnlyOneChar_Pass()
        {
            double expect = 2.3;
            Assert.AreEqual(expect, calculator.Add("//[*]\n1.1*1.2"));
        }

        [TestMethod]
        public void vst_WithSwithAndAnySpecialDelimiters_Pass()
        {
            double expect = 2.3;
            Assert.AreEqual(expect, calculator.Add("//***\n1.1***1.2"));
        }

        [TestMethod]
        public void vst_WithThreeSpecialDelimiters_Pass()
        {
            double expect = 9.0;
            Assert.AreEqual(expect, calculator.Add("//[***][kkk][GGG]\n1.1***1.2kkk2GGG4.7"));
        }

        [TestMethod]
        public void vst_WithTwoSpecialDelimiters_Pass()
        {
            double expect = 4.3;
            Assert.AreEqual(expect, calculator.Add("//[***][kkk]\n1.1***1.2kkk2"));
        }

        [TestMethod]
        public void vst_WithTwoSpecialDelimitersOnlyOneChar_Pass()
        {
            int expect = 6;
            Assert.AreEqual(expect, calculator.Add("//[*][%]\n1*2%3"));
        }
    }
}