using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private Transform player;

    [SerializeField]
    private float _followSpeed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(this.transform.position, player.position) > 7f)
            this.transform.position = Vector3.MoveTowards(this.transform.position, player.position, _followSpeed * Time.deltaTime);

        if (Input.GetButton("Fire2"))
            this.transform.position = player.position;
    }

}
