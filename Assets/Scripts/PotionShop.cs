using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionShop : MonoBehaviour
{
    public void Enter(PlayerStat _playerStat)
    {

        if (GameObject.Find("Potion_Shop") == null)
        {
            Managers.Resource.Instantiate("Potion_Shop", GameObject.Find("Item Shop Group").transform);
        }
    }

    public void Exit()
    {
        if (GameObject.Find("Potion_Shop") != null)
            Managers.Resource.Destroy(GameObject.Find("Potion_Shop"));
    }
}
