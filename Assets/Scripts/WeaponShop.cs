using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponShop : MonoBehaviour
{
    public void Enter(PlayerStat _playerStat)
    {

        if (GameObject.Find("Weapon_Shop") == null)
        {
            Managers.Resource.Instantiate("Weapon_Shop", GameObject.Find("Item Shop Group").transform);
        }
    }

    public void Exit()
    {
        if (GameObject.Find("Weapon_Shop") != null)
            Managers.Resource.Destroy(GameObject.Find("Weapon_Shop"));
    }
}
