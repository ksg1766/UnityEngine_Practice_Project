using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Main : UI_Base
{
    enum GameObjects
    {
        PlayerCharacter=0,
        HP_front,
        MP_front,
        EXP_front
    }

    PlayerStat _stat;

    public override void Init()
    {
        _stat = GameObject.FindWithTag("Player").GetComponent<PlayerStat>();
    }

    void Update()
    {
        float hp_ratio = _stat.Hp / (float)_stat.MaxHp;
        RectTransform hp_front = Util.FindChild(transform.gameObject, "HP_front").GetComponent<RectTransform>();
        hp_front.sizeDelta = new Vector2((float)350 * hp_ratio, 20);

        
        float exp_ratio = _stat.Exp / (float)_stat.TotalExp;
        RectTransform exp_front = Util.FindChild(transform.gameObject, "EXP_front").GetComponent<RectTransform>();
        exp_front.sizeDelta = new Vector2((float)1920 * exp_ratio, 20);

        Util.FindChild(transform.gameObject, "Level_text").GetComponent<Text>().text = $"Level  {_stat.Level}";

        Util.FindChild(transform.gameObject, "Exp_text").GetComponent<Text>().text = $"Exp  {_stat.Exp} / {_stat.TotalExp}";
        Util.FindChild(transform.gameObject, "MyGold").GetComponent<Text>().text = $"{_stat.Gold}";
    }
}
