using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SigninUI : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] Button Btn_Exit;
    [SerializeField] InputField Input_Id;
    [SerializeField] InputField Input_Password;
    [SerializeField] Button Btn_Sign;
    [SerializeField] Text Text_Error;
    
    private void OnEnable()
    {
        Btn_Exit.onClick.AddListener(OnClick_Exit);
        Btn_Sign.onClick.AddListener(OnClick_Sign);
    }

    private void OnDisable()
    {
        Btn_Exit.onClick.RemoveListener(OnClick_Exit);
        Btn_Sign.onClick.RemoveListener(OnClick_Sign);
    }

    public void OnClick_Exit()
    {
        //�α��� ȭ������ ���ư���
    }

    public void OnClick_Sign()
    {
        //������ ���� ��û
    }

}
