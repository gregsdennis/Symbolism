using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using static Symbolism.Functions;
using static Symbolism.Tests.Symbols;

namespace Symbolism.Tests
{
	[TestClass]
	public class DeepSelectTests
	{
		// sin(u) cos(v) - cos(u) sin(v) -> sin(u - v)
		public static MathObject SumDifferenceFormulaFunc(MathObject elt)
		{
			if (elt is Sum)
			{
				var items = new List<MathObject>();

				foreach (var item in (elt as Sum).Elements)
				{
					if (
						item is Product &&
						(item as Product).Elements[0] == -1 &&
						(item as Product).Elements[1] is Cos &&
						(item as Product).Elements[2] is Sin
						)
					{
						var u_ = ((item as Product).Elements[1] as Cos).Parameters[0];
						var v_ = ((item as Product).Elements[2] as Sin).Parameters[0];

						Func<MathObject, bool> match = obj =>
						                               obj is Product &&
						                               (obj as Product).Elements[0] is Cos &&
						                               (obj as Product).Elements[1] is Sin &&

						                               ((obj as Product).Elements[1] as Sin).Parameters[0] == u_ &&
						                               ((obj as Product).Elements[0] as Cos).Parameters[0] == v_;

						if (items.Any(obj => match(obj)))
						{
							items = items.Where(obj => match(obj) == false).ToList();

							items.Add(sin(u_ - v_));
						}
						else items.Add(item);
					}
					else items.Add(item);
				}

				return new Sum(items).Simplify();
			}

			return elt;
		}
		// sin(u) cos(v) + cos(u) sin(v) -> sin(u + v)
		public static MathObject SumDifferenceFormulaAFunc(MathObject elt)
		{
			if (elt is Sum)
			{
				var items = new List<MathObject>();

				foreach (var item in (elt as Sum).Elements)
				{
					if (
						item is Product &&
						(item as Product).Elements[0] is Cos &&
						(item as Product).Elements[1] is Sin
						)
					{
						var u_ = ((item as Product).Elements[0] as Cos).Parameters[0];
						var v_ = ((item as Product).Elements[1] as Sin).Parameters[0];

						Func<MathObject, bool> match = obj =>
						                               obj is Product &&
						                               (obj as Product).Elements[0] is Cos &&
						                               (obj as Product).Elements[1] is Sin &&
						                               ((obj as Product).Elements[1] as Sin).Parameters[0] == u_ &&
						                               ((obj as Product).Elements[0] as Cos).Parameters[0] == v_;

						if (items.Any(obj => match(obj)))
						{
							items = items.Where(obj => match(obj) == false).ToList();

							items.Add(sin(u_ + v_));
						}
						else items.Add(item);
					}
					else items.Add(item);
				}

				return new Sum(items).Simplify();
			}

			return elt;
		}
		public static MathObject DoubleAngleFormulaFunc(MathObject elt)
		{
			if (elt is Product)
			{
				var items = new List<MathObject>();

				foreach (var item in (elt as Product).Elements)
				{
					if (item is Sin)
					{
						var sym = (item as Sin).Parameters.First();

						if (items.Any(obj => (obj is Cos) && (obj as Cos).Parameters.First() == sym))
						{
							items = items.Where(obj => ((obj is Cos) && (obj as Cos).Parameters.First() == sym) == false).ToList();

							items.Add(sin(2*sym)/2);
						}
						else items.Add(item);
					}

					else if (item is Cos)
					{
						var sym = (item as Cos).Parameters.First();

						if (items.Any(obj => (obj is Sin) && (obj as Sin).Parameters.First() == sym))
						{
							items = items.Where(obj => ((obj is Sin) && (obj as Sin).Parameters.First() == sym) == false).ToList();

							items.Add(sin(2*sym)/2);
						}
						else items.Add(item);
					}

					else items.Add(item);

				}
				return new Product(items).Simplify();
			}
			return elt;
		}
		public static MathObject SinCosToTanFunc(MathObject elt)
		{
			if (elt is Product)
			{
				if ((elt as Product).Elements.Any(obj1 =>
												  obj1 is Sin &&
												  (elt as Product).Elements.Any(obj2 => obj2 == 1 / cos((obj1 as Sin).Parameters[0]))))
				{
					var sin_ = (elt as Product).Elements.First(obj1 =>
															   obj1 is Sin &&
															   (elt as Product).Elements.Any(obj2 => obj2 == 1 / cos((obj1 as Sin).Parameters[0])));

					var arg = (sin_ as Sin).Parameters[0];

					return elt * cos(arg) / sin(arg) * tan(arg);
				}

				return elt;
			}

			return elt;
		}


		[TestMethod]
		public void SumDifferenceFormulaFunc1()
		{
			var u = new Symbol("u");
			var v = new Symbol("v");

			Assert.AreEqual(sin(u - v), (sin(u)*cos(v) - cos(u)*sin(v)).DeepSelect(SumDifferenceFormulaFunc));
		}
		[TestMethod]
		public void SumDifferenceFormulaFunc2()
		{
			var u = new Symbol("u");
			var v = new Symbol("v");

			Assert.AreEqual(sin(u + v), (sin(u)*cos(v) + cos(u)*sin(v)).DeepSelect(SumDifferenceFormulaAFunc));
		}
		[TestMethod]
		public void DoubleAngleFormulaFunc()
		{
			// sin(u) cos(u) -> sin(2 u) / 2

			var u = new Symbol("u");
			var v = new Symbol("v");

			Assert.AreEqual(sin(2 * u) / 2, (sin(u) * cos(u)).DeepSelect(DoubleAngleFormulaFunc));
		}
		[TestMethod]
		public void SinCosToTanFunc()
		{
			// sin(x) / cos(x) -> tan(x)

			Func<MathObject, MathObject> SinCosToTanFunc = elt =>
				{
					if (elt is Product)
					{
						if ((elt as Product).Elements.Any(obj1 =>
						                                  obj1 is Sin &&
						                                  (elt as Product).Elements.Any(obj2 => obj2 == 1/cos((obj1 as Sin).Parameters[0]))))
						{
							var sin_ = (elt as Product).Elements.First(obj1 =>
							                                           obj1 is Sin &&
							                                           (elt as Product).Elements.Any(obj2 => obj2 == 1/cos((obj1 as Sin).Parameters[0])));

							var arg = (sin_ as Sin).Parameters[0];

							return elt*cos(arg)/sin(arg)*tan(arg);
						}

						return elt;
					}

					return elt;
				};

			Assert.AreEqual(tan(x), (sin(x)/cos(x)).DeepSelect(SinCosToTanFunc));

			Assert.AreEqual(tan(x)*y, (y*sin(x)/cos(x)).DeepSelect(SinCosToTanFunc));

			Assert.AreEqual(tan(x)*tan(y), (sin(x)*sin(y)/cos(x)/cos(y)).DeepSelect(SinCosToTanFunc)
			                                                            .DeepSelect(SinCosToTanFunc));
		}
	}
}
