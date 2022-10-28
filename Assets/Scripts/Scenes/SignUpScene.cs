using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UGS;
public class SignUpScene : MonoBehaviour
{
    // Start is called before the first frame update
    [Header("TempLoginScreen")]
    public TMP_InputField InputField_Name;
    public TMP_InputField InputField_ID;
    public TMP_InputField InputField_Password;
    public Button SignupButton;
    public bool IsLogin = false;
    void Start()
    {
   
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SignUpButtonClick()
    {
        var newData = new Userinfo.User();
        newData.name = InputField_Name.text;
        newData.ID = InputField_ID.text;
        newData.Password = InputField_Password.text;

        UnityGoogleSheet.Write<Userinfo.User>(newData);
    }
}
