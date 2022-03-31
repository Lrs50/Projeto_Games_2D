using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update

    static GameManager gameManager;
    void Awake()
    {
        
        if(gameManager==null){
            gameManager = this;
        }else{
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        if(Input.GetKeyDown("space")){
            Loader.Load(Loader.Scene.MainMenu);
        }
    }

    public void MenuToHub(){
        Loader.Load(Loader.Scene.MainHub);
    }

}
