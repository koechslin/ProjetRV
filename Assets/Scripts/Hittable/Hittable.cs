
using UnityEngine;

[RequireComponent(typeof(Collider))]
public abstract class Hittable : MonoBehaviour
{
    public abstract void Hit();
}
