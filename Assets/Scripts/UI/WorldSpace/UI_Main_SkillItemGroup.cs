using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Main_SkillItemGroup: UI_Base
{
    PlayerStat _stat;

    public override void Init()
    {
        _stat = GameObject.FindWithTag("Player").GetComponent<PlayerStat>();
        for(int i = 1; i < 4; i++)
            GameObject.Find($"Item{i}").transform.Find($"number_item_slot{i}").gameObject.GetComponent<Text>().text = "0";
    }

    void Update()
    {
        if (_stat.PlayerInventory.Inven_Items.ContainsKey("SmallPotion"))
            GameObject.Find("Item1").transform.Find("number_item_slot1").gameObject.GetComponent<Text>().text = $"{_stat.PlayerInventory.Inven_Items["SmallPotion"]}";

        if (_stat.PlayerInventory.Inven_Items.ContainsKey("MediumPotion"))
            GameObject.Find("Item2").transform.Find("number_item_slot2").gameObject.GetComponent<Text>().text = $"{_stat.PlayerInventory.Inven_Items["MediumPotion"]}";

        if (_stat.PlayerInventory.Inven_Items.ContainsKey("LargePotion"))
            GameObject.Find("Item3").transform.Find("number_item_slot3").gameObject.GetComponent<Text>().text = $"{_stat.PlayerInventory.Inven_Items["LargePotion"]}";
    }
}
