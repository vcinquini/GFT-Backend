﻿using Application.Interfaces;
using Application.Services;
using Domain;
using FluentAssertions;
using Moq;
using Persistense.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Api.Tests.Services.Tests
{
	public class OrderServiceTests
	{
		private readonly Mock<IInputValidator> _mockInputValidator;
		private readonly Mock<IFoodRepository> _mockFoodRepository;
		private readonly Mock<IOrderRepository> _mockOrderRepository;
		private readonly Mock<IMenuRepository> _mockMenuRepository;

		public OrderServiceTests()
		{
			_mockInputValidator = new Mock<IInputValidator>();
			_mockFoodRepository = new Mock<IFoodRepository>();
			_mockOrderRepository = new Mock<IOrderRepository>();
			_mockMenuRepository = new Mock<IMenuRepository>();
		}

		[Fact]
		public async void ProcessOrder_When_Morning_And_InputOk_Should_ReturnNoError()
		{
			// Arrange
			string input = "morning, 1, 2, 3";
			string output = "Output: Eggs, Toast, Coffe";
			
			Arrange01();

			IOrderService service = new OrderService(_mockInputValidator.Object,
													 _mockFoodRepository.Object,
													 _mockOrderRepository.Object,
													 _mockMenuRepository.Object);

			// Act
			var result = await service.ProcessOrderAsync(input);


			// Assert
			result
				.Should()
				.NotBeEmpty()
				.And
				.NotBeNull();

			result
				.Should()
				.Be(output);
		}

		[Fact]
		public async void ProcessOrder_When_Night_And_InputOk_Should_ReturnNoError()
		{
			// Arrange
			string input = "night, 1, 2, 3, 4";
			string output = "Output: Steak, Potato, Wine, Cake";
			
			Arrange02();

			IOrderService service = new OrderService(_mockInputValidator.Object,
													 _mockFoodRepository.Object,
													 _mockOrderRepository.Object,
													 _mockMenuRepository.Object);

			// Act
			var result = await service.ProcessOrderAsync(input);


			// Assert
			result
				.Should()
				.NotBeEmpty()
				.And
				.NotBeNull();

			result
				.Should()
				.Be(output);
		}

		[Fact]
		public async void ProcessOrder_When_Morning_And_InputIsOutOfOrder_Should_ReturnNoError()
		{
			// Arrange
			string input = "morning, 2, 1, 3";
			string output = "Output: Eggs, Toast, Coffe";

			Arrange01();

			IOrderService service = new OrderService(_mockInputValidator.Object,
													 _mockFoodRepository.Object,
													 _mockOrderRepository.Object,
													 _mockMenuRepository.Object);

			// Act
			var result = await service.ProcessOrderAsync(input);


			// Assert
			result
				.Should()
				.NotBeEmpty()
				.And
				.NotBeNull();

			result
				.Should()
				.Be(output);
		}

		[Fact]
		public async void ProcessOrder_When_Night_And_InputIsOutOfOrder_Should_ReturnNoError()
		{
			// Arrange
			string input = "night, 2, 4, 1, 3";
			string output = "Output: Steak, Potato, Wine, Cake";

			Arrange02();

			IOrderService service = new OrderService(_mockInputValidator.Object,
													 _mockFoodRepository.Object,
													 _mockOrderRepository.Object,
													 _mockMenuRepository.Object);

			// Act
			var result = await service.ProcessOrderAsync(input);


			// Assert
			result
				.Should()
				.NotBeEmpty()
				.And
				.NotBeNull();

			result
				.Should()
				.Be(output);
		}

		[Fact]
		public async void ProcessOrder_When_Morning_And_InputInvalidSelection_Should_ReturnError()
		{
			// Arrange
			string input = "morning, 1, 2, 3, 4";
			string output = "Output: Eggs, Toast, Coffe, Error";

			Arrange03();

			IOrderService service = new OrderService(_mockInputValidator.Object,
													 _mockFoodRepository.Object,
													 _mockOrderRepository.Object,
													 _mockMenuRepository.Object);

			// Act
			var result = await service.ProcessOrderAsync(input);


			// Assert
			result
				.Should()
				.NotBeEmpty()
				.And
				.NotBeNull();

			result
				.Should()
				.Be(output);
		}



		private void Arrange01()
		{
			Order order = new Order()
			{
				DateOrder = DateTime.Today,
				Id = 0,
				Items = new List<Item>() {
					new Item() { Id = 0, DishType = 1, Food = "Eggs", Qty = 1  },
					new Item() { Id = 0, DishType = 2, Food = "Toast", Qty = 1  },
					new Item() { Id = 0, DishType = 3, Food = "Coffe", Qty = 1  } }
			};
			_mockMenuRepository.Setup(m => m.GetAsync(1, 1)).Returns(Task.FromResult(new Menu() { DayTimeId = 1, DishTypeId = 1, FoodId = 1 }));
			_mockMenuRepository.Setup(m => m.GetAsync(1, 2)).Returns(Task.FromResult(new Menu() { DayTimeId = 1, DishTypeId = 2, FoodId = 2 }));
			_mockMenuRepository.Setup(m => m.GetAsync(1, 3)).Returns(Task.FromResult(new Menu() { DayTimeId = 1, DishTypeId = 3, FoodId = 3 }));
			_mockFoodRepository.Setup(m => m.GetAsync(1)).Returns(Task.FromResult(new Food() { Id = 1, Name = "Eggs" }));
			_mockFoodRepository.Setup(m => m.GetAsync(2)).Returns(Task.FromResult(new Food() { Id = 2, Name = "Toast" }));
			_mockFoodRepository.Setup(m => m.GetAsync(3)).Returns(Task.FromResult(new Food() { Id = 3, Name = "Coffee" }));
			_mockOrderRepository.Setup(m => m.AddAsync(It.IsAny<Order>())).Returns(Task.FromResult(order));
		}

		private void Arrange02()
		{
			Order order = new Order()
			{
				DateOrder = DateTime.Now,
				Id = 0,
				Items = new List<Item>() {
					new Item() { Id = 0, DishType = 1, Food = "Steak", Qty = 1  },
					new Item() { Id = 0, DishType = 2, Food = "Potato", Qty = 1  },
					new Item() { Id = 0, DishType = 3, Food = "Wine", Qty = 1  },
					new Item() { Id = 0, DishType = 4, Food = "Cake", Qty = 1  } }
			};
			_mockMenuRepository.Setup(m => m.GetAsync(2, 1)).Returns(Task.FromResult(new Menu() { DayTimeId = 2, DishTypeId = 1, FoodId = 4 }));
			_mockMenuRepository.Setup(m => m.GetAsync(2, 2)).Returns(Task.FromResult(new Menu() { DayTimeId = 2, DishTypeId = 2, FoodId = 5 }));
			_mockMenuRepository.Setup(m => m.GetAsync(2, 3)).Returns(Task.FromResult(new Menu() { DayTimeId = 2, DishTypeId = 3, FoodId = 6 }));
			_mockMenuRepository.Setup(m => m.GetAsync(2, 4)).Returns(Task.FromResult(new Menu() { DayTimeId = 2, DishTypeId = 4, FoodId = 7 }));
			_mockFoodRepository.Setup(m => m.GetAsync(4)).Returns(Task.FromResult(new Food() { Id = 4, Name = "Steak" }));
			_mockFoodRepository.Setup(m => m.GetAsync(5)).Returns(Task.FromResult(new Food() { Id = 5, Name = "Potato" }));
			_mockFoodRepository.Setup(m => m.GetAsync(6)).Returns(Task.FromResult(new Food() { Id = 6, Name = "Wine" }));
			_mockFoodRepository.Setup(m => m.GetAsync(7)).Returns(Task.FromResult(new Food() { Id = 7, Name = "Cake" }));
			_mockOrderRepository.Setup(m => m.AddAsync(It.IsAny<Order>())).Returns(Task.FromResult(order));
		}

		private void Arrange03()
		{
			Order order = new Order()
			{
				DateOrder = DateTime.Today,
				Id = 0,
				Items = new List<Item>() {
					new Item() { Id = 0, DishType = 1, Food = "Eggs", Qty = 1  },
					new Item() { Id = 0, DishType = 2, Food = "Toast", Qty = 1  },
					new Item() { Id = 0, DishType = 3, Food = "Coffe", Qty = 1  },
					new Item() { Id = 0, DishType = 4, Food = "Error", Qty = 0  } }
			};
			_mockMenuRepository.Setup(m => m.GetAsync(1, 1)).Returns(Task.FromResult(new Menu() { DayTimeId = 1, DishTypeId = 1, FoodId = 1 }));
			_mockMenuRepository.Setup(m => m.GetAsync(1, 2)).Returns(Task.FromResult(new Menu() { DayTimeId = 1, DishTypeId = 2, FoodId = 2 }));
			_mockMenuRepository.Setup(m => m.GetAsync(1, 3)).Returns(Task.FromResult(new Menu() { DayTimeId = 1, DishTypeId = 3, FoodId = 3 }));
			_mockMenuRepository.Setup(m => m.GetAsync(1, 4)).Returns(Task.FromResult(new Menu() { DayTimeId = 1, DishTypeId = 4, FoodId = 7 }));
			_mockFoodRepository.Setup(m => m.GetAsync(1)).Returns(Task.FromResult(new Food() { Id = 1, Name = "Eggs" }));
			_mockFoodRepository.Setup(m => m.GetAsync(2)).Returns(Task.FromResult(new Food() { Id = 2, Name = "Toast" }));
			_mockFoodRepository.Setup(m => m.GetAsync(3)).Returns(Task.FromResult(new Food() { Id = 3, Name = "Coffee" }));
			_mockOrderRepository.Setup(m => m.AddAsync(It.IsAny<Order>())).Returns(Task.FromResult(order));
		}
	}
}
