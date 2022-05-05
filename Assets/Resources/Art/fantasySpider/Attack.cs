using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public void AttackEnemy()
    {
        transform.parent.GetComponent<MonsterController>().OnHitEvent();
    }
}
