using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UGS;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
public class LoginScene : BaseScene
{
    [Header("TempLoginScreen")]
    public TMP_InputField InputField_ID;
    public TMP_InputField InputField_Password;
    public Button PlayButton;
    public GameObject LoginFailedPanel;
    public Button LoginFailedButton;
    public bool IsLogin = false;
    protected override void Init()
    {
        base.Init();
        SceneType = Define.Scene.Login;
    }
    private void Start()
    {
        LoginFailedPanel.SetActive(false);
    }
    private void Update()
    {
        if(IsLogin)
        {
            LoginFailedPanel.SetActive(false);
            Managers.Scene.LoadScene(Define.Scene.Stage1);
        }
    }
    public override void Clear()
    {
        UnityGoogleSheet.LoadFromGoogle<string, Userinfo.User>((list, map) =>
        {
            foreach(var value in list)
            {
                if (InputField_ID.text == value.ID && InputField_Password.text == value.Password)
                {
                    PlayerPrefs.SetString("ID", value.ID);
                    IsLogin = true;
                    System.GC.Collect();
                    continue;
                }
            }
            LoginFailedPanel.SetActive(true);
        }, true);
    }
    public void SignUpButtonClick()
    {
        Managers.Scene.LoadScene(Define.Scene.SignUp);
    }
    public void LoginFailedButtonClick()
    {
        IsLogin = false;
        LoginFailedPanel.SetActive(false);
    }
}
