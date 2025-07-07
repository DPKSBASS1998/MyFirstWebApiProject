using Elit.BuyerService;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KBDTypeServer.Application.Services.ElitApiServices
{
    public interface IElitApiService
    {
        Task<IEnumerable<itemInfo>> GetItemInfoAsync(string itemNo, int quantity);
        Task<IEnumerable<item>> GetListOfItemsAsync(string groupCode);
    }
}