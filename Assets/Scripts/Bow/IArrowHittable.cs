using UnityEngine;

public interface IArrowHittable
{
    public void Hit(Arrow arrow, Vector3 hitPoint);
}
