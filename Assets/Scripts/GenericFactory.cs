using UnityEngine;

public class GenericFactory : MonoBehaviour
{
    [SerializeField] private GameObject m_prefab;
    [SerializeField] private float m_minSpawnDelay;
    [SerializeField] private float m_maxSpawnDelay;

    protected BoxCollider2D _spawnerCollider;
    protected float _spawnerLength;
    protected float _spawnerWidth;

    private void Awake()
    {
        _spawnerCollider = GetComponent<BoxCollider2D>();
    }

    private void Start()
    {
        GetSpawnerParametrs();
        SpawnObject();
    }

    private GameObject GetNewInstance()
    {
        Vector2 pos = (Vector2)transform.position + new Vector2(Random.Range(-_spawnerWidth / 2, _spawnerWidth / 2), Random.Range(-_spawnerLength / 2, _spawnerLength / 2));
        return Instantiate(m_prefab, pos, Quaternion.identity);
    }

    private void SpawnObject()
    {
        float spawnDelay = Random.Range(m_minSpawnDelay, m_maxSpawnDelay);
        GetNewInstance();
        Invoke("SpawnObject", spawnDelay);
    }

    private void GetSpawnerParametrs()
    {
        _spawnerWidth = _spawnerCollider.bounds.size.x;
        _spawnerLength = _spawnerCollider.bounds.size.y;
    }
}
