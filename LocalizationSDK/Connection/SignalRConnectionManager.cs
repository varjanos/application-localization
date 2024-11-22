using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace LocalizationSDK.Connection
{
    public class SignalRConnectionManager
    {
        // Lazy initialization of the HubConnection, only created when needed
        private Lazy<HubConnection> _lazyConnection;
        private readonly ILogger<SignalRConnectionManager> _logger;
        private readonly int _maxRetryAttempts = 5; // Maximum retry attempts for reconnecting
        private readonly TimeSpan _retryDelay = TimeSpan.FromSeconds(5); // Delay between retry attempts

        // Events to notify when the connection is lost or reconnected
        public event EventHandler? ConnectionLost;
        public event EventHandler? Reconnected;

        public SignalRConnectionManager(string hubUrl, ILogger<SignalRConnectionManager> logger)
        {
            // Initialize the HubConnection with the provided URL
            _lazyConnection = new Lazy<HubConnection>(() =>
                new HubConnectionBuilder()
                    .WithUrl(hubUrl, options =>
                    {
                        options.AccessTokenProvider = async () => await GetJwtToken();
                    })
                    .Build());
            _logger = logger;

            // Register an event handler to handle connection closure and attempt reconnection
            _lazyConnection.Value.Closed += async (exception) => await OnConnectionClosed(exception);
        }

        // Connect to the SignalR Hub
        public async Task ConnectAsync()
        {
            await AttemptConnectionAsync();
        }

        // Disconnect from the SignalR Hub
        public async Task DisconnectAsync()
        {
            try
            {
                // Disconnect only if the connection has been created
                if (_lazyConnection.IsValueCreated)
                {
                    await _lazyConnection.Value.StopAsync();
                    _logger.LogInformation("Disconnected from SignalR Hub.");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while disconnecting from SignalR Hub.");
            }
        }

        // Handle connection closure and attempt reconnection
        private async Task OnConnectionClosed(Exception? exception)
        {
            _logger.LogWarning("Connection closed. Reason: {Reason}", exception?.Message);
            ConnectionLost?.Invoke(this, EventArgs.Empty);
            await AttemptConnectionAsync();
        }

        // Attempt to connect to the SignalR Hub with retry logic
        private async Task AttemptConnectionAsync()
        {
            int retryCount = 0;
            while (retryCount < _maxRetryAttempts)
            {
                try
                {
                    await _lazyConnection.Value.StartAsync();
                    _logger.LogInformation("Connected to SignalR Hub.");
                    Reconnected?.Invoke(this, EventArgs.Empty);
                    break; // Exit loop if connection is successful
                }
                catch (Exception ex)
                {
                    retryCount++;
                    _logger.LogError(ex, "Attempt {Attempt} failed to connect to SignalR Hub.", retryCount);
                    await Task.Delay(_retryDelay); // Wait before retrying
                }
            }

            if (retryCount == _maxRetryAttempts)
            {
                _logger.LogError("Max retry attempts reached. Failed to connect to SignalR Hub.");
            }
        }

        // Get the current HubConnection instance
        public HubConnection GetConnection() => _lazyConnection.Value;

        // Method to get JWT token (to be implemented)
        private async Task<string> GetJwtToken()
        {
            // TODO: Implement token retrieval logic
            return await Task.FromResult("your_jwt_token_here");
        }
    }
}