using Application.Interfaces;
using Domain;
using Domain.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Application.Validators
{
	public class InputValidator : IInputValidator
	{
		private readonly List<string> daytimes = new List<string> { Constants.MORNING, Constants.NIGHT };

		public InputValidator()
		{
		}

		public void CheckInputArguments(OrderDTO inputs)
		{
			// check if parameters are ther
			if (String.IsNullOrEmpty(inputs.DayTime) && inputs.Items == null)
				throw new ArgumentException("Invalid parameters!");

			// check if times of day are correct;
			if (!daytimes.Contains(inputs.DayTime, StringComparer.OrdinalIgnoreCase))
				throw new ArgumentException("Invalid time of day!");

			// check if thare are dishes after time of day
			if (inputs.Items == null || inputs.Items.Count == 0)
				throw new ArgumentException("Invalid input argument! You must select at least one dish type.");

			// check if any dish is empty
			if(inputs.Items.Exists(x => String.IsNullOrEmpty(x)))
				throw new ArgumentException("Invalid selection argument!");
		}
	}
}
