using Assets.Scripts.Scenes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.Interactable
{
    public class SceneLoadingPortal : InteractableBase
    {
        public SceneData SceneData;

        public override void Use()
        {
            Debug.Log($"Portal used - {SceneData.SceneName}");
            SceneManager.LoadScene(SceneData.SceneName, LoadSceneMode.Single);
        }
    }
}