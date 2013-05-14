using System;
using System.Diagnostics.Contracts;

namespace CodeContactsSamples
{
    public class Rational
    {
	    private int numerator;
	    private int denominator;

		public Rational(int numerator, int denominator)
		{
			// Precondition
			Contract.Requires(denominator != 0);

			this.numerator = numerator;
			this.denominator = denominator;
		}

	    public int Denominator
	    {
		    get
		    {
				// Postcondition

				// Within postconditions the method's return value can be referred to via the
				// expression Contract.Result<T>(), where T is replaced with the return type of the method. When the compiler
				// is unable to infer the type it must be explicitly given. For instance, the C# compiler is unable to infer types
				// for methods that do not take any arguments.

			    Contract.Ensures(Contract.Result<int>() != 0);

			    return this.denominator;
		    }
	    }

		// Invariant
		[ContractInvariantMethod]
		private void ObjectInvariant()
		{
			Contract.Invariant(this.denominator != 0);
		}
    }
}
