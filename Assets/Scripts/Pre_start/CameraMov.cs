using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMov : MonoBehaviour
{
    public float velH;
    public float velV;

    void Start()
    {
    }

    void Update()
    {
        Camera.main.transform.position = new Vector3(Camera.main.transform.position.x+(velH*Time.deltaTime),Camera.main.transform.position.y+(velV*Time.deltaTime),Camera.main.transform.position.z);
    }
}
