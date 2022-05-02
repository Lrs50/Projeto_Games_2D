using System.Collections;
using UnityEngine;

public class AttackLibraryInitializer : MonoBehaviour{
    [SerializeField] private GameObject prefabDefault;
    [SerializeField] private GameObject prefabHoming;
    [SerializeField] private GameObject prefabArc;
    void Awake() => AttackLibrary.Initialize(prefabDefault,prefabHoming,prefabArc);
    
}