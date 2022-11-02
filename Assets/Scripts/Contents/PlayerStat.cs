using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UGS;
public class PlayerStat : Stat
{
    [SerializeField]
    protected int _exp;
    [SerializeField]
    protected int _gold;
    [SerializeField]
    public float _totalExp;
    
    public string Name;
    static string ID;
    public float TotalExp { get { return _totalExp; } set { _totalExp = value; } }
    public int Gold { get { return _gold; } set { _gold = value; } }
     
    public int Exp {
        get { return _exp; }
        set
        {
            _exp = value;

            int level = Level;

            Data.Stat stat;
            Managers.Data.StatDict.TryGetValue(level + 1, out stat);
            TotalExp = stat.totalExp;

            while (true)
            {
                //Data.Stat stat;
                if (Managers.Data.StatDict.TryGetValue(level + 1, out stat) == false)
                    break;
                if (_exp < stat.totalExp)
                    break;
                level++;
                
                _exp = _exp - stat.totalExp;
                TotalExp = stat.totalExp;
            }
            if(level != Level)
            {
                Debug.Log($"Level Up!");
                Level = level;
                SetStat(Level);

                Managers.Sound.Play("Effects/LevelUp");
            }
        }
    }

    private void Start()
    {
        ID = PlayerPrefs.GetString("ID");
        UnityGoogleSheet.LoadFromGoogle<string, Userinfo.User>((list, map) =>
        {
            foreach (var value in list)
            {
                if(value.ID == ID)
                {
                    Name = value.name;
                    _level = value.Level;
                    _exp = value.Exp;
                    _gold = value.Money;
                    _moveSpeed = 5.0f;
                }
            }
            SetStat(_level);
        }, true);

        Managers.Input.MouseAction -= OnMouseEvent;
        Managers.Input.MouseAction += OnMouseEvent;
    }

    public void SetStat(int level)
    {
        Dictionary<int, Data.Stat> dict = Managers.Data.StatDict;
        Data.Stat stat = dict[level];
        _hp = stat.maxHp;
        _maxHp = stat.maxHp;
        _attack = stat.attack;
        _defense = stat.defense;
    }

    private bool isDead = false;
    protected override void OnDead(Stat attacker)
    {
        Debug.Log("Player Dead");

        //GameOver 문구를 화면에 보여줌
        UI_GameOver.instance.ShowGameOver();

        isDead = true;
    }

    void OnMouseEvent(Define.MouseEvent evt)
    {
        if(isDead && evt == Define.MouseEvent.Press)
        {
            //GameOver 문구를 화면에서 숨김
            UI_GameOver.instance.HideGameOver();

            ReSpawn();

            isDead = false;
        }
    }

    private void ReSpawn()
    {
        Vector3 _respawnPos = new Vector3(0, 0, 0);
        _respawnPos.Set(0, 0, 0);
        transform.position = _respawnPos;

        _level = 1;
        _exp = 0;
        _moveSpeed = 5.0f;
        _gold = 0;
        SetStat(1);
    }
}
