using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{

    EntityStats entityStats;

    float moveSpeed;
    public float horizontalMoviment;
    float invertMoviment;

    float cooldownCount;
    bool can = true;
    // Start is called before the first frame update
    void Start()
    {
        entityStats = gameObject.GetComponent<EntityStats>();
        moveSpeed = entityStats.baseSpeed;
        invertMoviment = horizontalMoviment;
     
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2( horizontalMoviment, -moveSpeed * Time.deltaTime));
        TradeDirection();
        Cooldown();
    }


    void TradeDirection()
    {
        if ( horizontalMoviment > 0 && can == true)
        {
            horizontalMoviment = -invertMoviment;
            can = false;
            cooldownCount = 0;
        }
        else if (horizontalMoviment < 0 && can == true)
        {
            horizontalMoviment = invertMoviment;
            can = false;
            cooldownCount = 0;
        }
    }

    void Cooldown()
    {
        if (can == false && cooldownCount > 1.5f)
        {
            can = true;

        }
        else
        {
            cooldownCount += Time.deltaTime;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
           collision.GetComponent<EntityStats>().TakeDamage(entityStats.dmg);
            Destroy(this.gameObject);
        }
        else if (collision.gameObject.tag == "Wall")
            { Destroy(this.gameObject); }
    }
}
