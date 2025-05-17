using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    GameObject _enemyPrefab;

    float _timer = 10f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        _timer -= Time.deltaTime;

        if (_timer <= 0f)
        {
            Instantiate(_enemyPrefab, transform.position, Quaternion.identity);
            _timer += 10f;
        }
    }
}
