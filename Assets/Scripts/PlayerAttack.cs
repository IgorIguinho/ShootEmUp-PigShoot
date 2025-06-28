using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    EntityStats entityStats;

    public GameObject bullet;

    public List<GameObject> target;

    public AudioSource audioSource;
    public AudioClip attackNormalSound;
    public AudioClip attackShootSound;
    public AudioClip attackHadukenSound;

    //powerUpsBullets
    public GameObject shootgunBullet;
    public GameObject machinegunBullet;
    public GameObject MASTERBULLET;

    float cooldownCount;
    bool canAtack;

    void Start()
    {
        entityStats = gameObject.GetComponent<EntityStats>();
        canAtack = true;
    }

    // Update is called once per frame
    void Update()
    {
        AttackPlayer();
        Cooldown();
    }

    void AttackPlayer()
    {
        if (Input.GetMouseButton(0) && canAtack == true)
        {

            if (entityStats.canShootGun == true  && entityStats.machineGun == true)
            {
                GameObject newBullet = Instantiate(MASTERBULLET, transform.position, Quaternion.identity);

                newBullet.GetComponent<BulletDamage>().damage = entityStats.dmg;
                newBullet.GetComponent<BulletDamage>().speed = entityStats.speedBullet;
                audioSource.clip = attackHadukenSound; audioSource.Play();
              

                powerUpShootGunMaster();
            }
            else if (entityStats.canShootGun == true)
            {
                GameObject newBullet = Instantiate(shootgunBullet, transform.position, Quaternion.identity);
                audioSource.clip = attackShootSound; audioSource.Play();
                newBullet.GetComponent<BulletDamage>().damage = entityStats.dmg;
                newBullet.GetComponent<BulletDamage>().speed = entityStats.speedBullet;

                newBullet.GetComponent<BulletDamage>().transform.rotation = Quaternion.Euler(0f, 0f, 90);


                powerUpShootGun();
            }
            else if (entityStats.machineGun == true)
            {
                audioSource.clip = attackNormalSound; audioSource.Play();
                GameObject newBullet = Instantiate(machinegunBullet, transform.position, Quaternion.identity);

                newBullet.GetComponent<BulletDamage>().damage = entityStats.dmg;
                newBullet.GetComponent<BulletDamage>().speed = entityStats.speedBullet;

                newBullet.GetComponent<BulletDamage>().transform.rotation = Quaternion.Euler(0f, 0f, 90);
            }
            else { 
          GameObject newBullet =  Instantiate(bullet, transform.position, Quaternion.identity);
                audioSource.clip = attackNormalSound; audioSource.Play();
                newBullet.GetComponent<BulletDamage>().damage = entityStats.dmg;
            newBullet.GetComponent<BulletDamage>().speed = entityStats.speedBullet;

            newBullet.GetComponent<BulletDamage>().transform.rotation = Quaternion.Euler(0f, 0f, 90);

            }
            
            canAtack = false;
            cooldownCount = 0;
        }
        
    }

    void powerUpShootGun()
    {
        if (entityStats.canShootGun == true)
        {
            for (int i = 0; i < target.Count; i++)
            {
                GameObject newBulletShootGun = Instantiate(shootgunBullet, transform.position, Quaternion.identity);

                newBulletShootGun.GetComponent<BulletDamage>().damage = entityStats.dmg;
                newBulletShootGun.GetComponent<BulletDamage>().isShootGung = true;
                newBulletShootGun.GetComponent<BulletDamage>().speed = entityStats.speedBullet; //* 0.66f;
                if (i == 0) { newBulletShootGun.GetComponent<BulletDamage>().direction = 1;
                    newBulletShootGun.GetComponent<BulletDamage>().transform.rotation = Quaternion.Euler(0f, 0f, 65);
                }
                else if (i == 1) { newBulletShootGun.GetComponent<BulletDamage>().direction = -1;
                    newBulletShootGun.GetComponent<BulletDamage>().transform.rotation = Quaternion.Euler(0f, 0f, 115);
                }



            }
        }
    }

    void powerUpShootGunMaster()
    {
        if (entityStats.canShootGun == true)
        {
            for (int i = 0; i < target.Count; i++)
            {
                GameObject newBulletShootGun = Instantiate(MASTERBULLET, transform.position, Quaternion.identity);

                newBulletShootGun.GetComponent<BulletDamage>().damage = entityStats.dmg;
                newBulletShootGun.GetComponent<BulletDamage>().isShootGung = true;
                newBulletShootGun.GetComponent<BulletDamage>().speed = entityStats.speedBullet; //* 0.66f;
                if (i == 0)
                {
                    newBulletShootGun.GetComponent<BulletDamage>().direction = 1;
                    newBulletShootGun.GetComponent<BulletDamage>().transform.rotation = Quaternion.Euler(0f, 0f, -45);
                }
                else if (i == 1)
                {
                    newBulletShootGun.GetComponent<BulletDamage>().direction = -1;
                    newBulletShootGun.GetComponent<BulletDamage>().transform.rotation = Quaternion.Euler(0f, 0f, 45);
                }



            }
        }
    }
    void Cooldown()
    {
        if (canAtack == false && cooldownCount >(entityStats.atckSpeed * ((100 - entityStats.bonusAtackkSpeed)) / 100))
        {
            canAtack=true;
            
        }
        else 
        {
            cooldownCount += Time.deltaTime;
        }
    }
}
