using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaremaFollow : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform target;
    public float smoothing;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void LateUpdate()
    {
        if(target != null)
        {
            if(transform.position != target.position)
            {
                Vector2 targetPos = target.position;
                transform.position = Vector2.Lerp(transform.position, targetPos, smoothing);//œﬂ–‘≤Ó÷µ
            }
        }
    }
}
