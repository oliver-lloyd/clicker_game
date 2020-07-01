using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class RaceController : MonoBehaviour
{

    public int maxSpeed;
    public int acceleration;
    public int maxStamina;
    public int agility;
    public int agilityResistance;
    public GameObject winScreen;

    private GameObject creature;
    private GameObject camera;
    private GameObject _Manager;
    private Dictionary<string, Dictionary<string, int>> statDict = new Dictionary<string, Dictionary<string, int>>();
    private Rigidbody2D rb2d;
    private Vector3 cameraOffset;
    private float speedLimitingForce;
    private float currentStamina;
    private System.Random rand = new System.Random();
    private Text staminaDisplay;

    void Awake()
    {
        _Manager = GameObject.FindWithTag("_Manager");
        
        // Initialise creature
        statDict = _Manager.GetComponent<CreatureController>().statDict;
        maxSpeed = statDict["Speed"]["XP"];
        acceleration = statDict["Acceleration"]["XP"];
        maxStamina = statDict["Stamina"]["XP"];
        agility = statDict["Agility"]["XP"];
        creature = GameObject.FindWithTag("Creature");
        rb2d = creature.GetComponent<Rigidbody2D>();
        speedLimitingForce = -acceleration * 10;
        currentStamina = maxStamina;

        // Initialise UI stuff
        camera = GameObject.FindWithTag("MainCamera");
        cameraOffset = camera.transform.position - creature.transform.position;
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
            rb2d.AddForce((currentStamina / maxStamina) * (transform.right * acceleration));
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
