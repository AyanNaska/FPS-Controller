using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class orientation : MonoBehaviour
{
    public GameObject Cameraobject;
    void Update()
    {
        this.transform.rotation = Cameraobject.transform.rotation;
    }
}
