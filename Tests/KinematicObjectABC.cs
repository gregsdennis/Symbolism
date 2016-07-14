using Symbolism;
using static Symbolism.Functions;

namespace Tests
{
	public class KinematicObjectABC
	{
		public Symbol xA, yA, vxA, vyA, vA, thA;
		public Symbol xB, yB, vxB, vyB, vB, thB;
		public Symbol xC, yC, vxC, vyC, vC, thC;

		public Symbol tAB, tBC, tAC;

		public Symbol ax, ay;

		public KinematicObjectABC(string name)
		{
			xA = new Symbol($"{name}.xA");
			yA = new Symbol($"{name}.yA");

			vxA = new Symbol($"{name}.vxA");
			vyA = new Symbol($"{name}.vyA");

			vA = new Symbol($"{name}.vA");
			thA = new Symbol($"{name}.thA");


			xB = new Symbol($"{name}.xB");
			yB = new Symbol($"{name}.yB");

			vxB = new Symbol($"{name}.vxB");
			vyB = new Symbol($"{name}.vyB");

			vB = new Symbol($"{name}.vB");
			thB = new Symbol($"{name}.thB");


			xC = new Symbol($"{name}.xC");
			yC = new Symbol($"{name}.yC");

			vxC = new Symbol($"{name}.vxC");
			vyC = new Symbol($"{name}.vyC");

			vC = new Symbol($"{name}.vC");
			thC = new Symbol($"{name}.thC");

			tAB = new Symbol($"{name}.tAB");
			tBC = new Symbol($"{name}.tBC");
			tAC = new Symbol($"{name}.tAC");

			ax = new Symbol($"{name}.ax");
			ay = new Symbol($"{name}.ay");
		}

		public And EquationsAB() =>

			new And(

				vxB == vxA + ax * tAB,
				vyB == vyA + ay * tAB,

				xB == xA + vxA * tAB + ax * (tAB ^ 2) / 2,
				yB == yA + vyA * tAB + ay * (tAB ^ 2) / 2
                
				);

		public And EquationsBC() =>

			new And(

				vxC == vxB + ax * tBC,
				vyC == vyB + ay * tBC,

				xC == xB + vxB * tBC + ax * (tBC ^ 2) / 2,
				yC == yB + vyB * tBC + ay * (tBC ^ 2) / 2

				);

		public And EquationsAC() =>

			new And(

				vxC == vxA + ax * tAC,
				vyC == vyA + ay * tAC,

				xC == xA + vxA * tAC + ax * (tAC ^ 2) / 2,
				yC == yA + vyA * tAC + ay * (tAC ^ 2) / 2

				);

		public And TrigEquationsA() =>
        
			new And(

				vxA == vA * cos(thA),
				vyA == vA * sin(thA)

				);
        
	}
}