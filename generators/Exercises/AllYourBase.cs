using System;
using Generators.Output;
using Newtonsoft.Json.Linq;

namespace Generators.Exercises
{
    public class AllYourBase : Exercise
    {
        public AllYourBase()
        {
            foreach (var canonicalDataCase in CanonicalData.Cases)
            {
                dynamic input = canonicalDataCase.Input;
                input["input_digits"] = ((JArray)input["input_digits"]).Values<int>();
                canonicalDataCase.Input = input;

                canonicalDataCase.ExceptionThrown = canonicalDataCase.Expected is null ? typeof(ArgumentException) : null;
                canonicalDataCase.UseVariablesForInput = true;
                canonicalDataCase.UseVariableForExpected = canonicalDataCase.ExceptionThrown == null;
            }
        }

        protected override TestClass CreateTestClass()
        {
            var testClass = base.CreateTestClass();
            testClass.UsingNamespaces.Add(typeof(ArgumentException).Namespace);

            return testClass;
        }
    }
}