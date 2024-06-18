using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] GameObject LoginUI;
    [SerializeField] GameObject SignupUI;

    private static UIManager _instance = null;

    public static UIManager Instance
    {
        get
        {
            return _instance;
        }
    }

    private void Awake()
    {
       _instance = this;
    }

    public void ShowLoginUI()
    {
        //������ ����. SetActive(false)�� ���� �Ͼ�� DB�� �ٽ� ������
        SignupUI.SetActive(false);
        LoginUI.SetActive(true);
    }

    public void ShowSignupUI()
    {
        LoginUI.SetActive(false);
        SignupUI.SetActive(true);
    }
}
