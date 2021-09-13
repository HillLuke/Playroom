using Assets.Scripts.Scenes;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.Interactable
{
    public class SceneLoadingPortal : InteractableBase
    {
        public SceneData SceneData;

        public override string InteractUIMessage => $"Portal to {SceneData.SceneName} ({_inputManager.PlayerInputData.Action_Use.KeyCode})";

        public override void Interact(GameObject Interactor)
        {
            SceneManager.LoadScene(SceneData.SceneName, LoadSceneMode.Single);
        }

        //public override void LookAt(GameObject Interactor)
        //{
        //    base.LookAt(Interactor);
        //}

        //public override void LookAway(GameObject Interactor)
        //{
        //    base.LookAway(Interactor);
        //}
    }
}