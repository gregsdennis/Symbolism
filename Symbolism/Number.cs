namespace Symbolism
{
	public abstract class Number : MathObject
	{
		public abstract DoubleFloat ToDouble();

		internal override MathObject Expand() => this;
	}
}