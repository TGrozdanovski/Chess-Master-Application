using System;

namespace Chess_Bot_2
{


	internal class __SH
	{
		private static int _m = 0;

		private static int _d = 0;

		public static void _gg(int p)
		{
			Random random = new Random();
			_d = random.Next(1000, 999999);
			_m = _d * p;
		}

		public static void __()
		{
			_m -= _d;
		}

		public static void _yt(int _6)
		{
			_m = _6;
		}

		public static int _A()
		{
			return _m / _d;
		}

		public static bool __z()
		{
			return _m < _d;
		}
	}
}