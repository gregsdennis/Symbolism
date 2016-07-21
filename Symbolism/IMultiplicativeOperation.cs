using System.Collections.Generic;

namespace Symbolism
{
	internal interface IMultiplicativeOperation
	{
		IReadOnlyList<MathObject> Elements { get; }
	}
}