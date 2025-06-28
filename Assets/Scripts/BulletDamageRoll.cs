using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletDamageRoll : MonoBehaviour
{

    public int damage ;
    public float speed;

    public bool isPlayer;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(this.gameObject, 5);
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy" && isPlayer == true)
        {
            collision.GetComponent<EntityStats>().TakeDamage(damage);
            Destroy(this.gameObject);
        }
        else if (collision.gameObject.tag == "Player" && isPlayer == false)
        {
            collision.GetComponent<EntityStats>().TakeDamage(damage);
            Destroy(this.gameObject);

        }
        else if (collision.gameObject.tag == "Wall" || collision.gameObject.tag == "Wall2")
        { Destroy(this.gameObject); }
    }
}
