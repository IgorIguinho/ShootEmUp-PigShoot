using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;

public class BossBehavior : MonoBehaviour
{
    EntityStats entityStats;
    GameObject player;
    Animator animator;
    public List<GameObject> powerUpsList;


    float moveSpeed;
    public float atckSpeed;

    AudioSource skillAudioSource;
    public AudioClip laserEffcts;
    public AudioClip dashEffcts;
    public AudioClip bulletEffcts;


    //Attack
    public GameObject laser;
    public GameObject bullet;
    public float dashSpeed;
    public GameObject hitParticle;
    float cooldownCountA;
    bool canA = true;
    bool canMove = false;
    Vector3 origanlPosition;

    //selectAtack
    int numberSpecial;
    //float cooldownCountS;
    //public bool canS = true;


    //cooldownSuperAtacks
    float cooldownCountC;
    bool canC = true;

    // Start is called before the first frame update
    void Start()
    {
        skillAudioSource = GameObject.FindGameObjectWithTag("SkillAudioSource").GetComponent<AudioSource>();    
        animator = gameObject.GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
        entityStats = GetComponent<EntityStats>();
         powerUpsList = entityStats.powerUpsList;
        atckSpeed = entityStats.atckSpeed;
        moveSpeed = entityStats.baseSpeed;
        origanlPosition = transform.position;

        StartCoroutine(specialAttack());
    }

    // Update is called once per frame
    

    private void FixedUpdate()
    {
        LaserAttck();

        Attack();
        Cooldown();

    }
    void Attack()
    {
        if (canA == true)
        {
            GameObject newBullet = Instantiate(bullet, transform.position, Quaternion.identity);
            skillAudioSource.clip = bulletEffcts; skillAudioSource.Play();
            newBullet.GetComponent<BulletDamage>().damage = entityStats.dmg;
            newBullet.GetComponent<BulletDamage>().speed = entityStats.speedBullet;
            canA = false;
            cooldownCountA = 0;
        }
    }

    IEnumerator specialAttack()
    {

        while (true)
        {
            yield return new WaitForSeconds(8);

             numberSpecial = Random.Range(0, 3);
            //numberSpecial = 2;
            if (numberSpecial == 0)
            {
                gameObject.GetComponent<SpriteRenderer>().color = Color.red;
                yield return new WaitForSeconds(2);
                gameObject.GetComponent<SpriteRenderer>().color = Color.white;
                atckSpeed = 0.5f;
                Debug.Log("ATAQUE 1");
                yield return new WaitForSeconds(3);
                Debug.Log("ATAQUE 1 acabou fi");
                atckSpeed = entityStats.atckSpeed;

            }
            else if (numberSpecial == 1)
            {
                canMove = true;
                animator.Play("JumbBoss");
                yield return new WaitForSeconds(2);
                skillAudioSource.clip = laserEffcts; skillAudioSource.Play();
                gameObject.GetComponent<SpriteRenderer>().color = Color.white;
                animator.Play("IdleBoss");
                //laser
                GameObject newLaser = Instantiate(laser);
                newLaser.transform.parent = transform;
                newLaser.transform.position = new Vector2( transform.position.x, -0.49f);
                Debug.Log("ATAQUE 2");
                yield return new WaitForSeconds(3);
                Destroy(newLaser);
                Debug.Log("ATAQUE 2 acabou fi");
                canMove = false;
                canA = true;
                animator.Play("SentadoBoss");
            }
            else if (numberSpecial == 2) 
            {
                canMove = true;
            
                animator.Play("ChargeAttackBoss");
                yield return new WaitForSeconds(2);
                skillAudioSource.clip = dashEffcts; skillAudioSource.Play();
                gameObject.GetComponent<SpriteRenderer>().color = Color.black;
                animator.Play("AttackBoss");
                Debug.Log("ATAQUE TRES");
                canMove =false;
                Dash(true);
                yield return new WaitForSeconds(1.5f);
                gameObject.GetComponent<SpriteRenderer>().color = Color.white;
                Debug.Log("Vorta man");
                animator.Play("SentadoBoss");
                Dash(false);

            }

        }

    }

    void Cooldown()
    {


        //Attack
        if (canA == false && cooldownCountA > atckSpeed)
        {
            canA = true;

        }
        else
        {
            cooldownCountA += Time.deltaTime;
        }


        ////Choice Super Attack
        //if (canS == false && cooldownCountS > 8)
        //{
        //    canS = true;

        //}
        //else
        //{
        //    cooldownCountS += Time.deltaTime;
        //}

    }

    void CooldownSuper()
    {


        //Attack
        if (canC == false && cooldownCountC > 4)
        {
            canC = true;

        }
        else
        {
            cooldownCountC += Time.deltaTime;
        }

    }


    void Dash(bool reset)
    {
        
        if (reset) 
        {
            gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2( 0, -dashSpeed));
        }
        else if (reset == false) { gameObject.GetComponent<Rigidbody2D>().Sleep();transform.position = origanlPosition; }
    }
    void LaserAttck()
    {
        if (canMove == true)
        {
            
            //Movimento
            float targeX = Mathf.MoveTowards(transform.position.x, player.transform.position.x, moveSpeed * Time.deltaTime);

            Vector2 newPosition = new Vector2(targeX, transform.position.y);

            gameObject.GetComponent<Rigidbody2D>().position = newPosition;
            canA = false;
        }
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        { collision.GetComponent<EntityStats>().TakeDamage(2); }

        if (collision.gameObject.tag == "PlayerBullets")
        {
            Instantiate(hitParticle, transform.position, Quaternion.identity);
            int random = Random.Range(0, 100);
            if (random <= 10)
            {
                int randomPowerup = Random.Range(0, powerUpsList.Count);
                GameObject newPowerUp = Instantiate(powerUpsList[randomPowerup], transform.position, Quaternion.identity);
            }
        }
    }
}
