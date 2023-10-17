using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject m_projectilePrefab;
    [SerializeField] private float m_minSpawnDelay;
    [SerializeField] private float m_maxSpawnDelay;

    private BoxCollider2D _spawnerCollider;
    private float _spawnerLenth;
    private float _spawnerWidth;

    private void Awake()
    {
        _spawnerCollider = GetComponent<BoxCollider2D>();
    }

    private void Start()
    {
        GetSpawnerParametrs();
        SpawnProjectile();
    }

    private void SpawnProjectile()
    {
        float spawnDelay = Random.Range(m_minSpawnDelay, m_maxSpawnDelay);
        Instantiate(m_projectilePrefab, GetSpawnPoint(), transform.rotation);
        Invoke("SpawnProjectile", spawnDelay);
    }

    private Vector2 GetSpawnPoint()
    {
        Vector2 resultVector;
        resultVector = (Vector2)transform.position + new Vector2(Random.Range(-_spawnerWidth/2, _spawnerWidth/2), Random.Range(-_spawnerLenth/2, _spawnerLenth/2));
        return resultVector;
    }

    private void GetSpawnerParametrs()
    {
        _spawnerWidth = _spawnerCollider.bounds.size.x;
        _spawnerLenth = _spawnerCollider.bounds.size.y;
    }
}
