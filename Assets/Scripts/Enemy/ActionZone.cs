using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionZone : MonoBehaviour
{
    private bool inRange;
    private Animator anim;
    private EnemyAI enemy;
    void Start()
    {
        anim = GetComponentInParent<Animator>();
        enemy = GetComponentInParent<EnemyAI>();
    }

    // Update is called once per frame
    void Update()
    {
        if (inRange && !anim.GetCurrentAnimatorStateInfo(0).IsName("Attack1"))
        {
            enemy.Flip();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            inRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            inRange = false;
            gameObject.SetActive(false);
            enemy.triggerZone.SetActive(true);
            enemy.inRange = false;
            enemy.SelectTarget();
            
        }
    }

}
