using System.ComponentModel.DataAnnotations;

namespace KBDTypeServer.Application.DTOs
{
    /// <summary>
    /// ДТО для адреси доставки
    /// </summary>
    public class AddressDto
    {
        /// <summary>Область</summary>
        [MaxLength(100)]
        public string Region { get; set; }

        /// <summary>Місто</summary>
        [MaxLength(100)]
        public string City { get; set; }

        /// <summary>Вулиця</summary>
        [MaxLength(150)]
        public string Street { get; set; }

        /// <summary>Номер квартири/будинку</summary>
        [MaxLength(20)]
        public string Apartment { get; set; }

        /// <summary>Поштовий індекс</summary>
        [MaxLength(20)]
        public string PostalCode { get; set; }
    }
}
