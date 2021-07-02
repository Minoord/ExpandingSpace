using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public bool exitShop = false;
    //shop
    [SerializeField] Randomizer CallFunctionRandomizer1;
    [SerializeField] Randomizer CallFunctionRandomizer2;
    [SerializeField] Randomizer CallFunctionRandomizer3;
    [SerializeField] RandomText CanBuy1;
    [SerializeField] RandomText CanBuy2;
    [SerializeField] RandomText CanBuy3;

    // Game and Shop activeren
    [SerializeField] private GameObject SpaceShip;
    [SerializeField] private WaveSpawner spawner;
    [SerializeField]private GameObject HubFoundation;
    [SerializeField] private TriggerDialogue Shopkeep;
    [SerializeField] private TriggerDialogue SlakDia;
    [SerializeField] bool ActiverenShop;
    [SerializeField] bool ActiverenExit;
   


    //Walking
    public bool canMove;
    [SerializeField] private Rigidbody2D rb;
    public float speed = 4;


    //art
    [SerializeField] private GameObject RightWalk;
    [SerializeField] private GameObject LeftWalk;
    [SerializeField] private GameObject Standing;

    void Update()
    {
        if (canMove)
        {
            //Links
            if (Input.GetKey("a"))
            {
                rb.velocity = new Vector3(-speed, 0, 0);
                RightWalk.SetActive(true);
                LeftWalk.SetActive(false);
                Standing.SetActive(false);
            }
            //Rechts
            if (Input.GetKey("d"))
            {
                rb.velocity = new Vector3(speed, 0, 0);
                RightWalk.SetActive(false);
                LeftWalk.SetActive(true);
                Standing.SetActive(false);
            }

            if (Input.GetKeyUp("a"))
            {
                rb.velocity = new Vector3(0, 0, 0);
                RightWalk.SetActive(false);
                LeftWalk.SetActive(false);
                Standing.SetActive(true);
            }
            if (Input.GetKeyUp("d"))
            {
                rb.velocity = new Vector3(0, 0, 0);
                RightWalk.SetActive(false);
                LeftWalk.SetActive(false);
                Standing.SetActive(true);
            }
        }


        if (ActiverenExit == true && (Input.GetKeyDown(KeyCode.F)))
        {
                if (PlayerPrefs.GetInt("Slak") != 1)
                {
                    exitShop = true;
                    Debug.Log("HEy");
                    canMove = false;
                    StartCoroutine(SlakDia.ActivateDialogue());
                    PlayerPrefs.SetInt("Slak", 1);
                    Invoke("Exits", 25);

                }
                else
                {
                    Exits();
                }

        }
        
        if (ActiverenShop == true && (Input.GetKeyDown(KeyCode.F)))
        { 
            Shopkeep.StartCoroutine(Shopkeep.ActivateDialogue());
        }

    }
    public void Skipped(){
        CancelInvoke();
        Exits();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Exit")
        {
            ActiverenExit = true;
        }
        if (collision.gameObject.tag == "Shop")
        {
            ActiverenShop = true; 
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Exit")
        {
            ActiverenExit = false;
        }
        if (collision.gameObject.tag == "Shop")
        {
            ActiverenShop = false;
        }
    }

    void Exits()
    {
        exitShop = false;
        FindObjectOfType<WaveMusic>().playMusic();
        HubFoundation.SetActive(false);
        spawner.waitForPlayerChoice = true;
        SpaceShip.SetActive(true);
        CallFunctionRandomizer1.Randomized();
        CallFunctionRandomizer2.Randomized();
        CallFunctionRandomizer3.Randomized();
        canMove = true;
        FindObjectOfType<AudioManager>().Stop("OST");
    }
}
