namespace Chess_Bot_2
{

	internal class Coord
	{
		public double X;

		public double Y;

		public double W;

		public double H;

		public string name;

		public Coord(string name, double x, double y, double w, double h)
		{
			this.name = name;
			X = x;
			Y = y;
			W = w;
			H = h;
		}

		public override string ToString()
		{
			return name + ": " + X + ", " + Y + ", " + W + ", " + H;
		}
	}
}
