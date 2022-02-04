using Application.Interfaces;
using Application.Validators;
using Domain;
using FluentAssertions;
using System;
using System.Collections.Generic;
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
		public void CheckInputArguments_When_DayTime_IsMIssing_Should_ReturnError(string daytime, params string[] dishes)
		{
			// Arrange
			OrderDTO inputs = new OrderDTO() { DayTime = daytime, Items = new List<string>(dishes) };

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
		public void CheckInputArguments_When_Parameter_IsMIssing_Should_ReturnError(string daytime, params string[] dishes)
		{
			// Arrange
			OrderDTO inputs = new OrderDTO() { DayTime = daytime, Items = new List<string>(dishes) };

			// Act
			Action action = () => _inputValidator.CheckInputArguments(inputs);

			action
				.Should()
				.Throw<ArgumentException>();
		}


	}
}
