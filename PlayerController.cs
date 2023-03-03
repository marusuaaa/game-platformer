using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] TextMeshProUGUI coinsText;
    float vert = 0;
    float horiz = 0;
    Rigidbody rb;
    float JumpForce = 12f;
    bool isGround = false;
    static int coins = 0;
     
    float timeCount = 40;
    [SerializeField] TextMeshProUGUI timeText;
    [SerializeField] GameObject win;
    [SerializeField] GameObject lose;
    
    
   
    

    public bool isEnd = false;
   
    void Start()
    
    
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        vert = Input.GetAxis("Vertical");
        horiz = Input.GetAxis("Horizontal");

        transform.Translate(0,0,1 * vert * Time.deltaTime * 7f);


        //rb.AddRelativeForce(0, 0, 1 * vertical * 2f );
        //rb.velocity = transform.forward * vertical * 3f;

        transform.Rotate(0,1 * horiz,0);

        if(Input.GetKeyDown("space")){
            if(isGround == true){
                rb.AddForce(0, JumpForce, 0, ForceMode.Impulse);
                rb.drag = 2;
                rb.angularDrag = 2;
            }
            

        }
        if(isEnd == false){
        timeCount = timeCount - Time.deltaTime;
        timeText.text = "Time: " + Mathf.Round(timeCount).ToString();}
        
        if(timeCount < 0){
            isEnd = true;
           
            if(coins == 4){
                Debug.Log("выйграл");
                win.SetActive(true);
            }else{
                Debug.Log("проиграл");
                lose.SetActive(true);
            }
          
        }
            
        }
        void OnCollisionEnter(Collision coll){
            if(coll.gameObject.tag == "ground"){
                isGround = true;
                rb.drag = 0;
                rb.angularDrag = 0.05f;
            }
             if(coll.gameObject.tag == "coin"){
             coll.gameObject.GetComponent<ParticleSystem>().Play();

             
             coins++;
             //Destroy(coll.gameObject, 0.3f);
             coll.gameObject.GetComponent<BoxCollider>().enabled = false;
             coll.gameObject.GetComponent<MeshRenderer>().enabled = false;
             

            
            coinsText.text = "coins: " + coins;
        }
          
         if(coll.gameObject.tag == "destroy"){
             Destroy(coll.gameObject);
             
        }
        }


        
            
        void OnCollisionExit(Collision coll){
            if(coll.gameObject.tag == "ground"){
                isGround = false;


            }
        }
 
}






   