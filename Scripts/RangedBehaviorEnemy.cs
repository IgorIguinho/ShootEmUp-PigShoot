using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RangedBehaviorEnemy : MonoBehaviour
{
    EntityStats entityStats;

    float moveSpeed;
    public float horizontalMoviment;
    public float verticalMoviment;
    float invertMoviment;

    public GameObject bullet;

    public GameObject objectLimits;

    //Moviment
    float cooldownCountM;
    bool canM = true;

    //Attack
    float cooldownCountA;
    bool canA = true;
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
        
        moviment();
        Attack();
        Cooldown();
    }


    void moviment()
    {
        float dist = Vector2.Distance(transform.position, objectLimits.transform.position);

        if (dist > 4)
        {
            gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(horizontalMoviment, 0));
            TradeDirection();
        }
        else
        {
            gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, verticalMoviment));
        }
    }

    void TradeDirection()
    {
        if (horizontalMoviment > 0 && canM == true)
        {
            horizontalMoviment = -invertMoviment;
            canM = false;
            cooldownCountM = 0;
        }
        else if (horizontalMoviment < 0 && canM == true)
        {
            horizontalMoviment = invertMoviment;
            canM = false;
            cooldownCountM = 0;
        }

       
    }

    void Attack()
    {
        if (canA == true)
        {
            GameObject newBullet = Instantiate(bullet, transform.position, Quaternion.identity);

            newBullet.GetComponent<BulletDamage>().damage = entityStats.dmg;
            newBullet.GetComponent<BulletDamage>().speed = entityStats.speedBullet;
            canA = false;
            cooldownCountA = 0;
        }
    }

    void Cooldown()
    {
        //Moviment
        if (canM == false && cooldownCountM > Random.Range(0.5f, 5))
        {
            canM = true;

        }
        else
        {
            cooldownCountM += Time.deltaTime;
        }

        //Attack
        if (canA == false && cooldownCountA > 2)
        {
            canA = true;

        }
        else
        {
            cooldownCountA += Time.deltaTime;
        }

    }
}
