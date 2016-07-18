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
			throw new NotImplementedException();
		}
		[TestMethod]
		public void Example_5_4()
		{
			throw new NotImplementedException();
		}
		[TestMethod]
		public void Example_5_9()
		{
			throw new NotImplementedException();
		}
		[TestMethod]
		public void Example_5_10()
		{
			throw new NotImplementedException();
		}
		[TestMethod]
		public void Example_5_10_UsingObject()
		{
			throw new NotImplementedException();
		}
		[TestMethod]
		public void Example_5_12()
		{
			throw new NotImplementedException();
		}
		[TestMethod]
		public void Example_5_13()
		{
			throw new NotImplementedException();
		}
		[TestMethod]
		public void Example_5_14()
		{
			throw new NotImplementedException();
		}
		[TestMethod]
		public void Example_5_14_UsingObject()
		{
			throw new NotImplementedException();
		}
		[TestMethod]
		public void Example_5_25()
		{
			throw new NotImplementedException();
		}
		[TestMethod]
		public void Example_5_25_UsingObject()
		{
			throw new NotImplementedException();
		}
		[TestMethod]
		public void Example_5_31()
		{
			throw new NotImplementedException();
		}
		[TestMethod]
		public void Example_5_31_UsingObject()
		{
			throw new NotImplementedException();
		}
		[TestMethod]
		public void Example_5_55()
		{
			throw new NotImplementedException();
		}
		[TestMethod]
		public void Example_5_59()
		{
			throw new NotImplementedException();
		}
		[TestMethod]
		public void Example_5_69()
		{
			throw new NotImplementedException();
		}
		[TestMethod]
		public void Example_7_7()
		{
			throw new NotImplementedException();
		}
		[TestMethod]
		public void Example_7_8()
		{
			throw new NotImplementedException();
		}
		[TestMethod]
		public void Example_7_11()
		{
			throw new NotImplementedException();
		}
		[TestMethod]
		public void Example_7_3()
		{
			throw new NotImplementedException();
		}
		[TestMethod]
		public void Example_7_23()
		{
			throw new NotImplementedException();
		}
		[TestMethod]
		public void Example_7_35()
		{
			throw new NotImplementedException();
		}
		[TestMethod]
		public void Example_7_39()
		{
			throw new NotImplementedException();
		}
		[TestMethod]
		public void Example_7_41()
		{
			throw new NotImplementedException();
		}
		[TestMethod]
		public void Example_7_55()
		{
			throw new NotImplementedException();
		}
		[TestMethod]
		public void Example_8_2()
		{
			throw new NotImplementedException();
		}
		[TestMethod]
		public void Example_8_3()
		{
			throw new NotImplementedException();
		}
		[TestMethod]
		public void Example_8_4()
		{
			throw new NotImplementedException();
		}
		[TestMethod]
		public void Example_8_5()
		{
			throw new NotImplementedException();
		}
	}
}
