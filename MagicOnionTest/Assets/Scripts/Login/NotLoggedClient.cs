using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ElleRealTimeStd.Shared.Test.Interfaces.Service;
using Grpc.Core;
using MagicOnion.Client;
using UnityEngine;

public class NotLoggedClient : /*MonoBehaviour, */ILoginServiceReceiver
{
    public static NotLoggedClient Instance { get { return _instance; } }
    private Channel channel;
    private ILoginService streamingClient;
    private static NotLoggedClient _instance;
    private string RoomName;
    private bool isJoin;
    private bool isSelfDisConnected;

    private string formUsername;
    private string formPassword;

    public NotLoggedClient(string username, string password)
    {
        formUsername = username;
        formPassword = password;
        _instance = this;
    }

    public async void Connect()
    {
        this.channel = new Channel("localhost", 12345, ChannelCredentials.Insecure);
        RoomName = Guid.NewGuid().ToString();
        int playerId = await ConnectAsync(channel);
        this.RegisterDisconnectEvent(streamingClient);

        if (playerId > 0)
        {
            Login.HandleAfterLogin(true, playerId);
        }
        else
        {
            Login.HandleAfterLogin(false, -1);
        }
    }

    private async Task<int> ConnectAsync(Channel grpcChannel)
    {
        this.streamingClient = StreamingHubClient.Connect<ILoginService, ILoginServiceReceiver>(grpcChannel, this);
        int playerId = await this.streamingClient.JoinAsync(RoomName, formUsername, formPassword);
        //(this as ILoginServiceReceiver).OnJoin(playerId);
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
        }
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

    public async Task OnDestroy()
    {
        await LeaveAsync();
        await DisposeAsync();
    }


    void ILoginServiceReceiver.OnJoin(int accountId)
    {
        Debug.Log($"[NotLoggedClient/OnJoin] Received OnJoin, playerId: {accountId}.");
        if (accountId > 0)
        {
            Login.HandleAfterLogin(true, accountId);
        }
        else
        {
            Login.HandleAfterLogin(false, -1);
        }
    }

}
