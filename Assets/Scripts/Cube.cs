using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Cube : MonoBehaviour
{
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private Renderer _renderer;

    [SerializeField] private ColorChanger _colorChanger;
    [SerializeField] private CollisionHandling _collisionHandling;

    private bool isChanged = false;
    private int _minLiveTime = 2;
    private int _maxLiveTime = 5;

    public event Action<Cube> TimeOver;

    private void OnEnable()
    {
        _collisionHandling.ObjectTouchedPlatform += Change;
    }

    private void OnDisable()
    {
        _collisionHandling.ObjectTouchedPlatform -= Change;
    }

    private void Change()
    {
        if (isChanged)
            return;
        
        _colorChanger.ChangeToRandomColor(_renderer);
        StartCoroutine(Lifetime());
        isChanged = true;
    }

    private IEnumerator Lifetime()
    {
        yield return new WaitForSeconds(UnityEngine.Random.Range(_minLiveTime,_maxLiveTime));

        TimeOver?.Invoke(this);
    }
}
