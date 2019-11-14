using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ElleRealTimeStd.Shared.Test.Interfaces.Service;
using Grpc.Core;
using MagicOnion.Client;
using UnityEngine;

public class NotLoggedClient : MonoBehaviour, ILoginServiceReceiver
{
    public static NotLoggedClient Instance { get { return _instance; } }
    private static NotLoggedClient _instance;

    private Channel channel;
    private ILoginService streamingClient;
    private string RoomName;

    public NotLoggedClient()
    {
        
    }

    void Awake()
    {
        _instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        try
        {

            this.InitializeClient();
        }
        catch (RpcException ex)
        {
            Debug.Log("Cannot connect to a server.");
        }
        finally
        {
            Debug.Log("Initialized.");
        }
    }
    
    private async void InitializeClient()
    {
        // Initialize the Hub
        this.channel = new Channel("localhost", 12345, ChannelCredentials.Insecure);
        RoomName = Guid.NewGuid().ToString();
        await ConnectAsync(channel);
    }

    private async Task ConnectAsync(Channel grpcChannel)
    {
        this.streamingClient = StreamingHubClient.Connect<ILoginService, ILoginServiceReceiver>(grpcChannel, this);
    }

    public async Task<int> CheckLogin(string formUsername, string formPassword)
    {
        Debug.Log($"Sending username: \"{formUsername}\" AND password \"{formPassword}\".");
        int playerId = await this.streamingClient.JoinAsync(RoomName, formUsername, formPassword);

        Debug.Log($"CheckLogin, playerId: {playerId}.");

        return playerId;
    }

    public async Task Disconnect()
    {
        await this.streamingClient.DisposeAsync();
    }

    // methods send to server.

    public Task LeaveAsync()
    {
        return streamingClient.LeaveAsync();
    }
    
    // dispose client-connection before channel.ShutDownAsync is important!
    public Task DisposeAsync()
    {
        return streamingClient.DisposeAsync();
    }

    protected async void OnDestroy()
    {
        //It seems it doesn't let the client close.
        //await LeaveAsync();
        await this.streamingClient.DisposeAsync();
    }

    // You can watch connection state, use this for retry etc.
    public Task WaitForDisconnect()
    {
        return streamingClient.WaitForDisconnect();
    }

    void ILoginServiceReceiver.OnJoin(int accountId)
    {
        Debug.Log($"Received OnJoin, playerId: {accountId}.");
        if (accountId > 0)
        {
            Login.Instance.HandleAfterLogin(true, accountId);
        }
        else
        {
            Login.Instance.HandleAfterLogin(false, -1);
        }
    }

}
