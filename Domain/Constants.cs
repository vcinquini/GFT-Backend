using System;

namespace Domain
{
	public class Constants
	{
		public const String MORNING = "morning";
		public const String NIGHT = "night";
		public const String ERROR = "Error";

		public const int MORNING_ID = 1;
		public const int NIGHT_ID = 2;
		public const int ERROR_ID = 99;

		public const int ENTREE_ID = 1;
		public const int SIDE_ID = 2;
		public const int DRINK_ID = 3;
		public const int DESSERT_ID = 4;

		public static readonly int[] DISHTYPES = new int[] { ENTREE_ID, SIDE_ID, DRINK_ID, DESSERT_ID };
	}
}
