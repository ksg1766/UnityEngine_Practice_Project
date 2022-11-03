using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static UnityEngine.UI.Image;
using static UnityEngine.UIElements.UxmlAttributeDescription;

public class PotionShop : UI_Base //MonoBehaviour
{
    PlayerStat _stat;

    List<string> WeaponList = new List<string>()
    {
        {"PickAxe"},
        {"SteelAxe"},
        {"RecurveBow"}
    };

    int totalPrice = 0;

    Dictionary<string, int> SelectedList = new Dictionary<string, int>(3);
    //List<Sprite> ShowSelected = new List<Sprite>();
    //List<string> ShowSelected_debug = new List<string>();

    int selected_list_count = 1;

    //Dictionary<Sprite, int> ShowSelected = new Dictionary<Sprite, int>();

    //public Action<PointerEventData> OnClickHandler = null;

    public override void Init()
    {
        //_stat = GameObject.FindWithTag("Player").GetComponent<PlayerStat>();
    }

    public void Enter()
    {
        //_playerStat = GameObject.FindWithTag("Player").GetComponent<PlayerStat>();
        //_stat = _playerStat;
        _stat = GameObject.FindWithTag("Player").GetComponent<PlayerStat>();

        if (GameObject.Find("Potion_Shop") == null)
        {
            Managers.Resource.Instantiate("Potion_Shop", GameObject.Find("Item Shop Group").transform);
        }

        totalPrice = 0;
        //selected_list_count = 0;

        GameObject.Find("Gold").transform.GetComponent<Text>().text = $"GOLD\t\t\t: {_stat.Gold}";
        GameObject.Find("Price").transform.GetComponent<Text>().text = $"PRICE\t\t\t : 0";
        GameObject.Find("Balance").transform.GetComponent<Text>().text = $"BALANCE\t : {_stat.Gold}";
        // Util.FindChild(transform.gameObject, "MyGold").GetComponent<Text>().text = $"GOLD : {_stat.Gold}";
    }

    public void Exit()  //여긴 그냥 엉망진창...
    {
        //WeaponList.RemoveAll();
        if (totalPrice != 0)
        {
            Debug.Log($"totalPrice is not 0");
            totalPrice = 0;
        }
        if (SelectedList != null)
        {
            Debug.Log($"SelectedList is not null");
            SelectedList.Clear();
            //SelectedList = null;
        }
        if (selected_list_count != 0)
            selected_list_count = 0;
        //SelectedList = null;
        //selected_list_count = 6;
        Debug.Log($"selected list count = {selected_list_count}");
        Debug.Log("Exit");
        //foreach (KeyValuePair<string, int> item in SelectedList)
        //    Debug.Log(item.Key + " : " + item.Value);


        if (GameObject.Find("Potion_Shop") != null)
            Managers.Resource.Destroy(GameObject.Find("Potion_Shop"));


    }

