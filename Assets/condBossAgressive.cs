using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class condBossAgressive : MonoBehaviour
{
    public GameObject boss;
    public BossStateManager bossSM;
    public DialogueManager dManager;

    void Awake()
    {
        bossSM = boss.GetComponent<BossStateManager>();
        dManager = FindObjectOfType<DialogueManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if(dManager.counter > 2){
            bossSM.readyToAttack = true;
            StartCoroutine(disableThis());
        }
    }

    IEnumerator disableThis(){
        gameObject.SetActive(false);
        yield return null;
    }
}
