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
        UniqueInstance = this;

        GoLobbyScene();
    }

    void Update()
    {
    }

    public void GoLobbyScene()
    {
        SceneManager.LoadScene("LobbyScene");
    }

    public void GoInGameScene()
    {
        SceneManager.LoadScene("InGameScene");
    }

    public void GoTutorialScene()
    {
        SceneManager.LoadScene("TutorialScene");
    }

    public void GoStoryScene()
    {
        SceneManager.LoadScene("StoryScene");
    }

    public void GoStageSelectScene()
    {
        
    }

    public void GoResultScene()
    {

    }

    public void GoCharacterMakingScene()
    {

    }

}
