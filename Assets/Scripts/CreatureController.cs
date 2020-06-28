using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CreatureController : MonoBehaviour
{

    public float maxSpeed;
    public float xForce;
    public float resistance;
    public GameObject winScreen;

   
    private GameObject creature;
    private Rigidbody2D rb2d;
    private float fixedDeltaTime;


    void Awake()
    {
        creature = GameObject.FindWithTag("Creature");
        rb2d = creature.GetComponent<Rigidbody2D>();
        this.fixedDeltaTime = Time.fixedDeltaTime;
    }

    
    void Update()
    {
        
    }
    private void FixedUpdate()
    {
        if (rb2d.velocity.x <= maxSpeed) 
        {
            rb2d.AddForce(transform.right * (xForce - resistance));
        }
        else  
        {
            rb2d.AddForce(transform.right * -resistance);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        print("a");
        if (collision.gameObject.CompareTag("Flag")){
            print("b");
            winScreen.SetActive(true);
        }
    }
}
