using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using ElleRealTimeStd.Shared.Test.Entities.StreamingHub.Player;
using ElleRealTimeStd.Shared.Test.Interfaces.StreamingHub;
using Grpc.Core;
using MagicOnion.Client;
using UnityEngine;

public class InitClient : MonoBehaviour, IGamingHubReceiver
{
    private Channel channel;
    private IGamingHub streamingClient;
    Dictionary<string, GameObject> players = new Dictionary<string, GameObject>();

    private bool isJoin;
    private bool isSelfDisConnected;

    public GameObject myModel;

    // Start is called before the first frame update
    void Start()
    {
        this.InitializeClient();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private async void InitializeClient()
    {
        // Initialize the Hub
        this.channel = new Channel("localhost", 12345, ChannelCredentials.Insecure);
        // for SSL/TLS connection
        //var serverCred = new SslCredentials(File.ReadAllText(Path.Combine(Application.streamingAssetsPath, "server.crt")));
        //this.channel = new Channel("test.example.com", 12345, serverCred);
        GameObject player = await ConnectAsync(channel, "test", "Elle");
        //this.streamingClient = StreamingHubClient.Connect<IGamingHub, IGamingHubReceiver>(this.channel, this);
        this.RegisterDisconnectEvent(streamingClient);
        //this.client = MagicOnionClient.Create<IChatService>(this.channel);
    }

    private async Task<GameObject> ConnectAsync(Channel grpcChannel, string roomName, string playerName)
    {
        this.streamingClient = StreamingHubClient.Connect<IGamingHub, IGamingHubReceiver>(grpcChannel, this);

        var roomPlayers = await this.streamingClient.JoinAsync(roomName, playerName, Vector3.zero, Quaternion.identity);
        foreach (var player in roomPlayers)
        {
            (this as IGamingHubReceiver).OnJoin(player);
        }

        return players[playerName];
    }

    private async void RegisterDisconnectEvent(IGamingHub streamingClient)
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
        this.streamingClient = StreamingHubClient.Connect<IGamingHub, IGamingHubReceiver>(this.channel, this);
        this.RegisterDisconnectEvent(streamingClient);
        Debug.Log("Reconnected server.");

        this.isSelfDisConnected = false;
    }

    // methods send to server.

    public Task LeaveAsync()
    {
        return streamingClient.LeaveAsync();
    }

    public Task MoveAsync(Vector3 position, Quaternion rotation)
    {
        return streamingClient.MoveAsync(position, rotation);
    }

    // dispose client-connection before channel.ShutDownAsync is important!
    public Task DisposeAsync()
    {
        return streamingClient.DisposeAsync();
    }

    // You can watch connection state, use this for retry etc.
    public Task WaitForDisconnect()
    {
        return streamingClient.WaitForDisconnect();
    }

    void IGamingHubReceiver.OnJoin(Player player)
    {
        Debug.Log("Join Player:" + player.Name);
		
		Vector3 alexVector3 = new Vector3(18.886f, 2.27f, 28.98f);

        Instantiate(myModel, alexVector3, Quaternion.identity);
        myModel.AddComponent<Rigidbody>();

        var cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        cube.name = player.Name;
        cube.transform.SetPositionAndRotation(player.Position, player.Rotation);
        players[player.Name] = cube;
    }

    void IGamingHubReceiver.OnLeave(Player player)
    {
        Debug.Log("Leave Player:" + player.Name);

        if (players.TryGetValue(player.Name, out var cube))
        {
            GameObject.Destroy(cube);
        }
    }

    void IGamingHubReceiver.OnMove(Player player)
    {
        Debug.Log("Move Player:" + player.Name);

        if (players.TryGetValue(player.Name, out var cube))
        {
            cube.transform.SetPositionAndRotation(player.Position, player.Rotation);
        }
    }
}
