using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class RaceController : MonoBehaviour
{

    public int maxSpeed;
    public int xForce;
    public int maxStamina;
    public int agilityResistance;
    public GameObject winScreen;
    
    

    private GameObject creature;
    private GameObject camera;
    private Rigidbody2D rb2d;
    private Vector3 cameraOffset;
    private float speedLimitingForce;
    private float currentStamina;
    private System.Random rand = new System.Random();
    private Text staminaDisplay;

    void Awake()
    {
        creature = GameObject.FindWithTag("Creature");
        camera = GameObject.FindWithTag("MainCamera");
        cameraOffset = camera.transform.position - creature.transform.position;
        rb2d = creature.GetComponent<Rigidbody2D>();
        speedLimitingForce = -xForce * 10;
        currentStamina = maxStamina;
        staminaDisplay = GameObject.FindWithTag("StaminaDisplay").GetComponent<Text>();
        staminaDisplay.text = $"Stamina: {currentStamina}/{maxStamina}";
    }


    void Update()
    {
        camera.transform.position = creature.transform.position + cameraOffset;
        
        
    }
    private void FixedUpdate()
    {
        print(rb2d.velocity);
        if (rb2d.velocity.x <= maxSpeed) 
        {
            if (currentStamina > 1)
            {
                int randNum = rand.Next((int) currentStamina + 1);
                if (randNum == currentStamina)
                {
                    currentStamina--;
                    staminaDisplay.text = $"Stamina: {currentStamina}/{maxStamina}";
                }
            }
            rb2d.AddForce((currentStamina / maxStamina) * (transform.right * xForce));
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
            rb2d.AddForce(transform.right * -agilityResistance);
        }

    }
}
