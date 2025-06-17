// Domain/Factories/OrderFactory.cs
using System;
using System.Collections.Generic;
using System.Linq;
using KBDTypeServer.Domain.Entities.OrderEntity;
using KBDTypeServer.Domain.Enums;

namespace KBDTypeServer.Domain.Factories
{
    public static class OrderFactory
    {
        public static Order Create(
            /// <summary>
            /// Id of user who placed the order. Can be nullable if order is placed by guest.
            /// </summary>
            string userId,
            /// <summary>
            /// Account data of user who placed the order.
            /// param name="firstName">First name of user.</param>
            /// param name="lastName">Last name of user.</param>
            /// param name="email">Email of user. Can be nullable if order is placed by guest.</param>
            /// param name="phoneNumber">Phone number of user.</param>
            /// </summary>
            string firstName,
            string lastName,
            string? email,
            string phoneNumber,
            /// <summary>
            /// Address details for the order.
            /// param name="region">Region of delivery.</param>
            /// param name="city">City of delivery.</param>
            /// param name="street">Street of delivery.</param>
            /// param name="building">Building of delivery.</param>
            /// param name="apartment">Apartment of delivery. Can be nullable.</param>
            /// param name="postalCode">Postal code of delivery.</param>
            /// </summary>
            string region,
            string city,
            string street,
            string building,
            string? apartment,
            string postalCode,
            /// <summary>
            /// Items in the order.
            /// param name="items">List of OrderItem entities.</param>
            /// </summary>
            List<OrderItem> items,
            /// <summary>
            /// Comment or note about the order. Can be nullable.
            /// param name="comment">Comment for the order.</param>
            /// </summary>
            string? comment
            
            // Якщо в майбутньому буде логіка для shippingPrice і discount, то можна додати їх як параметри
            // Окремо теж
            //decimal shippingPrice = 0m,
            //decimal discount = 0m
            )
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
                FirstName = Require(firstName, nameof(firstName)),
                LastName = Require(lastName, nameof(lastName)),
                Email = Normalize(email),
                PhoneNumber = Require(phoneNumber, nameof(phoneNumber)),
                Region = Require(region, nameof(region)),
                City = Require(city, nameof(city)),
                Street = Require(street, nameof(street)),
                Building = Require(building, nameof(building)),
                Apartment = Normalize(apartment),
                PostalCode = Require(postalCode, nameof(postalCode)),
                Items = new List<OrderItem>(items),
                Comment = Normalize(comment),
                PaymentId = null,
                Status = OrderStatus.Created,
                CreatedAt = DateTime.UtcNow,
                PaidAt = null,
                ShippedAt = null,
                DeliveredAt = null,
                CancelledAt = null
            };
            order.Subtotal = order.Items.Sum(i => i.Price * i.Quantity);
            // В майбутньому може бути логіка для shippingPrice і discount
            order.TotalPrice = order.Subtotal;

            return order;
        }
    }
}
    

