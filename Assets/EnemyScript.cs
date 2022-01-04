using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public int enemyHP = 100;
    public float destroyTime = 10f;

    public Animator animator;
    public delegate void EnemyKilled();
    public static event EnemyKilled OnEnemyKilled;

    // Start is called before the first frame update

    public void Start()
    {
        
    }

    
    public void TakeDamage(int damageAmount)
    {
        enemyHP -= damageAmount;
        if(enemyHP <= 0)
        {
            //Play Death Animation
            animator.SetTrigger("death");
            Destroy(this.gameObject, destroyTime);
            GetComponent<CapsuleCollider>().enabled = false;
        }
        if (OnEnemyKilled != null)
        {
            OnEnemyKilled();
        }
        else
        {
            //Play Damage Animation
            animator.SetTrigger("damage");
        }
    }


}
