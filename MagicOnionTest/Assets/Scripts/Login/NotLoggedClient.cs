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
    private Channel channel;
    private ILoginService streamingClient;
    public static string formUsername = "";
    public static string formPassword = "";

    private bool isJoin;
    private bool isSelfDisConnected;

    public static bool CanConnect = false;
    public static int PlayerID = -1;

    private static NotLoggedClient _instance;

    void Awake()
    {
        _instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (CanConnect)
        {
            PlayerID = -1;
            try
            {
                PlayerID = this.InitializeClient().Result;
                LeaveAsync();
                DisposeAsync();
            }
            catch (RpcException ex)
            {
                Debug.Log("Cannot connect to a server.");
            }
            finally
            {
                Debug.Log("Initialized.");
                
                CanConnect = false;
            }
        }
    }

    private async Task<int> InitializeClient()
    {
        // Initialize the Hub
        this.channel = new Channel("localhost", 12345, ChannelCredentials.Insecure);
        int playerId = await ConnectAsync(channel, "test");
        this.RegisterDisconnectEvent(streamingClient);
        return playerId;
    }

    private async Task<int> ConnectAsync(Channel grpcChannel, string roomName)
    {
        this.streamingClient = StreamingHubClient.Connect<ILoginService, ILoginServiceReceiver>(grpcChannel, this);

        var playerId = await this.streamingClient.JoinAsync(formUsername, formPassword);

        return playerId;
    }

    private async void RegisterDisconnectEvent(ILoginService streamingClient)
    {
        try
        {
            // you can wait disconnected event
            await streamingClient.WaitForDisconnect();
        }
        finally
        {
            // try-to-reconnect? logging event? close? etc...
            Debug.Log("disconnected server.");

            if (this.isSelfDisConnected)
            {
                // there is no particular meaning
                await Task.Delay(2000);

                // reconnect
                this.ReconnectServer();
            }
        }
    }

    private void ReconnectServer()
    {
        this.streamingClient = StreamingHubClient.Connect<ILoginService, ILoginServiceReceiver>(this.channel, this);
        this.RegisterDisconnectEvent(streamingClient);
        Debug.Log("Reconnected server.");

        this.isSelfDisConnected = false;
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

    void ILoginServiceReceiver.OnJoin()
    {
        
    }

}
