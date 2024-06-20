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

    public override GameObject OnRoomServerCreateRoomPlayer(NetworkConnectionToClient conn)
    {
        //���⼭ Ŀ�������� RoomPlayer ����.
        GameObject roomPlayer = Instantiate(roomPlayerPrefab.gameObject, Vector3.zero, Quaternion.identity);

        var myRoomPlayer = roomPlayer.GetComponent<MyNetworkRoomPlayer>();

        if (myRoomPlayer != null)
        {
            myRoomPlayer.Uid = 123345; //���⿡ ���� ����� Dic�� conn�� �Բ� ����� uid �� �ֱ�
        }

        return roomPlayer;
    }
}
