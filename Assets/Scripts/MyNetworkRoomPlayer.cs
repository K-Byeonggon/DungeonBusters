using JetBrains.Annotations;
using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyNetworkRoomPlayer : NetworkRoomPlayer
{
    [SyncVar]
    public int _uid;

    public override void OnStartClient()
    {
        base.OnStartClient();
        if (isLocalPlayer)
        {
            CmdSetUserID(LoginManager.Instance.UserID);
        }
    }

    [Command]
    private void CmdSetUserID(int uid)
    {
        _uid = uid;
        NetworkConnection conn = connectionToClient;
        ServerLoginManager.Instance.RegisterUserID(conn, uid);
    }
    //����� CmdChangeReadyState�� Ȱ���ؼ� �غ�.
}
