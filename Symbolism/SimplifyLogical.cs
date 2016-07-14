using System.Collections.Generic;
using System.Linq;

namespace Symbolism
{
    public static partial class Extensions
    {
	    private static bool HasDuplicates(this IEnumerable<MathObject> ls)
	    {
		    var elements = ls as IList<MathObject> ?? ls.ToList();
		    return elements.Any(elt => elements.Count(item => item.Equals(elt)) > 1);
	    }

		// TODO: can this be replaced with .Distinct()?
	    private static IEnumerable<MathObject> RemoveDuplicates(this IEnumerable<MathObject> seq)
        {
            var ls = new List<MathObject>();

            foreach (var elt in seq)
                if (ls.Any(item => item.Equals(elt)) == false)
                    ls.Add(elt);

		    return ls;
        }

        public static MathObject SimplifyLogical(this MathObject expr)
        {
	        var and = expr as And;
	        if (and != null && and.Parameters.HasDuplicates())
                return new And (and.Parameters.RemoveDuplicates());

	        var or = expr as Or;
	        if (or != null && or.Parameters.HasDuplicates())
		        return new Or (or.Parameters.RemoveDuplicates()).SimplifyLogical();

            if (or != null) return or.Map(elt => elt.SimplifyLogical());

            return expr;
        }
    }
}
