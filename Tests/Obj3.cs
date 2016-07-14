using Symbolism;
using static Symbolism.Functions;

namespace Tests
{
	public class Obj3
	{
		public Symbol ΣFx;
		public Symbol ΣFy;
		public Symbol m;
		public Symbol ax;
		public Symbol ay;

		public Symbol F1, F2, F3;
		public Symbol th1, th2, th3;
		public Symbol F1x, F2x, F3x;
		public Symbol F1y, F2y, F3y;

		public Obj3(string name)
		{
			ΣFx = new Symbol($"{name}.ΣFx");
			ΣFy = new Symbol($"{name}.ΣFy");

			m = new Symbol($"{name}.m");

			ax = new Symbol($"{name}.ax");
			ay = new Symbol($"{name}.ay");

			F1 = new Symbol($"{name}.F1");
			F2 = new Symbol($"{name}.F2");
			F3 = new Symbol($"{name}.F3");

			th1 = new Symbol($"{name}.th1");
			th2 = new Symbol($"{name}.th2");
			th3 = new Symbol($"{name}.th3");

			F1x = new Symbol($"{name}.F1x");
			F2x = new Symbol($"{name}.F2x");
			F3x = new Symbol($"{name}.F3x");

			F1y = new Symbol($"{name}.F1y");
			F2y = new Symbol($"{name}.F2y");
			F3y = new Symbol($"{name}.F3y");
		}

		public And Equations()
		{
			return new And(

				F1x == F1 * cos(th1),
				F1y == F1 * sin(th1),

				F2x == F2 * cos(th2),
				F2y == F2 * sin(th2),

				F3x == F3 * cos(th3),
				F3y == F3 * sin(th3),

				ΣFx == F1x + F2x + F3x,
				ΣFx == m * ax,

				ΣFy == F1y + F2y + F3y,
				ΣFy == m * ay

				);
		}
	}
}