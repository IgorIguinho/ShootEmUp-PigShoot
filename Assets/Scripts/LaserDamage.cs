using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserDamage : MonoBehaviour
{
    public int tempDestroy;
    public GameObject hitParticle;
    // Start is called before the first frame update
    void Start()
    {
        transform.rotation = Quaternion.Euler(0f, 0f, 180);
       // Destroy(gameObject, 3);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Instantiate(hitParticle, collision.transform.position, Quaternion.identity);
            Debug.Log("acertou");
            collision.GetComponent<EntityStats>().TakeDamage(2);
        }
    }
}
