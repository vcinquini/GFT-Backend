using Application.Interfaces;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Validators
{
	public class InputValidator : IInputValidator
	{
		private readonly List<string> daytimes = new List<string> { Constants.MORNING, Constants.NIGHT };

		public InputValidator()
		{
		}

		public void CheckInputArguments(string[] inputs)
		{
			// 1st: check if times of day are correct;
			if (!daytimes.Contains(inputs[0], StringComparer.OrdinalIgnoreCase))
				throw new ArgumentException("Invalid time of day!");

			// 2nd: check if thare are dishes after time of day
			if (inputs.Length < 2)
				throw new ArgumentException("Invalid input argument! You must select at least one dish type.");
		}

	}
}
