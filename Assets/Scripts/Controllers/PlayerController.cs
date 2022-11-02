using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : BaseController
{
    int _mask = (1 << (int)Define.Layer.Ground) | (1 << (int)Define.Layer.Monster);

    PlayerStat _stat;
    bool _stopSkill = false;

    private float _attackRange = 2.0f;

    GameObject nearObject;

    //skill effect2
    [SerializeField]
    ParticleSystem skillEffect2;

    public override void Init()
    {
        WorldObjectType = Define.WorldObject.Player;
        _stat = gameObject.GetComponent<PlayerStat>();
        Managers.Input.MouseAction -= OnMouseEvent;
        Managers.Input.KeyAction -= OnKeyboardEvent;
        Managers.Input.MouseAction += OnMouseEvent;
        Managers.Input.KeyAction += OnKeyboardEvent;

        if (gameObject.GetComponentInChildren<UI_Nickname>() == null)
            Managers.UI.MakeWorldSpaceUI<UI_Nickname>(transform);

        skillEffect2.Stop();
    }

    protected override void UpdateMoving()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            //Debug.Log("SKILL");
            if (_lockTarget != null)
            {    
                _destPos = _lockTarget.transform.position;
                float distance = (_destPos - transform.position).magnitude;
                if (distance <= _attackRange)
                {
                    State = Define.State.Skill1;
                    return;
                }
            }
        }

        if (_lockTarget != null)
        {
            _destPos = _lockTarget.transform.position;
            float distance = (_destPos - transform.position).magnitude;
            if(distance <= _attackRange)
            {
                State = Define.State.Skill;
                return;
            }
        }

        Vector3 dir = _destPos - transform.position;
        dir.y = 0;

        if (dir.magnitude < 0.1f)
        {
            State = Define.State.Idle;
        }
        else
        {
            //Debug.DrawRay(transform.position + Vector3.up * 0.5f, dir.normalized, Color.green);
            if (Physics.Raycast(transform.position + Vector3.up * 0.5f, dir, 1.0f, LayerMask.GetMask("Block")))
            {
                if(Input.GetMouseButton(0) == false)
                    State = Define.State.Idle;

                return;
            }

            float moveDist = Mathf.Clamp(_stat.MoveSpeed * Time.deltaTime, 0, dir.magnitude);
            transform.position += dir.normalized * moveDist;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), 20 * Time.deltaTime);
        }

        //죽었을 때 상태 변경
        if (_stat.Hp <= 0)
        {
            State = Define.State.Die;
            return;
        }
    }

    private float time = 0.0f;

    protected override void UpdateIdle()
    {
        time += Time.deltaTime;
        if(time >= 5.0f)
        {
            if (_stat.Hp + 5 > _stat.MaxHp)
                _stat.Hp = _stat.MaxHp;
            else
                _stat.Hp += 5;
            time = 0.0f;
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            //Debug.Log("SKILL");
            if (_lockTarget != null)
            {
                _destPos = _lockTarget.transform.position;
                float distance = (_destPos - transform.position).magnitude;
                if (distance <= _attackRange)
                {
                    State = Define.State.Skill1;
                    return;
                }
            }
        }

        //죽었을 때 상태 변경
        if (_stat.Hp <= 0)
        {
            State = Define.State.Die;
            return;
        }
    }

    protected override void UpdateSkill()
    {
        if(_lockTarget != null)
        {
            Vector3 dir = _lockTarget.transform.position - transform.position;
            Quaternion quat = Quaternion.LookRotation(dir);
            transform.rotation = Quaternion.Lerp(transform.rotation, quat, 20 * Time.deltaTime);
        }

        //죽었을 때 상태 변경
        if (_stat.Hp <= 0)
        {
            State = Define.State.Die;
            return;
        }
    }

    protected override void UpdateDie()
    {

    }

    void OnHitEvent()
    {
        if(_lockTarget != null)
        {
            Stat targetStat = _lockTarget.GetComponent<Stat>();

            if (targetStat.Hp > 0)
            {
                float distance = (_lockTarget.transform.position - transform.position).magnitude;
                if (distance <= _attackRange)
                {
                    targetStat.OnAttacked(_stat);

                    _lockTarget.GetComponent<MonsterController>().ShowHitEffect();
                    Managers.Sound.Play("Effects/PlayerHit");
                }
            }
            else
            {
                _stopSkill = true;
            }
        }

        if (_stopSkill)
        {
            State = Define.State.Idle;
        }
        else
        {
            State = Define.State.Skill;
        }
    }

    void OnSkillEvent1()
    {
        if (_lockTarget != null)
        {
            Stat targetStat = _lockTarget.GetComponent<Stat>();

            if (targetStat.Hp > 0)
            {
                float distance = (_lockTarget.transform.position - transform.position).magnitude;
                if (distance <= _attackRange)
                {
                    targetStat.OnAttacked(_stat, 2.0f);

                    _lockTarget.GetComponent<MonsterController>().ShowSkillEffect1();
                    Managers.Sound.Play("Effects/PlayerSkill1");
                }
            }
            else
            {
                _stopSkill = true;
            }
        }

        if (_stopSkill)
        {
            State = Define.State.Idle;
        }
        else
        {
            State = Define.State.Skill1;
        }
    }

    void OnSkillEvent2()
    {
        if (!skillEffect2.isPlaying)
        {
            skillEffect2.Play();
            Managers.Sound.Play("Effects/PlayerSkill2");

            _stat.Defense = 15;
        }
        else
        {
            _stat.Defense = 5;

            skillEffect2.Stop();
        }
    }

    void OnMouseEvent(Define.MouseEvent evt)
    {
        switch (State)
        {
            case Define.State.Idle:
                OnMouseEvent_IdleRun(evt);
                break;
            case Define.State.Moving:
                OnMouseEvent_IdleRun(evt);
                break;
            case Define.State.Skill:
                {
                    if(evt == Define.MouseEvent.PointerUp)
                        _stopSkill = true;         
                }
                break;
            case Define.State.Die:
                {
                    //죽었을 때 클릭하면 부활
                    State = Define.State.Idle;
                }
                break;
        }
    }

    //전에 선택한 대상을 위한 변수
    private GameObject preTarget;

    void OnMouseEvent_IdleRun(Define.MouseEvent evt)
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        bool raycastHit = Physics.Raycast(ray, out hit, 100.0f, _mask);
        //Debug.DrawRay(Camera.main.transform.position, ray.direction * 100.0f, Color.red, 1.0f);

        switch (evt)
        {
            case Define.MouseEvent.PointerDown:
                {
                    if (raycastHit)
                    {
                        _destPos = hit.point;
                        State = Define.State.Moving;
                        _stopSkill = false;

                        if (hit.collider.gameObject.layer == (int)Define.Layer.Monster)
                            _lockTarget = hit.collider.gameObject;
                        else
                            _lockTarget = null;
                    }
                }
                break;
            case Define.MouseEvent.Press:
                {
                    if (_lockTarget == null && raycastHit)
                        _destPos = hit.point;

                    //선택한 대상을 표시
                    if (preTarget != null)
                    {
                        //전에 선택한 대상의 표시마크를 숨김
                        preTarget.GetComponent<MonsterController>().HideSelection();
                    }
                    if (_lockTarget != null)
                    {
                        //지금 선택한 대상의 표시마크를 보여줌
                        _lockTarget.GetComponent<MonsterController>().ShowSelection();
                    }
                    preTarget = _lockTarget;
                }
                break;
            case Define.MouseEvent.PointerUp:
                _stopSkill = true;
                break;
        }
    }

    void OnKeyboardEvent(Define.KeyboardEvent evt)
    {
        switch (evt)
        {
            case Define.KeyboardEvent.Key_G:
                {
                    Interaction();
                }
                break;

            case Define.KeyboardEvent.Key_ESC:
                {
                    if (nearObject.CompareTag("PotionShop"))
                    {
                        PotionShop potionshop = nearObject.GetComponent<PotionShop>();
                        potionshop.Exit();
                    }
                    else if (nearObject.CompareTag("WeaponShop"))
                    {
                        WeaponShop weaponshop = nearObject.GetComponent<WeaponShop>();
                        weaponshop.Exit();
                        Debug.Log("Esc pushed");
                    }
                }
                break;
            case Define.KeyboardEvent.Key_W:
                {
                    OnSkillEvent2();
                }
                break;
        }
    }

    void Interaction()
    {
        if (nearObject.CompareTag("PotionShop"))
        {
            PotionShop potionshop = nearObject.GetComponent<PotionShop>();
            potionshop.Enter();
        }
        else if (nearObject.CompareTag("WeaponShop"))
        {
            WeaponShop weaponshop = nearObject.GetComponent<WeaponShop>();
            weaponshop.Enter();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Managers.Resource.Instantiate("Interactive Button", GameObject.Find("PopUp Canvas/Others").transform);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("PotionShop"))
        {
            nearObject = other.gameObject;
        }
        else if (other.CompareTag("WeaponShop"))
        {
            nearObject = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("PotionShop"))
        {
            PotionShop potionshop = nearObject.GetComponent<PotionShop>();
            potionshop.Exit();
            nearObject = null;
            Managers.Resource.Destroy(GameObject.Find("Interactive Button"));
        }
        else if (other.CompareTag("WeaponShop"))
        {
            WeaponShop weaponshop = nearObject.GetComponent<WeaponShop>();
            weaponshop.Exit();
            nearObject = null;
            Managers.Resource.Destroy(GameObject.Find("Interactive Button"));
        }
    }
}