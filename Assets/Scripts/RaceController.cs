using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class RaceController : MonoBehaviour
{

    public float maxSpeed;
    public float xForce;
    
    public float agilityResistance;
    public GameObject winScreen;

   
    private GameObject creature;
    private GameObject camera;
    private Rigidbody2D rb2d;
    private Vector3 cameraOffset;
    private float speedLimitingForce;


    void Awake()
    {
        creature = GameObject.FindWithTag("Creature");
        camera = GameObject.FindWithTag("MainCamera");
        cameraOffset = camera.transform.position - creature.transform.position;
        rb2d = creature.GetComponent<Rigidbody2D>();
        speedLimitingForce = -xForce * 10;
    }


    void Update()
    {
        camera.transform.position = creature.transform.position + cameraOffset;
    }
    private void FixedUpdate()
    {
        if (rb2d.velocity.x <= maxSpeed) 
        {
            rb2d.AddForce(transform.right * (xForce));
        }
        else  
        {
            rb2d.AddForce(transform.right * speedLimitingForce);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Flag"))
        {
            winScreen.SetActive(true);
        }
        
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("AgilityObstacle"))
        {
            print("Traversing agility obstacle");
            rb2d.AddForce(transform.right * -agilityResistance);
        }

    }
}
