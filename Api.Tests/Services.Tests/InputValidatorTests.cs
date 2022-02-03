using Application.Interfaces;
using Application.Validators;
using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Api.Tests.Services.Tests
{
	public class InputValidatorTests
	{
		private readonly IInputValidator _inputValidator;

		public InputValidatorTests()
		{
			_inputValidator = new InputValidator();
		}

		[Theory]
		[InlineData("morning", "1", "2", "3")]
		public void CheckInputArguments_When_DayTime_IsMIssing_Should_ReturnError(params string[] inputs)
		{
			// Act
			Action action = () => _inputValidator.CheckInputArguments(inputs);

			action
				.Should()
				.NotThrow<ArgumentException>();
		}

		[Theory]
		[InlineData("1", "2", "3")]
		[InlineData("moning")]
		[InlineData("night")]
		public void CheckInputArguments_When_Parameter_IsMIssing_Should_ReturnError(params string[] inputs)
		{
			// Act
			Action action = () => _inputValidator.CheckInputArguments(inputs);

			action
				.Should()
				.Throw<ArgumentException>();
		}


	}
}
