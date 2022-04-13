using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public int damage;

    private Animator myAnimator;
    private PolygonCollider2D myCollider;

    // Start is called before the first frame update
    void Start()
    {
        myAnimator = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();
        myCollider = GetComponent<PolygonCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Attack();
    }

    void Attack()
    {
        if (Input.GetButtonDown("Attack"))
        {
            if (!myAnimator.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
            {
                //myCollider.enabled = true;
                myAnimator.SetTrigger("Attack");
                StartCoroutine(StartAttack());
            }  
        }
    }
    IEnumerator StartAttack()
    {
        yield return new WaitForSeconds(0.27f);//试了几个数据，0.27时碰撞箱和动画比较契合
        myCollider.enabled = true;
        StartCoroutine(DisableHitBox());
    }
    IEnumerator DisableHitBox()
    {
        yield return new WaitForSeconds(0.1f);
        myCollider.enabled = false;
        StartCoroutine(AttackCD());
    }
    IEnumerator AttackCD()
    {
        yield return new WaitForSeconds(0.2f);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            other.GetComponent<Enemy>().Hurt(damage);
        }
    }
}
