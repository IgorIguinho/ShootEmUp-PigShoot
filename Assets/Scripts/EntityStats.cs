using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EntityStats : MonoBehaviour
{

    public float baseSpeed;
    public int hpMax;
    public int hp;
    public int dmg;
    public float speedBullet;
    public float atckSpeed;

    public AudioSource audioSource;
    public AudioClip damageClip;

    public GameObject deadParticle;
    public GameObject hitParticle;

    //only Enemy
    public int pointsForDead;
    public List<GameObject> powerUpsList;

    //only players
        //PowerUps
    public bool machineGun;
    public bool shotGun;
    public bool invencible;
    public GameObject animationInvecible;
    EntityStats player;

    //upgrades
    public float bonusAtackkSpeed;
    

    //cooldown
    float cooldownCountM = 0;
    float cooldownCountS = 0;
    float cooldownCountI = 0;
    bool canM = false;
    bool canS = false;
    bool canI = false;
    public bool canShootGun;

    // Start is called before the first frame update
    void Start()
    {
        hp = hpMax;
        if (gameObject.name != "Player")
        {
            audioSource = GameObject.FindGameObjectWithTag("AudioSourceEnemy").GetComponent<AudioSource>();   
        }
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<EntityStats>();
    }

    // Update is called once per frame
    void Update()
    {
        Cooldown();

        if (hp >= hpMax +1 )
        {
            hp = hpMax;
        }
        
    }


    public void Dead()
    {
        if (hp <= 0)
        {
            if (this.gameObject.tag == "Enemy")
            {

                HudManager.Instance.points += pointsForDead;
                ShakeCam.Instance.start = true;
                int random = Random.Range(0,100);
              if (random <= 30)
                {                
                        int randomPowerup = Random.Range(0, powerUpsList.Count);
                        GameObject newPowerUp = Instantiate(powerUpsList[randomPowerup], transform.position, Quaternion.identity);                               
                }
            }else if (this.gameObject.tag == "Player")
            {

                HudManager.Instance.deadScreen.SetActive(true);
                HudManager.Instance.aliveUI.SetActive(false);
                Time.timeScale = 0;
            }

            Instantiate(deadParticle, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
    }

    public void PowerUps(string namePowerUp)
    {
        
        //Machine Gun
        if (machineGun == true && namePowerUp == "MachineGun")
        {
            bonusAtackkSpeed += 70;
            canM = true;

        }
        else if(machineGun == false && namePowerUp == "MachineGun")  { bonusAtackkSpeed = 0;
            cooldownCountM = 0;
        }

        //Invencible
        
             if (invencible == true && namePowerUp == "Invencible")
                {
                 //gameObject.GetComponent<BoxCollider2D>().enabled = false;
                 canI = true;
            animationInvecible.SetActive(true);
                }
             else if (invencible == false && namePowerUp == "Invencible") {
            animationInvecible.SetActive(false);
            //gameObject.GetComponent<BoxCollider2D>().enabled = true;
            cooldownCountI = 0;
        }

        //shootgun

        if (shotGun == true && namePowerUp == "ShootGun")
        {
            canShootGun = true;
            canS = true;
        }
        else if (shotGun == false && namePowerUp == "ShootGun") { canShootGun = false;
            cooldownCountS = 0;
        }

        //HP Restor
        if (namePowerUp == "HP")
        {
            if (hp < hpMax)
            {
                hp += 2;
                HudManager.Instance.updateLifeUi();
            }
        }

    }

    void Cooldown()
    {
        //Machine Gun
        if (canM == true && cooldownCountM > 6)
        {


           
            machineGun = false;
            PowerUps("MachineGun");
            canM = false;

        }
        else if (canM == true) 
        {
            cooldownCountM += Time.deltaTime;
        }

        //Invencible
        if (canI == true && cooldownCountI > 3)
        {



            invencible = false;
            PowerUps("Invencible");
            canI = false;

        }
        else if (canI == true)
        {
            cooldownCountI += Time.deltaTime;
        }

        //Shotgn
        if (canS == true && cooldownCountS > 6)
        {



            shotGun = false;
            PowerUps("ShootGun");
            canS = false;

        }
        else if (canS == true)
        {
            cooldownCountS += Time.deltaTime;
        }
    }
    public void TakeDamage(int damage)
    {
        hp -= damage;
        audioSource.clip = damageClip; audioSource.Play();
         if (gameObject.tag == "Player")
        {


            if (invencible == true)
            {
                hp += damage;
                hitParticle.GetComponent<ParticleSystem>().startColor = Color.cyan;
                hitParticle.GetComponentInChildren<ParticleSystem>().startColor = Color.cyan;
                Instantiate(hitParticle, transform.position, Quaternion.identity);
            }
            else
            {
                hitParticle.GetComponent<ParticleSystem>().startColor = Color.red;
                hitParticle.GetComponentInChildren<ParticleSystem>().startColor = Color.red;
                Instantiate(hitParticle, transform.position, Quaternion.identity);
            }
            HudManager.Instance.updateLifeUi();
        }
        Dead();
    }


    
}
