// Domain/Factories/OrderFactory.cs
using System;
using System.Collections.Generic;
using System.Linq;
using KBDTypeServer.Domain.Entities.OrderEntity;
using KBDTypeServer.Domain.Enums;
using KBDTypeServer.Domain.ValueObjects;

namespace KBDTypeServer.Domain.Factories
{
    public static class OrderFactory
    {
        public static Order CreateOrder(OrderInitData orderInitData, int userId)
        {
            static string Normalize(string? value, string defaultValue = "Не вказано")
                => string.IsNullOrWhiteSpace(value) ? defaultValue : value.Trim();

            static string Require(string value, string fieldName)
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException($"{fieldName} is required.");
                return value.Trim();
            }

            var order = new Order
            {
                UserId = userId,
                FirstName = Require(orderInitData.FirstName, nameof(orderInitData.FirstName)),
                LastName = Require(orderInitData.LastName, nameof(orderInitData.LastName)),
                Email = Normalize(orderInitData.Email),
                PhoneNumber = Require(orderInitData.PhoneNumber, nameof(orderInitData.PhoneNumber)),
                Region = Require(orderInitData.Region, nameof(orderInitData.Region)),
                City = Require(orderInitData.City, nameof(orderInitData.City)),
                Street = Require(orderInitData.Street, nameof(orderInitData.Street)),
                Building = Require(orderInitData.Building, nameof(orderInitData.Building)),
                Apartment = Normalize(orderInitData.Apartment),
                PostalCode = Require(orderInitData.PostalCode, nameof(orderInitData.PostalCode)),
                Items = new List<OrderItem>(orderInitData.Items),
                Comment = Normalize(orderInitData.Comment),
                PaymentId = null,
                Status = OrderStatus.Created,
                CreatedAt = DateTime.UtcNow,
                PaidAt = null,
                ShippedAt = null,
                DeliveredAt = null,
                CancelledAt = null
            };
            order.Subtotal = order.Items.Sum(i => i.Price * i.Quantity);
            order.TotalPrice = order.Subtotal;

            return order;
        }
    }
}
    

