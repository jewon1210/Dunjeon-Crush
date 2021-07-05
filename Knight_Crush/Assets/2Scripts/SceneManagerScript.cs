using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerScript : MonoBehaviour
{
    static SceneManagerScript UniqueInstance;
    public static SceneManagerScript _instance
    {
        get { return UniqueInstance; }
    }

    public enum CurScene
    {
        EmptyS = 0,
        LobbyS,
        
    }

    void Start()
    {
    }

    void Update()
    {
    }

    void GoLobbyScene()
    {
        SceneManager.LoadScene("LobbyScene");
    }

    void GoInGameScene()
    {
        SceneManager.LoadScene("InGameScene");
    }

    void GoStageSelectScene()
    {
        
    }

    void GoResultScene()
    {

    }

    void GoCharacterMakingScene()
    {

    }

}
