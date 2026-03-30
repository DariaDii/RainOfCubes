using System;
using UnityEngine;

public class ObstacleDetector : MonoBehaviour
{
    public event Action ObjectTouchedPlatform;

    private void OnCollisionEnter(Collision collision)
    {
       if(collision.gameObject.TryGetComponent<Platform>(out _))
        {
            ObjectTouchedPlatform?.Invoke();
        }

    }
}
