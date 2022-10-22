using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static UnityEngine.UI.Image;
using static UnityEngine.UIElements.UxmlAttributeDescription;

public class WeaponShop : MonoBehaviour
{
    List<string> WeaponList = new List<string>()
    {
        {"PickAxe"},
        {"SteelAxe"},
        {"RecurveBow"}
    };

    Dictionary<string, int> SelectedList = new Dictionary<string, int>();
    //List<Sprite> ShowSelected = new List<Sprite>();
    //List<string> ShowSelected_debug = new List<string>();

    int selected_list_count = 1;

    //Dictionary<Sprite, int> ShowSelected = new Dictionary<Sprite, int>();

    //public Action<PointerEventData> OnClickHandler = null;

    public void Enter(PlayerStat _playerStat)
    {
        if (GameObject.Find("Weapon_Shop") == null)
        {
            Managers.Resource.Instantiate("Weapon_Shop", GameObject.Find("Item Shop Group").transform);
        }
        SelectedList.Clear();
        selected_list_count = 1;
    }

    public void Exit()
    {
        //WeaponList.RemoveAll();
        SelectedList.Clear();
        //SelectedList = null;
        selected_list_count = 1;
        Debug.Log("Exit");
        foreach (KeyValuePair<string, int> item in SelectedList)
            Debug.Log(item.Key + " : " + item.Value);

       
        if (GameObject.Find("Weapon_Shop") != null)
            Managers.Resource.Destroy(GameObject.Find("Weapon_Shop"));

        
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
            foreach (KeyValuePair<string, int> item in SelectedList)
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

        selected_list_count = 1;

        // 예약된 아이템 이미지 딕셔너리에 출력
        foreach (KeyValuePair<string, int> item in SelectedList)
        {
            //ShowSelected.Add(Resources.Load<Sprite>($"Icons/{item.Key}"), item.Value);
            if (Resources.Load<Sprite>($"Icons/{item.Key}") == null)
                Debug.Log($"{item.Key} = null");

            GameObject temp = GameObject.Find($"Selected Item {selected_list_count}").transform.Find($"weaponshop_selected{selected_list_count}").gameObject;
            if(!temp.activeSelf)
                temp.SetActive(true);

            temp.GetComponent<Image>().sprite = Resources.Load<Sprite>($"Icons/{item.Key}") as Sprite;
            selected_list_count++;
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
