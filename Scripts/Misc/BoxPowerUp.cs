using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class BoxPowerUp : MonoBehaviour
{
    public string namePowerUp;
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position -= new Vector3(0,speed *Time.deltaTime,0);   
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" )
        {
            if (namePowerUp == "MachineGun")
            {
                collision.GetComponent<EntityStats>().machineGun = true;
            }else if (namePowerUp == "Invencible") { collision.GetComponent<EntityStats>().invencible = true; }
            else if (namePowerUp == "ShootGun") { collision.GetComponent<EntityStats>().shotGun = true; }
            collision.GetComponent<EntityStats>().PowerUps(namePowerUp);
            Destroy(this.gameObject);

        }
        else if (collision.gameObject.tag == "Wall")
        { Destroy(this.gameObject); }
    }
}
