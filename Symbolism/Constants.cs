namespace Symbolism
{
	// ReSharper disable InconsistentNaming
	// These use non-standard naming conventions,
	// but it actually makes the code look nicer,
	// so we're using it.
	public static class Constants
	{
		public static readonly Symbol pi = new Symbol("pi");
		public static readonly Symbol e = new Symbol("e");
		public static readonly Undefined undef = Undefined.Instance;
	}
}