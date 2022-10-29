using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UGS;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
public class LoginScene : BaseScene
{
    [Header("TempLOginScreen")]
    public TMP_InputField InputField_ID;
    public TMP_InputField InputField_Password;
    public Button PlayButton;
    public bool IsLogin = false;
    protected override void Init()
    {
        base.Init();
        SceneType = Define.Scene.Login;
    }
    private void Update()
    {
        if(IsLogin)
        {
            Managers.Scene.LoadScene(Define.Scene.TestScene);
        }
    }
    public override void Clear()
    {
        UnityGoogleSheet.Load<Userinfo.User>();
        foreach(var value in Userinfo.User.UserList)
        {
            if(InputField_ID.text == value.ID && InputField_Password.text == value.Password)
            {
                Debug.Log(InputField_ID.text);
                Debug.Log(InputField_Password.text);
                Debug.Log("LoginScene Clear!");
                IsLogin = true;
                break;
            }
        }
    }
    public void SignUpButtonClick()
    {
        Managers.Scene.LoadScene(Define.Scene.TestScene);
    }
}
