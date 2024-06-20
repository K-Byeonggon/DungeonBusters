using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyManager : MonoBehaviour
{
    private static LobbyManager _instance = null;

    public static LobbyManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new LobbyManager();
            }
            return _instance;
        }
    }

    public void LobbyUI_OnClick_StartWitClient()
    {
        MyNetworkRoomManager.Instance.StartClient();
    }


    public void Popup_OnClick_3PlayerStart()
    {
        LoginManager.Instance.OnRoomJoin();
        //MyNetworkRoomManager.Instance.minPlayers = 3;
        MyNetworkRoomManager.Instance.minPlayers = 1;
        MyNetworkRoomManager.Instance.StartHost();
    }

    public void Popup_OnClick_4PlayerStart()
    {
        MyNetworkRoomManager.Instance.minPlayers = 4;
    }

    public void Popup_OnClick_5PlayerStart()
    {
        MyNetworkRoomManager.Instance.minPlayers = 5;
    }
}
