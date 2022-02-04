using System;
using System.Diagnostics;
using System.Linq;

using static UnityEngine.Debug;

namespace Vanilla.FuncOlympics
{

	public static class FuncOlympics
	{

		public static void Sprint(Action a,
		                          Action b,
		                          int runCount = 100)
		{
			if (a == null ||
			    b == null) return;

			var aTicks = new long[runCount];
			var bTicks = new long[runCount];

			var w = new Stopwatch();

			// These warm-up runs prevent compiler optimizations from giving us strange results.
			// Without warm-ups, some methods perform significantly better or worse on their first calling.

			a();
			b();

			for (var i = 0;
			     i < runCount;
			     i++)
			{
				w.Restart();

				a();

				w.Stop();

				aTicks[i] = w.ElapsedTicks;
			}

			for (var i = 0;
			     i < runCount;
			     i++)
			{
				w.Restart();

				b();

				w.Stop();

				bTicks[i] = w.ElapsedTicks;
			}

			var aResult = aTicks.Average();
			var bResult = bTicks.Average();

			Log($"Method [{(aResult < bResult ? $"A] ({a.Method.Name})" : $"B] ({b.Method.Name})")} was [{Math.Round(value: (aResult < bResult ? (bResult - aResult) / bResult : (aResult - bResult) / aResult) * 100.0f, digits: 1)}%] faster by [{(aResult < bResult ? bResult - aResult : aResult - bResult)}] ticks!\nA average [{aResult}]\nB average [{bResult}]");
		}


		public static void Sprint<InputType>(Action<InputType> a,
		                                     Action<InputType> b,
		                                     InputType input,
		                                     int runCount = 100)
		{
			if (a == null ||
			    b == null) return;

			var aTicks = new long[runCount];
			var bTicks = new long[runCount];

			var w = new Stopwatch();

			// These warm-up runs prevent compiler optimizations from giving us strange results.
			// Without warm-ups, some methods perform significantly better or worse on their first calling.

			a(input);
			b(input);

			for (var i = 0;
			     i < runCount;
			     i++)
			{
				w.Restart();

				a(input);

				w.Stop();

				aTicks[i] = w.ElapsedTicks;
			}

			for (var i = 0;
			     i < runCount;
			     i++)
			{
				w.Restart();

				b(input);

				w.Stop();

				bTicks[i] = w.ElapsedTicks;
			}

			var aResult = aTicks.Average();
			var bResult = bTicks.Average();

			Log($"Method [{(aResult < bResult ? $"A] ({a.Method.Name})" : $"B] ({b.Method.Name})")} was [{Math.Round(value: (aResult < bResult ? (bResult - aResult) / bResult : (aResult - bResult) / aResult) * 100.0f, digits: 1)}%] faster by [{(aResult < bResult ? bResult - aResult : aResult - bResult)}] ticks!\nA average [{aResult}]\nB average [{bResult}]");
		}


		public static void Sprint<Output>(Func<Output> a,
		                                  Func<Output> b,
		                                  int runCount = 100)
		{
			if (a == null ||
			    b == null) return;

			var aTicks = new long[runCount];
			var bTicks = new long[runCount];

			var w = new Stopwatch();

			// These warm-up runs prevent compiler optimizations from giving us strange results.
			// Without warm-ups, some methods perform significantly better or worse on their first calling.

			a();
			b();

			for (var i = 0;
			     i < runCount;
			     i++)
			{
				w.Restart();

				a();

				w.Stop();

				aTicks[i] = w.ElapsedTicks;
			}

			for (var i = 0;
			     i < runCount;
			     i++)
			{
				w.Restart();

				b();

				w.Stop();

				bTicks[i] = w.ElapsedTicks;
			}

			var aResult = aTicks.Average();
			var bResult = bTicks.Average();

			Log($"Method [{(aResult < bResult ? $"A] ({a.Method.Name})" : $"B] ({b.Method.Name})")} was [{Math.Round(value: (aResult < bResult ? (bResult - aResult) / bResult : (aResult - bResult) / aResult) * 100.0f, digits: 1)}%] faster by [{(aResult < bResult ? bResult - aResult : aResult - bResult)}] ticks!\nA average [{aResult}]\nB average [{bResult}]");
		}


		public static void Sprint<Input, Output>(Func<Input, Output> a,
		                                         Func<Input, Output> b,
		                                         Input p,
		                                         int runCount = 100)
		{
			if (a == null ||
			    b == null) return;

			var aTicks = new long[runCount];
			var bTicks = new long[runCount];

			var w = new Stopwatch();

			// These warm-up runs prevent compiler optimizations from giving us strange results.
			// Without warm-ups, some methods perform significantly better or worse on their first calling.

			a(p);
			b(p);

			for (var i = 0;
			     i < runCount;
			     i++)
			{
				w.Restart();

				a(p);

				w.Stop();

				aTicks[i] = w.ElapsedTicks;
			}

			for (var i = 0;
			     i < runCount;
			     i++)
			{
				w.Restart();

				b(p);

				w.Stop();

				bTicks[i] = w.ElapsedTicks;
			}

			var aResult = aTicks.Average();
			var bResult = bTicks.Average();

			Log($"Method [{(aResult < bResult ? $"A] ({a.Method.Name})" : $"B] ({b.Method.Name})")} was [{Math.Round(value: (aResult < bResult ? (bResult - aResult) / bResult : (aResult - bResult) / aResult) * 100.0f, digits: 1)}%] faster by [{(aResult < bResult ? bResult - aResult : aResult - bResult)}] ticks!\nA average [{aResult}]\nB average [{bResult}]");
		}

	}

}