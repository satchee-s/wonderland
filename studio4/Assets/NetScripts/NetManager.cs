using UnityEngine;
using System.Net.Sockets;
using System.Net;
using System.Text;
using core;
using UnityEngine.UI;
using TMPro;
using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;
using System.Threading;

public class NetManager : MonoBehaviour
{
    delegate void ConnectedToServer();
    ConnectedToServer ConnectedToServerEvent;

    [Header("Connect Panel")]
    [SerializeField] Button connectButton;
    [SerializeField] TMP_InputField playerNameInputField;
    [SerializeField] GameObject connectPanel;
    [SerializeField] GameObject matchMakingPanel;
    [SerializeField] GameObject Player2Panel;
    [SerializeField] GameObject TransitionPanel;

    [SerializeField] GameObject opponentFound;
    [SerializeField] GameObject lookingForOpponent;

    [SerializeField] Button startButton;
    [SerializeField] Button exitButton;
    [SerializeField] TextMeshProUGUI setPlayerName;

    SceneController sC;
    [HideInInspector] public Socket socket;

    public Player player;
    public Player playerEnemy;

    public NetworkComponent nc;
    Card card;
    int slotId; //holder variables that temporarily hold the received slotId and cardId just so they card be passed to the constructor
    int cardId;
    GameManager gameManager;
    PlayerManager playerManager;
    PlayerTurnSystem turnSystem;
    [SerializeField] PlayerRole role;

    List<GameObject> playerObjs = new List<GameObject>();
    public TextMeshProUGUI[] playerName;
    public PlayerManager[] playerManagers = new PlayerManager[2];
    public PlayerSlotsManager[] playerSlotsManagers = new PlayerSlotsManager[2];
    public int numberOfLocalCardsPlaced = 0;
    public int numberOfEnemyCardsPlaced = 0;

    void Start()
    {
        if (connectButton)


            connectButton.onClick.AddListener(() =>
            {
                try
                {
                    player = new Player(Guid.NewGuid().ToString(), playerNameInputField.text);
                    socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                    socket.Connect(new IPEndPoint(IPAddress.Parse("127.0.0.1"), 3000));
                    TransitionPanel.SetActive(true);
                    connectPanel.SetActive(false);
                    startButton.gameObject.SetActive(false);


                    socket.Blocking = false;

                    //if one player joins, and lobby is empty look for another player UI should pop up and wait until another player Joins

                    //Debug.Log(nc.GameObjectID );
                    //Debug.Log(nc.prefabName);
                    //Debug.Log(nc.GameId);
                    //Debug.Log(nc.OwnerID);

                    InstantiateOverNetwork(nc.prefabName, Vector3.zero, Quaternion.identity);
                    //Thread.Sleep(2000);
                    Rig();

                    if (ConnectedToServerEvent != null) ConnectedToServerEvent();

                }
                catch (SocketException e)
                {
                    print(e);
                }

                turnSystem = FindObjectOfType<PlayerTurnSystem>();
            });
        DontDestroyOnLoad(gameObject);
        //DontDestroyOnLoad(gameObject);
        bool falseness = false;
        do
        {
            //send slot packet while 
        } while (falseness);
    }

