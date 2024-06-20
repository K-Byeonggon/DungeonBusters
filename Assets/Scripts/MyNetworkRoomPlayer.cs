using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyNetworkRoomPlayer : NetworkRoomPlayer
{
    private void OnEnable()
    {
        OnEnable_RegisterRoomPlayer();
    }

    private void OnEnable_RegisterRoomPlayer()
    {
        RoomManager.Instance.RoomPlayers.Add(LoginManager.Instance.UserID, this);
    }

    //����� CmdChangeReadyState�� Ȱ���ؼ� �غ�.
}
