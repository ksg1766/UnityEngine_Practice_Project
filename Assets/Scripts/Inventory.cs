using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class Inventory : UI_Popup
{
    PlayerStat _stat;

    public Dictionary<string, int> Inven_Items = new Dictionary<string, int>(20);
    //int count_of_items = 0;

    public override void Init()
    {

    }

    public void addItem(Dictionary<string, int> newList)
    {
        foreach (KeyValuePair<string, int> newItem in newList)
        {
            if (Inven_Items.ContainsKey(newItem.Key))
                Inven_Items[newItem.Key] += newItem.Value;
            else
                Inven_Items.Add(newItem.Key, newItem.Value);

            //++count_of_items;
        }

    }

    public void subItem()
    {

    }

    public void InvenOn()
    {
        _stat = GameObject.FindWithTag("Player").GetComponent<PlayerStat>();

        if (GameObject.Find("Inventory") == null)
        {
            Managers.Resource.Instantiate("UI/Popup/Inventory", GameObject.Find("User UI Group").transform);
        }

        ShowItem();
    }

    public void ShowItem()
    {
        int number_of_index = 1;
        if (number_of_index > 20)
            return;
        foreach (KeyValuePair<string, int> item in Inven_Items)
        {
            GameObject iconImage = GameObject.Find($"Inven items{number_of_index}").transform.Find($"icon_of_items{number_of_index}").gameObject;
            GameObject numOfItems = GameObject.Find($"Inven items{number_of_index}").transform.Find($"number_of_items{number_of_index}").gameObject;

            if (!iconImage.activeSelf)
            {
                iconImage.SetActive(true);
                numOfItems.SetActive(true);
            }

            if (item.Value <= 0 || item.Key == null)
            {
                Inven_Items.Remove(item.Key);
                iconImage.SetActive(false);
                numOfItems.SetActive(false);
            }

            iconImage.GetComponent<Image>().sprite = Resources.Load<Sprite>($"Icons/{item.Key}") as Sprite;
            numOfItems.GetComponent<Text>().text = $"{item.Value}";
            ++number_of_index;
        }
    }

    public void InvenOff()
    {
        /*
        if (Inven_Items != null)
        {
            Debug.Log($"Inven_Items is not null");
            Inven_Items.Clear();
            //SelectedList = null;
        }
        
        //SelectedList = null;
        //selected_list_count = 6;
        Debug.Log($"count of items = {count_of_items}");
        Debug.Log("Exit");
        //foreach (KeyValuePair<string, int> item in SelectedList)
        //    Debug.Log(item.Key + " : " + item.Value);
        */
        //if (count_of_items != 0)
        //    count_of_items = 0;

        if (GameObject.Find("Inventory") != null)
            Managers.Resource.Destroy(GameObject.Find("Inventory"));
    }
}
