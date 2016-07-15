namespace Symbolism.Tests
{
	public static class Symbols
	{
		public static readonly MathObject a = new Symbol("a");
		public static readonly MathObject b = new Symbol("b");
		public static readonly MathObject c = new Symbol("c");
		public static readonly MathObject d = new Symbol("d");

		public static readonly MathObject x = new Symbol("x");
		public static readonly MathObject y = new Symbol("y");
		public static readonly MathObject z = new Symbol("z");
	}

	public static class Constants
	{
		public static readonly MathObject half = new Fraction(1, 2);
	}
}