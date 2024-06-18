using MySql.Data.MySqlClient;
using System;
using System.Data;
using UnityEngine;

public class LoginViewModel : MonoBehaviour
{
    [Header("ConnectionInfo")]
    [SerializeField] string _ip;
    [SerializeField] string _dbName;
    [SerializeField] string _uid;
    [SerializeField] string _pwd;

    private static MySqlConnection _dbConnection;



    public LoginViewModel()
    {
        _ip = DBConfig.Instance.IP;
        _dbName = DBConfig.Instance.DBname;
        _uid = DBConfig.Instance.Uid;
        _pwd = DBConfig.Instance.Pwd;
    }

    private void OnEnable()
    {
        LoginEventManager._connectDBCallback += ConnectDB;
        LoginEventManager._disConnectDBCallback += DisconnectDB;
        LoginEventManager._loginCallback += Login_SendQuery;
        //LoginEventManager._signupCallback
    }

    public bool ConnectDB()
    {
        _dbConnection = DBConnectionManager.Instance.OpenDBConnection();
        return (_dbConnection.State == ConnectionState.Open);
    }

    public bool DisconnectDB()
    {
        DBConnectionManager.Instance.CloseDBConnection();
        return (_dbConnection.State == ConnectionState.Closed);
    }

    public bool Login_SendQuery(string id, string password)
    {
        /*
        COUNT(*) : ���� ���� ���� ���� �Լ�. *�� ��� ���� �ǹ��ϸ�, �� ��� ���ǿ� �´� ��� ���� ���� ����.
        FROM user_info : �����͸� ������ ���̺��� �����ϴ� Ű����. user_info�� �츮�� ��ȸ�Ϸ��� ���̺� �̸�.
        WHERE : Ư�� ������ �����ϴ� Ű����.
        U_Name=@id : U_Name�� @id�� ��ġ�ϴ� ���� ã�� ����. @�� ���������� �Ű������� ��Ÿ��.
         */
        string query = "SELECT COUNT(*) FROM user_info WHERE U_Name=@id AND U_Password=@password";

        try
        {
            using (MySqlCommand cmd = new MySqlCommand(query, _dbConnection))
            {
                // MySqlCommand.Parameters.AddWithValue("@id", id); �� �������� �Ű������� ����� �Է°��� �Ҵ��� �� �ִ�.
                cmd.Parameters.AddWithValue("@id", id);
                cmd.Parameters.AddWithValue("@password", password);

                // MySqlCommand.ExecuteScalar()�� SQL������ �����ϰ� ���� ���� ��ȯ�Ѵ�.
                // ���⼭�� ���� �� COUNT(*)�� ��ȯ�Ѵ�.
                // Convert.ToInt32�� ��ȯ���� int������ �ٲ۴�.
                int userCount = Convert.ToInt32(cmd.ExecuteScalar());

                if (userCount > 0)
                {
                    Debug.Log("Login successful!");
                    return true;
                }
                else
                {
                    Debug.Log("Login failed.");
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
}
