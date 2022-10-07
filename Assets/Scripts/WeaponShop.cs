using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class WeaponShop : MonoBehaviour
{
    List<string> WeaponList = new List<string>()
    {
        {"PickAxe"},
        {"SteelAxe"},
        {"RecurveBow"}
    };

    Dictionary<string, int> SelectedList = new Dictionary<string, int>();

    //public Action<PointerEventData> OnClickHandler = null;

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
        WeaponList.Clear();
        SelectedList.Clear();
    }

    // 작업 중... : 아이템 선택시 선택된 아이템 창(버튼)에 아이템 등록하는 내용 추가 필요
    public void SelectItem()
    {
        if (EventSystem.current.currentSelectedGameObject.name == "Button_SelectPickAxe")
        {
            Debug.Log("PickAxe selected");
            
            if (SelectedList.ContainsKey("PickAxe"))
                SelectedList["PickAxe"] += 1;
            else
                SelectedList.Add("PickAxe", 1);

            // 리스트 잘 올라갔는지 확인용
            foreach(KeyValuePair<string, int> item in SelectedList)
                Debug.Log(item.Key + " : " + item.Value);

            // if(SelectedList.Count < 3)
            // Texture2D?texture?=?(Texture2D)AssetDatabase.LoadAssetAtPath("Assets/Textures/texture.jpg",?typeof(Texture2D));
            // GameObject?obj?=?(GameObject)AssetDatabase.LoadAssetAtPath("Assets/Prefab/player.prefab",?typeof(GameObject));
        }

        if (EventSystem.current.currentSelectedGameObject.name == "Button_SelectSteelAxe")
        {
            Debug.Log("SteelAxe selected");
            
            if (SelectedList.ContainsKey("SteelAxe"))
                SelectedList["SteelAxe"] += 1;
            else
                SelectedList.Add("SteelAxe", 1);

            // 리스트 잘 올라갔는지 확인용
            foreach (KeyValuePair<string, int> item in SelectedList)
                Debug.Log(item.Key + " : " + item.Value);
        }

        if (EventSystem.current.currentSelectedGameObject.name == "Button_SelectRecurveBow")
        {
            Debug.Log("RecurveBow selected");

            if (SelectedList.ContainsKey("RecurveBow"))
                SelectedList["RecurveBow"] += 1;
            else
                SelectedList.Add("RecurveBow", 1);

            // 리스트 잘 올라갔는지 확인용
            foreach (KeyValuePair<string, int> item in SelectedList)
                Debug.Log(item.Key + " : " + item.Value);
        }
    }

    // 선택된거 클릭 했을 때 취소 하는 것 추가 예정 : 해당 버튼에 등록된 아이템이 null이 아니라면 이름을 확인 후 SelectedList에서 지워보자
    public void deleteItem()
    {
        /*
        if()
        {
            
        }
        */
    }
}
