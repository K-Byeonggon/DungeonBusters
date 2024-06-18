using MySql.Data.MySqlClient;
using System;
using System.Data;
using UnityEngine;

public class SignupManager
{
    [Header("ConnectionInfo")]
    [SerializeField] string _ip;
    [SerializeField] string _dbName;
    [SerializeField] string _uid;
    [SerializeField] string _pwd;

    private static MySqlConnection _dbConnection;

    private static SignupManager _instance = null;

    public static SignupManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new SignupManager();
            }
            return _instance;
        }
    }

    public SignupManager()
    {
        _ip = DBConfig.Instance.IP;
        _dbName = DBConfig.Instance.DBname;
        _uid = DBConfig.Instance.Uid;
        _pwd = DBConfig.Instance.Pwd;
    }

    public bool SignupUI_OnEnable_ConnectDB()
    {
        _dbConnection = DBConnectionManager.Instance.OpenDBConnection();
        return (_dbConnection.State == ConnectionState.Open);
    }

    public bool SignupUI_OnDisable_DisconnectDB()
    {
        DBConnectionManager.Instance.CloseDBConnection();
        return (_dbConnection.State == ConnectionState.Closed);
    }

    public bool SignupUI_OnClick_Signup_SendQuery(string id, string password)
    {
        if(CheckNullOrWhiteSpace(id, password) == false) { Debug.Log("�������� ���� ���� �Ұ�."); return false; }

        string query = "SELECT COUNT(*) FROM user_info WHERE U_Name=@id";

        try
        {
            using (MySqlCommand cmd = new MySqlCommand(query, _dbConnection))
            {
                cmd.Parameters.AddWithValue("@id", id);

                int userCount = Convert.ToInt32(cmd.ExecuteScalar());

                if (userCount == 0)
                {
                    Debug.Log("DB�� �ߺ��Ǵ� ���̵� ����. ȸ������ ����.");
                    return Signup_SendQuery(id, password);
                }
                else
                {
                    Debug.Log("DB�� �̹� ���� ���̵� ����.");
                    return false;
                }
            }
        }
        catch (Exception e)
        {
            Debug.LogError($"Error: {e.Message}");
            return false;
        }
    }

    private bool Signup_SendQuery(string id, string password)
    {
        int uid = GetMaxUid_SendQuery();

        //������ ������ id�� password�� DB�� U_Name�� U_Password�� ����.
        string query = "INSERT INTO user_info (U_Name, U_Password, U_id) VALUES (@id, @password, @uid)";

        try
        {
            using (MySqlCommand cmd = new MySqlCommand(query, _dbConnection))
            {
                cmd.Parameters.AddWithValue("@id", id);
                cmd.Parameters.AddWithValue("@password", password);
                cmd.Parameters.AddWithValue("@uid", uid);
                cmd.ExecuteNonQuery();
                Debug.Log("Signup successful!");
                return true;
            }
        }
        catch (Exception e)
        {
            Debug.LogError($"Error: {e.Message}");
            return false;
        }
    }

    private int GetMaxUid_SendQuery()
    {
        string query = "SELECT IFNULL(MAX(U_id), 0) FROM user_info";
        int newUserId;

        using (MySqlCommand cmd = new MySqlCommand(query, _dbConnection))
        {
            newUserId = Convert.ToInt32(cmd.ExecuteScalar()) + 1;
        }
        return newUserId;
    }

    private bool CheckNullOrWhiteSpace(string id, string password)
    {
        if(string.IsNullOrWhiteSpace(id)) return false;
        if(string.IsNullOrWhiteSpace(password)) return false;
        return true;
    }

    public void SignupUI_OnClick_Exit()
    {
        UIManager.Instance.ShowLoginUI();
    }
}
