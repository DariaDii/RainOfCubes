using System;
using UnityEngine;

public class CollisionHandling : MonoBehaviour
{
    public event Action ObjectTouchedPlatform;

    private void OnCollisionEnter(Collision collision)
    {
       if(collision.gameObject.GetComponent<Platform>() != null)
        {
            ObjectTouchedPlatform?.Invoke();
        }
    }
}
