using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyNetworkRoomManager : NetworkRoomManager
{
    public static MyNetworkRoomManager Instance { get; private set; }

    public override void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // ���� ����: �� ��ȯ �� ����
        }
    }


    public override void OnServerAddPlayer(NetworkConnectionToClient conn)
    {
        base.OnServerAddPlayer(conn);

        GameObject roomPlayerObject = conn.identity.gameObject;
        MyNetworkRoomPlayer roomPlayer = roomPlayerObject.GetComponent<MyNetworkRoomPlayer>();

        if(roomPlayer != null)
        {
            int userID = ServerLoginManager.Instance.GetUserID(conn);
            roomPlayer._uid = userID;
        }
    }
}
