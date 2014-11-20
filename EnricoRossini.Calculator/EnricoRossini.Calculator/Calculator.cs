/*
 *   Enrico Rossini - 17/05/2014
 *   String Calcultor
 * 
 *   This is a TDD example for Kata resolution
 * 
 */
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace EnricoRossini.Calculator
{
    public class Calculator
    {
        static string uiSep = CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator;

        /// <summary>
        /// This method can sum two or more numbers (integer or double)
        /// </summary>
        /// <param name="numbers">String with numbers</param>
        /// <returns>
        ///     Return the sum of all numbers.
        ///     If string is null or empty return 0.
        ///     Into the string it can be
        /// </returns>
        public double Add(string numbers)
        {
            Double sum = 0;
            if (string.IsNullOrEmpty(numbers))
            {
                sum = 0;
            }
            else
            {
                // define the search string for regex
                const string regex1 = "(\\//)";
                const string regex2 = "(.*?)";
                const string regex3 = "(\\n)";
                const string regex4 = "(.*?\\d)";
                const string regex5 = "(\\[.*?\\])";

                // create a list of string for delimiters
                var delimiters = new List<string>();

                // check the string
                var r = new Regex(regex1 + regex2 + regex3 + regex4, 
                                  RegexOptions.IgnoreCase | RegexOptions.Singleline);
                Match m = r.Match(numbers);
                if (m.Success)
                {
                    // find delimiter char or string
                    string delimiterCollection = m.Groups[2].ToString();
                    int numberStartIndex = m.Groups[3].Index;
                    
                    // check other delimiters
                    var r2 = new Regex(regex5, RegexOptions.IgnoreCase | RegexOptions.Singleline);
                    MatchCollection m2 = r2.Matches(delimiterCollection);
                    if (m2.Count > 0)
                    {
                        foreach (Match x in m2)
                        {
                            delimiters.Add(x.ToString().Replace("[", "").Replace("]", ""));
                        }
                    }
                    else
                    {
                        delimiters.Add(delimiterCollection);
                    }

                    // change the string and remove the delimiters or other character
                    numbers = numbers.Remove(0, numberStartIndex + 1);
                }
                else
                {
                    delimiters.Add("\n");
                    delimiters.Add(",");
                } 

                // split the string that now it has only numbers
                string[] splittedNumbers = numbers.Split(delimiters.ToArray(), StringSplitOptions.None);
                // check each number and if the number doesn't permit it'll throw an exception
                ValidateNumbers(splittedNumbers); 

                // check if each number is under 1000. If a number is greater than 1000, this will change to 0
                foreach (string s in splittedNumbers)
                {
                    double ss = Double.Parse(s.Trim().Replace(uiSep, ","), 
                                             System.Globalization.NumberStyles.AllowDecimalPoint, 
                                             System.Globalization.NumberFormatInfo.InvariantInfo);
                    sum += ss <= 1000 ? ss : 0;
                }
            }

            // return the sum of numbers
            return sum;
        } 

        /// <summary>
        /// Validate each numbers in a string and check if there is a negative numbers
        /// </summary>
        /// <param name="numbers">string contains numbers</param>
        private static void ValidateNumbers(IEnumerable<string> numbers)
        {
            double x;
            var negativeNumbers = new List<string>();
            foreach (string s in numbers)
            {
                Validator.IsRequiredField(Double.TryParse(s, out x), "Validation Error");

                Double d = Double.Parse(s.Trim().Replace(uiSep, ","));
                if (d < 0)
                {
                    negativeNumbers.Add(s);
                }
            }

            // create an exception and view all negative numbers
            Validator.IsRequiredField(negativeNumbers.Count <= 0,
                                      "Negative numbers don't accept " + ShowAllNegatives(negativeNumbers));
        }

        /// <summary>
        /// Create a string with all negative numbers from string
        /// </summary>
        /// <param name="negativeNumbers">List of negative numbers</param>
        /// <returns>Get a string with all negative numbers with separator</returns>
        private static string ShowAllNegatives(List<string> negativeNumbers)
        {
            var sb = new StringBuilder();
            int counter = 0;
            negativeNumbers.ForEach(k =>
                                        {
                                        if (counter == 0)
                                            {
                                                sb.Append(k);
                                                counter++;
                                            }
                                        else
                                            {
                                                sb.Append(";" + k);
                                            }
                                        }); 

            return sb.ToString();
        }
    } 

    /// <summary>
    /// Define a validator class
    /// </summary>
    public static class Validator
    {
        // define a method
        public static void IsRequiredField(bool criteria, string message)
        {
        if (!criteria)
            {
                // generate the exception
                throw new ValidationException(message);
            }
        }
    } 

    /// <summary>
    /// Define a validator exception
    /// </summary>
    public class ValidationException : ApplicationException
    {
        public ValidationException(string message) : base(message)
        {
        }
    }

    /// <summary>
    /// Define a calculator exception
    /// </summary>
    public class CalculatorException : ApplicationException
    {
        public CalculatorException(string message) : base(message)
        {
        }
    }
}