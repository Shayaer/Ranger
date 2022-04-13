using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    public int health;
    public int damage;

    public float flashTime;

    public GameObject bloodEffect;

    private SpriteRenderer sr;
    private Color originalColour;//记录原始的colour


    // Start is called before the first frame update
    public void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        originalColour = sr.color;
    }

    // Update is called once per frame
    public void Update()
    {
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void Hurt(int damage)
    {
        health -= damage;
        FlashColour(flashTime);
        Instantiate(bloodEffect, transform.position, Quaternion.identity);//实例化
    }

    void FlashColour(float time)
    {
        sr.color = Color.red;
        Invoke("ResetColour", time);
    }
    void ResetColour()
    {
        sr.color = originalColour;
    }
}
