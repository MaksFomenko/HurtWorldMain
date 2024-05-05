using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagScript : MonoBehaviour
{
    public int damedeAmount = 20;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            other.GetComponent<EnemyScript>().TakeDamage(damedeAmount);
        }
    }
}
