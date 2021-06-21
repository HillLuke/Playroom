using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.UI
{
public class FaceCamera : MonoBehaviour
{        
    UnityEngine.Camera _camera;

    private void Awake()
    {
        _camera = UnityEngine.Camera.main;    
    }

    void Update()
    {
        transform.rotation = Quaternion.LookRotation(_camera.transform.forward);
    }
}
}