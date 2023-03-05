using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bang : MonoBehaviour
{

    public Transform firePoint;
    public float rightOffset;
    public float leftOffset;
    private PlayerDirection dir;

    private void Start()
    {
        firePoint = GameObject.Find("GunTip").GetComponent<Transform>();
        dir = GameObject.Find("Player").GetComponent<PlayerDirection>();
        StartCoroutine(DeleteAfterFrames(12, gameObject));
    }

    private void Update()
    {
        
        if (!dir.facingRight)
        {
            transform.rotation = Quaternion.Euler(0, 180f, 0);
            transform.position = new Vector3(firePoint.position.x + leftOffset, firePoint.position.y, 0f);
        } else
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
            transform.position = new Vector3(firePoint.position.x + rightOffset, firePoint.position.y, 0f);
        }
    }

    IEnumerator DeleteAfterFrames(int framesToWait, GameObject bang)
    {
        for (int i = 0; i < framesToWait; i++)
        {
            yield return null;
        }

        Destroy(bang);
    }
}
