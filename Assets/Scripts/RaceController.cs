using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class RaceController : MonoBehaviour
{

    public float maxSpeed;
    public float xForce;
    public float resistance;
    public GameObject winScreen;

   
    private GameObject creature;
    private GameObject camera;
    private Rigidbody2D rb2d;
    private Vector3 cameraOffset;


    void Awake()
    {
        creature = GameObject.FindWithTag("Creature");
        camera = GameObject.FindWithTag("MainCamera");
        cameraOffset = camera.transform.position - creature.transform.position;
        rb2d = creature.GetComponent<Rigidbody2D>();
    }


    void Update()
    {
        camera.transform.position = creature.transform.position + cameraOffset;
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
