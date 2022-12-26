using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamageArea : MonoBehaviour
{
     [SerializeField]
     private float deactivateWaitTime = 0.1f;
     private float deactivateTimer;

     [SerializeField]
     private LayerMask playerLayer;

     private bool canDealDamage;

     [SerializeField]
     private float damageAmount = 5f;

    // Player Health
    private PlayerHealth playerHealth;

    private void Awake()
    {
        playerHealth = GameObject.FindWithTag("Player").GetComponent<PlayerHealth>();

        gameObject.SetActive(false);
    }

    private void Update()
    {
        if(Physics2D.OverlapCircle(transform.position, 1f, playerLayer))
        {
            if(canDealDamage)
            {
                canDealDamage = false;
                // deal damage to player
                playerHealth.TakeDamage(damageAmount);
            }
        }
        DeactivateDamageArea();
    }

    void DeactivateDamageArea()
    {
        if (Time.time > deactivateTimer)
            gameObject.SetActive(false);
    }

    public void ResetDeactivateTimer()
    {
        canDealDamage = true;
        deactivateTimer = Time.time + deactivateWaitTime;
    }
}
