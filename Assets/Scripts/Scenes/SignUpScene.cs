using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using UGS;
public class SignUpScene : BaseScene//MonoBehaviour
{
    // Start is called before the first frame update
    [Header("TempLoginScreen")]
    public TMP_InputField InputField_Name;
    public TMP_InputField InputField_ID;
    public TMP_InputField InputField_Password;
    public GameObject SuccessPanel;
    public GameObject FailPanel;
    public Button SuccessButton;
    public Button FailedButton;
    public Button SignupButton;
    public bool IsSignUp = false;
    void Start()
    {
        UnityGoogleSheet.Load<Userinfo.User>();
        SuccessPanel.SetActive(false);
        FailPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public void SignUpButtonClick()
    {
        foreach (var value in Userinfo.User.UserList)
        {
            if (InputField_ID.text == value.ID)
            {
                Debug.Log("Find");
                IsSignUp = true;
                continue;
            }
        }
        if (!IsSignUp)
        {
            SuccessPanel.SetActive(true);
        }
        else
        {
            FailPanel.SetActive(true);
        }
    }
    public void SignUpSuccess()
    {
        var newData = new Userinfo.User();
        newData.name = InputField_Name.text;
        newData.ID = InputField_ID.text;
        newData.Password = InputField_Password.text;
        newData.ID = InputField_ID.text;
        newData.HP = 200;
        newData.MP = 50;
        newData.Level = 1;
        newData.Exp = 0;
        newData.ATK = 40;
        newData.DEF = 10;
        newData.Money = 10000;

        UnityGoogleSheet.Write<Userinfo.User>(newData);
        

        System.GC.Collect();
       
        SuccessPanel.SetActive(false);
        IsSignUp = false;

        GoBackToLogin();
    }
    public void SignUpFailed()
    {
        FailPanel.SetActive(false);
        IsSignUp = false;
    }
    public void GoBackToLogin()
    {
        Managers.Scene.LoadScene(Define.Scene.Login);
    }
    public override void Clear()
    {

    }
}
