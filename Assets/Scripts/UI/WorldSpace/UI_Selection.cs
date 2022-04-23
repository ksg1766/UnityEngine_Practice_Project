using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Selection : UI_Base
{
    enum GameObjects
    {
        Selection
    }

    public override void Init()
    {
        Bind<GameObject>(typeof(GameObjects));
    }

    private void Update()
    {
        
    }
}
