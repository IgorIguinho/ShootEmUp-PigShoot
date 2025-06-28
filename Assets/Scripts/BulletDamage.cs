using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletDamage : MonoBehaviour
{

    public int damage;
    public float speed;

    public bool isPlayer;

    public float direction;
    public bool isShootGung;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(this.gameObject, 5);

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (isPlayer == true && isShootGung == false)
        {
            transform.position += new Vector3(0, speed * Time.deltaTime, 0);

        }
        else if (isPlayer == true && isShootGung == true)
        {
            transform.position += new Vector3(((speed * direction)/2) * Time.deltaTime, speed * Time.deltaTime, 0);
        }
        else if (isPlayer == false)
        {
            transform.position -= new Vector3(0, speed * Time.deltaTime, 0);
        }
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
