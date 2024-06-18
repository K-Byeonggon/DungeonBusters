using UnityEngine;
using UnityEngine.UI;

public class LoginView : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] InputField Input_Id;
    [SerializeField] InputField Input_Password;
    [SerializeField] Button Btn_Login;
    [SerializeField] Button Btn_Sign;
    [SerializeField] Text Text_Error;

    private LoginManager _loginManager;

    private void OnEnable()
    {
        if (_loginManager == null)
        {
            _loginManager = new LoginManager();
        }



        Btn_Login.onClick.AddListener(OnClick_Login);
        Btn_Sign.onClick.AddListener(OnClick_Sign);

        //bool DBConnected = LoginManager.Instance.LoginUI_OnEnable_ConnectDB();
        bool DBConnected = LoginEventManager.OnEnable_ConnectDB_ActionInvoke();
        if (DBConnected) { Text_Error.text = "DB ���� ����!"; }
        else { Text_Error.text = "DB ���� ����!"; }
    }

    private void OnDisable()
    {
        Btn_Login.onClick.RemoveListener(OnClick_Login);
        Btn_Sign.onClick.RemoveListener(OnClick_Sign);

        //bool DBDisconnected = LoginManager.Instance.LoginUI_OnDisable_DisconnectDB();
        bool DBDisconnected = LoginEventManager.OnDisable_DisconnetDB_ActionInvoke();
    }



    public void OnClick_Login()
    {
        bool LoginSuccess = LoginEventManager.OnClick_Login_ActionInvoke(Input_Id.text, Input_Password.text);
        //bool LoginSuccess = LoginManager.Instance.LoginUI_OnClick_Login_SendQuery(Input_Id.text, Input_Password.text);
        if (LoginSuccess) { Text_Error.text = "�α��� ����!"; }
        else { Text_Error.text = "�α��� ����!"; }
    }

    public void OnClick_Sign()
    {
        //������ ���� ��û
    }


}
