using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoviment : MonoBehaviour
{
    EntityStats entityStats;

    public AudioSource footSound;
    float moveSpeed;
    // Start is called before the first frame update
    void Start()
    {
        entityStats = gameObject.GetComponent<EntityStats>();
        moveSpeed = entityStats.baseSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        Mov();
    }


    void Mov()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(horizontal * moveSpeed * Time.deltaTime, 0));
        if (horizontal != 0) { footSound.Play(); }
        else { footSound.Stop(); }        

    }
}
