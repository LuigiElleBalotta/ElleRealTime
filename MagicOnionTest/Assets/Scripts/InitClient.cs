using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Assets.Scripts.Game;
using ElleRealTimeStd.Shared.Test.Entities.StreamingHub.Creatures;
using ElleRealTimeStd.Shared.Test.Entities.StreamingHub.Player;
using ElleRealTimeStd.Shared.Test.Interfaces.StreamingHub;
using Grpc.Core;
using MagicOnion.Client;
using UnityEngine;

public class InitClient : MonoBehaviour, IGamingHubReceiver
{
    public static InitClient Instance { get { return _instance; } }
    private Channel channel;
    private IGamingHub streamingClient;
    Dictionary<int, GameObject> players = new Dictionary<int, GameObject>();
    private static string currentPlayerName = "";

    private bool isJoin;
    private bool isSelfDisConnected;

    public GameObject myModel;
    private Player MyPlayerRef;
    public List<GameObject> ListGameobjects = new List<GameObject>(); //Dummy, "Super Zombie"
    public List<int> instantiatedGameobjects = new List<int>();
    private static InitClient _instance;

    void Awake()
    {
        _instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        try
        {

            if (ListGameobjects.Count > 0)
            {
                foreach (GameObject gameobject in ListGameobjects)
                {
                    Debug.Log(gameobject.name);
                }
            }
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

    // Update is called once per frame
    void Update()
    {
        
    }

    public static string GetCurrentPlayerName()
    {
        return currentPlayerName;
    }

    private async void InitializeClient()
    {
        // Initialize the Hub
        this.channel = new Channel("localhost", 12345, ChannelCredentials.Insecure);
        GameObject player = await ConnectAsync(channel, "Lordaeron");
        await this.streamingClient.QueryCreaturesAsync();
        this.RegisterDisconnectEvent(streamingClient);
    }

    private async Task<GameObject> ConnectAsync(Channel grpcChannel, string roomName)
    {
        this.streamingClient = StreamingHubClient.Connect<IGamingHub, IGamingHubReceiver>(grpcChannel, this);

        //Returns already joined players + me
        var roomPlayers = await this.streamingClient.JoinAsync(roomName, Client.GlobalVariables.CurrentAccountID);
        foreach (var player in roomPlayers)
        {
            if (player.ID != Client.GlobalVariables.CurrentAccountID)
                (this as IGamingHubReceiver).OnJoin(player);
            else
            {
                MyPlayerRef = player;
                TextManager.Instance.UpdatePlayerText(player);
            }
                
        }

        currentPlayerName = players[Client.GlobalVariables.CurrentAccountID].name;

        return players[Client.GlobalVariables.CurrentAccountID];
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
        }
    }

    // methods send to server.

    public async Task LeaveAsync()
    {
        await streamingClient.LeaveAsync();
    }

    public Task MoveAsync(Vector3 position, Quaternion rotation)
    {
        return streamingClient.MoveAsync(position, rotation);
    }

    public Task SendAnimationAsync(CharAnimState state)
    {
        return streamingClient.SendAnimStateAsync((int)state);
    }

    public Task SavePlayer()
    {
        return streamingClient.SavePlayerAsync();
    }

    public Task SendChatMessageAsync(string text)
    {
        return streamingClient.SendChatMessageAsync(text);
    }

    // dispose client-connection before channel.ShutDownAsync is important!
    public async Task DisposeAsync()
    {
        await streamingClient.DisposeAsync();
    }

    protected async void OnDestroy()
    {
        //It seems it doesn't let the client close.
        await SavePlayer();
        await LeaveAsync();
        await DisposeAsync();
        #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
        #else
                Application.Quit();
        #endif
    }

    void IGamingHubReceiver.OnJoin(Player player)
    {
        GameObject p = null;
        Debug.Log($"[Client/OnJoin] New player has joined: \"{player.Name}\"");
        if (!players.ContainsKey(player.ID))
        {
            InstantiateGameObject(player, out p);
            Debug.Log($"[Client/OnJoin] GameObject with name \"{p.name}\" instantiated.");
        }
        else
        {
            Debug.Log("OnJoin: player already joined!!");
        }
        
    }

    private void InstantiateGameObject(Player player, out GameObject p)
    {
        p = Instantiate(myModel, player.Position, player.Rotation);
        p.name = player.Name;
        players.Add(player.ID, p);
    }

    void IGamingHubReceiver.OnLeave(Player player)
    {
        Debug.Log($"[Client] Player \"{player.Name}\" disconnected from server.");

        if (players.TryGetValue(player.ID, out GameObject otherPerson))
        {
            GameObject.Destroy(otherPerson);
            players.Remove(player.ID);
        }
    }

    void IGamingHubReceiver.OnMove(Player player)
    {
        Debug.Log($"{player.Name} si sta muovendo!!");

        if (players.TryGetValue(player.ID, out var otherPerson))
        {
            if (otherPerson != null && otherPerson.name != currentPlayerName)
            {
                otherPerson.transform.position = Vector3.MoveTowards(otherPerson.transform.position, player.Position, 1.0f * Time.deltaTime);
                otherPerson.transform.Rotate(Vector3.up, Quaternion.Angle(otherPerson.transform.rotation, player.Rotation), Space.World);
                //otherPerson.transform.rotation = Quaternion.RotateTowards(otherPerson.transform.rotation, player.Rotation, 1.0f * Time.deltaTime);
            }
        }
    }

    void IGamingHubReceiver.OnAnimStateChange(int playerId, int state)
    {
        Debug.Log($"{playerId} CAMBIA ANIM STATE!!");

        if (players.TryGetValue(playerId, out var otherPerson))
        {
            if (otherPerson != null && otherPerson.name != currentPlayerName)
            {
                var animator = otherPerson.GetComponent<Animator>();
                animator.SetInteger("CharAnimState", state);
            }
        }
    }

    void IGamingHubReceiver.OnPlayerInfoSaved()
    {
        //ToDO: close the Pause menu
        Debug.Log("Successfully saved!");
    }

    void IGamingHubReceiver.OnQueriedCreatures(CreatureUnity[] creatures)
    {
        Debug.Log($"[Client/OnQueriedCreatures] Received {creatures.Length} creatures.");
        foreach (CreatureUnity creature in creatures)
        {
            if (ListGameobjects.Where(x => x.name == creature.PrefabName).ToArray().Length > 0)
            {
                GameObject gobj = ListGameobjects.Where(x => x.name == creature.PrefabName).ToArray()[0];
                Debug.Log($"[OnQueriedCreatures] spawning {gobj.name} with Guid: {creature.Guid}.");
                if (!instantiatedGameobjects.Contains(creature.Guid))
                {
                    Instantiate(gobj, creature.Position, creature.Rotation);
                    instantiatedGameobjects.Add(creature.Guid);
                }
            }
        }
    }

    void IGamingHubReceiver.OnChatMessage(string playerName, string text)
    {
        Debug.Log($"[OnChatMessage] {playerName} say: \"{text}\"");
        //ChatFrame.Instance.AddMessageToChat(playerName, text);
    }
}
