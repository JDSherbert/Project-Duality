using System.Collections;
using UnityEngine;

using Sherbert.AI;
using Sherbert.GameplayStatics;

public class MJB_BirdScript : JDH_AIBaseFramework
{
    public float cooldown = 3.0f;
    public float spawnDelay = 3.0f;

    void Start()
    {
        InitializeAI();
    }

    private void FixedUpdate()
    {
        BehaviourHandler();
    }

    private void OnTriggerEnter2D(Collider2D collision)
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

    private void ChasePlayer()
    {
        if (baseProperties.target != null)
        {
            transform.Translate((baseProperties.target.transform.position - transform.position) * Time.deltaTime * baseProperties.chaseSpeed);
        }
    }

    private void PlayerInteraction(GameObject playerObject)
    {
        // Kill the player here
    }

    private void BunnyInteraction(GameObject bunny)
    {
        StartCoroutine(SpawnKind(bunny.transform.position));
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

    //!------------- VIRTUAL METHODS ----------------!//

    //! Override - Sets defaults
    public override void InitializeAI()
    {
        base.InitializeAI();
        baseProperties.target = base.AcquireTarget();
        baseProperties.chaseSpeed = 5.0f;

    }

    //! Override - update behaviour
    public override void BehaviourHandler()
    {
        base.BehaviourHandler();
        if (JDH_World.GetWorldIsEvil())
        {
            if (baseProperties.chasing)
            {
                base.events.OnDetectPlayer.Invoke(base.baseProperties.target);
                ChasePlayer();
            }
        }
    }

    //! Override - world transform state
    public override void Transformation()
    {
        base.Transformation();
    }
}
