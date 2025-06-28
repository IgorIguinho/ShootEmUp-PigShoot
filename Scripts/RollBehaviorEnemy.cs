using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollBehaviorEnemy : MonoBehaviour
{
    EntityStats entityStats;

    public GameObject bullet;

    bool movimentNormal = true;
    float movimentVerticalSpeed;

   

    float cooldownCount;
    bool can = true;

    public List<GameObject> target;
    // Start is called before the first frame update
    void Start()
    {
        entityStats = gameObject.GetComponent<EntityStats>();
        movimentVerticalSpeed = entityStats.baseSpeed ;
      
    }

    // Update is called once per frame
    void FixedUpdate()
    {
         
        Moviment();
        Attck();
        Cooldown();
    }

    void Moviment()
    {
        if (movimentNormal == true)
        {
            gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, -entityStats.baseSpeed  * Time.deltaTime));
        }
        
        else 
        {
            gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2( movimentVerticalSpeed * Time.deltaTime,0));
        }
    }

    void Attck()
    {
        if (can == true)
        {
            for (int i = 0; i < 9; i++)
            {
                GameObject newBullet = Instantiate(bullet, transform.position, Quaternion.identity);


                newBullet.GetComponent<BulletDamageRoll>().damage = entityStats.dmg;
                //newBullet.GetComponent<BulletDamage>().speed = entityStats.speedBullet;

                Vector2 bullet_direction = target[i].transform.position;

                bullet_direction.Normalize();

                //rotação
                float rotZ = Mathf.Atan2(bullet_direction.y, bullet_direction.x) * Mathf.Rad2Deg;
                newBullet.transform.rotation = Quaternion.Euler(0f, 0f, rotZ - 90);

                newBullet.GetComponent<Rigidbody2D>().AddForce(bullet_direction * entityStats.speedBullet, ForceMode2D.Impulse);
             
            }

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
        if (collision.gameObject.name == "stop")
        {
            movimentNormal = false;
        }
        else if (collision.gameObject.name == "TradeForLeft")
        {
            movimentVerticalSpeed = -entityStats.baseSpeed;
        }
        else if (collision.gameObject.name == "TradeForRight")
        {
            movimentVerticalSpeed = entityStats.baseSpeed;
        }
       
    }
}
