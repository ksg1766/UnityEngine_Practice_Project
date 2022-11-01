using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UGS;
public class Stat : MonoBehaviour
{
    [SerializeField]
    protected int _level;
    [SerializeField]
    protected int _hp;
    [SerializeField]
    protected int _maxHp;
    [SerializeField]
    protected int _attack;
    [SerializeField]
    protected int _defense;
    [SerializeField]
    protected float _moveSpeed;
    public int Level { get { return _level; } set { _level = value; } }
    public int Hp { get { return _hp; } set { _hp = value; } }
    public int MaxHp { get { return _maxHp; } set { _maxHp = value; } }
    public int Attack { get { return _attack; } set { _attack = value; } }
    public int Defense { get { return _defense; } set { _defense = value; } }
    public float MoveSpeed { get { return _moveSpeed; } set { _moveSpeed = value; } }

    private void Start()
    {
        // 데이터베이스에서 가지고 올 스텟
        //UnityGoogleSheet.LoadFromGoogle<string, Userinfo.User>((list, map) =>
        //{
        //    foreach (var value in list)
        //    {
        //        if (value.ID == ID)
        //        {
        //            _level = value.Level;
        //            _hp = value.HP;
        //            _maxHp = value.MaxHP;
        //            _attack = value.ATK;
        //            _defense = value.DEF;
        //        }
        //    }
        //}, true);\
        _level = 1;
        _hp = 50;
        _maxHp = 50;
        _attack = 10;
        _defense = 5;
        _moveSpeed = 4.0f;
    }

    public virtual void OnAttacked(Stat attacker, float skillCoeff = 1.0f)
    {
        int damage = Mathf.Max(0, (int)(skillCoeff * attacker.Attack) - Defense);
        Hp -= damage;
        if (Hp <= 0)
        {
            Hp = 0;
            OnDead(attacker);
        }
    }

    protected virtual void OnDead(Stat attacker)
    {
        PlayerStat playerStat = attacker as PlayerStat;
        if (playerStat != null)
        {
            playerStat.Exp += 5;
        }
        
        //죽고나서 죽음 애니메이션이 재생되는 동안 지연
        Invoke("Despawn", 2.0f);
    }

    private void Despawn()
    {
        Managers.Game.Despawn(gameObject);
    }

}
