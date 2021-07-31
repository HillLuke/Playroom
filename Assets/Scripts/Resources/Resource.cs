using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Resources
{
    [CreateAssetMenu(fileName = "Resource", menuName = "Resource/New Resource")]
    public class Resource : ScriptableObject
    {
        public string ResourceName;
    }
}