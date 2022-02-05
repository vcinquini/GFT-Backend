using System;

namespace Domain
{
	public class Constants
	{
		public const String MORNING = "morning";
		public const String NIGHT = "night";
		public const String ERROR = "Error";

		public const int ERROR_ID = 99;

		public enum DayTime
        {
			Morning = 1,
			Night = 2
		}

		public enum DishType
		{
			Entree = 1,
			Side = 2,
			Drink = 3,
			Dessert = 4,
		}


	}
}
