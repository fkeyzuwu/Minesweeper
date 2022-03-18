using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGame : MonoBehaviour
{
    public void StartGameScene()
    {
        GameSceneManager.instance.ChangeScene("Game");
    }
}
