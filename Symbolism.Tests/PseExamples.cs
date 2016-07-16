using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tests;
using static Symbolism.Constants;
using static Symbolism.Functions;
using static Symbolism.Tests.Symbols;

namespace Symbolism.Tests
{
	[TestClass]
	public class PseExamples
	{
		private static And Kinematic(Symbol s, Symbol u, Symbol v, Symbol a, Symbol t) =>
			new And(v == u + a*t,
			        s == (u + v)*t/2);
		private static And Kinematic(Symbol sA, Symbol sB, Symbol vA, Symbol vB, Symbol a, Symbol tA, Symbol tB) =>
			new And(vB == vA + a*(tB - tA),
			        sB - sA == (vA + vB)*(tB - tA)/2);

		[TestMethod]
		public void Example_2_6()
		{
			var sAC = new Symbol("sAC");
			var sAB = new Symbol("sAB");

			var vA = new Symbol("vA");
			var vB = new Symbol("vB");
			var vC = new Symbol("vC");

			var tAC = new Symbol("tAC");
			var tAB = new Symbol("tAB");

			var eqs = and(tAB == tAC/2,
			              Kinematic(sAC, vA, vC, a, tAC),
			              Kinematic(sAB, vA, vB, a, tAB));

			var vals = new List<Equation>
				{
					vA == 10,
					vC == 30,
					tAC == 10
				};

			Assert.AreEqual(a == 2, eqs.EliminateVariables(tAB, sAC, vB, sAB)
			                           .IsolateVariable(a)
			                           .Substitute(vals));
			Assert.AreEqual(sAB == 75, eqs.EliminateVariables(vB, a, tAB, sAC)
			                              .Substitute(vals));
		}
		[TestMethod]
		public void Example_2_7()
		{
			var s = new Symbol("s");
			var u = new Symbol("u");
			var v = new Symbol("v");
			var t = new Symbol("t");

			var eqs = Kinematic(s, u, v, a, t);

			var vals = new List<Equation>
				{
					u == 63,
					v == 0,
					t == 2.0
				};

			Assert.AreEqual(a == -31.5, eqs.EliminateVariable(s)
			                               .IsolateVariable(a)
			                               .Substitute(vals));

			Assert.AreEqual(s == 63.0, eqs.EliminateVariable(a)
			                              .Substitute(vals));
		}
		[TestMethod]
		public void Example_2_8()
		{
			var s1 = new Symbol("s1");
			var u1 = new Symbol("u1");
			var v1 = new Symbol("v1");
			var a1 = new Symbol("a1");
			var t1 = new Symbol("t1");

			var s2 = new Symbol("s2");
			var u2 = new Symbol("u2");
			var v2 = new Symbol("v2");
			var a2 = new Symbol("a2");
			var t2 = new Symbol("t2");

			var eqs = and(u1 == v1,
						  s1 == s2,
						  t2 == t1 - 1,
						  Kinematic(s1, u1, v1, a1, t1),
						  Kinematic(s2, u2, v2, a2, t2));

			var vals = new List<Equation>
				{
					v1 == 45.0,
					u2 == 0,
					a2 == 3
				};

			Assert.AreEqual(or(t2 == -0.96871942267131317, t2 == 30.968719422671313), eqs.EliminateVariables(s2, t1, a1, s1, v2, u1)
																						 .IsolateVariable(t2)
																						 .Substitute(vals));
		}
		[TestMethod]
		public void Example_2_12()
		{
			var yA = new Symbol("yA");
			var yB = new Symbol("yB");
			var yC = new Symbol("yC");
			var yD = new Symbol("yD");

			var tA = new Symbol("tA");
			var tB = new Symbol("tB");
			var tC = new Symbol("tC");
			var tD = new Symbol("tD");

			var vA = new Symbol("vA");
			var vB = new Symbol("vB");
			var vC = new Symbol("vC");
			var vD = new Symbol("vD");

			var a = new Symbol("a");

			var eqs = and(Kinematic(yA, yB, vA, vB, a, tA, tB),
			              Kinematic(yB, yC, vB, vC, a, tB, tC),
			              Kinematic(yC, yD, vC, vD, a, tC, tD));

			var vals = new List<Equation>
				{
					yA == 50,
					yC == 50,
					vA == 20,
					vB == 0,
					a == -9.8,
					tA == 0,
					tD == 5
				};

			// velocity and position at t = 5.00 s

			DoubleFloat.Tolerance = 0.000000001;

			Assert.AreEqual(or(vD == -29.000000000000004, vD == -29.000000000000007), eqs.EliminateVariables(tB, tC, vC, yB, yD)
			                                                                             .Substitute(vals));

			Assert.AreEqual(or(yD == 27.499999999, yD == 27.499999999), eqs.EliminateVariables(tB, tC, vC, yB, vD)
			                                                               .IsolateVariable(yD)
			                                                               .Substitute(vals));

			DoubleFloat.Tolerance = null;
		}
		[TestMethod]
		public void Example_4_3()
		{
			// A long-jumper leaves the ground at an angle of 20.0° above
			// the horizontal and at a speed of 11.0 m/s.

			// (a) How far does he jump in the horizontal direction?
			// (Assume his motion is equivalent to that of a particle.)

			// (b) What is the maximum height reached?

			var xA = new Symbol("xA");
			var xB = new Symbol("xB");
			var xC = new Symbol("xC");

			var yA = new Symbol("yA");
			var yB = new Symbol("yB");
			var yC = new Symbol("yC");

			var vxA = new Symbol("vxA");
			var vxB = new Symbol("vxB");
			var vxC = new Symbol("vxC");

			var vyA = new Symbol("vyA");
			var vyB = new Symbol("vyB");
			var vyC = new Symbol("vyC");

			var tAB = new Symbol("tAB");
			var tAC = new Symbol("tAC");

			var ax = new Symbol("ax");
			var ay = new Symbol("ay");

			var vA = new Symbol("vA");
			var thA = new Symbol("thA");

			var eqs = and(vxA == vA*cos(thA),
			              vyA == vA*sin(thA),
			              tAC == 2*tAB,
			              vxB == vxA + ax*tAB,
			              vyB == vyA + ay*tAB,
			              xB == xA + vxA*tAB + ax*(tAB ^ 2)/2,
			              yB == yA + vyA*tAB + ay*(tAB ^ 2)/2,
			              vxC == vxA + ax*tAB,
			              vyC == vyA + ay*tAB,
			              xC == xA + vxA*tAC + ax*(tAC ^ 2)/2,
			              yC == yA + vyA*tAC + ay*(tAC ^ 2)/2);

			var zeros = new List<Equation>
				{
					xA == 0,
					yA == 0,
					ax == 0,
					vyB == 0
				};

			var vals = new List<Equation>
				{
					thA == 20.ToRadians(),
					vA == 11.0,
					ay == -9.8,
					pi == Math.PI
				};

			Assert.AreEqual(xC == 7.9364592624562507, eqs.EliminateVariables(xB, yC, vxB, vxC, vyC, yB, tAC, vxA, vyA, tAB)
			                                             .Substitute(zeros)
			                                             .Substitute(vals));

			Assert.AreEqual(yB == 0.72215873425009314, eqs.EliminateVariables(xB, yC, vxB, vxC, vyC, xC, vxA, tAC, vyA, tAB)
			                                              .Substitute(zeros)
			                                              .Substitute(vals));
		}
		[TestMethod]
		public void Example_4_3_UsingObject()
		{
			// A long-jumper leaves the ground at an angle of 20.0° above
			// the horizontal and at a speed of 11.0 m/s.

			// (a) How far does he jump in the horizontal direction?
			// (Assume his motion is equivalent to that of a particle.)

			// (b) What is the maximum height reached?

			var obj = new KinematicObjectABC("obj");

			var yB = new Symbol("yB");
			var xC = new Symbol("xC");
			var ay = new Symbol("ay");
			var thA = new Symbol("thA");
			var vA = new Symbol("vA");

			var eqs = and(

				obj.TrigEquationsA(),

				obj.tAC == 2 * obj.tAB,

				obj.EquationsAB(),
				obj.EquationsAC()

				);

			var vals = new List<Equation>
				{
					obj.xA == 0,
					obj.yA == 0,

					obj.vA == vA,
					obj.thA == thA,

					obj.yB == yB,
					obj.vyB == 0,

					obj.xC == xC,

					obj.ax == 0,
					obj.ay == ay
				};

			var numerical_vals = new List<Equation>
				{
					thA == 20.ToRadians(),
					vA == 11.0,
					ay == -9.8,
					pi == Math.PI
				};

			Assert.AreEqual(xC == 7.9364592624562507, eqs.Substitute(vals)
			                                             .EliminateVariables(obj.vxA, obj.vyA, obj.vyC, obj.vxC, obj.vxB, obj.xB, yB, obj.yC, obj.tAC, obj.tAB)
			                                             .Substitute(numerical_vals));

			Assert.AreEqual(yB == 0.72215873425009314, eqs.Substitute(vals)
			                                              .EliminateVariables(obj.tAB, obj.tAC, obj.vxA, obj.vxB, obj.vxC, obj.vyC, obj.vyA, obj.xB, xC, obj.yC)
			                                              .Substitute(numerical_vals));
		}
		[TestMethod]
		public void Example_4_5()
		{
			var yA = new Symbol("yA");
			var yC = new Symbol("yC");

			var vxA = new Symbol("vxA");
			var vxC = new Symbol("vxC");

			var vyA = new Symbol("vyA");
			var vyC = new Symbol("vyC");
			var tAC = new Symbol("tAC");

			var ax = new Symbol("ax");
			var ay = new Symbol("ay");

			var vA = new Symbol("vA");
			var thA = new Symbol("thA");

			var vC = new Symbol("vC");

			var eqs = and(vxA == vA*cos(thA),
			              vyA == vA*sin(thA),
			              vxC == vxA + ax*tAC,
			              vyC == vyA + ay*tAC,
			              yC == yA + vyA*tAC + ay*(tAC ^ 2)/2,
			              vC == sqrt((vxC ^ 2) + (vyC ^ 2)),
			              ay != 0);

			var zeros = new List<Equation>
				{
					ax == 0,
					yC == 0
				};
			var vals = new List<Equation>
				{
					yA == 45,
					vA == 20,
					thA == 30.ToRadians(),
					ay == -9.8,
					pi == Math.PI
				};

			DoubleFloat.Tolerance = 0.00001;

			Assert.AreEqual(or(tAC == 4.2180489012229376, tAC == -2.1772325746923267), eqs.EliminateVariables(vC, vxA, vxC, vyC, vyA)
			                                                                              .IsolateVariable(tAC)
			                                                                              .LogicalExpand()
			                                                                              .SimplifyEquation()
			                                                                              .SimplifyLogical()
			                                                                              .CheckVariable(ay)
			                                                                              .Substitute(zeros)
			                                                                              .Substitute(vals));

			Assert.AreEqual(or(vC == 35.805027579936315, vC == 35.805027579936322), eqs.Substitute(zeros)
			                                                                           .EliminateVariables(vxC, vxA, vyA, vyC, tAC)
			                                                                           .SimplifyEquation()
			                                                                           .SimplifyLogical()
			                                                                           .CheckVariable(ay)
			                                                                           .Substitute(vals));

			DoubleFloat.Tolerance = null;
		}
	}
}
