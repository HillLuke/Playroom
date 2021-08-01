using UnityEngine;

namespace Assets.Scripts.Scenes
{
    [CreateAssetMenu(fileName = "SceneData", menuName = "SceneData/New")]
    public class SceneData : ScriptableObject
    {
        public string SceneName;
    }
}