    void Update()
    {
        if (socket != null)
        {
            Debug.Log(socket.Available);
            if (socket.Available > 0)
            {
                byte[] recievedBuffer = new byte[socket.Available];

                socket.Receive(recievedBuffer);

                BasePacket pb = new BasePacket().StartDeserialization(recievedBuffer);
                Debug.Log("Heres your base packet:" + pb.Type);
                Debug.Log("received packet");
                switch (pb.Type)
                {
                    case BasePacket.PacketType.Lobby:
                        Debug.Log("case 1");


                        LobbyPacket LP = (LobbyPacket)new LobbyPacket().StartDeserialization(recievedBuffer);
                        LP.player = playerEnemy; // this works

                        for (int i = 0; i < LP.clientsName.Count; i++) // loop 
                        {
                            print(LP.clientsName[i]);
                            playerName[i].text = LP.clientsName[i];
                        }
                        if (LP.clientsName.Count == 1) //this is the first player that joins
                        {
                            Debug.Log("player 1 joined");
                            //set this player as player 1 from player manager + roles
                            TransitionPanel.SetActive(false);
                            matchMakingPanel.SetActive(true);
                            Player2Panel.SetActive(false);
                            opponentFound.SetActive(false);
                            lookingForOpponent.SetActive(true);
                            playerManagers[0].role = PlayerManager.Role.Player1;
                        }
                        if (LP.clientsName.Count == 2) //this is when the 2nd client joins
                        {
                            Debug.Log("player 2 joined");
                            //set this player as player 2
                            matchMakingPanel.SetActive(false);
                            Player2Panel.SetActive(false);
                            opponentFound.SetActive(false);
                            lookingForOpponent.SetActive(true);
                            playerManagers[1].role = PlayerManager.Role.Player2;


                        }

                        break;

                    case BasePacket.PacketType.Message:
                        {
                            MessagePacket mp = (MessagePacket)new MessagePacket().StartDeserialization(recievedBuffer);
                            Debug.Log("case 2");

                            print($"{mp.player.Name}Said:{mp.message}");
                            break;
                        }
                    case BasePacket.PacketType.Instantiate:
                        {
                            Debug.Log("Received instantiate packet");
                            InstantiatePacket ip = new InstantiatePacket();
                            ip.StartDeserialization(recievedBuffer);

                            print(ip.player.ID);
                            print(ip.player.Name);
                            print(ip.PrefabName);
                            InstantiateFromResources(ip.PrefabName, ip.Position, ip.Rotation);
                            break;
                        }

                    case BasePacket.PacketType.Rigidbody:
                        {
                            RigidbodyPacket Rp = new RigidbodyPacket();
                            Rp.StartDeserialization(recievedBuffer);
                            break;
                        }

                    case BasePacket.PacketType.Destroy:
                        {
                            DestroyPacket Dp = new DestroyPacket();
                            Dp.StartDeserialization(recievedBuffer);
                            print(Dp.GameObjectId);

                            DestroyObject(Dp.GameObjectId);
                            break;
                        }

                    case BasePacket.PacketType.Position:
                        PositionPacket PP = new PositionPacket();
                        PP.StartDeserialization(recievedBuffer);
                        getPosition();
                        break;

                    case BasePacket.PacketType.Rotation:
                        RotationPacket RotatP = new RotationPacket();
                        RotatP.StartDeserialization(recievedBuffer);

                        getRotation();
                        break;

                    case BasePacket.PacketType.Card:
                        CardPacket cp = new CardPacket();
                        cp.StartDeserialization(recievedBuffer);
                        //CardInformation();
                        LoadCardInformation(cp.cardID, cp.cardName, cp.cardHealth, cp.cardAttack, cp.sleep);
                        playerManagers[1].playedCards.Add(card);


                        UpdateCardStats(cp.cardHealth, cp.sleep);
                        break;

                    case BasePacket.PacketType.Acknowledged:
                        AcknowledgedPacket AP = new AcknowledgedPacket();
                        AP.StartDeserialization(recievedBuffer);
                        socket.Send(new InformationPacket(player).StartSerialization());
                        Debug.Log("Acknowledged");
                        break;

                    case BasePacket.PacketType.RotationAndPosition:
                        RotationAndPositonPacket RPP = new RotationAndPositonPacket();

                        getPositionAndRotation();
                        break;

                    case BasePacket.PacketType.PlayerData:
                        PlayerDataPacket PD = new PlayerDataPacket();
                        PD.StartDeserialization(recievedBuffer);
                        PlayerData(PD);
                        break;


                    case BasePacket.PacketType.StartGame:
                        StartGamePacket SG = new StartGamePacket(player);

                        startGame();

                        Debug.Log("startGame");
                        break;
                    case BasePacket.PacketType.SlotPacket:
                        SlotPacket sP = new SlotPacket();
                        sP.StartDeserialization(recievedBuffer);
                        //INCREMENT THE ENEMY'S NUMBEROFENEMYCARDSPLACED
                        //GOINTO PLAYERMANAGERS[ENEMYPLAYER].PLAYERSLOTSMANAGER.CARDSPLACED
                        //JUST ADD A NEW CARD OBJECT LOCALLY TO THE DUMMY ENEMY PLAYERMANAGERS[ENEMYPLAYER].PLAYERSLOTSMANAGER.cARDSPLACED<>

                        //NEW STUFF NEED TO DOUBLE CHECK
                        numberOfEnemyCardsPlaced = numberOfEnemyCardsPlaced + 1;

                        if (playerManagers[1].slotsManager.cardsPlaced[0])
                        {
                            playerManagers[1].slotsManager.cardsPlaced.Add(playerManagers[0].slotsManager.cardsPlaced[0]);
                        }

                        //NEW STUFF NEED TO DOUBLE CHECK
                        break;
                    default:
                        break;
                }
            }



            //NEW STUFF NEED TO DOUBLE CHECK
            //IF PLAYERMAMANGERS[LOCALPLAYER].PLAYERSLOTSMANAGER.CARDSPLACED != 0
            //IF ALL OF THE ABOVE IS NOT EQUAL TO numberOfLocalCardsPlaced
            //numberOfLocalCardsPlaced = THE NEW LENGTH/SIZE/COUNT OF THE CARDSpLACED

            if (playerManagers[0].slotsManager.cardsPlaced.Count != 0)
            {
                if (playerManagers[0].slotsManager.cardsPlaced.Count != numberOfLocalCardsPlaced)
                {
                    numberOfLocalCardsPlaced = playerManagers[0].slotsManager.cardsPlaced.Count;
                }
                GetNewSlots(slotId, cardId);
            }
            //NEW STUFF NEED TO DOUBLE CHECK

            //call a function that gets the newly added PLAYERMAMANGERS[LOCALPLAYER].PLAYERSLOTSMANAGER.CARDSPLACED<i>.cardID & [i] <-THIS IS THE SLOT ID
            //AND A SENDS  THEM IN A PACKET CALLED SLOT PACKET

            ///DO  NOT FORGET TO MAKE THE FUNCTUION THAT SEND THE PACKETS DOWNSTAIRS. YOU CALL IT IN THI IF STATEMENT HERE
        }
    }
    //in the update, keep checking if cardsPlaced gets a new value


