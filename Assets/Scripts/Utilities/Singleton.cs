using Sirenix.OdinInspector;
using System;
using UnityEngine;

namespace Assets.Scripts.Utilities
{
    /// <summary>
    /// Singleton class
    /// </summary>
    /// <typeparam name="T">Type of the singleton</typeparam>
    public abstract class Singleton<T> : MonoBehaviour where T : Singleton<T>
    {
        [ReadOnly]
        public bool isInitialized = false;
        [ReadOnly]
        public bool isSetup = false;

        public Action ActionInitialized;
        public Action ActionSetup;

        /// <summary>
        /// The static reference to the instance
        /// </summary>
        public static T instance { get; protected set; }

        /// <summary>
        /// Gets whether an instance of this singleton exists
        /// </summary>
        public static bool instanceExists
        {
            get { return instance != null; }
        }

        protected virtual void Start()
        {
        }

        protected virtual void Setup()
        {
            isSetup = true;
            if (ActionSetup != null)
            {
                ActionSetup.Invoke();
            }
        }

        /// <summary>
        /// Awake method to associate singleton with instance
        /// </summary>
        protected virtual void Awake()
        {
            if (instanceExists)
            {
                Destroy(gameObject);
            }
            else
            {
                instance = (T)this;
            }

            isInitialized = true;

            if (ActionInitialized != null)
            {
                ActionInitialized.Invoke();
            }
        }

        /// <summary>
        /// OnDestroy method to clear singleton association
        /// </summary>
        protected virtual void OnDestroy()
        {
            if (instance == this)
            {
                instance = null;
            }
        }
    }
}