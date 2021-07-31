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

        public override string InteractUIMessage => $"Portal to {SceneData.SceneName} ({_inputManager.PlayerInputData.Use})";

        public override void Interact(GameObject Interactor)
        {
            SceneManager.LoadScene(SceneData.SceneName, LoadSceneMode.Single);
        }

        public override void LookAt(GameObject Interactor)
        {
            base.LookAt(Interactor);
        }

        public override void LookAway(GameObject Interactor)
        {
            base.LookAway(Interactor);
        }

    }
}