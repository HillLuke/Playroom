using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.Interactable
{
public class SceneLoadingPortal : InteractableBase
{
    public string SceneToLoad;

    public override void Use()
    {
        Debug.Log($"Portal used - {SceneToLoad}");
        SceneManager.LoadScene(SceneToLoad, LoadSceneMode.Single);
    }    
}
}