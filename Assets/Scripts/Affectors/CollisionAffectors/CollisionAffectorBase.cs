using UnityEngine;

[RequireComponent(typeof(Collider))]
public abstract class CollisionAffectorBase : MonoBehaviour
{
    protected virtual void Start() {}

    protected virtual void Update() {}
}
