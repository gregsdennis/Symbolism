using System.Collections.Generic;

namespace Symbolism
{
	internal interface IAdditiveOperation
	{
		IReadOnlyList<MathObject> Elements { get; }
	}
}