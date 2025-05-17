using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int hp = 10;

    Transform _playerPos;

    // Start is called before the first frame update
    void Start()
    {
        _playerPos = GameObject.Find("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (hp <= 0f)
            Destroy(this.gameObject);

        this.transform.position = Vector3.MoveTowards(this.transform.position, _playerPos.position, 3f * Time.deltaTime);
    }
}
