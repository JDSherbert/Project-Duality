using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sherbert.AI;
using Sherbert.GameplayStatics;
using Sherbert.Framework;

public class MJB_BirdScript : JDH_AIBaseFramework
{
    [SerializeField] private float cooldown = 3.0f;
    [SerializeField] private float spawnDelay = 3.0f;

    private Vector3 lastPlayerPosition;

    void Start()
    {
        InitializeAI();
    }

    private void FixedUpdate()
    {
        BehaviourHandler();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (JDH_World.GetWorldIsEvil())
        {
            if (collision.gameObject.CompareTag(EntityTypes.PLAYER))
            {
                PlayerInteraction(collision.gameObject);
            }
            else if (collision.gameObject.CompareTag(EntityTypes.BUNNY))
            {
                BunnyInteraction(collision.gameObject);
            }
        }
    }

    public override void InitializeAI()
    {
        base.InitializeAI();
        baseProperties.target = base.AcquireTarget();
        baseProperties.chaseSpeed = 5.0f;
        lastPlayerPosition = baseProperties.target.transform.position;
    }

    public override void BehaviourHandler()
    {
        base.BehaviourHandler();
        base.UpdateAnimator(GetDirection());
        if (JDH_World.GetWorldIsEvil())
        {
            if (baseProperties.chasing)
            {
                base.events.OnDetectPlayer.Invoke(base.baseProperties.target);
                ChasePlayer();
            }
        }
    }

    private void ChasePlayer()
    {
        if (baseProperties.target != null)
        {
            if (CanStillSeePlayer())
            {
                transform.Translate((baseProperties.target.transform.position - transform.position) * Time.deltaTime * baseProperties.chaseSpeed);
            }
            else
            {
                transform.Translate((lastPlayerPosition - transform.position) * Time.deltaTime * baseProperties.chaseSpeed);
            }
        }
    }

    private bool CanStillSeePlayer()
    {
        Vector3 playerDirection = baseProperties.target.transform.position - transform.position;
        playerDirection.Normalize();
        RaycastHit2D checkForPlayer = Physics2D.Raycast(transform.position + playerDirection, playerDirection);
        if (checkForPlayer.collider.gameObject.CompareTag(EntityTypes.PLAYER))
        {
            lastPlayerPosition = checkForPlayer.collider.gameObject.transform.position;
            return true;
        }
        return false;
    }

    private void PlayerInteraction(GameObject playerObject)
    {
        playerObject.GetComponent<JDH_HealthSystem>().DealDamage();
        Destroy(gameObject);
    }

    private void BunnyInteraction(GameObject bunny)
    {
        StartCoroutine(SpawnKind(bunny.transform.position));
        GameObject.Find("BunnySoundManager").GetComponent<MJB_BunnySoundManager>().RemoveBunny(bunny);
        bunny.GetComponent<JDH_HealthSystem>().DealDamage();
        Destroy(bunny);
    }

    public void TriggerBird()
    {
        if (!baseProperties.chasing)
        {
            StartCoroutine(ChaseCooldown());
        }
        baseProperties.chasing = true;
    }

    private IEnumerator ChaseCooldown()
    {
        yield return new WaitForSeconds(cooldown);
        baseProperties.chasing = false;
    }

    private IEnumerator SpawnKind(Vector3 spawnPosition)
    {
        yield return new WaitForSeconds(spawnDelay);
        Instantiate(baseProperties.self, spawnPosition, transform.rotation);
    }

    public override void Transformation()
    {
        base.Transformation();
    }

    public float GetSpeed()
    {
        if (baseProperties.chasing)
        {
            return baseProperties.chaseSpeed;
        }
        return 0;
    }

    public Vector3 GetDirection()
    {
        if (!baseProperties.chasing)
        {
            return Vector3.zero;
        }
        if (CanStillSeePlayer())
        {
            return baseProperties.target.transform.position - transform.position;
        }
        else
        {
            return lastPlayerPosition - transform.position;
        }
    }
}