    // 작업 중... : 아이템 선택시 선택된 아이템 창(버튼)에 아이템 등록하는 내용 추가 필요
    public void SelectItem()
    {
        _stat = GameObject.FindWithTag("Player").GetComponent<PlayerStat>();

        if (EventSystem.current.currentSelectedGameObject.name == "Button_SelectSmallPotion")
        {
            Debug.Log("SmallPotion selected");

            if (SelectedList.ContainsKey("SmallPotion"))
                SelectedList["SmallPotion"] += 1;
            else
                SelectedList.Add("SmallPotion", 1);

            // 리스트 잘 올라갔는지 확인용
            foreach (KeyValuePair<string, int> item in SelectedList)
                Debug.Log(item.Key + " : " + item.Value);

            totalPrice += 100;
            // if(SelectedList.Count < 3)
            // Texture2D?texture?=?(Texture2D)AssetDatabase.LoadAssetAtPath("Assets/Textures/texture.jpg",?typeof(Texture2D));
            // GameObject?obj?=?(GameObject)AssetDatabase.LoadAssetAtPath("Assets/Prefab/player.prefab",?typeof(GameObject));
        }

        if (EventSystem.current.currentSelectedGameObject.name == "Button_SelectMediumPotion")
        {
            Debug.Log("MediumPotion selected");

            if (SelectedList.ContainsKey("MediumPotion"))
                SelectedList["MediumPotion"] += 1;
            else
                SelectedList.Add("MediumPotion", 1);

            // 리스트 잘 올라갔는지 확인용
            foreach (KeyValuePair<string, int> item in SelectedList)
                Debug.Log(item.Key + " : " + item.Value);

            totalPrice += 500;
        }

        if (EventSystem.current.currentSelectedGameObject.name == "Button_SelectLargePotion")
        {
            Debug.Log("LargePotion selected");

            if (SelectedList.ContainsKey("LargePotion"))
                SelectedList["LargePotion"] += 1;
            else
                SelectedList.Add("LargePotion", 1);

            // 리스트 잘 올라갔는지 확인용
            foreach (KeyValuePair<string, int> item in SelectedList)
                Debug.Log(item.Key + " : " + item.Value);

            totalPrice += 1000;
        }

        selected_list_count = 1;

        // 예약된 아이템 이미지 딕셔너리에 출력
        foreach (KeyValuePair<string, int> item in SelectedList)
        {
            //ShowSelected.Add(Resources.Load<Sprite>($"Icons/{item.Key}"), item.Value);
            if (Resources.Load<Sprite>($"Icons/{item.Key}") == null)
                Debug.Log($"{item.Key} = null");

            GameObject iconImage = GameObject.Find($"Selected Item {selected_list_count}").transform.Find($"potionshop_selected{selected_list_count}").gameObject;
            GameObject numOfSelected = GameObject.Find($"Selected Item {selected_list_count}").transform.Find($"number_of_selected{selected_list_count}").gameObject;

            if (!iconImage.activeSelf)
            {
                iconImage.SetActive(true);
                numOfSelected.SetActive(true);
            }

            iconImage.GetComponent<Image>().sprite = Resources.Load<Sprite>($"Icons/{item.Key}") as Sprite;
            numOfSelected.GetComponent<Text>().text = $"{item.Value}";
            ++selected_list_count;
        }

        GameObject.Find("Gold").transform.GetComponent<Text>().text = $"GOLD\t\t\t: {_stat.Gold}";
        if (totalPrice > _stat.Gold)
        {
            GameObject.Find("Price").transform.GetComponent<Text>().text = "<color=#FF0000>" + $"PRICE \t\t\t: {totalPrice}" + "</color>";
            GameObject.Find("Balance").transform.GetComponent<Text>().text = "<color=#FF0000>" + $"BALANCE\t: {_stat.Gold - totalPrice}" + "</color>";
        }
        else
        {
            GameObject.Find("Price").transform.GetComponent<Text>().text = $"PRICE \t\t\t: {totalPrice}";
            GameObject.Find("Balance").transform.GetComponent<Text>().text = $"BALANCE\t: {_stat.Gold - totalPrice}";
        }


        //Util.FindChild(transform.gameObject, "MyGold").GetComponent<Text>().text = $"GOLD : {_stat.Gold}";
    }

    public void clickBuyButton()
    {

        if (totalPrice <= _stat.Gold)
        {
            _stat.Gold -= totalPrice;
            _stat.PlayerInventory.addItem(SelectedList);
        }
        else
            Debug.Log("Not Enough Gold");
        SelectedList.Clear();
        for (int i = 1; i<4; i++)
        {
            GameObject.Find($"Selected Item {i}").transform.Find($"potionshop_selected{i}").gameObject.SetActive(false);
            GameObject.Find($"Selected Item {i}").transform.Find($"number_of_selected{i}").gameObject.SetActive(false);
        }
        totalPrice = 0;
        GameObject.Find("Gold").transform.GetComponent<Text>().text = $"GOLD\t\t\t: {_stat.Gold}";
        GameObject.Find("Price").transform.GetComponent<Text>().text = $"PRICE\t\t\t: {totalPrice}";
        GameObject.Find("Balance").transform.GetComponent<Text>().text = $"BALANCE\t: {_stat.Gold - totalPrice}";
        _stat.PlayerInventory.ShowItem();
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

    private void OnDestroy()
    {

    }
}
