using MySql.Data.MySqlClient;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoginUI : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] InputField Input_Id;
    [SerializeField] InputField Input_Password;
    [SerializeField] Button Btn_Login;
    [SerializeField] Button Btn_Sign;
    [SerializeField] Text Text_Error;






    private void OnEnable()
    {
        Btn_Login.onClick.AddListener(OnClick_Login);
        Btn_Sign.onClick.AddListener(OnClick_Sign);
    }

    private void OnDisable()
    {
        Btn_Login.onClick.RemoveListener(OnClick_Login);
        Btn_Sign.onClick.RemoveListener(OnClick_Sign);
    }

    private void Start()
    {
        bool DBConnected = DBManager.Instance.LoginUI_OnStart_ConnectDB();
        if (DBConnected) { Text_Error.text = "DB ���� ����!"; }
        else { Text_Error.text = "DB ���� ����!"; }
    }






    public void OnClick_Login()
    {
        bool LoginSuccess =  DBManager.Instance.LoginUI_OnClick_Login_SendQuery(Input_Id.text, Input_Password.text);
        if (LoginSuccess) { Text_Error.text = "�α��� ����!"; }
        else { Text_Error.text = "�α��� ����!"; }
    }

    public void OnClick_Sign()
    {
        //������ ���� ��û
    }


}