    //NEW STUFF NEED TO DOUBLE CHECK
    void GetNewSlots(int slotId, int cardId)
    {
        this.slotId = slotId;
        this.cardId = cardId;

        for (int i = 0; i < playerManager.playedCards.Count; i++)
        {
            playerManagers[i].slotsManager.cardsPlaced[i].cardId = slotId;
        }
        socket.Send(new SlotPacket(slotId, cardId, player).StartSerialization());
    }
    //NEW STUFF NEED TO DOUBLE CHECK


    void UpdateCardStats(int health, bool sleep)
    {
        for (int i = 0; i < playerManager.playedCards.Count; i++)
        {
            if (playerManager.playedCards[i] == card)
            {
                playerManager.playedCards[i].health = health;
                playerManager.playedCards[i].sleep = sleep;
            }
        }
    }

    private void PlayerData(PlayerDataPacket PD)
    {
        if (PD.player != player)
        {
            turnSystem.ChangeOpponentMana(PD.Mana);
            if (playerManager.IsPlayer2)
            {
                this.playerManager.health = PD.Health;
            }

        }
    }

    GameObject InstantiateOverNetwork(string prefabName, Vector3 position, Quaternion rotation)
    {
        Debug.Log(Resources.Load($"Prefabs/{prefabName}"));
        GameObject go = Instantiate(Resources.Load($"Prefabs/{prefabName}"), position, rotation) as GameObject;
        card = go.AddComponent<Card>();

        playerObjs.Add(go);
        socket.Send(new InstantiatePacket(prefabName, position, rotation).StartSerialization());

        return go;
    }


    GameObject InstantiateFromResources(string prefabName, Vector3 position, Quaternion rotation)
    {
        GameObject go = Instantiate(Resources.Load($"Prefabs/{prefabName}"), position, rotation) as GameObject;
        card = go.AddComponent<Card>();
        //nc.GameObjectID = gameObjectID;
        return go;
    }

    private void Rig()
    {
        GameObject go = playerObjs[0];
        go.GetComponent<Rigidbody>();

        Debug.Log(go);
        Debug.Log(go.GetComponent<Rigidbody>());

        Vector3 Velocity = go.GetComponent<Rigidbody>().velocity;
        Debug.Log(Velocity);

        socket.Send(new RigidbodyPacket(player, nc.GameId, Velocity).StartSerialization());
        Debug.Log("send");
    }

    private void DestroyObject(int instanceID)
    {
        NetworkComponent[] nc = FindObjectsOfType<NetworkComponent>();
        for (int i = 0; i < nc.Length; i++)
        {
            if (nc[i].GameObjectID == instanceID)
            {
                Destroy(nc[i].gameObject);
                break;
            }

        }
    }

    private Card CardInformation()
    {
        Card card = GetComponent<Card>();
        socket.Send(new CardPacket(card.cardId, card.cardName, card.health, card.attack, card.sleep).StartSerialization());
        return card;
    }

    void LoadCardInformation(int cardID, string cardName, int cardHealth, int cardAttack, bool sleep)
    {
        card.cardId = cardID;
        card.cardName = cardName;
        card.health = cardHealth;
        card.attack = cardAttack;
        card.sleep = sleep;
    }

    private void AcknowledgedInformation()
    {
        //gameManager = GetComponent<GameManager>();
        playerManager = GetComponent<PlayerManager>();


        socket.Send(new AcknowledgedPacket(player).StartSerialization());
    }

    private void getPosition()
    {
        GameObject go = playerObjs[0];

        go.GetComponent<Transform>();
        socket.Send(new PositionPacket(go.transform.position, player).StartSerialization());
    }


    private void getRotation()
    {
        GameObject go = playerObjs[0];
        go.GetComponent<Transform>();
        socket.Send(new RotationPacket(go.transform.rotation, player).StartSerialization());
    }

    private void getPositionAndRotation()
    {
        GameObject go = playerObjs[0];
        go.GetComponent<Transform>();


        socket.Send(new RotationAndPositonPacket(go.transform.position, go.transform.rotation, player).StartSerialization());
    }

    public void SendPacket(byte[] buffer)
    {
        socket.Send(buffer);
    }

    public void startGame()
    {
        SceneManager.LoadScene("Level Design 2");
    }
}
