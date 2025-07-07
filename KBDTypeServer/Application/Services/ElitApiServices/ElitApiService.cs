using Elit.BuyerService; // Додано для BuyerClient
using Microsoft.Extensions.Configuration; // Додано для IConfiguration
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.ServiceModel; // Додано для CommunicationState

namespace KBDTypeServer.Application.Services.ElitApiServices
{
    public class ElitApiService : IElitApiService
    {
        private readonly IConfiguration _configuration;
        private string _company;
        private string _login;
        private string _password;

        // Сервіс отримує IConfiguration через конструктор
        public ElitApiService(IConfiguration configuration)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _company = _configuration["ElitApi:Company"];
            _login = _configuration["ElitApi:Login"];
            _password = _configuration["ElitApi:Password"];
        }

        public async Task<IEnumerable<itemInfo>> GetItemInfoAsync(string itemNo, int quantity)
        {
            if (string.IsNullOrEmpty(_company) || string.IsNullOrEmpty(_login) || string.IsNullOrEmpty(_password))
            {
                throw new InvalidOperationException("API settings for Elit are not configured in appsettings.json.");
            }

            var client = new BuyerClient();
            try
            {
                // Використовуємо отримані з конфігурації змінні
                var response = await client.getItemInfoAsync(
                    _company,
                    _login,
                    _password,
                    itemNo,
                    quantity);

                return response.@return;
            }
            finally
            {
                if (client.State != CommunicationState.Closed)
                {
                    await client.CloseAsync();
                }
            }
        }

        public async Task<IEnumerable<item>> GetListOfItemsAsync(string groupCode)
        {
            throw new NotImplementedException("This method is not implemented yet.");
        }
    }
}