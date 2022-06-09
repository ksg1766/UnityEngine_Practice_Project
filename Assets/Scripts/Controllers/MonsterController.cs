using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MonsterController : BaseController
{
    Stat _stat;

    [SerializeField]
    float _scanRange = 10.0f;

    [SerializeField]
    float _attackRange = 2.0f;

    //표시마크 오브젝트
    [SerializeField]
    GameObject selectMark;

    //피격효과
    [SerializeField]
    ParticleSystem hitEffect;
    [SerializeField]
    ParticleSystem skillEffect1;

    public override void Init()
    {
        WorldObjectType = Define.WorldObject.Monster;
        _stat = gameObject.GetComponent<Stat>();

        if (gameObject.GetComponentInChildren<UI_HPBar>() == null)
            Managers.UI.MakeWorldSpaceUI<UI_HPBar>(transform);

        //생성되었을 때 상태 초기화
        State = Define.State.Idle;

        //표시마크를 숨김
        HideSelection();

        //피격효과 멈춤
        hitEffect.Stop();
        skillEffect1.Stop();
    }

    //표시마크를 숨김
    public void HideSelection()
    {
        selectMark.SetActive(false);
    }
    //표시마크를 보여줌
    public void ShowSelection()
    {
        selectMark.SetActive(true);
    }

    //피격효과 재생
    public void ShowHitEffect()
    {
        hitEffect.Play();
    }
    public void ShowSkillEffect1()
    {
        skillEffect1.Play();
    }

    protected override void UpdateIdle()
    {
        GameObject player = Managers.Game.GetPlayer();
        if (player == null || player.GetComponent<PlayerController>().State == Define.State.Die)
            return;

        float distance = (player.transform.position - transform.position).magnitude;
        if (distance <= _scanRange)
        {
            _lockTarget = player;
            State = Define.State.Moving;
            return;
        }

        //죽었을 때 상태 변경
        if (_stat.Hp <= 0)
        {
            State = Define.State.Die;
            return;
        }
    }

    protected override void UpdateMoving()
    {
        if (_lockTarget != null)
        {
            _destPos = _lockTarget.transform.position;
            float distance = (_destPos - transform.position).magnitude;
            if (distance <= _attackRange)
            {
                NavMeshAgent nma = gameObject.GetOrAddComponent<NavMeshAgent>();
                nma.SetDestination(transform.position);
                State = Define.State.Skill;
                return;
            }
        }

        Vector3 dir = _destPos - transform.position;
        if (dir.magnitude < 0.1f)
        {
            State = Define.State.Idle;
        }
        else
        {
            NavMeshAgent nma = gameObject.GetOrAddComponent<NavMeshAgent>();
            nma.SetDestination(_destPos);
            nma.speed = _stat.MoveSpeed;

            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), 20 * Time.deltaTime);
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
        if (_lockTarget != null)
        {
            Vector3 dir = _lockTarget.transform.position - transform.position;
            Quaternion quat = Quaternion.LookRotation(dir);
            transform.rotation = Quaternion.Lerp(transform.rotation, quat, 20 * Time.deltaTime);
        }

        //스파이더는 이상하게 공격을 한번만 해서 넣음
        if (name == "Spider")
        {
            _destPos = _lockTarget.transform.position;
            float distance = (_destPos - transform.position).magnitude;
            if (distance > _attackRange)
            {
                State = Define.State.Moving;
            }
            else
            {
                State = Define.State.Skill;
            }
        }

        //플레이어가 죽었을 때 공격을 멈춤
        if (_lockTarget.GetComponent<PlayerController>().State == Define.State.Die)
        {
            State = Define.State.Idle;
            return;
        }

        //죽었을 때 상태 변경
        if (_stat.Hp <= 0)
        {
            State = Define.State.Die;
            return;
        }
    }

    public void OnHitEvent()
    {
        if (_lockTarget != null)
        {
            Stat targetStat = _lockTarget.GetComponent<Stat>();

            if (targetStat.Hp > 0)
            {
                float distance = (_lockTarget.transform.position - transform.position).magnitude;
                if (distance <= _attackRange)
                {
                    targetStat.OnAttacked(_stat);
                    Managers.Sound.Play("Effects/EnemyHit");

                    State = Define.State.Skill;
                }
                else
                {
                    State = Define.State.Moving;
                }
            }
            else
            {
                State = Define.State.Idle;
            }
        }
        else
        {
            State = Define.State.Idle;
        }
    }
}
