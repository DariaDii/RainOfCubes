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
            actionOnGet: (cube) => ActionOnGet(cube),
            actionOnRelease: (cube) => cube.gameObject.SetActive(false),
            actionOnDestroy: (cube) => Destroy(cube),
            collectionCheck: true,
            defaultCapacity: _poolCapacity,
            maxSize: _poolMaxSize);
    }

    private void ActionOnGet(Cube cube)
    {
        int randomIndex = Random.Range(0, _spawnPoint.Length);
        Transform randomPoint = _spawnPoint[randomIndex];
        cube.gameObject.transform.position = randomPoint.position;
        cube.gameObject.SetActive(true);
        cube.TimeOver += ReleaseCube;
    }

    private void ReleaseCube(Cube cube)
    {
        cube.TimeOver -= ReleaseCube;
        _pool.Release(cube);
    }

    private void Start()
    {
        InvokeRepeating(nameof(GetSphere), 0.0f, _repatRate);
    }

    private void GetSphere()
    {
        _pool.Get();
    }
}