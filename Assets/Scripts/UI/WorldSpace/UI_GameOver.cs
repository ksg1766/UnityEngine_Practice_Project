using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_GameOver : UI_Base
{
    //다른 스크립트에서도 이 스크립트에 쉽게 접근할 수 있도록 하기위한 정적변수 선언
    public static UI_GameOver instance;

    public Text gameOver;

    Animator anim;

    enum GameObjects
    {
        GameOver
    }

    public override void Init()
    {
        Bind<GameObject>(typeof(GameObjects));
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private void Start()
    {
        anim = gameOver.gameObject.GetComponent<Animator>();

        gameOver.enabled = false;
    }

    public void ShowGameOver()
    {
        gameOver.enabled = true;

        anim.CrossFade("GameOver", 0.1f, -1, 0);
    }

    public void HideGameOver()
    {
        gameOver.enabled = false;
    }

    void Update()
    {

    }
}