uusing Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NetworkManager : MonoBehaviourPunCallbacks
{
    public static NetworkManager Instance;

    [Header("Room Settings")]
    public string roomName = "DefaultRoom";
    public int maxPlayers = 10;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        ConnectToServer();
    }

    // Connect to the Photon server
    public void ConnectToServer()
    {
        Debug.Log("Connecting to Photon server...");
        PhotonNetwork.ConnectUsingSettings();
    }

    // Create or join a room
    public void CreateOrJoinRoom()
    {
        RoomOptions options = new RoomOptions
        {
            MaxPlayers = (byte)maxPlayers
        };

        PhotonNetwork.JoinOrCreateRoom(roomName, options, TypedLobby.Default);
    }

    // Leave the current room
    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
    }

    #region Photon Callbacks

    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected to Photon Master Server.");
        CreateOrJoinRoom();
    }

    public override void OnJoinedRoom()
    {
        Debug.Log($"Joined room: {PhotonNetwork.CurrentRoom.Name}");
        LoadGameScene();
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Debug.Log($"{newPlayer.NickName} has joined the room.");
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        Debug.Log($"{otherPlayer.NickName} has left the room.");
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.LogWarning($"Disconnected from Photon: {cause}");
    }

    #endregion

    // Load the game scene
    private void LoadGameScene()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.LoadLevel("GameScene");
        }
    }

    // Public utility to set room name
    public void SetRoomName(string newRoomName)
    {
        roomName = newRoomName;
    }
}

