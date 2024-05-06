using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class EnemyScript : MonoBehaviour
{

    public int HP = 100;
    public Animator animator;
    public Slider healthBar;
    
    void Update()
    {
        healthBar.value = HP;
        healthBar.transform.position = transform.position + new Vector3(0f,1.3f,0);
    }

    public void TakeDamage(int damageAmount)
    {
        HP -= damageAmount;

        if (HP <= 0)
        {
            GetComponent<Collider>().enabled = false;
            healthBar.gameObject.SetActive(false);
            Destroy(healthBar);
        }
    }
}
