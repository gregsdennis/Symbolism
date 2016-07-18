namespace Symbolism
{
	public class Undefined : MathObject
	{
		internal static Undefined Instance { get; private set; }

		static Undefined()
		{
			Instance = new Undefined();
		}
		private Undefined() {}

		public override string ToString()
		{
			return "undef";
		}
	}
}