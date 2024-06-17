using MySql.Data.MySqlClient;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using UnityEngine.UI;

public class DBManager : MonoBehaviour
{
    [Header("ConnectionInfo")]
    [SerializeField] string _ip = "127.0.0.1";
    [SerializeField] string _dbName = "dungeon_busters";
    [SerializeField] string _uid = "root";
    [SerializeField] string _pwd = "1234";

    private static MySqlConnection _dbConnection;
    private bool _isConnectTestComplete;
    private string str_DBResult;

    private static DBManager _instance = null;

    public static DBManager Instance
    {
        get
        {
            if(_instance == null)
            {
                _instance = new DBManager();
            }
            return _instance;
        }
    }

    public bool LoginUI_OnStart_ConnectDB()
    {
        string connectStr = $"Server={_ip};Database={_dbName};Uid={_uid};PWD={_pwd};";

        try
        {
            _dbConnection = new MySqlConnection(connectStr);
            _dbConnection.Open();
            return true;
        }
        catch (Exception e)
        {
            Debug.LogWarning($"e: {e.ToString()}");
            return false;
        }
    }

    public bool LoginUI_OnClick_Login_SendQuery(string id, string password)
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


    /*
    private void SendQuery(String query, string tableName)
    {
        if (query.Contains("SELECT"))
        {
            DataSet dataSet = OnSelectRequest(query, tableName);

            str_DBResult = DeformatResult(dataSet);
            
            Debug.Log(str_DBResult);
        }
    }

    private DataSet OnSelectRequest(string query, string tableName)
    {
        try
        {
            _dbConnection.Open();
            //MySqlCommand�� DB����� query ������ ���,
            MySqlCommand sqlCmd = new MySqlCommand();
            sqlCmd.Connection = _dbConnection;
            sqlCmd.CommandText = query;

            //DataSet�� ä���� MySqlDataAdapter�� MySqlCommand�� ������ش�.
            MySqlDataAdapter adapter = new MySqlDataAdapter(sqlCmd);
            DataSet dataSet = new DataSet();
            adapter.Fill(dataSet, tableName);
            _dbConnection.Close();
            return dataSet;
        }
        catch (Exception e)
        {
            Debug.LogError($"{e}");
            return null;
        }
    }

    //DataSet�� string���� �ٲ��ش�.
    private string DeformatResult(DataSet dataSet)
    {
        string resultStr = string.Empty;

        foreach(DataTable table in dataSet.Tables)
        {
            foreach(DataRow row in table.Rows)
            {
                foreach(DataColumn column in table.Columns)
                {
                    resultStr += $"{column.ColumnName}: {row[column]}";
                }
                resultStr += "\n";
            }
        }
        return resultStr;
    }*/
}
