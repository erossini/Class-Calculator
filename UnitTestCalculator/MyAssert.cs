﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UnitTestCalculator
{
    /// <summary>
    /// This class implement the some functionality exists in NUnit
    /// that it doesn't exists in Microsoft Test
    /// </summary>
    public static class MyAssert
    {
        /// <summary>
        /// Helper for asserting that a function throws an exception of a particular type.
        /// </summary>
        public static void Throws<T>(Action func) where T : Exception
        {
            Exception exceptionOther = null;
            var exceptionThrown = false;
            try
            {
                func.Invoke();
            }
            catch (T)
            {
                exceptionThrown = true;
            }
            catch (Exception e)
            {
                exceptionOther = e;
            }

            if (!exceptionThrown)
            {
                if (exceptionOther != null)
                {
                    throw new AssertFailedException(
                        String.Format(exceptionOther.Message, typeof(T), exceptionOther.GetType()),
                        exceptionOther
                        );
                }

                throw new AssertFailedException(
                    String.Format("An exception of type {0} was expected, but no exception was thrown.", typeof(T))
                    );
            }
        }
    }
}