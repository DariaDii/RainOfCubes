using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Cube : MonoBehaviour
{
    [SerializeField] private Rigidbody _rigidbody;    

    [SerializeField] private ColorChanger _colorChanger;
    [SerializeField] private ObstacleDetector _obstacleDetector;

    private bool _canChange = true;
    private int _minLiveTime = 2;
    private int _maxLiveTime = 5;    

    public event Action<Cube> TimeOver;

    private void OnEnable()
    {
        _obstacleDetector.ObjectTouchedPlatform += ChangeParameters;
    }

    private void OnDisable()
    {
        _obstacleDetector.ObjectTouchedPlatform -= ChangeParameters;
    }

    public void ResetParameters()
    {
        _colorChanger.ResetState();        
        _rigidbody.linearVelocity = Vector3.zero;
        _rigidbody.angularVelocity = Vector3.zero;
        _canChange = true;
    }

    private void ChangeParameters()
    {
        if (!_canChange)
            return;
        
        _colorChanger.ChangeToRandomColor();
        StartCoroutine(ChangingLifetime());
        _canChange = false;
    }

    private IEnumerator ChangingLifetime()
    {
        yield return new WaitForSeconds(UnityEngine.Random.Range(_minLiveTime,_maxLiveTime));

        TimeOver?.Invoke(this);
    }
}
