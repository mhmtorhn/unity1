using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponStat : MonoBehaviour
{
    float attackPower;
    float totalAttack;
    public float weaponAttack;

    PlayerController player;
    void Start()
    {
        player = GetComponent<PlayerController>();
        attackPower = player.damage;
    }

    
    void Update()
    {
        
    }

    public float DamageInput()
    {
        totalAttack = attackPower + weaponAttack;
        float finalAttack=Mathf.Round(Random.Range(totalAttack-7,totalAttack+5));
        if(finalAttack > totalAttack+1) 
        {
            finalAttack *= 2;
        }
        return finalAttack; 
    }



}
