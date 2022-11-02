using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScene : BaseScene
{
    Coroutine co;
    UI_Popup tempInven;
    UI_Popup tempMenu;
    bool InvenOn = false;

    protected override void Init()
    {
        base.Init();
        SceneType = Define.Scene.Game;
        //
        //Managers.UI.ShowSceneUI<UI_Inven>();
        Dictionary<int, Data.Stat> dict = Managers.Data.StatDict;

        gameObject.GetOrAddComponent<CursorController>();

        GameObject player = Managers.Game.Spawn(Define.WorldObject.Player, "PlayerCharacter(temp)");//, GameObject.Find("PlayerCharacter").transform);// GameObject.Find("PlayerCharacter").transform
        Camera.main.gameObject.GetOrAddComponent<CameraController>().SetPlayer(player);
        GameObject.FindWithTag("MiniCam").GetOrAddComponent<MiniCamController>().SetPlayer(player);

        //Managers.Game.Spawn(Define.WorldObject.Monster, "Knight");
        GameObject go = new GameObject { name = "SpawningPool" };
        SpawningPool pool = go.GetOrAddComponent<SpawningPool>();
        pool.SetKeepMonsterCount(3);
    }

    private void Update()
    {
        if (InvenOn == false && Input.GetKeyDown(KeyCode.I))
        {
            tempInven = Managers.UI.ShowPopupUI<UI_Inven>();
            InvenOn = true;
        }
        else if (InvenOn == true && Input.GetKeyDown(KeyCode.I))
        {
            Managers.UI.ClosePopupUI(tempInven);
            InvenOn = false;
        }
    }

    public override void Clear()
    {

    }
}
