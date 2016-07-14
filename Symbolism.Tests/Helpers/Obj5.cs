using Symbolism;
using static Symbolism.Functions;

namespace Tests
{
	public class Obj5
	{
		public Symbol ΣFx;
		public Symbol ΣFy;
		public Symbol m;
		public Symbol ax;
		public Symbol ay;

		public Symbol F1, F2, F3, F4, F5;
		public Symbol th1, th2, th3, th4, th5;
		public Symbol F1x, F2x, F3x, F4x, F5x;
		public Symbol F1y, F2y, F3y, F4y, F5y;

		public Obj5(string name)
		{
			ΣFx = new Symbol($"{name}.ΣFx");
			ΣFy = new Symbol($"{name}.ΣFy");

			m = new Symbol($"{name}.m");

			ax = new Symbol($"{name}.ax");
			ay = new Symbol($"{name}.ay");

			F1 = new Symbol($"{name}.F1");
			F2 = new Symbol($"{name}.F2");
			F3 = new Symbol($"{name}.F3");
			F4 = new Symbol($"{name}.F4");
			F5 = new Symbol($"{name}.F5");

			th1 = new Symbol($"{name}.th1");
			th2 = new Symbol($"{name}.th2");
			th3 = new Symbol($"{name}.th3");
			th4 = new Symbol($"{name}.th4");
			th5 = new Symbol($"{name}.th5");

			F1x = new Symbol($"{name}.F1x");
			F2x = new Symbol($"{name}.F2x");
			F3x = new Symbol($"{name}.F3x");
			F4x = new Symbol($"{name}.F4x");
			F5x = new Symbol($"{name}.F5x");

			F1y = new Symbol($"{name}.F1y");
			F2y = new Symbol($"{name}.F2y");
			F3y = new Symbol($"{name}.F3y");
			F4y = new Symbol($"{name}.F4y");
			F5y = new Symbol($"{name}.F5y");
		}

		public And Equations()
		{
			return new And(F1x == F1*cos(th1),
			               F1y == F1*sin(th1),

			               F2x == F2*cos(th2),
			               F2y == F2*sin(th2),

			               F3x == F3*cos(th3),
			               F3y == F3*sin(th3),

			               F4x == F4*cos(th4),
			               F4y == F4*sin(th4),

			               F5x == F5*cos(th5),
			               F5y == F5*sin(th5),

			               ΣFx == F1x + F2x + F3x + F4x + F5x,
			               ΣFx == m*ax,

			               ΣFy == F1y + F2y + F3y + F4y + F5y,
			               ΣFy == m*ay);
		}
	}
}