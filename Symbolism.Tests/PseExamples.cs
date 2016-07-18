using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Symbolism.Tests.Helpers;

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
		[TestMethod]
		public void Example_4_6()
		{
			// An Alaskan rescue plane drops a package of emergency rations to 
			// a stranded party of explorers, as shown in Figure 4.13.
			// If the plane is traveling horizontally at 40.0 m/s and is
			// 100 m above the ground, where does the package strike the
			// ground relative to the point at which it was released?

			// What are the horizontal and vertical components
			// of the velocity of the package just before it hits the ground?

			var xA = new Symbol("xA");
			var xB = new Symbol("xB");

			var yA = new Symbol("yA");
			var yB = new Symbol("yB");

			var vxA = new Symbol("vxA");
			var vxB = new Symbol("vxB");

			var vyA = new Symbol("vyA");
			var vyB = new Symbol("vyB");

			var tAB = new Symbol("tAB");

			var ax = new Symbol("ax");
			var ay = new Symbol("ay");

			var eqs = and(vxB == vxA + ax*tAB,
						  vyB == vyA + ay*tAB,
						  xB == xA + vxA*tAB + ax*(tAB ^ 2)/2,
						  yB == yA + vyA*tAB + ay*(tAB ^ 2)/2,
						  vxA != 0,
						  ay != 0);

			var vals = new List<Equation>
				{
					xA == 0,
					yA == 100,
					vxA == 40,
					vyA == 0,
					yB == 0,
					ax == 0,
					ay == -9.8,
					pi == Math.PI
				};

			var zeros = vals.Where(eq => eq.b == 0).ToList();

			DoubleFloat.Tolerance = 0.00001;

			Assert.AreEqual(or(xB == 180.70158058105022, xB == -180.70158058105022),
							eqs.EliminateVariables(vxB, vyB, tAB)
							   .IsolateVariable(xB)
							   .LogicalExpand().SimplifyEquation()
							   .CheckVariable(ay)
							   .CheckVariable(vxA).SimplifyLogical()
							   .Substitute(ax == 0)
							   .Substitute(zeros)
							   .Substitute(vals));

			Assert.AreEqual(or(vyB == 44.271887242357309, vyB == -44.271887242357309),
							eqs.EliminateVariables(vxB, xB, tAB)
							   .IsolateVariable(vyB)
							   .LogicalExpand().SimplifyEquation()
							   .CheckVariable(ay)
							   .Substitute(zeros)
							   .Substitute(vals));

			DoubleFloat.Tolerance = null;
		}
		[TestMethod]
		public void Example_4_7()
		{
			var xA = new Symbol("xA");
			var yA = new Symbol("yA");
			var xB = new Symbol("xB");
			var yB = new Symbol("yB");
			var vxA = new Symbol("vxA");
			var vyA = new Symbol("vyA");
			var vxB = new Symbol("vxB");
			var vyB = new Symbol("vyB");
			var tAB = new Symbol("tAB");
			var ax = new Symbol("ax");
			var ay = new Symbol("ay");
			var th = new Symbol("th");
			var d = new Symbol("d");

			var eqs = and(cos(th) == (xB - xA)/d,
						  sin(th) == (yA - yB)/d,
						  vxB == vxA + ax*tAB,
						  vyB == vyA + ay*tAB,
						  xB == xA + vxA*tAB + ax*(tAB ^ 2)/2,
						  yB == yA + vyA*tAB + ay*(tAB ^ 2)/2,
						  yB != 0,
						  ay != 0);

			var vals = new List<Equation>()
				{
					xA == 0,
					yA == 0,
					vxA == 25,
					vyA == 0,
					ax == 0,
					ay == -9.8,
					th == (35).ToRadians(),
					pi == Math.PI
				};

			var zeros = vals.Where(eq => eq.b == 0).ToList();

			DoubleFloat.Tolerance = 0.00001;

			Assert.AreEqual(or(and(xB == 89.312185996136435, xB != 0), and(xB == 7.0805039835788038E-15, xB != 0)),
							eqs.Substitute(zeros)
							   .EliminateVariables(vxB, vyB, d, yB, tAB)
							   .IsolateVariable(xB)
							   .LogicalExpand()
							   .CheckVariable(ay)
							   .SimplifyEquation()
							   .Substitute(vals)
							   .SimplifyEquation());

			Assert.AreEqual(and(yB == -62.537065888482395, yB != 0),
							eqs.Substitute(zeros)
							   .EliminateVariables(vxB, vyB, d, xB, tAB)
							   .IsolateVariable(yB)
							   .LogicalExpand()
							   .CheckVariable(yB)
							   .Substitute(vals));

			Assert.AreEqual(and(tAB == 3.5724874398454571, tAB != 0),
							eqs.Substitute(zeros)
							   .EliminateVariables(vxB, vyB, d, xB, yB)
							   .IsolateVariable(tAB)
							   .LogicalExpand()
							   .CheckVariable(ay)
							   .SimplifyEquation()
							   .SimplifyLogical()
							   .Substitute(vals)
							   .CheckVariable(tAB)
							   .SimplifyEquation());

			Assert.AreEqual(and(vyB == -35.010376910485483, vyB != 0),
							eqs.Substitute(zeros)
							   .EliminateVariables(vxB, d, tAB, xB, yB)
							   .IsolateVariable(vyB)
							   .LogicalExpand()
							   .CheckVariable(ay)
							   .SimplifyEquation()
							   .CheckVariable(ay)
							   .Substitute(vals)
							   .CheckVariable(vyB)
							   .SimplifyEquation()
							   .CheckVariable(vyB));

			DoubleFloat.Tolerance = null;
		}
		[TestMethod]
		public void Example_4_9()
		{
			// In a local bar, a customer slides an empty beer mug
			// down the counter for a refill. The bartender is momentarily 
			// distracted and does not see the mug, which slides
			// off the counter and strikes the floor 1.40 m from the
			// base of the counter. If the height of the counter is 
			// 0.860 m, (a) with what velocity did the mug leave the
			// counter and (b) what was the direction of the mug’s 
			// velocity just before it hit the floor?

			var xA = new Symbol("xA");
			var yA = new Symbol("yA");
			var xB = new Symbol("xB");
			var yB = new Symbol("yB");
			var vxA = new Symbol("vxA");
			var vyA = new Symbol("vyA");
			var vxB = new Symbol("vxB");
			var vyB = new Symbol("vyB");
			var tAB = new Symbol("tAB");
			var ax = new Symbol("ax");
			var ay = new Symbol("ay");
			var thB = new Symbol("thB");

			var eqs = and(vxB == vxA + ax*tAB,
						  vyB == vyA + ay*tAB,
						  tan(thB) == vyB/vxB,
						  xB == xA + vxA*tAB + ax*(tAB ^ 2)/2,
						  yB == yA + vyA*tAB + ay*(tAB ^ 2)/2,
						  xB != 0);

			var vals = new List<Equation>
				{
					xA == 0,
					yA == 0.86,
					/* vxA */ vyA == 0,
					xB == 1.4,
					yB == 0,
					/* vxB vyB vB thB */ /* tAB */ ax == 0,
					ay == -9.8
				};

			var zeros = vals.Where(eq => eq.b == 0).ToList();

			DoubleFloat.Tolerance = 0.00001;

			Assert.AreEqual(or(vxA == -3.3417722634053204, vxA == 3.3417722634053204),
							eqs.Substitute(zeros)
							   .EliminateVariables(thB, vxB, vyB, tAB)
							   .IsolateVariable(vxA)
							   .LogicalExpand()
							   .Substitute(vals));

			Assert.AreEqual(and(0.1020408163265306*tan(thB) != 0, thB == -0.88760488150470185),
							eqs.Substitute(zeros)
							   .EliminateVariables(vxB, vyB, tAB, vxA)
							   .LogicalExpand()
							   .CheckVariable(xB)
							   .SimplifyLogical()
							   .IsolateVariable(thB)
							   .Substitute(vals));

			DoubleFloat.Tolerance = null;
		}
		[TestMethod]
		public void Example_4_11()
		{
			// One strategy in a snowball fight is to throw a first snowball
			// at a high angle over level ground. While your opponent is watching
			// the first one, you throw a second one at a low angle and timed
			// to arrive at your opponent before or at the same time as the first one.

			// Assume both snowballs are thrown with a speed of 25.0 m/s.

			// The first one is thrown at an angle of 70.0° with respect to the horizontal. 

			// (a) At what angle should the second (lowangle) 
			// snowball be thrown if it is to land at the same
			// point as the first?

			// (b) How many seconds later should the second snowball 
			// be thrown if it is to land at the same time as the first?

			var xA = new Symbol("xA");
			var yA = new Symbol("yA");

			var vxA = new Symbol("vxA");
			var vyA = new Symbol("vyA");

			var vA = new Symbol("vA");

			var thA = new Symbol("thA");

			var xB = new Symbol("xB");
			var yB = new Symbol("yB");

			var vxB = new Symbol("vxB");
			var vyB = new Symbol("vyB");

			var tAB = new Symbol("tAB");
			var tAC = new Symbol("tAC");

			var ax = new Symbol("ax");
			var ay = new Symbol("ay");

			var eqs = new And(vxA == vA*cos(thA),
							  vyA == vA*sin(thA),
							  vxB == vxA + ax*tAB,
							  vyB == vyA + ay*tAB,
							  xB == xA + vxA*tAB + ax*(tAB ^ 2)/2,
							  yB == yA + vyA*tAB + ay*(tAB ^ 2)/2);

			DoubleFloat.Tolerance = 0.00001;
			var vals = new List<Equation>
				{
					xA == 0,
					yA == 0,
					/* vxA vyA */ vA == 25.0,
					/* thA == 70.0, */ /* xB == 20.497, */ /* yB */ /* vxB */ vyB == 0,
					/* tAB */ ax == 0,
					ay == -9.8,
					pi == Math.PI
				};

			var zeros = vals.Where(eq => eq.b == 0).ToList();

			// thA = ... || thA = ...

			var sub = eqs.Substitute(zeros);
			var elim = sub.EliminateVariables(yB, vxA, vyA, vxB, tAB);
			var sel = elim.DeepSelect(DeepSelectTests.DoubleAngleFormulaFunc);
			var expr = sel.IsolateVariable(thA);

			//var expr = eqs.Substitute(zeros)
			//              .EliminateVariables(yB, vxA, vyA, vxB, tAB)
			//              .DeepSelect(DoubleAngleFormulaFunc)
			//              .IsolateVariable(thA);

			// th_delta = ...

			var th1 = ((expr as Or).Parameters[0] as Equation).b;
			var th2 = ((expr as Or).Parameters[1] as Equation).b;

			var th_delta = new Symbol("th_delta");

			Assert.AreEqual(th_delta == -0.87266462599716454,
							and(eqs, th_delta == (th1 - th2).AlgebraicExpand())
								.Substitute(zeros)
								.EliminateVariables(yB, vxA, vyA, vxB, tAB)
								.DeepSelect(DeepSelectTests.DoubleAngleFormulaFunc)
								.EliminateVariable(xB)
								.Substitute(thA == 70.ToRadians())
								.Substitute(pi == Math.PI));
			// tAB = ...

			var tAB_eq = eqs.Substitute(zeros)
							.EliminateVariables(yB, vxA, vyA, vxB, xB)
							.IsolateVariable(tAB);

			Assert.AreEqual(or(tAC == 1.7450007312534115, tAC == 4.794350106050552),
							and(or(thA == 20.ToRadians(), thA == 70.ToRadians()), tAB_eq, tAC == tAB*2)
								.LogicalExpand()
								.EliminateVariables(thA, tAB)
								.Substitute(vals));

			DoubleFloat.Tolerance = null;
		}
		[TestMethod]
		public void Example_4_13()
		{
			// An artillery shell is fired with an initial velocity of 
			// 300 m/s at 55.0° above the horizontal. It explodes on a
			// mountainside 42.0 s after firing. What are the x and y
			// coordinates of the shell where it explodes, relative to its
			// firing point?

			var xA = new Symbol("xA");
			var yA = new Symbol("yA");

			var vxA = new Symbol("vxA");
			var vyA = new Symbol("vyA");

			var vA = new Symbol("vA");
			var thA = new Symbol("thA");

			var xB = new Symbol("xB");
			var yB = new Symbol("yB");

			var vxB = new Symbol("vxB");
			var vyB = new Symbol("vyB");

			var tAB = new Symbol("tAB");

			var ax = new Symbol("ax");
			var ay = new Symbol("ay");

			var eqs = and(vxA == vA*cos(thA),
						  vyA == vA*sin(thA),
						  vxB == vxA + ax*tAB,
						  vyB == vyA + ay*tAB,
						  xB == xA + vxA*tAB + ax*(tAB ^ 2)/2,
						  yB == yA + vyA*tAB + ay*(tAB ^ 2)/2);

			DoubleFloat.Tolerance = 0.00001;

			var vals = new List<Equation>
				{
					xA == 0,
					yA == 0,
					/* vxA vyA */ vA == 300.0,
					thA == (55).ToRadians(),
					/* xB yB vxB vyB */ tAB == 42,
					ax == 0,
					ay == -9.8,
					pi == Math.PI
				};

			var zeros = vals.Where(eq => eq.b == 0).ToList();

			Assert.AreEqual(and(vxB == 172.07293090531385, vyB == -165.85438671330249, xB == 7227.0630980231817, yB == 1677.7157580412968),
							eqs.Substitute(zeros)
							   .EliminateVariable(vxA)
							   .EliminateVariable(vyA)
							   .Substitute(vals));

			DoubleFloat.Tolerance = null;
		}
		[TestMethod]
		public void Example_4_15()
		{
			// A projectile is fired in such a way that its horizontal
			// range is equal to three times its maximum height.
			//
			// What is the angle of projection? 
			// Give your answer to three significant figures.

			var xA = new Symbol("xA");
			var yA = new Symbol("yA");
			var vxA = new Symbol("vxA");
			var vyA = new Symbol("vyA");
			var vA = new Symbol("vA");
			var thA = new Symbol("thA");
			var xB = new Symbol("xB");
			var yB = new Symbol("yB");
			var vxB = new Symbol("vxB");
			var vyB = new Symbol("vyB");
			var xC = new Symbol("xC");
			var yC = new Symbol("yC");
			var vxC = new Symbol("vxC");
			var vyC = new Symbol("vyC");
			var tAB = new Symbol("tAB");
			var tBC = new Symbol("tBC");
			var ax = new Symbol("ax");
			var ay = new Symbol("ay");

			var eqs = and(xC - xA == 3*yB,
						  tAB == tBC,
						  vxA == vA*cos(thA),
						  vyA == vA*sin(thA),
						  vxB == vxA + ax*tAB,
						  vyB == vyA + ay*tAB,
						  xB == xA + vxA*tAB + ax*(tAB ^ 2)/2,
						  yB == yA + vyA*tAB + ay*(tAB ^ 2)/2,
						  vxC == vxB + ax*tBC,
						  vyC == vyB + ay*tBC,
						  xC == xB + vxB*tBC + ax*(tBC ^ 2)/2,
						  yC == yB + vyB*tBC + ay*(tBC ^ 2)/2);

			DoubleFloat.Tolerance = 0.00001;

			var vals = new List<Equation>
				{
					xA == 0,
					yA == 0,
					/* vxA vyA vA thA */ /* xB yB vxB */ vyB == 0,
					/* tAB tBC */ 
					/* xC */ yC == 0,
					ax == 0,
					ay == -9.8,
					pi == Math.PI
				};

			var zeros = vals.Where(eq => eq.b == 0).ToList();

			Assert.AreEqual(thA == new Atan(new Integer(4)/3), eqs.Substitute(zeros)
																  .EliminateVariables(xC, tAB, vxA, vyA, vxB, xB, yB, vxC, vyC, tBC)
																  .IsolateVariable(thA));

			DoubleFloat.Tolerance = null;
		}
		[TestMethod]
		public void Example_4_17()
		{
			// A cannon with a muzzle speed of 1000 m/s is used to
			// start an avalanche on a mountain slope. The target is 
			// 2000 m from the cannon horizontally and 800 m above
			// the cannon.
			//
			// At what angle, above the horizontal, should the cannon be fired?

			var xA = new Symbol("xA");
			var yA = new Symbol("yA");
			var vxA = new Symbol("vxA");
			var vyA = new Symbol("vyA");
			var vA = new Symbol("vA");
			var thA = new Symbol("thA");
			var xB = new Symbol("xB");
			var yB = new Symbol("yB");
			var vxB = new Symbol("vxB");
			var vyB = new Symbol("vyB");
			var tAB = new Symbol("tAB");
			var ax = new Symbol("ax");
			var ay = new Symbol("ay");
			var phi = new Symbol("phi");

			var eqs = and(vxA == vA*cos(thA),
						  vyA == vA*sin(thA),
						  vxB == vxA + ax*tAB,
						  vyB == vyA + ay*tAB,
						  xB == xA + vxA*tAB + ax*(tAB ^ 2)/2,
						  yB == yA + vyA*tAB + ay*(tAB ^ 2)/2);

			DoubleFloat.Tolerance = 0.00001;

			var vals = new List<Equation>
				{
					xA == 0,
					yA == 0,
					/* vxA vyA */ vA == 1000,
					/* thA */ 
					xB == 2000,
					yB == 800.0,
					/* vxB vyB */ 
					/* tAB */ ax == 0,
					ay == -9.8,
					pi == Math.PI
				};

			var zeros = vals.Where(eq => eq.b == 0).ToList();

			Assert.AreEqual(or(thA == 0.39034573609628065, thA == -1.5806356857788124),
							eqs.Substitute(zeros)
							   .EliminateVariables(vxA, vyA, vxB, vyB, tAB)
							   .MultiplyBothSidesBy(cos(thA) ^ 2).AlgebraicExpand()
							   .Substitute(cos(thA) ^ 2, (1 + cos(2*thA))/2)
							   .DeepSelect(DeepSelectTests.DoubleAngleFormulaFunc).AlgebraicExpand()
							   .AddToBothSides(-sin(2*thA)*xB/2)
							   .AddToBothSides(-yB/2)
							   .MultiplyBothSidesBy(2/xB).AlgebraicExpand()

								// yB / xB = tan(phi)
								// yB / xB = sin(phi) / cos(phi)

								// phi = atan(yB / xB)
							   .Substitute(cos(2*thA)*yB/xB, cos(2*thA)*sin(phi)/cos(phi))
							   .MultiplyBothSidesBy(cos(phi)).AlgebraicExpand()
							   .DeepSelect(DeepSelectTests.SumDifferenceFormulaFunc)
							   .IsolateVariable(thA)
							   .Substitute(phi, new Atan(yB/xB).Simplify())
							   .Substitute(vals));

			DoubleFloat.Tolerance = null;
		}
		[TestMethod]
		public void Example_4_19()
		{
			// A placekicker must kick a football from a point 36.0 m
			// (about 40 yards) from the goal, and half the crowd
			// hopes the ball will clear the crossbar, which is 3.05 m
			// high. When kicked, the ball leaves the ground with a
			// speed of 20.0 m/s at an angle of 53.0° to the horizontal.
			//
			// (a) By how much does the ball clear or fall short of
			//     clearing the crossbar ?
			//
			// (b) Does the ball approach the crossbar while still 
			//     rising or while falling ?

			var xA = new Symbol("xA");
			var yA = new Symbol("yA");
			var vxA = new Symbol("vxA");
			var vyA = new Symbol("vyA");
			var vA = new Symbol("vA");
			var thA = new Symbol("thA");
			var xB = new Symbol("xB");
			var yB = new Symbol("yB");
			var vxB = new Symbol("vxB");
			var vyB = new Symbol("vyB");
			var tAB = new Symbol("tAB");
			var ax = new Symbol("ax");
			var ay = new Symbol("ay");
			var cleared_by = new Symbol("cleared_by");
			var goal_height = new Symbol("goal_height");

			var eqs = and(vxA == vA*cos(thA),
						  vyA == vA*sin(thA),
						  vxB == vxA + ax*tAB,
						  vyB == vyA + ay*tAB,
						  xB == xA + vxA*tAB + ax*(tAB ^ 2)/2,
						  yB == yA + vyA*tAB + ay*(tAB ^ 2)/2,
						  cleared_by == yB - goal_height);

			DoubleFloat.Tolerance = 0.00001;

			var vals = new List<Equation>
				{
					xA == 0,
					yA == 0,
					/* vxA vyA */ vA == 20,
					thA == (53).ToRadians(),
					xB == 36,
					/* yB */ /* vxB vyB */ 
					/* tAB */ ax == 0,
					ay == -9.8,
					pi == Math.PI,
					goal_height == 3.05
				};

			var zeros = vals.Where(eq => eq.b == 0).ToList();

			Assert.AreEqual(cleared_by == 0.88921618776713007,
							eqs.Substitute(zeros)
							   .EliminateVariables(vxA, vyA, vxB, vyB, tAB, yB)
							   .Substitute(vals));

			Assert.AreEqual(vyB == -13.338621888454744,
							eqs.Substitute(zeros)
							   .EliminateVariables(cleared_by, vxA, vyA, vxB, tAB, yB)
							   .IsolateVariable(vyB)
							   .Substitute(vals));

			DoubleFloat.Tolerance = null;
		}
		[TestMethod]
		public void Example_4_21()
		{
			// A firefighter a distance d from a burning building directs 
			// a stream of water from a fire hose at angle θi above
			// the horizontal as in Figure P4.20.If the initial speed of
			// the stream is vi, at what height h does the water strike
			// the building?

			var xA = new Symbol("xA");
			var yA = new Symbol("yA");
			var vxA = new Symbol("vxA");
			var vyA = new Symbol("vyA");
			var vA = new Symbol("vA");
			var thA = new Symbol("thA");
			var xB = new Symbol("xB");
			var yB = new Symbol("yB");
			var vxB = new Symbol("vxB");
			var vyB = new Symbol("vyB");
			var tAB = new Symbol("tAB");
			var ax = new Symbol("ax");
			var ay = new Symbol("ay");
			var d = new Symbol("d");
			var thi = new Symbol("thi");
			var vi = new Symbol("vi");
			var h = new Symbol("h");

			var eqs = and(vxA == vA*cos(thA),
						  vyA == vA*sin(thA),
						  vxB == vxA + ax*tAB,
						  vyB == vyA + ay*tAB,
						  xB == xA + vxA*tAB + ax*(tAB ^ 2)/2,
						  yB == yA + vyA*tAB + ay*(tAB ^ 2)/2);

			DoubleFloat.Tolerance = 0.00001;

			var vals = new List<Equation>
				{
					xA == 0,
					yA == 0,
					/* vxA vyA */ vA == vi,
					thA == thi,
					xB == d,
					yB == h,
					/* vxB vyB */ 
					/* tAB */ ax == 0,
					ay == -9.8,
					pi == Math.PI
				};

			var zeros = vals.Where(eq => eq.b == 0).ToList();

			Assert.AreEqual(h == d*sin(thi)/cos(thi) + ay*(d ^ 2)/(cos(thi) ^ 2)/(vi ^ 2)/2,
							eqs.Substitute(zeros)
							   .EliminateVariables(vxA, vyA, vxB, vyB, tAB)
							   .Substitute(vals.Where(eq => eq.b is Symbol).ToList()));

			DoubleFloat.Tolerance = null;
		}
		[TestMethod]
		public void Example_4_23()
		{
			// A basketball star covers 2.80 m horizontally in a jump to
			// dunk the ball. His motion through space can be modeled as 
			// that of a particle at a point called his center of mass. 
			// His center of mass is at elevation 1.02 m when he leaves 
			// the floor. It reaches a maximum height of 1.85 m above 
			// the floor and is at elevation 0.900 m when he touches down
			// again.

			// Determine:

			// (a) his time of flight (his “hang time”)

			// (b) his horizontal and (c) vertical velocity components at the instant of takeoff

			// (d) his takeoff angle. 

			// (e) For comparison, determine the hang time of a
			// whitetail deer making a jump with center-of-mass elevations
			// y_i = 1.20 m
			// y_max = 2.50 m
			// y_f = 0.700 m

			var xA = new Symbol("xA");
			var yA = new Symbol("yA");
			var vxA = new Symbol("vxA");
			var vyA = new Symbol("vyA");
			var thA = new Symbol("thA");
			var xB = new Symbol("xB");
			var yB = new Symbol("yB");
			var vxB = new Symbol("vxB");
			var vyB = new Symbol("vyB");
			var tAB = new Symbol("tAB");
			var xC = new Symbol("xC");
			var yC = new Symbol("yC");
			var vxC = new Symbol("vxC");
			var vyC = new Symbol("vyC");
			var tBC = new Symbol("tBC");
			var tAC = new Symbol("tAC");
			var ax = new Symbol("ax");
			var ay = new Symbol("ay");

			var eqs = and( //vxA == vA * cos(thA),
						  //vyA == vA * sin(thA),
						  vxB == vxA + ax*tAB,
						  vyB == vyA + ay*tAB,
						  xB == xA + vxA*tAB + ax*(tAB ^ 2)/2,
						  yB == yA + vyA*tAB + ay*(tAB ^ 2)/2,
						  vxC == vxB + ax*tBC,
						  vyC == vyB + ay*tBC,
						  xC == xB + vxB*tBC + ax*(tBC ^ 2)/2,
						  yC == yB + vyB*tBC + ay*(tBC ^ 2)/2,
						  tAC == tAB + tBC,
						  // vyA / vxA == tan(thA),
						  tan(thA) == vyA/vxA,
						  ay != 0);

			DoubleFloat.Tolerance = 0.00001;

			var vals = new List<Equation>
				{
					xA == 0,
					yA == 1.02,
					/* vxA vyA vA thA */
					/* xB */    yB == 1.85,
					/* vxB            */ vyB == 0,
					xC == 2.80,
					yC == 0.9,
					/* vxC vyC        */

					/* tAB tBC */ ax == 0,
					ay == -9.8,
					pi == Math.PI
				};

			var zeros = vals.Where(eq => eq.b == 0).ToList();

			Assert.AreEqual(or(tAC == 0.028747849043843032, tAC == -0.85188272280886768, tAC == 0.85188272280886768, tAC == -0.028747849043843032),
							eqs.Substitute(zeros)
							   .EliminateVariables(thA, vxB, xB, vxC, vyC, vxA, vyA, tAB)
							   .CheckVariable(ay).SimplifyEquation().SimplifyLogical()
							   .EliminateVariable(tBC)
							   .LogicalExpand().SimplifyEquation().CheckVariable(ay).SimplifyLogical()
							   .Substitute(vals));

			Assert.AreEqual(or(vxA == 97.398591307814215, vxA == -3.286837407346058, vxA == 3.286837407346058, vxA == -97.398591307814215),
							eqs.Substitute(zeros)
							   .EliminateVariables(thA, vxB, vxC, xB)
							   .IsolateVariable(vxA)
							   .EliminateVariables(tAC, vyC, tAB, vyA)
							   .SimplifyEquation().CheckVariable(ay)
							   .EliminateVariable(tBC)
							   .LogicalExpand().SimplifyEquation().CheckVariable(ay).SimplifyLogical()
							   .Substitute(vals));

			Assert.AreEqual(or(vyA == -4.0333608814486217, vyA == 4.0333608814486217),
							eqs.Substitute(zeros)
							   .EliminateVariables(thA, vxA, vxC, vyC, vxB, xB, tAB, tAC, tBC)
							   .SimplifyEquation().CheckVariable(ay).SimplifyLogical()
							   .IsolateVariable(vyA)
							   .LogicalExpand().SimplifyEquation().CheckVariable(ay)
							   .Substitute(vals));

			Assert.AreEqual(or(thA == 0.88702813023277882, thA == -0.041387227947930878, thA == -0.041387227947930878, thA == 0.88702813023277882),
							eqs.Substitute(zeros)
							   .EliminateVariables(vxA, vyA, vxB, xB, vxC, tBC, tAB, vyC, tAC)
							   .LogicalExpand()
							   .SimplifyEquation()
							   .SimplifyLogical()
							   .CheckVariable(ay)
							   .IsolateVariable(thA)
							   .Substitute(vals));

			DoubleFloat.Tolerance = null;
		}
		[TestMethod]
		public void Example_5_1()
		{
			// A hockey puck having a mass of 0.30 kg slides on the horizontal, 
			// frictionless surface of an ice rink. Two forces act on
			// the puck, as shown in Figure 5.5.The force F1 has a magnitude 
			// of 5.0 N, and the force F2 has a magnitude of 8.0 N.

			// Determine both the magnitude and the direction of the puck’s acceleration.

			// Determine the components of a third force that,
			// when applied to the puck, causes it to have zero acceleration.

			var F = new Symbol("F");
			var th = new Symbol("th");
			var Fx = new Symbol("Fx");
			var Fy = new Symbol("Fy");
			var F1 = new Symbol("F1");
			var th1 = new Symbol("th1");
			var F1x = new Symbol("F1x");
			var F1y = new Symbol("F1y");
			var F2 = new Symbol("F2");
			var th2 = new Symbol("th2");
			var F2x = new Symbol("F2x");
			var F2y = new Symbol("F2y");
			var F3 = new Symbol("F3");
			var th3 = new Symbol("th3");
			var F3x = new Symbol("F3x");
			var F3y = new Symbol("F3y");
			var ax = new Symbol("ax");
			var ay = new Symbol("ay");
			var m = new Symbol("m");

			var eqs = and(Fx == F*cos(th),
						  Fy == F*sin(th),
						  Fx == ax*m,
						  Fy == ay*m,
						  Fx == F1x + F2x + F3x,
						  Fy == F1y + F2y + F3y,
						  F1x == F1*cos(th1), F1y == F1*sin(th1),
						  F2x == F2*cos(th2), F2y == F2*sin(th2),
						  F3x == F3*cos(th3), F3y == F3*sin(th3),
						  a == sqrt((ax ^ 2) + (ay ^ 2)));

			DoubleFloat.Tolerance = 0.00001;

			var vals1 = new List<Equation>
				{
					m == 0.3,
					F1 == 5.0,
					th1 == (-20).ToRadians(),
					F2 == 8.0,
					th2 == 60.ToRadians(),
					F3 == 0,
					pi == Math.PI
				};

			var zeros1 = vals1.Where(eq => eq.b == 0);

			// a 
			Assert.AreEqual(a == 33.811874017759315,
							eqs.Substitute(zeros1)
							   .EliminateVariables(ax, ay, Fx, Fy, F, F1x, F1y, F2x, F2y, F3x, F3y)
							   .DeepSelect(DeepSelectTests.SinCosToTanFunc)
							   .EliminateVariable(th)
							   .Substitute(vals1)
							   .Substitute(3, 3.0));

			// th
			Assert.AreEqual(th == 0.54033704850428876,
							eqs.Substitute(zeros1)
							   .EliminateVariables(a, F, Fx, Fy, ax, ay, F1x, F1y, F2x, F2y, F3x, F3y)
							   .DeepSelect(DeepSelectTests.SinCosToTanFunc)
							   .IsolateVariable(th)
							   .Substitute(vals1)
							   .Substitute(3, 3.0));

			var vals2 = new List<Equation>
				{
					m == 0.3,
					F1 == 5.0,
					th1 == (-20).ToRadians(),
					F2 == 8.0,
					th2 == 60.ToRadians(),
					ax == 0,
					ay == 0,
					pi == Math.PI
				};

			var zeros2 = vals2.Where(eq => eq.b == 0);

			// F3x
			Assert.AreEqual(F3x == -8.6984631039295444,
							eqs.Substitute(zeros2)
							   .EliminateVariables(F3, th3, F3y, F1x, F2x, Fx, F, Fy, F1y, F2y, a)
							   .IsolateVariable(F3x)
							   .Substitute(vals2));


			// F3y
			Assert.AreEqual(F3y == -5.2181025136471657,
							eqs.Substitute(zeros2)
							   .EliminateVariables(F3, th3, F3x, F1x, F2x, Fx, F, Fy, F1y, F2y, a)
							   .IsolateVariable(F3y)
							   .Substitute(vals2)
								// .DispLong()
							   .Substitute(3, 3.0));
		}
		[TestMethod]
		public void Example_5_4()
		{
			// A traffic light weighing 125 N hangs from a cable tied to two
			// other cables fastened to a support. The upper cables make
			// angles of 37.0° and 53.0° with the horizontal. Find the tension
			// in the three cables.

			var F = new Symbol("F"); // total force magnitude
			var th = new Symbol("th"); // total force direction
			var Fx = new Symbol("Fx"); // total force x-component
			var Fy = new Symbol("Fy"); // total force y-component
			var F1 = new Symbol("F1");
			var th1 = new Symbol("th1");
			var F1x = new Symbol("F1x");
			var F1y = new Symbol("F1y");
			var F2 = new Symbol("F2");
			var th2 = new Symbol("th2");
			var F2x = new Symbol("F2x");
			var F2y = new Symbol("F2y");
			var F3 = new Symbol("F3");
			var th3 = new Symbol("th3");
			var F3x = new Symbol("F3x");
			var F3y = new Symbol("F3y");
			var ax = new Symbol("ax");
			var ay = new Symbol("ay");
			var m = new Symbol("m");

			var eqs = and(Fx == F*cos(th),
						  Fy == F*sin(th),
						  Fx == ax*m,
						  Fy == ay*m,
						  Fx == F1x + F2x + F3x,
						  Fy == F1y + F2y + F3y,
						  F1x == F1*cos(th1), F1y == F1*sin(th1),
						  F2x == F2*cos(th2), F2y == F2*sin(th2),
						  F3x == F3*cos(th3), F3y == F3*sin(th3),
						  a == sqrt((ax ^ 2) + (ay ^ 2)));

			DoubleFloat.Tolerance = 0.00001;
			var vals = new List<Equation>
				{
					// m 
					/* F1 */    th1 == (180 - 37).ToRadians(), // F1x F1y
					/* F2 */    th2 == (53).ToRadians(), // F2x F2y
					F3 == 125,
					th3 == (270).ToRadians(), // F3x F3y
					ax == 0,
					ay == 0,
					pi == Math.PI
				};

			var zeros = vals.Where(eq => eq.b == 0).ToList();

			// F1
			Assert.AreEqual(F1 == 75.226877894006023,
							eqs.Substitute(zeros)
							   .EliminateVariables(Fx, Fy, F, F1x, F1y, F2x, F2y, F2, F3x, F3y, a)
							   .IsolateVariable(F1)
							   .Substitute(vals));

			// F2
			Assert.AreEqual(F2 == 99.829438755911582,
							eqs.Substitute(zeros)
							   .EliminateVariables(Fx, Fy, F, F1x, F1y, F2x, F2y, F1, F3x, F3y, a)
							   .IsolateVariable(F2)
							   .Substitute(vals));
		}
		[TestMethod]
		public void Example_5_6()
		{
			// A crate of mass m is placed on a frictionless inclined plane of
			// angle θ. (a) Determine the acceleration of the crate after it is
			// released.

			// (b) Suppose the crate is released from rest at the top of
			// the incline, and the distance from the front edge of the crate
			// to the bottom is d. How long does it take the front edge to
			// reach the bottom, and what is its speed just as it gets there?

			var F = new Symbol("F"); // total force magnitude
			var th = new Symbol("th"); // total force direction
			var Fx = new Symbol("Fx"); // total force x-component
			var Fy = new Symbol("Fy"); // total force y-component
			var F1 = new Symbol("F1");
			var th1 = new Symbol("th1");
			var F1x = new Symbol("F1x");
			var F1y = new Symbol("F1y");
			var F2 = new Symbol("F2");
			var th2 = new Symbol("th2");
			var F2x = new Symbol("F2x");
			var F2y = new Symbol("F2y");
			//var F3 = new Symbol("F3");
			//var th3 = new Symbol("th3");
			//var F3x = new Symbol("F3x");
			//var F3y = new Symbol("F3y");
			var ax = new Symbol("ax");
			var ay = new Symbol("ay");
			var m = new Symbol("m");
			var n = new Symbol("n");
			var g = new Symbol("g");
			var incline = new Symbol("incline");
			var xA = new Symbol("xA");
			var vxA = new Symbol("vxA");
			var xB = new Symbol("xB");
			var vxB = new Symbol("vxB");
			var tAB = new Symbol("tAB");
			var d = new Symbol("d");

			var eqs = and(Fx == F*cos(th),
						  Fy == F*sin(th),
						  Fx == ax*m,
						  Fy == ay*m,
						  Fx == F1x + F2x, //+ F3x,
						  Fy == F1y + F2y, //+ F3y,
						  F1x == F1*cos(th1), F1y == F1*sin(th1),
						  F2x == F2*cos(th2), F2y == F2*sin(th2),
						  //F3x == F3 * cos(th3), F3y == F3 * sin(th3),
						  a == sqrt((ax ^ 2) + (ay ^ 2)),
						  xB == xA + vxA*tAB + ax*(tAB ^ 2)/2,
						  vxB == vxA + ax*tAB,
						  d != 0);

			DoubleFloat.Tolerance = 0.00001;

			var vals = new List<Equation>
				{
					// m
					F1 == n,
					th1 == 90*pi/180, // F1x F1y
					F2 == m*g,
					th2 == 270*pi/180 + incline, // F2x F2y
					//F3 == 125,    th3 == (270).ToRadians(),        // F3x F3y
					/* ax */  ay == 0,
					// Pi == Math.PI
					xA == 0,
					xB == d,
					vxA == 0
				};

			var zeros = vals.Where(eq => eq.b == 0).ToList();

			// ax
			Assert.AreEqual(and(ax == g*sin(incline), d != 0),
							eqs.Substitute(zeros)
							   .EliminateVariables(a, F)
							   .DeepSelect(DeepSelectTests.SinCosToTanFunc)
							   .EliminateVariables(th, Fx, F1x, F2x, Fy, F1y, F2y, vxB, xB)
							   .Substitute(vals)
							   .EliminateVariable(n)
							   .IsolateVariable(ax));

			// tAB
			Assert.AreEqual(or(and(tAB == -sqrt(2*d*g*sin(incline))/sin(incline)/g, -g*sin(incline)/2 != 0, d != 0),
							   and(tAB == sqrt(2*d*g*sin(incline))/sin(incline)/g, -g*sin(incline)/2 != 0, d != 0)),
							eqs.Substitute(zeros)
							   .EliminateVariables(a, F)
							   .DeepSelect(DeepSelectTests.SinCosToTanFunc)
							   .EliminateVariables(th, Fx, F1x, F2x, Fy, F1y, F2y, ax, vxB)
							   .Substitute(vals)
							   .EliminateVariable(n)
							   .IsolateVariable(tAB).LogicalExpand().CheckVariable(d));

			// vxB
			Assert.AreEqual(or(and(-g*sin(incline)/2 != 0, vxB == -sqrt(2*d*g*sin(incline)), d != 0),
							   and(-g*sin(incline)/2 != 0, vxB == sqrt(2*d*g*sin(incline)), d != 0)),
							eqs.Substitute(zeros)
							   .EliminateVariables(a, F)
							   .DeepSelect(DeepSelectTests.SinCosToTanFunc)
							   .EliminateVariables(th, Fx, F1x, F2x, Fy, F1y, F2y, ax, tAB)
							   .Substitute(vals)
							   .CheckVariable(d)
							   .EliminateVariable(n));
		}
		[TestMethod]
		public void Example_5_9()
		{
			// When two objects of unequal mass are hung vertically over a
			// frictionless pulley of negligible mass, as shown in Figure
			// 5.15a, the arrangement is called an Atwood machine. The device 
			// is sometimes used in the laboratory to measure the freefall
			// acceleration.
			//
			// Determine the magnitude of the acceleration of the two 
			// objects and the tension in the lightweight cord.

			var F_m1 = new Symbol("F_m1"); // total force on mass 1
			var F_m2 = new Symbol("F_m2"); // total force on mass 2
			var F1_m1 = new Symbol("F1_m1"); // force 1 on mass 1
			var F2_m1 = new Symbol("F2_m1"); // force 2 on mass 1
			var F1_m2 = new Symbol("F1_m2"); // force 1 on mass 2
			var F2_m2 = new Symbol("F2_m2"); // force 2 on mass 2
			var m1 = new Symbol("m1");
			var m2 = new Symbol("m2");
			var T = new Symbol("T");
			var g = new Symbol("g");

			var eqs = and(F_m1 == F1_m1 - F2_m1,
						  F_m2 == F2_m2 - F1_m2,
						  F_m1 == m1*a,
						  F_m2 == m2*a,
						  F1_m1 == T,
						  F2_m1 == m1*g,
						  F1_m2 == T,
						  F2_m2 == m2*g);

			DoubleFloat.Tolerance = 0.00001;

			var vals = new List<Equation>
				{
					m1 == 2.0,
					m2 == 4.0,
					g == 9.8
				};

			// a
			Assert.AreEqual(a == 3.2666666666666666, eqs.EliminateVariables(F_m1, F_m2, F2_m1, F2_m2, F1_m1, F1_m2, T)
														.IsolateVariable(a)
														.Substitute(vals));

			// T
			Assert.AreEqual(T == 26.133333333333333, eqs.EliminateVariables(F_m1, F_m2, F2_m1, F2_m2, F1_m1, F1_m2, a)
														.IsolateVariable(T)
														.Substitute(vals));
		}
		[TestMethod]
		public void Example_5_10()
		{
			// A ball of mass m1 and a block of mass m2 are attached by a
			// lightweight cord that passes over a frictionless pulley of 
			// negligible mass, as shown in Figure 5.16a. The block lies 
			// on a frictionless incline of angle th. Find the magnitude 
			// of the acceleration of the two objects and the tension in the cord.

			////////////////////////////////////////////////////////////////////////////////

			var F1_m1 = new Symbol("F1_m1"); // force 1 on mass 1
			var F2_m1 = new Symbol("F2_m1"); // force 2 on mass 1
			var F3_m1 = new Symbol("F3_m1"); // force 3 on mass 1
			var th1_m1 = new Symbol("th1_m1"); // direction of force 1 on mass 1
			var th2_m1 = new Symbol("th2_m1"); // direction of force 2 on mass 1
			var th3_m1 = new Symbol("th3_m1"); // direction of force 3 on mass 1
			var F1x_m1 = new Symbol("F1x_m1"); // x-component of force 1 on mass 1
			var F2x_m1 = new Symbol("F2x_m1"); // x-component of force 2 on mass 1
			var F3x_m1 = new Symbol("F3x_m1"); // x-component of force 3 on mass 1
			var F1y_m1 = new Symbol("F1y_m1"); // y-component of force 1 on mass 1
			var F2y_m1 = new Symbol("F2y_m1"); // y-component of force 2 on mass 1
			var F3y_m1 = new Symbol("F3y_m1"); // y-component of force 3 on mass 1
			var Fx_m1 = new Symbol("Fx_m1"); // x-component of total force on mass 1
			var Fy_m1 = new Symbol("Fy_m1"); // y-component of total force on mass 1
			var ax_m1 = new Symbol("ax_m1"); // x-component of acceleration of mass 1
			var ay_m1 = new Symbol("ay_m1"); // y-component of acceleration of mass 1
			var m1 = new Symbol("m1");

			////////////////////////////////////////////////////////////////////////////////

			var F1_m2 = new Symbol("F1_m2"); // force 1 on mass 2
			var F2_m2 = new Symbol("F2_m2"); // force 2 on mass 2
			var F3_m2 = new Symbol("F3_m2"); // force 3 on mass 2
			var th1_m2 = new Symbol("th1_m2"); // direction of force 1 on mass 2
			var th2_m2 = new Symbol("th2_m2"); // direction of force 2 on mass 2
			var th3_m2 = new Symbol("th3_m2"); // direction of force 3 on mass 2
			var F1x_m2 = new Symbol("F1x_m2"); // x-component of force 1 on mass 2
			var F2x_m2 = new Symbol("F2x_m2"); // x-component of force 2 on mass 2
			var F3x_m2 = new Symbol("F3x_m2"); // x-component of force 3 on mass 2
			var F1y_m2 = new Symbol("F1y_m2"); // y-component of force 1 on mass 2
			var F2y_m2 = new Symbol("F2y_m2"); // y-component of force 2 on mass 2
			var F3y_m2 = new Symbol("F3y_m2"); // y-component of force 3 on mass 2
			var Fx_m2 = new Symbol("Fx_m2"); // x-component of total force on mass 2
			var Fy_m2 = new Symbol("Fy_m2"); // y-component of total force on mass 2
			var ax_m2 = new Symbol("ax_m2"); // x-component of acceleration of mass 2
			var ay_m2 = new Symbol("ay_m2"); // y-component of acceleration of mass 2
			var m2 = new Symbol("m2");

			////////////////////////////////////////////////////////////////////////////////

			var incline = new Symbol("incline");
			var T = new Symbol("T"); // tension in cable
			var g = new Symbol("g"); // gravity
			var n = new Symbol("n"); // normal force on block

			var eqs = and(ax_m2 == ay_m1, // the block moves right as the ball moves up
						  ////////////////////////////////////////////////////////////////////////////////
						  F1x_m1 == F1_m1*cos(th1_m1),
						  F2x_m1 == F2_m1*cos(th2_m1),
						  F3x_m1 == F3_m1*cos(th3_m1),
						  F1y_m1 == F1_m1*sin(th1_m1),
						  F2y_m1 == F2_m1*sin(th2_m1),
						  F3y_m1 == F3_m1*sin(th3_m1),
						  Fx_m1 == F1x_m1 + F2x_m1 + F3x_m1,
						  Fy_m1 == F1y_m1 + F2y_m1 + F3y_m1,
						  Fx_m1 == m1*ax_m1,
						  Fy_m1 == m1*ay_m1,
						  ////////////////////////////////////////////////////////////////////////////////
						  F1x_m2 == F1_m2*cos(th1_m2),
						  F2x_m2 == F2_m2*cos(th2_m2),
						  F3x_m2 == F3_m2*cos(th3_m2),
						  F1y_m2 == F1_m2*sin(th1_m2),
						  F2y_m2 == F2_m2*sin(th2_m2),
						  F3y_m2 == F3_m2*sin(th3_m2),
						  Fx_m2 == F1x_m2 + F2x_m2 + F3x_m2,
						  Fy_m2 == F1y_m2 + F2y_m2 + F3y_m2,
						  Fx_m2 == m2*ax_m2,
						  Fy_m2 == m2*ay_m2,
						  ////////////////////////////////////////////////////////////////////////////////
						  a == ax_m2);

			DoubleFloat.Tolerance = 0.00001;

			var vals = new List<Equation>
				{
					ax_m1 == 0, // ball  moves vertically
					ay_m2 == 0, // block moves horizontally

					F1_m1 == T,
					F2_m1 == m1*g,
					F3_m1 == 0,
					th1_m1 == 90*pi/180, // force 1 is straight up
					th2_m1 == 270*pi/180, // force 2 is straight down

					F1_m2 == n,
					F2_m2 == T,
					F3_m2 == m2*g,
					th1_m2 == 90*pi/180, // force 1 is straight up
					th2_m2 == 180*pi/180, // force 2 is straight down
					th3_m2 == 270*pi/180 + incline // force 3 direction
				};

			// a
			Assert.AreEqual(a == -4.2234511814572784,
							eqs.Substitute(vals)
							   .EliminateVariables(F1x_m1, F2x_m1, F3x_m1,
												   F1y_m1, F2y_m1, F3y_m1,
												   Fx_m1, Fy_m1,
												   F1x_m2, F2x_m2, F3x_m2,
												   F1y_m2, F2y_m2, F3y_m2,
												   Fx_m2, Fy_m2,
												   ax_m2, n, T, ay_m1)
							   .Substitute(m1 == 10.0)
							   .Substitute(m2 == 5.0)
							   .Substitute(incline == 45*Math.PI/180)
							   .Substitute(g == 9.8));

			// T
			Assert.AreEqual(T == m1*(-g*m2 - g*m2*sin(incline))/(-m1 - m2),
							eqs.Substitute(vals)
							   .EliminateVariables(F1x_m1, F2x_m1, F3x_m1,
												   F1y_m1, F2y_m1, F3y_m1,
												   Fx_m1, Fy_m1,
												   F1x_m2, F2x_m2, F3x_m2,
												   F1y_m2, F2y_m2, F3y_m2,
												   Fx_m2, Fy_m2,
												   ax_m2, n, a, ay_m1)
							   .IsolateVariable(T)
							   .RationalizeExpression());
		}
		[TestMethod]
		public void Example_5_10_UsingObject()
		{
			// A ball of mass m1 and a block of mass m2 are attached by a
			// lightweight cord that passes over a frictionless pulley of 
			// negligible mass, as shown in: 
			//
			//      http://i.imgur.com/XMHM6On.png
			//
			// The block lies on a frictionless incline of angle th.
			//
			// Find the magnitude of the acceleration of the two objects
			// and the tension in the cord.

			var bal = new Obj2("bal");
			var blk = new Obj3("blk");
			var th = new Symbol("th");
			var T = new Symbol("T");                // tension in cable
			var g = new Symbol("g");                // gravity
			var n = new Symbol("n");                // normal force on block
			var a = new Symbol("a");
			var m1 = new Symbol("m1");
			var m2 = new Symbol("m2");

			var eqs = and(blk.ax == bal.ay, // the block moves right as the ball moves up
						  a == blk.ax,
						  bal.Equations(),
						  blk.Equations());

			DoubleFloat.Tolerance = 0.00001;

			var vals = new List<Equation>
				{
					bal.ax == 0,
					bal.m == m1,
					bal.F1 == T,
					bal.th1 == (90).ToRadians(), // force 1 is straight up
					bal.F2 == m1*g,
					bal.th2 == (270).ToRadians(), // force 2 is straight down
					blk.ay == 0,
					blk.m == m2,
					blk.F1 == n,
					blk.th1 == (90).ToRadians(), // force 1 is straight up
					blk.F2 == T,
					blk.th2 == (180).ToRadians(), // force 2 is straight down
					blk.F3 == m2*g,
					blk.th3 == (270).ToRadians() + th // force 3 direction
				};

			// a
			Assert.AreEqual(a == (g*m1 - g*m2*sin(th))/(-m1 - m2),
							eqs.Substitute(vals)
							   .EliminateVariables(bal.ΣFx, bal.F1x, bal.F2x,
												   bal.ΣFy, bal.F1y, bal.F2y,
												   blk.ΣFx, blk.F1x, blk.F2x, blk.F3x,
												   blk.ΣFy, blk.F1y, blk.F2y, blk.F3y,
												   blk.ax, bal.ay,
												   T, n)
							   .IsolateVariable(a));

			// T
			Assert.AreEqual(T == m1*(-g*m2 - g*m2*sin(th))/(-m1 - m2),
							eqs.Substitute(vals)
							   .EliminateVariables(bal.ΣFx, bal.F1x, bal.F2x,
												   bal.ΣFy, bal.F1y, bal.F2y,
												   blk.ΣFx, blk.F1x, blk.F2x, blk.F3x,
												   blk.ΣFy, blk.F1y, blk.F2y, blk.F3y,
												   blk.ax, bal.ay,
												   a, n)
							   .IsolateVariable(T)
							   .RationalizeExpression());
		}
		[TestMethod]
		public void Example_5_12()
		{
			// The following is a simple method of measuring coefficients of
			// friction: Suppose a block is placed on a rough surface
			// inclined relative to the horizontal, as shown in Figure 5.19. 
			// The incline angle is increased until the block starts to move. 
			// Let us show that by measuring the critical angle θ_c at which this
			// slipping just occurs, we can obtain μs.

			////////////////////////////////////////////////////////////////////////////////

			var F1_m1 = new Symbol("F1_m1"); // force 1 on mass 1
			var F2_m1 = new Symbol("F2_m1"); // force 2 on mass 1
			var F3_m1 = new Symbol("F3_m1"); // force 3 on mass 1
			var th1_m1 = new Symbol("th1_m1"); // direction of force 1 on mass 1
			var th2_m1 = new Symbol("th2_m1"); // direction of force 2 on mass 1
			var th3_m1 = new Symbol("th3_m1"); // direction of force 3 on mass 1
			var F1x_m1 = new Symbol("F1x_m1"); // x-component of force 1 on mass 1
			var F2x_m1 = new Symbol("F2x_m1"); // x-component of force 2 on mass 1
			var F3x_m1 = new Symbol("F3x_m1"); // x-component of force 3 on mass 1
			var F1y_m1 = new Symbol("F1y_m1"); // y-component of force 1 on mass 1
			var F2y_m1 = new Symbol("F2y_m1"); // y-component of force 2 on mass 1
			var F3y_m1 = new Symbol("F3y_m1"); // y-component of force 3 on mass 1
			var Fx_m1 = new Symbol("Fx_m1"); // x-component of total force on mass 1
			var Fy_m1 = new Symbol("Fy_m1"); // y-component of total force on mass 1
			var ax_m1 = new Symbol("ax_m1"); // x-component of acceleration of mass 1
			var ay_m1 = new Symbol("ay_m1"); // y-component of acceleration of mass 1
			var m1 = new Symbol("m1");

			////////////////////////////////////////////////////////////////////////////////

			var incline = new Symbol("incline");
			var f_s = new Symbol("f_s"); // force due to static friction
			var g = new Symbol("g"); // gravity
			var n = new Symbol("n"); // normal force on block
			var mu_s = new Symbol("mu_s"); // coefficient of static friction

			var eqs = and(F1x_m1 == F1_m1*cos(th1_m1),
						  F2x_m1 == F2_m1*cos(th2_m1),
						  F3x_m1 == F3_m1*cos(th3_m1),
						  F1y_m1 == F1_m1*sin(th1_m1),
						  F2y_m1 == F2_m1*sin(th2_m1),
						  F3y_m1 == F3_m1*sin(th3_m1),
						  Fx_m1 == F1x_m1 + F2x_m1 + F3x_m1,
						  Fy_m1 == F1y_m1 + F2y_m1 + F3y_m1,
						  Fx_m1 == m1*ax_m1,
						  Fy_m1 == m1*ay_m1,
						  f_s == mu_s*n);

			DoubleFloat.Tolerance = 0.00001;

			var vals = new List<Equation>
				{
					ax_m1 == 0,
					ay_m1 == 0,
					F1_m1 == n,
					F2_m1 == f_s,
					F3_m1 == m1*g,
					th1_m1 == 90*pi/180, // force 1 is straight up
					th2_m1 == 180*pi/180, // force 2 is straight down
					th3_m1 == 270*pi/180 + incline // force 3 direction 
				};

			// mu_s
			Assert.AreEqual(mu_s == tan(incline),
							eqs.Substitute(vals)
							   .EliminateVariables(F1x_m1, F2x_m1, F3x_m1,
												   F1y_m1, F2y_m1, F3y_m1,
												   Fx_m1, Fy_m1,
												   f_s, n)
							   .IsolateVariable(mu_s)
							   .DeepSelect(DeepSelectTests.SinCosToTanFunc));
		}
		[TestMethod]
		public void Example_5_13()
		{
			// A hockey puck on a frozen pond is given an initial speed of
			// 20.0  m/s. If the puck always remains on the ice and slides
			// 115 m before coming to rest, determine the coefficient of
			// kinetic friction between the puck and ice.

			////////////////////////////////////////////////////////////////////////////////

			var s = new Symbol("s"); // displacement
			var u = new Symbol("u"); // initial velocity
			var v = new Symbol("v"); // final velocity
			var t = new Symbol("t"); // time elapsed
			var F1_m1 = new Symbol("F1_m1"); // force 1 on mass 1
			var F2_m1 = new Symbol("F2_m1"); // force 2 on mass 1
			var F3_m1 = new Symbol("F3_m1"); // force 3 on mass 1
			var th1_m1 = new Symbol("th1_m1"); // direction of force 1 on mass 1
			var th2_m1 = new Symbol("th2_m1"); // direction of force 2 on mass 1
			var th3_m1 = new Symbol("th3_m1"); // direction of force 3 on mass 1
			var F1x_m1 = new Symbol("F1x_m1"); // x-component of force 1 on mass 1
			var F2x_m1 = new Symbol("F2x_m1"); // x-component of force 2 on mass 1
			var F3x_m1 = new Symbol("F3x_m1"); // x-component of force 3 on mass 1
			var F1y_m1 = new Symbol("F1y_m1"); // y-component of force 1 on mass 1
			var F2y_m1 = new Symbol("F2y_m1"); // y-component of force 2 on mass 1
			var F3y_m1 = new Symbol("F3y_m1"); // y-component of force 3 on mass 1
			var Fx_m1 = new Symbol("Fx_m1"); // x-component of total force on mass 1
			var Fy_m1 = new Symbol("Fy_m1"); // y-component of total force on mass 1
			var ax_m1 = new Symbol("ax_m1"); // x-component of acceleration of mass 1
			var ay_m1 = new Symbol("ay_m1"); // y-component of acceleration of mass 1
			var m1 = new Symbol("m1");

			////////////////////////////////////////////////////////////////////////////////

			// var incline = new Symbol("incline");

			var f_s = new Symbol("f_s"); // force due to static friction
			var f_k = new Symbol("f_k"); // force due to kinetic friction
			var g = new Symbol("g"); // gravity
			var n = new Symbol("n"); // normal force on block
			var mu_s = new Symbol("mu_s"); // coefficient of static friction
			var mu_k = new Symbol("mu_k"); // coefficient of kinetic friction

			var eqs = and(a == ax_m1,
						  v == u + a*t,
						  s == (u + v)*t/2,
						  F1x_m1 == F1_m1*cos(th1_m1),
						  F2x_m1 == F2_m1*cos(th2_m1),
						  F3x_m1 == F3_m1*cos(th3_m1),
						  F1y_m1 == F1_m1*sin(th1_m1),
						  F2y_m1 == F2_m1*sin(th2_m1),
						  F3y_m1 == F3_m1*sin(th3_m1),
						  Fx_m1 == F1x_m1 + F2x_m1 + F3x_m1,
						  Fy_m1 == F1y_m1 + F2y_m1 + F3y_m1,
						  Fx_m1 == m1*ax_m1,
						  Fy_m1 == m1*ay_m1,
						  f_s == mu_s*n,
						  f_k == mu_k*n);

			DoubleFloat.Tolerance = 0.00001;

			var symbolic_vals = new List<Equation>
				{
					F1_m1 == n,
					F2_m1 == f_k,
					F3_m1 == m1*g,
					th1_m1 == 90*pi/180, // force 1 is straight up
					th2_m1 == 180*pi/180, // force 2 is left
					th3_m1 == 270*pi/180 // force 3 is straight down
				};

			var vals = new List<Equation>
				{
					//ax_m1 == 0,
					ay_m1 == 0,
					s == 115,
					u == 20,
					v == 0,
					g == 9.8
				};

			var zeros = vals.Where(eq => eq.b == 0).ToList();

			// mu_k
			Assert.AreEqual(mu_k == 0.17746228926353147,
							eqs.Substitute(zeros)
							   .Substitute(symbolic_vals)
							   .EliminateVariables(t,
												   F1x_m1, F2x_m1, F3x_m1,
												   F1y_m1, F2y_m1, F3y_m1,
												   Fx_m1, Fy_m1,
												   f_s, f_k,
												   n,
												   ax_m1, a)
							   .IsolateVariable(mu_k)
							   .Substitute(vals));
		}
		[TestMethod]
		public void Example_5_14()
		{
			// A block of mass m1 on a rough, horizontal surface is connected
			// to a ball of mass m2 by a lightweight cord over a lightweight,
			// frictionless pulley, as shown:
			//
			// http://i.imgur.com/0fHOmGJ.png
			//
			// A force of magnitude F at an angle th with the horizontal is
			// applied to the block as shown. The coefficient of kinetic
			// friction between the block and surface is mu_k.
			// 
			// Determine the magnitude of the acceleration of the two objects.

			////////////////////////////////////////////////////////////////////////////////

			var F1_m1 = new Symbol("F1_m1"); // force 1 on mass 1
			var F2_m1 = new Symbol("F2_m1"); // force 2 on mass 1
			var F3_m1 = new Symbol("F3_m1"); // force 3 on mass 1
			var F4_m1 = new Symbol("F4_m1"); // force 4 on mass 1
			var F5_m1 = new Symbol("F5_m1"); // force 5 on mass 1
			var th1_m1 = new Symbol("th1_m1"); // direction of force 1 on mass 1
			var th2_m1 = new Symbol("th2_m1"); // direction of force 2 on mass 1
			var th3_m1 = new Symbol("th3_m1"); // direction of force 3 on mass 1
			var th4_m1 = new Symbol("th4_m1"); // direction of force 4 on mass 1
			var th5_m1 = new Symbol("th5_m1"); // direction of force 5 on mass 1
			var F1x_m1 = new Symbol("F1x_m1"); // x-component of force 1 on mass 1
			var F2x_m1 = new Symbol("F2x_m1"); // x-component of force 2 on mass 1
			var F3x_m1 = new Symbol("F3x_m1"); // x-component of force 3 on mass 1
			var F4x_m1 = new Symbol("F4x_m1"); // x-component of force 4 on mass 1
			var F5x_m1 = new Symbol("F5x_m1"); // x-component of force 5 on mass 1
			var F1y_m1 = new Symbol("F1y_m1"); // y-component of force 1 on mass 1
			var F2y_m1 = new Symbol("F2y_m1"); // y-component of force 2 on mass 1
			var F3y_m1 = new Symbol("F3y_m1"); // y-component of force 3 on mass 1
			var F4y_m1 = new Symbol("F4y_m1"); // y-component of force 4 on mass 1
			var F5y_m1 = new Symbol("F5y_m1"); // y-component of force 5 on mass 1
			var Fx_m1 = new Symbol("Fx_m1"); // x-component of total force on mass 1
			var Fy_m1 = new Symbol("Fy_m1"); // y-component of total force on mass 1
			var ax_m1 = new Symbol("ax_m1"); // x-component of acceleration of mass 1
			var ay_m1 = new Symbol("ay_m1"); // y-component of acceleration of mass 1
			var m1 = new Symbol("m1");

			////////////////////////////////////////////////////////////////////////////////

			var F1_m2 = new Symbol("F1_m2"); // force 1 on mass 2
			var F2_m2 = new Symbol("F2_m2"); // force 2 on mass 2
			var th1_m2 = new Symbol("th1_m2"); // direction of force 1 on mass 2
			var th2_m2 = new Symbol("th2_m2"); // direction of force 2 on mass 2
			var F1x_m2 = new Symbol("F1x_m2"); // x-component of force 1 on mass 2
			var F2x_m2 = new Symbol("F2x_m2"); // x-component of force 2 on mass 2
			var F1y_m2 = new Symbol("F1y_m2"); // y-component of force 1 on mass 2
			var F2y_m2 = new Symbol("F2y_m2"); // y-component of force 2 on mass 2
			var Fx_m2 = new Symbol("Fx_m2"); // x-component of total force on mass 2
			var Fy_m2 = new Symbol("Fy_m2"); // y-component of total force on mass 2
			var ax_m2 = new Symbol("ax_m2"); // x-component of acceleration of mass 2
			var ay_m2 = new Symbol("ay_m2"); // y-component of acceleration of mass 2
			var m2 = new Symbol("m2");

			////////////////////////////////////////////////////////////////////////////////

			var F = new Symbol("F"); // force applied at angle on block
			var th = new Symbol("th"); // angle of force applied on block
			var T = new Symbol("T"); // tension in cable
			var g = new Symbol("g"); // gravity
			var n = new Symbol("n"); // normal force on block
			var f_k = new Symbol("f_k"); // force due to kinetic friction
			var mu_k = new Symbol("mu_k"); // coefficient of kinetic friction

			var eqs = and(ax_m1 == ay_m2, // the block moves right as the ball moves up
						  ////////////////////////////////////////////////////////////////////////////////
						  F1x_m1 == F1_m1*cos(th1_m1),
						  F2x_m1 == F2_m1*cos(th2_m1),
						  F3x_m1 == F3_m1*cos(th3_m1),
						  F4x_m1 == F4_m1*cos(th4_m1),
						  F5x_m1 == F5_m1*cos(th5_m1),
						  F1y_m1 == F1_m1*sin(th1_m1),
						  F2y_m1 == F2_m1*sin(th2_m1),
						  F3y_m1 == F3_m1*sin(th3_m1),
						  F4y_m1 == F4_m1*sin(th4_m1),
						  F5y_m1 == F5_m1*sin(th5_m1),
						  Fx_m1 == F1x_m1 + F2x_m1 + F3x_m1 + F4x_m1 + F5x_m1,
						  Fy_m1 == F1y_m1 + F2y_m1 + F3y_m1 + F4y_m1 + F5y_m1,
						  Fx_m1 == m1*ax_m1,
						  Fy_m1 == m1*ay_m1,
						  ////////////////////////////////////////////////////////////////////////////////
						  F1x_m2 == F1_m2*cos(th1_m2),
						  F2x_m2 == F2_m2*cos(th2_m2),
						  F1y_m2 == F1_m2*sin(th1_m2),
						  F2y_m2 == F2_m2*sin(th2_m2),
						  Fx_m2 == F1x_m2 + F2x_m2,
						  Fy_m2 == F1y_m2 + F2y_m2,
						  Fx_m2 == m2*ax_m2,
						  Fy_m2 == m2*ay_m2,
						  ////////////////////////////////////////////////////////////////////////////////
						  f_k == mu_k*n,
						  a == ax_m1);

			DoubleFloat.Tolerance = 0.00001;

			var vals = new List<Equation>
				{
					ay_m1 == 0, // block moves horizontally
					ax_m2 == 0, // ball moves vertically
						
					F1_m1 == F,
					th1_m1 == th, // force applied at angle
					F2_m1 == n,
					th2_m1 == 90*pi/180, // normal force is straight up
					F3_m1 == T,
					th3_m1 == 180*pi/180, // force due to cord is left
					F4_m1 == f_k,
					th4_m1 == 180*pi/180, // force due to friction is left
					F5_m1 == m1*g,
					th5_m1 == 270*pi/180, // force due to gravity is down
						
					F1_m2 == T,
					th1_m2 == 90*pi/180, // force due to cord is up
					F2_m2 == m2*g,
					th2_m2 == 270*pi/180 // force due to gravity is down                                
				};

			// a
			Assert.AreEqual(a == (g*m2 + g*m1*mu_k - F*mu_k*sin(th) - cos(th)*F)/(-m1 - m2),
							eqs.Substitute(vals)
							   .EliminateVariables(ax_m1,
												   Fx_m1, Fy_m1,
												   Fx_m2, Fy_m2,
												   F1x_m1, F2x_m1, F3x_m1, F4x_m1, F5x_m1,
												   F1y_m1, F2y_m1, F3y_m1, F4y_m1, F5y_m1,
												   F1x_m2, F2x_m2,
												   F1y_m2, F2y_m2,
												   T, f_k, n,
												   ay_m2));
		}
		[TestMethod]
		public void Example_5_14_UsingObject()
		{
			// A block of mass m1 on a rough, horizontal surface is connected
			// to a ball of mass m2 by a lightweight cord over a lightweight,
			// frictionless pulley, as shown:
			//
			// http://i.imgur.com/0fHOmGJ.png
			//
			// A force of magnitude F at an angle th with the horizontal is
			// applied to the block as shown. The coefficient of kinetic
			// friction between the block and surface is mu_k.
			// 
			// Determine the magnitude of the acceleration of the two objects.

			var blk = new Obj5("blk");
			var bal = new Obj3("bal");
			var F = new Symbol("F");                // force applied at angle on block
			var th = new Symbol("th");              // angle of force applied on block
			var T = new Symbol("T");                // tension in cable
			var g = new Symbol("g");                // gravity
			var n = new Symbol("n");                // normal force on block
			var f_k = new Symbol("f_k");            // force due to kinetic friction
			var mu_k = new Symbol("mu_k");          // coefficient of kinetic friction
			var m1 = new Symbol("m1");
			var m2 = new Symbol("m2");

			var eqs = and(blk.ax == bal.ay,                   // the block moves right as the ball moves up
				blk.Equations(),
				bal.Equations(),
				f_k == mu_k * n,
				a == blk.ax);

			var vals = new List<Equation>
				{
					blk.ay == 0,                                        // block moves horizontally
					blk.F1 == F,            blk.th1 == th,              // block moves horizontally
					blk.F2 == n,            blk.th2 == 90 * pi / 180,   // normal force is straight up
					blk.F3 == T,            blk.th3 == 180 * pi / 180,  // force due to cord is left
					blk.F4 == f_k,          blk.th4 == 180 * pi / 180,  // force due to friction is left
					blk.F5 == blk.m * g,    blk.th5 == 270 * pi / 180,  // force due to gravity is down
					bal.ax == 0,                                        // ball moves vertically
					bal.F1 == T,            bal.th1 == 90 * pi / 180,   // force due to cord is up
					bal.F2 == bal.m * g,    bal.th2 == 270 * pi / 180,  // force due to gravity is down
					bal.F3 == 0,
					blk.m == m1,
					bal.m == m2
				};

			// a
			Assert.AreEqual(a == (g*m2 + g*m1*mu_k - F*mu_k*sin(th) - cos(th)*F)/(-m1 - m2),
							eqs.Substitute(vals)
							   .EliminateVariables(blk.ax,
												   blk.ΣFx, blk.ΣFy,
												   bal.ΣFx, bal.ΣFy,
												   blk.F1x, blk.F2x, blk.F3x, blk.F4x, blk.F5x,
												   blk.F1y, blk.F2y, blk.F3y, blk.F4y, blk.F5y,
												   bal.F1x, bal.F2x, bal.F3x,
												   bal.F1y, bal.F2y, bal.F3y,
												   T, f_k, n,
												   bal.ay));
		}
		[TestMethod]
		public void Example_5_25()
		{
			// A bag of cement of weight F_g hangs from three wires as
			// shown in http://i.imgur.com/f5JpLjY.png
			//  
			// Two of the wires make angles th1 and th2 with the horizontal.
			// If the system is in equilibrium, show that the tension in the
			// left -hand wire is:
			//
			//          T1 == F_g cos(th2) / sin(th1 + th2)

			////////////////////////////////////////////////////////////////////////////////

			var F1_m1 = new Symbol("F1_m1"); // force 1 on mass 1
			var F2_m1 = new Symbol("F2_m1"); // force 2 on mass 1
			var F3_m1 = new Symbol("F3_m1"); // force 3 on mass 1
			var th1_m1 = new Symbol("th1_m1"); // direction of force 1 on mass 1
			var th2_m1 = new Symbol("th2_m1"); // direction of force 2 on mass 1
			var th3_m1 = new Symbol("th3_m1"); // direction of force 3 on mass 1
			var F1x_m1 = new Symbol("F1x_m1"); // x-component of force 1 on mass 1
			var F2x_m1 = new Symbol("F2x_m1"); // x-component of force 2 on mass 1
			var F3x_m1 = new Symbol("F3x_m1"); // x-component of force 3 on mass 1
			var F1y_m1 = new Symbol("F1y_m1"); // y-component of force 1 on mass 1
			var F2y_m1 = new Symbol("F2y_m1"); // y-component of force 2 on mass 1
			var F3y_m1 = new Symbol("F3y_m1"); // y-component of force 3 on mass 1
			var Fx_m1 = new Symbol("Fx_m1"); // x-component of total force on mass 1
			var Fy_m1 = new Symbol("Fy_m1"); // y-component of total force on mass 1
			var ax_m1 = new Symbol("ax_m1"); // x-component of acceleration of mass 1
			var ay_m1 = new Symbol("ay_m1"); // y-component of acceleration of mass 1
			var m1 = new Symbol("m1");

			////////////////////////////////////////////////////////////////////////////////

			var g = new Symbol("g"); // gravity
			var T1 = new Symbol("T1");
			var T2 = new Symbol("T2");
			var th1 = new Symbol("th1");
			var th2 = new Symbol("th2");

			var eqs = and(F1x_m1 == F1_m1*cos(th1_m1),
						  F2x_m1 == F2_m1*cos(th2_m1),
						  F3x_m1 == F3_m1*cos(th3_m1),
						  F1y_m1 == F1_m1*sin(th1_m1),
						  F2y_m1 == F2_m1*sin(th2_m1),
						  F3y_m1 == F3_m1*sin(th3_m1),
						  Fx_m1 == F1x_m1 + F2x_m1 + F3x_m1,
						  Fy_m1 == F1y_m1 + F2y_m1 + F3y_m1,
						  Fx_m1 == m1*ax_m1,
						  Fy_m1 == m1*ay_m1);

			DoubleFloat.Tolerance = 0.00001;

			var vals = new List<Equation>
				{
					ax_m1 == 0,
					ay_m1 == 0,
					F1_m1 == T2,
					F2_m1 == T1,
					F3_m1 == m1*g,
					th1_m1 == th2,
					th2_m1 == 180*pi/180 - th1,
					th3_m1 == 270*pi/180
				};

			// T1
			Assert.AreEqual(T1 == cos(th2)*g*m1/sin(th1 + th2),
							eqs.Substitute(vals)
							   .EliminateVariables(F1x_m1, F2x_m1, F3x_m1,
												   F1y_m1, F2y_m1, F3y_m1,
												   Fx_m1, Fy_m1,
												   T2)
							   .IsolateVariable(T1)
							   .RationalizeExpression()
							   .DeepSelect(DeepSelectTests.SumDifferenceFormulaAFunc));
		}
		[TestMethod]
		public void Example_5_25_UsingObject()
		{
			// A bag of cement of weight F_g hangs from three wires as
			// shown in http://i.imgur.com/f5JpLjY.png
			//  
			// Two of the wires make angles th1 and th2 with the horizontal.
			// If the system is in equilibrium, show that the tension in the
			// left -hand wire is:
			//
			//          T1 == F_g cos(th2) / sin(th1 + th2)

			var bag = new Obj3("bag");
			var T1 = new Symbol("T1");
			var T2 = new Symbol("T2");
			var F_g = new Symbol("F_g");
			var th1 = new Symbol("th1");
			var th2 = new Symbol("th2");

			var eqs = bag.Equations();

			var vals = new List<Equation>
				{
					bag.ax == 0,
					bag.ay == 0,

					bag.F1 == T1,       bag.th1 == (180).ToRadians() - th1,
					bag.F2 == T2,       bag.th2 == th2,
					bag.F3 == F_g,      bag.th3 == (270).ToRadians()
				};

			Assert.AreEqual(T1 == cos(th2)*F_g/sin(th1 + th2),
							eqs.Substitute(vals)
							   .EliminateVariables(bag.ΣFx, bag.F1x, bag.F2x, bag.F3x,
												   bag.ΣFy, bag.F1y, bag.F2y, bag.F3y,
												   T2)
							   .IsolateVariable(T1)
							   .RationalizeExpression()
							   .DeepSelect(DeepSelectTests.SumDifferenceFormulaAFunc));
		}
		[TestMethod]
		public void Example_5_31()
		{
			// Two people pull as hard as they can on ropes attached
			// to a boat that has a mass of 200 kg. If they pull in the
			// same direction, the boat has an acceleration of
			// 1.52 m/s^2 to the right. If they pull in opposite directions,
			// the boat has an acceleration of 0.518 m/s^2 to the
			// left.
			// 
			// What is the force exerted by each person on the
			// boat? (Disregard any other forces on the boat.)

			////////////////////////////////////////////////////////////////////////////////

			var F1_m1 = new Symbol("F1_m1"); // force 1 on mass 1
			var F2_m1 = new Symbol("F2_m1"); // force 2 on mass 1
			var th1_m1 = new Symbol("th1_m1"); // direction of force 1 on mass 1
			var th2_m1 = new Symbol("th2_m1"); // direction of force 2 on mass 1
			var F1x_m1 = new Symbol("F1x_m1"); // x-component of force 1 on mass 1
			var F2x_m1 = new Symbol("F2x_m1"); // x-component of force 2 on mass 1
			var F1y_m1 = new Symbol("F1y_m1"); // y-component of force 1 on mass 1
			var F2y_m1 = new Symbol("F2y_m1"); // y-component of force 2 on mass 1
			var Fx_m1 = new Symbol("Fx_m1"); // x-component of total force on mass 1
			var Fy_m1 = new Symbol("Fy_m1"); // y-component of total force on mass 1
			var ax_m1 = new Symbol("ax_m1"); // x-component of acceleration of mass 1
			var ay_m1 = new Symbol("ay_m1"); // y-component of acceleration of mass 1
			var m1 = new Symbol("m1");

			////////////////////////////////////////////////////////////////////////////////

			var F1_m2 = new Symbol("F1_m2"); // force 1 on mass 2
			var F2_m2 = new Symbol("F2_m2"); // force 2 on mass 2
			var th1_m2 = new Symbol("th1_m2"); // direction of force 1 on mass 2
			var th2_m2 = new Symbol("th2_m2"); // direction of force 2 on mass 2
			var F1x_m2 = new Symbol("F1x_m2"); // x-component of force 1 on mass 2
			var F2x_m2 = new Symbol("F2x_m2"); // x-component of force 2 on mass 2
			var F1y_m2 = new Symbol("F1y_m2"); // y-component of force 1 on mass 2
			var F2y_m2 = new Symbol("F2y_m2"); // y-component of force 2 on mass 2
			var Fx_m2 = new Symbol("Fx_m2"); // x-component of total force on mass 2
			var Fy_m2 = new Symbol("Fy_m2"); // y-component of total force on mass 2
			var ax_m2 = new Symbol("ax_m2"); // x-component of acceleration of mass 2
			var ay_m2 = new Symbol("ay_m2"); // y-component of acceleration of mass 2
			var m2 = new Symbol("m2");

			////////////////////////////////////////////////////////////////////////////////

			var T1 = new Symbol("T1");
			var T2 = new Symbol("T2");

			var eqs = and(m1 == m2,
						  F1x_m1 == F1_m1*cos(th1_m1),
						  F2x_m1 == F2_m1*cos(th2_m1),
						  F1y_m1 == F1_m1*sin(th1_m1),
						  F2y_m1 == F2_m1*sin(th2_m1),
						  Fx_m1 == F1x_m1 + F2x_m1,
						  Fy_m1 == F1y_m1 + F2y_m1,
						  Fx_m1 == m1*ax_m1,
						  Fy_m1 == m1*ay_m1,
						  F1x_m2 == F1_m2*cos(th1_m2),
						  F2x_m2 == F2_m2*cos(th2_m2),
						  F1y_m2 == F1_m2*sin(th1_m2),
						  F2y_m2 == F2_m2*sin(th2_m2),
						  Fx_m2 == F1x_m2 + F2x_m2,
						  Fy_m2 == F1y_m2 + F2y_m2,
						  Fx_m2 == m2*ax_m2,
						  Fy_m2 == m2*ay_m2);

			DoubleFloat.Tolerance = 0.00001;

			var vals = new List<Equation>
				{
					ay_m1 == 0,
					F1_m1 == T1,
					th1_m1 == 0,
					F2_m1 == T2,
					th2_m1 == 0,
					ay_m2 == 0,
					F1_m2 == T1,
					th1_m2 == 180*pi/180,
					F2_m2 == T2,
					th2_m2 == 0
				};

			var numerical_vals = new List<Equation>
				{
					m1 == 200,
					ax_m1 == 1.52,
					ax_m2 == -0.518
				};

			// T1
			Assert.AreEqual(T1 == 203.8,
							eqs.Substitute(vals)
							   .EliminateVariables(m2,
												   F1x_m1, F2x_m1,
												   F1y_m1, F2y_m1,
												   F1x_m2, F2x_m2,
												   F1y_m2, F2y_m2,
												   Fx_m1, Fy_m1,
												   Fx_m2, Fy_m2,
												   T2)
							   .IsolateVariable(T1)
							   .Substitute(numerical_vals));

			// T2
			Assert.AreEqual(T2 == 100.19999999999999,
							eqs.Substitute(vals)
							   .EliminateVariables(m2,
												   F1x_m1, F2x_m1,
												   F1y_m1, F2y_m1,
												   F1x_m2, F2x_m2,
												   F1y_m2, F2y_m2,
												   Fx_m1, Fy_m1,
												   Fx_m2, Fy_m2,
												   T1)
							   .IsolateVariable(T2)
							   .Substitute(numerical_vals));
		}
		[TestMethod]
		public void Example_5_31_UsingObject()
		{
			// Two people pull as hard as they can on ropes attached
			// to a boat that has a mass of 200 kg. If they pull in the
			// same direction, the boat has an acceleration of
			// 1.52 m/s^2 to the right. If they pull in opposite directions,
			// the boat has an acceleration of 0.518 m/s^2 to the
			// left.
			// 
			// What is the force exerted by each person on the
			// boat? (Disregard any other forces on the boat.)

			////////////////////////////////////////////////////////////////////////////////

			var b1 = new Obj2("b1");            // boat in scenario 1 (same direction)
			var b2 = new Obj2("b2");            // boat in scenario 2 (opposite directions)
			var m = new Symbol("m");

			////////////////////////////////////////////////////////////////////////////////

			var T1 = new Symbol("T1");
			var T2 = new Symbol("T2");

			var eqs = and(b1.Equations(),
				b2.Equations());

			DoubleFloat.Tolerance = 0.00001;

			var vals = new List<Equation>
				{
					b1.m == m,
					b1.ay == 0,
					b1.F1 == T1, b1.th1 == 0,
					b1.F2 == T2, b1.th2 == 0,
					b2.m == m,
					b2.ay == 0,
					b2.F1 == T1, b2.th1 == (180).ToRadians(),
					b2.F2 == T2, b2.th2 == 0};

			var numerical_vals = new List<Equation>
				{
					m == 200,
					b1.ax == 1.52,
					b2.ax == -0.518
				};

			// T1
			Assert.AreEqual(T1 == 203.8,
							eqs.Substitute(vals)
							   .EliminateVariables(b1.ΣFx, b1.F1x, b1.F2x,
												   b1.ΣFy, b1.F1y, b1.F2y,
												   b2.ΣFx, b2.F1x, b2.F2x,
												   b2.ΣFy, b2.F1y, b2.F2y,
												   T2)
							   .IsolateVariable(T1)
							   .Substitute(numerical_vals));

			// T2
			Assert.AreEqual(T2 == 100.19999999999999,
							eqs.Substitute(vals)
							   .EliminateVariables(b1.ΣFx, b1.F1x, b1.F2x,
												   b1.ΣFy, b1.F1y, b1.F2y,
												   b2.ΣFx, b2.F1x, b2.F2x,
												   b2.ΣFy, b2.F1y, b2.F2y,
												   T1)
							   .IsolateVariable(T2)
							   .Substitute(numerical_vals));
		}
		[TestMethod]
		public void Example_5_55()
		{
			// An inventive child named Pat wants to reach an apple
			// in a tree without climbing the tree. Sitting in a chair
			// connected to a rope that passes over a frictionless pulley
			// Pat pulls on the loose end of the rope with such a force
			// that the spring scale reads 250 N. Pat’s weight is 320 N,
			// and the chair weighs 160 N.
			//
			// http://i.imgur.com/wwlypzB.png
			//
			// (a) Draw free - body diagrams for Pat and the chair considered as
			// separate systems, and draw another diagram for Pat and
			// the chair considered as one system.
			//
			// (b) Show that the acceleration of the system is upward and
			// find its magnitude.
			//
			// (c) Find the force Pat exerts on the chair.

			var b = new Obj3("b");          // boy
			var c = new Obj3("c");          // chair
			var s = new Obj3("s");          // system
			var T = new Symbol("T");        // rope tension
			var n = new Symbol("n");        // normal force
			var Fg_b = new Symbol("Fg_b");  // force due to gravity of the boy
			var Fg_c = new Symbol("Fg_c");  // force due to gravity of the chair
			var Fg_s = new Symbol("Fg_s");  // force due to gravity of the system
			var g = new Symbol("g");

			var eqs = and(Fg_b == b.m * g,
				Fg_c == c.m * g,
				Fg_s == s.m * g,
				Fg_s == Fg_c + Fg_b,
				s.Equations(),
				c.Equations());

			var vals = new List<Equation>
				{
					//b.ax == 0,
					c.ax == 0,
					s.ax == 0,
					//b.F1 == T,          b.th1 == 90 * Pi / 180,
					//b.F2 == n,          b.th2 == 90 * Pi / 180,
					//b.F3 == b.m * g,    b.th3 == 270 * Pi / 180,
					c.F1 == T,          c.th1 == 90 * pi / 180,
					c.F2 == n,          c.th2 == 270 * pi / 180,
					c.F3 == Fg_c,       c.th3 == 270 * pi / 180,
					s.F1 == T,          s.th1 == 90 * pi / 180,
					s.F2 == T,          s.th2 == 90 * pi / 180,
					s.F3 == Fg_s,       s.th3 == 270 * pi / 180,
					//b.ay == a,
					c.ay == a,
					s.ay == a
				};

			var numerical_vals = new List<Equation>
				{
					T == 250.0,
					Fg_b == 320,
					Fg_c == 160,
					g == 9.8
				};

			DoubleFloat.Tolerance = 0.00001;

			// a
			Assert.AreEqual(a == 0.40833333333333333,
							eqs.Substitute(vals)
							   .EliminateVariables(s.ΣFx, s.F1x, s.F2x, s.F3x,
												   s.ΣFy, s.F1y, s.F2y, s.F3y,
												   c.ΣFx, c.F1x, c.F2x, c.F3x,
												   c.ΣFy, c.F1y, c.F2y, c.F3y,
												   n,
												   s.m,
												   Fg_s,
												   b.m, c.m)
							   .IsolateVariable(a)
							   .Substitute(numerical_vals));

			// n
			Assert.AreEqual(n == 83.333333333333343,
			                eqs.Substitute(vals)
			                   .EliminateVariables(s.ΣFx, s.F1x, s.F2x, s.F3x,
			                                       s.ΣFy, s.F1y, s.F2y, s.F3y,
			                                       c.ΣFx, c.F1x, c.F2x, c.F3x,
			                                       c.ΣFy, c.F1y, c.F2y, c.F3y,
			                                       c.m, s.m,
			                                       Fg_s,
			                                       b.m,
			                                       a)
			                   .IsolateVariable(n)
			                   .Substitute(numerical_vals));

			DoubleFloat.Tolerance = null;
		}
		[TestMethod]
		public void Example_5_59()
		{
			// A mass M is held in place by an applied force F and a
			// pulley system: 
			//
			//                 http://i.imgur.com/TPAHTlW.png
			//
			// The pulleys are massless and frictionless. Find 
			//                     
			// (a) the tension in each section of rope, T1, T2, T3, T4, and T5
			//                     
			// (b) the magnitude of F. 
			//                     
			// (Hint: Draw a free - body diagram for each pulley.)

			var pul1_F = new Symbol("pul1_F"); // magnitude of total force on pully 1
			var pul1_m = new Symbol("pul1_m"); // mass of pully 1
			var pul1_a = new Symbol("pul1_a"); // acceleration of pully 1
			var pul2_F = new Symbol("pul2_F"); // magnitude of total force on pully 2
			var pul2_m = new Symbol("pul2_m"); // mass of pully 2
			var pul2_a = new Symbol("pul2_a"); // acceleration of pully 2
			var T1 = new Symbol("T1");
			var T2 = new Symbol("T2");
			var T3 = new Symbol("T3");
			var T4 = new Symbol("T4");
			var T5 = new Symbol("T5");
			var F = new Symbol("F");
			var M = new Symbol("M");
			var g = new Symbol("g");

			var eqs = and(T1 == F,
			              T2 == T3,
			              T1 == T3,
			              T5 == M*g,
			              pul1_a == 0,
			              pul1_m == 0,
			              pul1_F == T4 - T1 - T2 - T3,
			              pul1_F == pul1_m*pul1_a,
			              pul2_m == 0,
			              pul2_F == T2 + T3 - T5,
			              pul2_F == pul2_m*pul2_a);

			DoubleFloat.Tolerance = 0.00001;

			// T1
			Assert.AreEqual(T1 == g*M/2,
			                eqs.EliminateVariables(pul1_F, pul2_F, pul1_m, pul2_m, pul1_a, T2, T3, T4, T5, F)
			                   .IsolateVariable(T1));

			// T2
			Assert.AreEqual(T2 == g*M/2,
			                eqs.EliminateVariables(pul1_F, pul2_F, pul1_m, pul2_m, pul1_a, T1, T3, T4, T5, F)
			                   .IsolateVariable(T2));

			// T3
			Assert.AreEqual(T3 == g*M/2,
			                eqs.EliminateVariables(pul1_F, pul2_F, pul1_m, pul2_m, pul1_a, T1, T2, T4, T5, F)
			                   .IsolateVariable(T3));

			// T4
			Assert.AreEqual(T4 == g*M*3/2,
			                eqs.EliminateVariables(pul1_F, pul2_F, pul1_m, pul2_m, pul1_a, T1, T2, T3, T5, F)
			                   .IsolateVariable(T4));

			// T5
			Assert.AreEqual(T5 == g*M,
			                eqs.EliminateVariables(pul1_F, pul2_F, pul1_m, pul2_m, pul1_a, T1, T2, T3, T4, F));

			// F
			Assert.AreEqual(F == g*M/2,
			                eqs.EliminateVariables(pul1_F, pul2_F, pul1_m, pul2_m, pul1_a, T1, T2, T3, T4, T5)
			                   .IsolateVariable(F));
		}
		[TestMethod]
		public void Example_5_69()
		{
			// What horizontal force must be applied to the cart shown:

			// http://i.imgur.com/fpkzsYI.png

			// so that the blocks remain stationary relative to the cart?
			// Assume all surfaces, wheels, and pulley are frictionless.
			// (Hint:Note that the force exerted by the string accelerates m1.)

			var blk1 = new Obj3("blk1");
			var blk2 = new Obj3("blk2");
			var sys = new Obj3("sys");
			var m1 = new Symbol("m1");
			var m2 = new Symbol("m2");
			var T = new Symbol("T");
			var F = new Symbol("F");
			var M = new Symbol("M");
			var g = new Symbol("g");

			var eqs = and(blk1.Equations(),
				blk2.Equations(),
				sys.Equations());

			var vals = new List<Equation>
				{
					blk1.ax == a,
					blk1.ay == 0,
					blk1.m == m1,
					blk1.F1 == T,
					blk1.th1 == 0,
					blk1.th2 == (90).ToRadians(),
					blk1.th3 == (270).ToRadians(),
					blk2.ax == a,
					blk2.ay == 0,
					blk2.m == m2,
					blk2.th1 == 0,
					blk2.F2 == T,
					blk2.th2 == (90).ToRadians(),
					blk2.F3 == m2*g,
					blk2.th3 == (270).ToRadians(),
					sys.ax == a,
					sys.ay == 0,
					sys.m == M + m1 + m2,
					sys.F1 == F,
					sys.th1 == 0,
					sys.th2 == (90).ToRadians(),
					sys.th3 == (270).ToRadians()
				};

			Assert.AreEqual(F == g*m2/m1*(M + m1 + m2),
			                eqs.Substitute(vals)
			                   .EliminateVariables(blk1.ΣFx, blk1.F1x, blk1.F2x, blk1.F3x,
			                                       blk1.ΣFy, blk1.F1y, blk1.F2y, blk1.F3y,
			                                       blk1.F2,
			                                       blk2.ΣFx, blk2.F1x, blk2.F2x, blk2.F3x,
			                                       blk2.ΣFy, blk2.F1y, blk2.F2y, blk2.F3y,
			                                       blk2.F1,
			                                       sys.ΣFx, sys.F1x, sys.F2x, sys.F3x,
			                                       sys.ΣFy, sys.F1y, sys.F2y, sys.F3y,
			                                       sys.F2,
			                                       T, a));
		}
		[TestMethod]
		public void Example_7_7()
		{
			// A  6.0-kg block initially at rest is pulled to the right along a
			// horizontal, frictionless surface by a constant horizontal force
			// of 12 N. Find the speed of the block after it has moved 3.0 m.

			var W = new Symbol("W");
			var F = new Symbol("F");
			var d = new Symbol("d");
			var Kf = new Symbol("Kf");
			var Ki = new Symbol("Ki");
			var m = new Symbol("m");
			var vf = new Symbol("vf");
			var vi = new Symbol("vi");

			var eqs = and(W == F * d,
				W == Kf - Ki,
				Kf == m * (vf ^ 2) / 2,
				Ki == m * (vi ^ 2) / 2,
				m != 0);

			var vals = new List<Equation>
				{
					m == 6.0,
					vi == 0,
					F == 12,
					d == 3
				};

			// vf
			Assert.AreEqual(or(vf == 3.4641016151377544, vf == -3.4641016151377544),
			                eqs.EliminateVariables(Kf, Ki, W)
			                   .IsolateVariable(vf)
			                   .LogicalExpand().CheckVariable(m).SimplifyEquation().SimplifyLogical()
			                   .Substitute(vi == 0)
			                   .Substitute(vals));
		}
		[TestMethod]
		public void Example_7_8()
		{
			// Find the final speed of the block described in Example 7.7 if
			// the surface is not frictionless but instead has a coefficient of
			// kinetic friction of 0.15.

			var W = new Symbol("W");
			var F = new Symbol("F");
			var d = new Symbol("d");
			var n = new Symbol("n");
			var g = new Symbol("g");
			var Kf = new Symbol("Kf");
			var Ki = new Symbol("Ki");
			var m = new Symbol("m");
			var vf = new Symbol("vf");
			var vi = new Symbol("vi");
			var fk = new Symbol("fk");
			var μk = new Symbol("μk");

			var eqs = and(Kf == m*(vf ^ 2)/2,
			              Ki == m*(vi ^ 2)/2,
			              W == F*d,
			              n == m*g,
			              fk == n*μk,
			              W - fk*d == Kf - Ki,
			              m != 0);

			var vals = new List<Equation>
				{
					vi == 0,
					F == 12.0,
					d == 3.0,
					m == 6.0,
					μk == 0.15,
					g == 9.8,
				};

			// vf
			Assert.AreEqual(or(vf == -1.7832554500127007, vf == 1.7832554500127007),
			                eqs.EliminateVariables(Kf, Ki, W, n, fk)
			                   .IsolateVariable(vf)
			                   .LogicalExpand().SimplifyEquation().SimplifyLogical().CheckVariable(m)
			                   .Substitute(vi == 0)
			                   .Substitute(vals));
		}
		[TestMethod]
		public void Example_7_11()
		{
			// A block of mass 1.6 kg is attached to a horizontal spring that
			// has a force constant of 1.0 x 10^3 N/m, as shown in Figure
			// 7.10. The spring is compressed 2.0 cm and is then released
			// from  rest.

			// (a) Calculate the  speed of  the block  as it  passes
			// through the equilibrium position x = 0 if the surface is frictionless.

			// (b) Calculate the speed of the block as it passes through
			// the equilibrium position if a constant frictional force of 4.0 N
			// retards its motion from the moment it is released.

			var ΣW = new Symbol("ΣW");
			var Kf = new Symbol("Kf");
			var Ki = new Symbol("Ki");
			var m = new Symbol("m");
			var d = new Symbol("d");
			var k = new Symbol("k");
			var vf = new Symbol("vf");
			var vi = new Symbol("vi");
			var fk = new Symbol("fk");
			var W_s = new Symbol("W_s");
			var W_f = new Symbol("W_f");
			var x_max = new Symbol("x_max");

			var eqs = and(W_s == k*(x_max ^ 2)/2,
			              Kf == m*(vf ^ 2)/2,
			              Ki == m*(vi ^ 2)/2,
			              W_f == -fk*d,
			              ΣW == Kf - Ki,
			              ΣW == W_s + W_f,
			              m != 0);

			var reduced = eqs.EliminateVariables(ΣW, Kf, Ki, W_f, W_s)
			                 .IsolateVariable(vf)
			                 .LogicalExpand()
			                 .SimplifyEquation()
			                 .SimplifyLogical()
			                 .CheckVariable(m);

			// vf
			var vals1 = new List<Equation>
				{
					m == 1.6,
					vi == 0,
					fk == 0,
					k == 1000,
					x_max == -0.02
				};

			Assert.AreEqual(or(vf == 0.5, vf == -0.5), reduced.Substitute(vals1));

			// vf
			var vals2 = new List<Equation>
				{
					m == 1.6,
					vi == 0,
					fk == 4,
					k == 1000,
					x_max == -0.02,
					d == 0.02
				};

			Assert.AreEqual(or(vf == 0.3872983346207417, vf == -0.3872983346207417), reduced.Substitute(vals2));
		}
		[TestMethod]
		public void Example_7_3()
		{
			// Batman, whose mass is 80.0kg, is dangling on the free end
			// of a 12.0m rope, the other end of which is fixed to a tree
			// limb above. He is able to get the rope in motion as only
			// Batman knows how, eventually getting it to swing enough
			// that he can reach a ledge when the rope makes a 60.0°
			// angle with the vertical. How much work was done by the
			// gravitational force on Batman in this maneuver?

			var m = new Symbol("m");
			var W = new Symbol("W");
			var F = new Symbol("F");
			var d = new Symbol("d");
			var yA = new Symbol("yA");
			var yB = new Symbol("yB");
			var th = new Symbol("th");
			var len = new Symbol("len");

			var eqs = and(yA == -len,
				yB == -len * cos(th),
				d == yB - yA,
				F == m * a,
				W == F * d);

			var vals = new List<Equation>
				{
					m == 80,
					len == 12,
					th == (60).ToRadians(),
					a == -9.8
				};

			Assert.AreEqual(W == -4704.0,
			                eqs.EliminateVariables(F, d, yA, yB)
			                   .Substitute(vals));
		}
		[TestMethod]
		public void Example_7_23()
		{
			// If it takes 4.00J of work to stretch a Hooke’s-law spring
			// 10.0cm from its unstressed length, determine the extra
			// work required to stretch it an additional 10.0cm.

			var WsAB = new Symbol("WsAB");
			var WsA = new Symbol("WsA");
			var WsB = new Symbol("WsB");
			var k = new Symbol("k");
			var xA = new Symbol("xA");
			var xB = new Symbol("xB");

			var eqs = and(WsA == k*(xA ^ 2)/2,
			              WsB == k*(xB ^ 2)/2,
			              WsAB == WsB - WsA);

			var vals = new List<Equation>
				{
					xA == 0.1,
					xB == 0.2,
					WsA == 4
				};

			Assert.AreEqual(WsAB == 12.0,
			                eqs.EliminateVariables(WsB, k)
			                   .Substitute(vals));
		}
		[TestMethod]
		public void Example_7_33()
		{
			// A 40.0-kg box initially at rest is pushed 5.00m along a
			// rough, horizontal floor with a constant applied horizontal
			// force of 130 N. If the coefficient of friction between
			// the box and the floor is 0.300, find

			// (a) the work done by the applied force
			// (b) the energy loss due to friction
			// (c) the work done by the normal force
			// (d) the work done by gravity
			// (e) the change in kinetic energy of the box
			// (f) the final speed of the box

			var ΣW = new Symbol("ΣW");
			var Kf = new Symbol("Kf");
			var Ki = new Symbol("Ki");
			var F = new Symbol("F");
			var m = new Symbol("m");
			var d = new Symbol("d");
			var n = new Symbol("n");
			var g = new Symbol("g");
			var vf = new Symbol("vf");
			var vi = new Symbol("vi");
			var fk = new Symbol("fk");
			var W_F = new Symbol("W_F");
			var W_f = new Symbol("W_f");
			var μk = new Symbol("μk");

			var eqs = and(n == m*g,
			              fk == μk*n,
			              Kf == m*(vf ^ 2)/2,
			              Ki == m*(vi ^ 2)/2,
			              W_F == F*d,
			              W_f == -fk*d,
			              ΣW == Kf - Ki,
			              ΣW == W_F + W_f,
			              m != 0);

			var vals = new List<Equation>
				{
					m == 40,
					vi == 0,
					d == 5,
					F == 130,
					μk == 0.3,
					g == 9.8
				};

			// W_F, W_f
			Assert.AreEqual(and(W_F == 650, W_f == -588.0),
			                eqs.EliminateVariables(fk, n, Kf, Ki, ΣW, vf)
			                   .LogicalExpand().SimplifyEquation().SimplifyLogical().CheckVariable(m)
			                   .Substitute(vals));

			// ΣW
			Assert.AreEqual(and(ΣW == 20*(vf ^ 2), ΣW == 62.0),
			                eqs.EliminateVariables(W_F, W_f, fk, n, Ki, Kf)
			                   .Substitute(vals));

			// vf
			Assert.AreEqual(or(vf == 1.7606816861659009, vf == -1.7606816861659009),
			                eqs.EliminateVariables(Kf, Ki, ΣW, W_F, W_f, fk, n)
			                   .IsolateVariable(vf)
			                   .LogicalExpand().SimplifyEquation().SimplifyLogical().CheckVariable(m)
			                   .Substitute(vals));
		}
		[TestMethod]
		public void Example_7_35()
		{
			// A crate of mass 10.0kg is pulled up a rough incline with
			// an initial speed of 1.50 m/s.The pulling force is 100 N
			// parallel to the incline, which makes an angle of 20.0°
			// with the horizontal. The coefficient of kinetic friction is
			// 0.400, and the crate is pulled 5.00 m.

			// (a) How much work is done by gravity?
			// (b) How much energy is lost because of friction?
			// (c) How much work is done by the 100-N force?
			// (d) What is the change in kinetic energy of the crate?
			// (e) What is the speed of the crate after it has been pulled 5.00 m?

			var ΣW = new Symbol("ΣW");
			var Kf = new Symbol("Kf");
			var Ki = new Symbol("Ki");
			var F = new Symbol("F");
			var m = new Symbol("m");
			var d = new Symbol("d");
			var n = new Symbol("n");
			var g = new Symbol("g");
			var vf = new Symbol("vf");
			var vi = new Symbol("vi");
			var fk = new Symbol("fk");
			var W_F = new Symbol("W_F");
			var W_f = new Symbol("W_f");
			var W_g = new Symbol("W_g");
			var μk = new Symbol("μk");
			var th = new Symbol("th");
			var F_g = new Symbol("F_g");

			var eqs = and(F_g == m*g,
			              n == F_g*cos(th),
			              fk == μk*n,
			              Kf == m*(vf ^ 2)/2,
			              Ki == m*(vi ^ 2)/2,
			              W_F == F*d,
			              W_f == -fk*d,
			              W_g == -F_g*sin(th)*d,
			              ΣW == Kf - Ki,
			              ΣW == W_F + W_f + W_g,
			              m != 0);

			var vals = new List<Equation>
				{
					m == 10.0,
					g == 9.8,
					d == 5.0,
					th == (20).ToRadians(),
					μk == 0.4,
					F == 100.0,
					vi == 1.5,
					pi == Math.PI
				};

			// W_g, W_f, W_F
			Assert.AreEqual(and(Kf == 5.0*(vf ^ 2),
			                    Ki == 11.25,
			                    W_F == 500.0,
			                    W_f == -184.17975367403804,
			                    W_g == -167.58987022957766,
			                    ΣW == Kf - Ki,
			                    ΣW == W_f + W_F + W_g),
			                eqs.EliminateVariables(F_g, fk, n)
			                   .Substitute(vals));

			// ΣW
			Assert.AreEqual(and(Kf == 5.0*(vf ^ 2),
			                    Ki == 11.25,
			                    ΣW == Kf - Ki,
			                    ΣW == 148.23037609638431),
			                eqs.EliminateVariables(F_g, fk, n, W_F, W_f, W_g)
			                   .Substitute(vals));

			// vf
			Assert.AreEqual(or(vf == 5.6476610396939435, vf == -5.6476610396939435),
			                eqs.EliminateVariables(F_g, fk, n, W_F, W_f, W_g, ΣW, Kf, Ki)
			                   .IsolateVariable(vf)
			                   .LogicalExpand()
			                   .SimplifyEquation()
			                   .SimplifyLogical()
			                   .CheckVariable(m)
			                   .Substitute(vals));
		}
		[TestMethod]
		public void Example_7_39()
		{
			// A bullet with a mass of 5.00 g and a speed of 600 m/s
			// penetrates a tree to a depth of 4.00 cm.

			// (a) Use work and energy considerations to find the average
			// frictional force that stops the bullet.

			// (b) Assuming that the frictional force is constant,
			// determine how much time elapsed between the moment
			// the bullet entered the tree and the moment it stopped.

			var ΣW = new Symbol("ΣW");
			var Kf = new Symbol("Kf");
			var Ki = new Symbol("Ki");
			var m = new Symbol("m");
			var d = new Symbol("d");
			var vf = new Symbol("vf");
			var vi = new Symbol("vi");
			var fk = new Symbol("fk");
			var W_f = new Symbol("W_f");
			var t = new Symbol("t");

			var eqs = and(Kf == m * (vf ^ 2) / 2,
				Ki == m * (vi ^ 2) / 2,
				W_f == -fk * d,
				ΣW == Kf - Ki,
				ΣW == W_f);

			var vals = new List<Equation>
				{
					m == 0.005,
					vi == 600.0,
					vf == 0.0,
					d == 0.04
				};

			// fk
			Assert.AreEqual(fk == 22500.0,
			                eqs.EliminateVariables(W_f, ΣW, Ki, Kf)
			                   .IsolateVariable(fk)
			                   .Substitute(vals));

			// t
			Assert.AreEqual(t == 1.3333333333333334e-4,
			                (d == (vi + vf)*t/2)
				                .IsolateVariable(t)
				                .Substitute(vals));
		}
		[TestMethod]
		public void Example_7_41()
		{
			// A 2.00-kg block is attached to a spring of force constant
			// 500 N/m, as shown in Figure 7.10. The block is pulled
			// 5.00 cm to the right of equilibrium and is then released
			// from rest. Find the speed of the block as it passes
			// through equilibrium if

			// (a) the horizontal surface is frictionless

			// (b) the coefficient of friction between the block and the surface is 0.350.

			var ΣW = new Symbol("ΣW");
			var Kf = new Symbol("Kf");
			var Ki = new Symbol("Ki");
			var m = new Symbol("m");
			var d = new Symbol("d");
			var n = new Symbol("n");
			var g = new Symbol("g");
			var k = new Symbol("k");
			var vf = new Symbol("vf");
			var vi = new Symbol("vi");
			var fk = new Symbol("fk");
			var W_f = new Symbol("W_f");
			var W_s = new Symbol("W_s");
			var μk = new Symbol("μk");
			var xi = new Symbol("xi");
			var xf = new Symbol("xf");

			var eqs = and(n == m*g,
			              fk == μk*n,
			              Kf == m*(vf ^ 2)/2,
			              Ki == m*(vi ^ 2)/2,
			              W_f == -fk*d,
			              W_s == k*(xi ^ 2)/2 - k*(xf ^ 2)/2,
			              ΣW == Kf - Ki,
			              ΣW == W_f + W_s,
			              m != 0);

			var vals = new List<Equation>
				{
					m == 2.0,
					k == 500,
					xi == 0.05,
					xf == 0.0,
					vi == 0,
					d == 0.05,
					g == 9.8
				};

			Assert.AreEqual(or(vf == 0.79056941504209488, vf == -0.79056941504209488),
			                eqs.EliminateVariables(Kf, Ki, ΣW, W_f, W_s, n, fk)
			                   .IsolateVariable(vf)
			                   .LogicalExpand()
			                   .SimplifyEquation()
			                   .SimplifyLogical()
			                   .CheckVariable(m)
			                   .Substitute(vals)
			                   .Substitute(μk == 0));

			Assert.AreEqual(or(vf == 0.53103672189407025, vf == -0.53103672189407025),
			                eqs.EliminateVariables(Kf, Ki, ΣW, W_f, W_s, n, fk)
			                   .IsolateVariable(vf)
			                   .LogicalExpand()
			                   .SimplifyEquation()
			                   .SimplifyLogical()
			                   .CheckVariable(m)
			                   .Substitute(vals)
			                   .Substitute(μk == 0.35));
		}
		[TestMethod]
		public void Example_7_55()
		{
			// A baseball outfielder throws a 0.150-kg baseball at a
			// speed of 40.0 m/s and an initial angle of 30.0°. What is
			// the kinetic energy of the baseball at the highest point of
			// the trajectory?

			var vx = new Symbol("vx");
			var vi = new Symbol("vi");
			var th = new Symbol("th");
			var m = new Symbol("m");
			var K = new Symbol("K");
			var vals = new List<Equation>
				{
					m == 0.15,
					vi == 40.0,
					th == (30).ToRadians()
				};

			var eqs = and(vx == vi*cos(th),
			              K == m*(vx ^ 2)/2);

			Assert.AreEqual(K == 90.0,
			                eqs.EliminateVariables(vx)
			                   .Substitute(vals));
		}
		[TestMethod]
		public void Example_8_2()
		{
			// A ball  of mass m is dropped from a height h above the
			// ground, as shown in Figure 8.6.

			// (a) Neglecting air resistance, determine the speed of
			// the ball when it is at a height ya bove the ground.

			// (b) Determine the speed of the ball at y if at the instant of
			// release it already has an initial speed vi at the initial altitude h.

			var m = new Symbol("m");
			var yi = new Symbol("yi");
			var yf = new Symbol("yf");
			var vi = new Symbol("vi");
			var vf = new Symbol("vf");
			var Ki = new Symbol("Ki");
			var Kf = new Symbol("Kf");
			var Ugi = new Symbol("Ugi");
			var Ugf = new Symbol("Ugf");
			var ΣUi = new Symbol("ΣUi");
			var ΣUf = new Symbol("ΣUf");
			var Ei = new Symbol("Ei");
			var Ef = new Symbol("Ef");
			var g = new Symbol("g");
			var h = new Symbol("h");
			var y = new Symbol("y");

			var eqs = and(Ki == m * (vi ^ 2) / 2,
				Kf == m * (vf ^ 2) / 2,
				Ugi == m * g * yi,
				Ugf == m * g * yf,
				ΣUi == Ugi,
				ΣUf == Ugf,
				Ei == Ki + ΣUi,
				Ef == Kf + ΣUf,
				Ei == Ef);

			var vals = new List<Equation>
				{
					yi == h,
					yf == y
				};

			// vf, vi == 0
			Assert.AreEqual(or(vf == -sqrt(2*(g*h - g*y)), vf == sqrt(2*(g*h - g*y))),
			                eqs.EliminateVariables(Ugi, Ugf, ΣUi, ΣUf, Ki, Kf, Ei, Ef)
			                   .MultiplyBothSidesBy(1/m)
			                   .AlgebraicExpand()
			                   .IsolateVariable(vf)
			                   .Substitute(vals)
			                   .Substitute(vi == 0));

			// vf
			Assert.AreEqual(or(vf == -sqrt(2*(g*h + (vi ^ 2)/2 - g*y)), vf == sqrt(2*(g*h + (vi ^ 2)/2 - g*y))),
			                eqs.EliminateVariables(Ugi, Ugf, ΣUi, ΣUf, Ki, Kf, Ei, Ef)
			                   .MultiplyBothSidesBy(1/m)
			                   .AlgebraicExpand()
			                   .IsolateVariable(vf)
			                   .Substitute(vals));
		}
		[TestMethod]
		public void Example_8_3()
		{
			// A pendulum consists of a sphere of mass mattached to a light
			// cord of length L, as shown in Figure 8.7. The sphere is released
			// from rest when the cord makes an angle thA with the vertical,
			// and the pivot at P is frictionless.

			// (a) Find the speed of the sphere when it is at the lowest point B.

			// (b) What is the tension T_B in the cord at B?

			// (c) A pendulum of length 2.00 m and mass 0.500 kg
			// is released from rest when the cord makes an angle of 30.0°
			// with the vertical. Find the speed of the sphere and the tension
			// in the cord when the sphere is at its lowest point.

			var m = new Symbol("m");
			var yi = new Symbol("yi");
			var yf = new Symbol("yf");
			var vi = new Symbol("vi");
			var vf = new Symbol("vf");
			var Ki = new Symbol("Ki");
			var Kf = new Symbol("Kf");
			var Ugi = new Symbol("Ugi");
			var Ugf = new Symbol("Ugf");
			var ΣUi = new Symbol("ΣUi");
			var ΣUf = new Symbol("ΣUf");
			var Ei = new Symbol("Ei");
			var Ef = new Symbol("Ef");
			var g = new Symbol("g");
			var L = new Symbol("L");
			var thA = new Symbol("thA");
			var ar_f = new Symbol("ar_f");
			var r = new Symbol("r");
			var ΣFr = new Symbol("ΣFr");
			var T_f = new Symbol("T_f");
			var vf_sq = new Symbol("vf_sq");

			var eqs = and(Ki == m * (vi ^ 2) / 2,
				Kf == m * (vf ^ 2) / 2,
				Ugi == m * g * yi,
				Ugf == m * g * yf,
				ΣUi == Ugi,
				ΣUf == Ugf,
				Ei == Ki + ΣUi,
				Ef == Kf + ΣUf,
				Ei == Ef,
				ar_f == (vf ^ 2) / r,
				ΣFr == T_f - m * g,
				ΣFr == m * ar_f);

			var vals = new List<Equation>
				{
					yi == -L * cos(thA),
					yf == -L,
					vi == 0,
					r == L
				};

			var numerical_vals = new List<Equation>
				{
					L == 2.0,
					m == 0.5,
					thA == (30).ToRadians(),
					g == 9.8
				};

			// vf
			Assert.AreEqual(or(vf == -2.2916815161906787, vf == 2.2916815161906787),
			                eqs.Substitute(vals)
			                   .EliminateVariables(ar_f, ΣFr, T_f, Ki, Kf, Ugi, Ugf, ΣUi, ΣUf, Ei, Ef)
			                   .MultiplyBothSidesBy(1/m)
			                   .AlgebraicExpand()
			                   .IsolateVariable(vf)
			                   .Substitute(numerical_vals)
			                   .Substitute(3, 3.0)); // TODO: why do we need to do this?

			// T_f
			Assert.AreEqual(T_f == (3*g - 2*cos(thA)*g)*m,
			                eqs.Substitute(vals)
			                   .Substitute(vf ^ 2, vf_sq)
			                   .EliminateVariables(Ki, Kf, Ugi, Ugf, ΣUi, ΣUf, Ei, Ef, ar_f, ΣFr, vf_sq)
			                   .MultiplyBothSidesBy(1/m)
			                   .AlgebraicExpand()
			                   .IsolateVariable(T_f));
		}
		[TestMethod]
		public void Example_8_4()
		{
			// A 3.00-kg crate slides down a ramp. The ramp is 1.00 m in
			// length and inclined at an angle of 30.0°, as shown in Figure
			// 8.8. The crate starts from rest at the top, experiences a
			// constant frictional force of magnitude 5.00 N, and continues to
			// move a short distance on the flat floor after it leaves the
			// ramp. Use energy methods to determine the speed of the
			// crate at the bottom of the ramp.

			var m = new Symbol("m");
			var yi = new Symbol("yi");
			var yf = new Symbol("yf");
			var vi = new Symbol("vi");
			var vf = new Symbol("vf");
			var Ki = new Symbol("Ki");
			var Kf = new Symbol("Kf");
			var Ugi = new Symbol("Ugi");
			var Ugf = new Symbol("Ugf");
			var ΣUi = new Symbol("ΣUi");
			var ΣUf = new Symbol("ΣUf");
			var Ei = new Symbol("Ei");
			var Ef = new Symbol("Ef");
			var fk = new Symbol("fk");
			var W_f = new Symbol("W_f");
			var ΔE = new Symbol("ΔE");
			var g = new Symbol("g");
			var d = new Symbol("d");
			var θ = new Symbol("θ");

			var eqs = and(yi == d*sin(θ),
			              Ki == m*(vi ^ 2)/2,
			              Kf == m*(vf ^ 2)/2,
			              Ugi == m*g*yi,
			              Ugf == m*g*yf,
			              ΣUi == Ugi,
			              ΣUf == Ugf,
			              W_f == -fk*d,
			              ΔE == W_f,
			              Ei == Ki + ΣUi,
			              Ef == Kf + ΣUf,
			              Ei + ΔE == Ef,
			              m != 0);

			var vals = new List<Equation>
				{
					m == 3.0,
					d == 1.0,
					θ == (30).ToRadians(),
					fk == 5.0,
					vi == 0.0,
					g == 9.8,
					yf == 0.0
				};

			Assert.AreEqual(or(vf == -2.54296414970142, vf == 2.54296414970142),
			                eqs.EliminateVariables(Ei, Ef, ΔE, Ki, Kf, ΣUi, ΣUf, W_f, Ugi, Ugf, yi)
			                   .IsolateVariable(vf)
			                   .LogicalExpand()
			                   .SimplifyEquation()
			                   .SimplifyLogical()
			                   .CheckVariable(m)
			                   .Substitute(vals));
		}
		[TestMethod]
		public void Example_8_5()
		{
			// A child of mass mrides on an irregularly curved slide of
			// height as shown in  Figure 8.9.The child starts
			// from rest at the top.

			// (a) Determine his speed at the bottom, assuming no friction is present.

			// (b) If a force of kinetic friction acts on the child, how
			// much mechanical energy does the system lose? Assume that
			// vf = 3.0 m/s and m = 20.0 kg.

			var m = new Symbol("m");
			var yi = new Symbol("yi");
			var yf = new Symbol("yf");
			var vi = new Symbol("vi");
			var vf = new Symbol("vf");
			var Ki = new Symbol("Ki");
			var Kf = new Symbol("Kf");
			var Ugi = new Symbol("Ugi");
			var Ugf = new Symbol("Ugf");
			var ΣUi = new Symbol("ΣUi");
			var ΣUf = new Symbol("ΣUf");
			var Ei = new Symbol("Ei");
			var Ef = new Symbol("Ef");
			var fk = new Symbol("fk");
			var W_f = new Symbol("W_f");
			var ΔE = new Symbol("ΔE");
			var g = new Symbol("g");
			var d = new Symbol("d");

			var eqs = and(Ki == m*(vi ^ 2)/2,
			              Kf == m*(vf ^ 2)/2,
			              Ugi == m*g*yi,
			              Ugf == m*g*yf,
			              ΣUi == Ugi,
			              ΣUf == Ugf,
			              W_f == -fk*d,
			              ΔE == W_f,
			              Ei == Ki + ΣUi,
			              Ef == Kf + ΣUf,
			              Ei + ΔE == Ef);

			var vals1 = new List<Equation>
				{
					yi == 2.0,
					yf == 0,
					vi == 0,
					fk == 0,
					g == 9.8
				};

			var zeros1 = vals1.Where(eq => eq.b == 0).ToList();

			// vf
			Assert.AreEqual(or(vf == -6.2609903369994111, vf == 6.2609903369994111),
			                eqs.Substitute(zeros1)
			                   .EliminateVariables(Ei, Ef, ΔE, Ki, Kf, ΣUi, ΣUf, W_f, Ugi, Ugf)
			                   .MultiplyBothSidesBy(1/m)
			                   .IsolateVariable(vf)
			                   .Substitute(vals1));

			var vals2 = new List<Equation>
				{
					m == 20.0,
					yi == 2.0,
					yf == 0,
					vi == 0,
					vf == 3.0,
					g == 9.8
				};

			var zeros2 = vals2.Where(eq => eq.b == 0).ToList();

			// ΔE
			Assert.AreEqual(ΔE == -302.0,
			                eqs.Substitute(zeros2)
			                   .EliminateVariables(fk, Ei, Ef, Ki, Kf, ΣUi, ΣUf, Ugi, Ugf, W_f)
			                   .IsolateVariable(ΔE)
			                   .Substitute(vals2));
		}
	}
}
