using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeScene : MonoBehaviour
{
    public void ChangeGameScene(string name)
    {
        GameSceneManager.instance.ChangeScene(name);
    }
}
