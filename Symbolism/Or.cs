using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Symbolism
{
	[DebuggerDisplay("{StandardForm()}")]
	public class Or : Function
	{
		private static MathObject OrProc(params MathObject[] ls)
		{
			if (ls.Length == 1) return ls.First();

			// 10 || false || 20   ->   10 || 20

			if (ls.Any(elt => elt == false))
				return new Or(ls.Where(elt => elt != false).ToList()).Simplify();

			if (ls.Any(elt => (elt as Bool)?.Value ?? false)) return true;

			if (ls.All(elt => !(elt as Bool)?.Value ?? false)) return false;

			if (ls.Any(elt => elt is Or))
			{
				var parameters = new List<MathObject>();

				foreach (var elt in ls)
				{
					if (elt is Or) parameters.AddRange((elt as Or).Parameters);
					else parameters.Add(elt);
				}

				return new Or(parameters).Simplify();
			}

			return new Or(ls);
		}

		public Or(params MathObject[] ls)
			: base("or", OrProc, ls) {}
		public Or(IEnumerable<MathObject> ls)
			: base("or", OrProc, ls) {}

		public override MathObject Map(Func<MathObject, MathObject> map) => new Or(Parameters.Select(map)).Simplify();
	}
}