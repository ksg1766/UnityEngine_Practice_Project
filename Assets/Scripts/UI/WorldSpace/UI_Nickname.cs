using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Nickname : UI_Base
{
    enum GameObjects
    {
        Nickname
    }

    PlayerStat _stat;

    public override void Init()
    {
        Bind<GameObject>(typeof(GameObjects));
        _stat = transform.parent.GetComponent<PlayerStat>();
    }

    private void Update()
    {
        Transform parent = transform.parent;
        transform.position = parent.position + Vector3.up * (parent.GetComponent<Collider>().bounds.size.y);
        transform.rotation = Camera.main.transform.rotation;

        SetNickname();
    }

    public void SetNickname()
    {
        GetObject((int)GameObjects.Nickname).GetComponent<Text>().text = _stat.Name;
    }
}
