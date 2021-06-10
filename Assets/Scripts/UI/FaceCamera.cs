using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceCamera : MonoBehaviour
{
    Camera _camera;

    private void Awake()
    {
        _camera = Camera.main;    
    }

    void Update()
    {
        transform.rotation = Quaternion.LookRotation(_camera.transform.forward);
    }
}
