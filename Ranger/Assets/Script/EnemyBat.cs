using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBat : Enemy
{
    public float speed;
    public float startWaitTime;
    private float waitTime;

    public Transform movePosition;
    public Transform leftDownPosition;
    public Transform rightUpPosition;


    // Start is called before the first frame update
    public void Start()
    {
        base.Start();//调用父类的Start方法
        waitTime = startWaitTime;
        movePosition.position = GetRandomPosition();
    }

    // Update is called once per frame
    public void Update()
    {
        base.Update();
        
        transform.position = Vector2.MoveTowards(transform.position, movePosition.position, speed * Time.deltaTime);
        if (Vector2.Distance(transform.position, movePosition.position) < Mathf.Epsilon)
        {
            if(waitTime<=0)
            {
                movePosition.position = GetRandomPosition();
                waitTime = startWaitTime;
            }
            else
            {
                waitTime -= Time.deltaTime;
            }
        }
    }

    Vector2 GetRandomPosition()
    {
        Vector2 randomPosition = new Vector2(Random.Range(leftDownPosition.position.x, rightUpPosition.position.x), Random.Range(leftDownPosition.position.y, rightUpPosition.position.y));
        return randomPosition;
    }
}
