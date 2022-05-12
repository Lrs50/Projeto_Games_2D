using UnityEngine.SceneManagement;
using UnityEngine;
using System;
using System.Collections;
public static class Loader
{

    private class LoadingMonoBehavior : MonoBehaviour {};
    public enum Scene{
        MainMenu,
        MainHub,
        Phase1_0,
        Phase1_1,
        Phase1_2,
        Loading,
        Pre_start,
        PhaseFinal,
        Credits
    }

    private static Action onLoaderCallBack; 
    private static AsyncOperation asyncOperation;

    public static void Load(Scene scene){
    
        onLoaderCallBack = () =>{
           GameObject loadingGameObject = new GameObject("Loading Game Object");
           loadingGameObject.AddComponent<LoadingMonoBehavior>().StartCoroutine(LoadAsync(scene));
        };

        SceneManager.LoadScene(Scene.Loading.ToString());

    }

    private static IEnumerator LoadAsync(Scene scene){
        yield return null;
        asyncOperation =  SceneManager.LoadSceneAsync(scene.ToString());
        while(!asyncOperation.isDone){
            yield return new WaitForSeconds(1f);
        }
    }

    public static float GetLoadingProgress(){
        if(asyncOperation!=null){
            return asyncOperation.progress;
        }else{
            return 1f;
        }
    }

    public static void LoaderCallBack(){
        if(onLoaderCallBack!=null){
            onLoaderCallBack();
            onLoaderCallBack = null;
        }
    }

}
