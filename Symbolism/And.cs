using System;
using System.Collections.Generic;
using System.Linq;

namespace Symbolism
{
	public class And : Function
	{
		private static MathObject AndProc(MathObject[] ls)
		{
			if (ls.Length == 0) return true;

			if (ls.Length == 1) return ls.First();
			
			if (ls.Any(elt => elt == false)) return false;

			if (ls.Any(elt => elt == true))
				return new And(ls.Where(elt => elt != true)).Simplify();

			if (ls.Any(elt => elt is And))
			{
				var parameters = new List<MathObject>();

				foreach (var elt in ls)
				{
					if (elt is And) parameters.AddRange((elt as And).Parameters);

					else parameters.Add(elt);
				}

				return new And(parameters).Simplify();
			}

			return new And(ls);
		}

		public And(params MathObject[] ls)
			: base("and", AndProc, ls) {}
		public And(IEnumerable<MathObject> ls)
			: base("and", AndProc, ls) { }

		public override MathObject Map(Func<MathObject, MathObject> map) => new And(Parameters.Select(map)).Simplify();
	}
}