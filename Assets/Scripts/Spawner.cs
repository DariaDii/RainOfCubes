using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Transform[] _spawnPoint;
    [SerializeField] private Cube _cubePrefab;
    [SerializeField] private int _poolMaxSize = 5;
    [SerializeField] private int _poolCapacity = 5;
    [SerializeField] private float _repatRate = 1.0f;    

    private ObjectPool<Cube> _pool;

    private void Awake()
    {
        _pool = new ObjectPool<Cube>(
            createFunc: () => Instantiate(_cubePrefab),
            actionOnGet: (cube) => OnGetAction(cube),
            actionOnRelease: (cube) => ActionOnRelease(cube),
            actionOnDestroy: (cube) => Destroy(cube.gameObject),
            collectionCheck: true,
            defaultCapacity: _poolCapacity,
            maxSize: _poolMaxSize);
    }

    private void OnGetAction(Cube cube)
    {
        int randomIndex = Random.Range(0, _spawnPoint.Length);
        Transform randomPoint = _spawnPoint[randomIndex];
        cube.gameObject.transform.position = randomPoint.position;
        cube.gameObject.SetActive(true);
        cube.TimeOver += ReturnToPool;
    }

    private void ReturnToPool(Cube cube)
    {
        _pool.Release(cube);
    }

    private void ActionOnRelease(Cube cube)
    {
        cube.gameObject.SetActive(false);
        cube.ResetParameters();
        cube.TimeOver -= ReturnToPool;
    }

    private void Start()
    {
        StartCoroutine(Spawn());
    }

    private IEnumerator Spawn()
    {
        var wait = new WaitForSeconds(_repatRate);

        while (enabled)
        {
            GetCube();
            yield return wait;
        }
    }

    private void GetCube()
    {
        _pool.Get();
    }
}