using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerZıone : MonoBehaviour
{
    private EnemyAI enemy;
    void Start()
    {
        enemy = GetComponentInParent<EnemyAI>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            gameObject.SetActive(false);
            enemy.target = collision.transform;
            enemy.inRange = true;
            enemy.actionZone.SetActive(true);

        }
    }
}
