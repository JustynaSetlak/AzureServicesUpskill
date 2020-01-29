using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace Orders.Hubs
{
    public class OrderHub : Hub<IClientOrderHubActions>
    {
    }

    public interface IClientOrderHubActions
    {
        Task BroadcastMessage(string name, string message);
    }
}
