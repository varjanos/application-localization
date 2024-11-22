using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Threading.Tasks;

namespace LocalizationSDK.Connection
{
    public interface ISignalRConnectionManager
    {
        Task ConnectAsync();
        Task DisconnectAsync();
        HubConnection GetConnection();
        event EventHandler? ConnectionLost;
        event EventHandler? Reconnected;
    }
}