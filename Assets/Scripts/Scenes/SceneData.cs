using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.Scenes
{
    [CreateAssetMenu(fileName = "SceneData", menuName = "SceneData/New")]
    public class SceneData : ScriptableObject
    {
        public string SceneName;
    }
}