using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//Static class, dont destroy when change scene
public static class Loader_Static 
{
    public enum Scene{
        MenuScene,
        GameScene,
        LoadingScene,
    }

    private static Scene targetScene;

    public static void Load(Scene _targetScene){
        //Set target
        Loader_Static.targetScene = _targetScene;

        //Call loading first
        SceneManager.LoadScene(Scene.LoadingScene.ToString());
    }

    public static void LoaderCallBack(){

        //Call target to next in loading
        SceneManager.LoadScene(targetScene.ToString());
    }
}
