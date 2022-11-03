using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : BaseScene
{
    // Start is called before the first frame update
    public void MoveToOtherScene()
    {
        Scene scene = SceneManager.GetActiveScene();
        if (scene.name == "TestScene")
            Managers.Scene.LoadScene(Define.Scene.Stage1);
        else if (scene.name == "Stage1")
            Managers.Scene.LoadScene(Define.Scene.TestScene);
    }

    public override void Clear()
    {

    }
}
