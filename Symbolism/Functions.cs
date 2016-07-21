/* Copyright 2013 Eduardo Cavazos

   Licensed under the Apache License, Version 2.0 (the "License");
   you may not use this file except in compliance with the License.
   You may obtain a copy of the License at

	   http://www.apache.org/licenses/LICENSE-2.0

   Unless required by applicable law or agreed to in writing, software
   distributed under the License is distributed on an "AS IS" BASIS,
   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
   See the License for the specific language governing permissions and
   limitations under the License. */

namespace Symbolism
{
	// ReSharper disable InconsistentNaming
	// These use non-standard naming conventions,
	// but it actually makes the code look nicer,
	// so we're using it.
	public static class Functions
	{
		public static MathObject sqrt(MathObject obj) => new Sqrt(obj);

		public static MathObject and(params MathObject[] ls) => new And(ls);
		public static MathObject or(params MathObject[] ls) => new Or(ls);

		public static MathObject sin(MathObject obj) => new Sin(obj);
		public static MathObject cos(MathObject obj) => new Cos(obj);
		public static MathObject tan(MathObject obj) => new Tan(obj);
		public static MathObject asin(MathObject obj) => new Asin(obj);
		public static MathObject atan(MathObject obj) => new Atan(obj);
	}
}
