using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class Login : MonoBehaviour
{
    [Header("LoginPanel")]
    public InputField IDInputField;
    public InputField PassInputField;
    [Header("AccountPanel")]
    public InputField New_IDInputField;
    public InputField New_PassInputField;

    public string LoginUrl;

    void Start()
    {
        LoginUrl = "localhost/Login/Login.php";
    }
    public void LoginBtn() 
    {
        StartCoroutine(LoginCo());
    }
    IEnumerator LoginCo()
    {
        Debug.Log(IDInputField.text);
        Debug.Log(PassInputField.text);
        if (IDInputField.text == "chopr159")
        {
            if (PassInputField.text == "gkdnem12")
            {
                Debug.Log("로그인에 성공하셨습니다.");
                Managers.Scene.LoadScene(Define.Scene.TestScene);
            }

            else
            {
                Debug.Log("아이디와 비밀번호를 다시 확인해 주세요.");
            }
        }
        else
        {
            Debug.Log("아이디와 비밀번호를 다시 확인해 주세요.");
        }
        yield return null;
    }
    public void CreateAccoutBtn()
    {
        //StartCoroutine(LoginCo());
    }
}
