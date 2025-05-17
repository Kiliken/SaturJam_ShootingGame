using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    [SerializeField]
    private Transform _playerPos;

    [SerializeField]
    private Transform _scope;

    [SerializeField]
    private LayerMask _enemyLayer;

    private Vector3 _mousePos;
    private Vector3 _screenPos;
    private Vector3 _direction;

    private float _sign;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Aim();

        Debug.DrawLine(this.transform.position, _screenPos);

        if (Input.GetButtonDown("Fire1"))
            Shoot();

    }

    void Aim()
    {
        _mousePos = Input.mousePosition;
        _mousePos.z = -Camera.main.transform.position.z;
        _screenPos = Camera.main.ScreenToWorldPoint(_mousePos);

        _direction = _screenPos - this.transform.position;
        _sign = (_direction.y >= 0) ? 1 : -1;

        _scope.transform.eulerAngles = new Vector3(0f, 0f, Vector3.Angle(Vector3.right, _direction) * _sign);

    }

    void Shoot()
    {
        RaycastHit hit;

        
        if (Physics.Raycast(this.transform.position, _direction, out hit,Mathf.Infinity, _enemyLayer))
        {
            hit.transform.GetComponent<Enemy>().hp -= 1;

            //Fix Direction and use AddForce
            hit.rigidbody.AddForceAtPosition(Vector2.one * 2f, hit.point, ForceMode.Impulse);
        }

    }
}
