using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxMovement : MonoBehaviour
{
    
    public GameObject chararcter;
    private GameObject mainbox;
    public List<GameObject> Boxes;
    private Rigidbody rb;
    public  int index;
    int counter;
    void Start()
    {
        chararcter = GameObject.Find("Character");
     
        mainbox = GameObject.Find("MainBox");
        rb = this.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
       
        Boxes = chararcter.GetComponent<CharController>().Boxes;
        index = Boxes.IndexOf(gameObject);
        counter = chararcter.GetComponent<CharController>().Counter;

        if (index - 1 != -1)
        {
            transform.position = new Vector3(
               Mathf.Lerp(transform.position.x, Boxes[index - 1].transform.position.x, Time.deltaTime*20),
               Boxes[index - 1].transform.position.y + 0.4f,
          Boxes[index - 1].transform.position.z);

            this.gameObject.transform.LookAt(Boxes[index - 1].transform);

        }
    
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Red")
        {
            chararcter.GetComponent<CharController>().Counter = index;
           
            for (int i = index+1; i <= counter; i++)
            {
                
                Boxes[i].SetActive(false);
              
            }
        }
        if (other.gameObject.tag == "Finish")
        {
            Destroy(gameObject.GetComponent<BoxMovement>());
        }
    }


}
