using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharController : MonoBehaviour
{

    [SerializeField] private float Forward;
    private SwerweInputSystem _swerveInputSystem;
    [SerializeField] private float swerveSpeed = 0.5f;
    [SerializeField] private float maxSwerveAmount = 1f;
    public List<GameObject> Boxes;
    public GameObject GroundTarget;
    public GameObject Target;
    public int Counter = 0;
    float time;
    int count;
    private void Awake()
    {
        _swerveInputSystem = GetComponent<SwerweInputSystem>();
    }
    private Animator animator;
    void Start()
    {
        Debug.Log(count);
        animator = GetComponent<Animator>();

        InvokeRepeating("increase", 1.0f, 1.5f);
    }

    // Update is called once per frame
    void Update()
    {
        Finish();

        float swerveAmount = Time.deltaTime * swerveSpeed * _swerveInputSystem.MoveFactorX;
        swerveAmount = Mathf.Clamp(swerveAmount, -maxSwerveAmount, maxSwerveAmount);
        this.transform.Translate(swerveAmount, 0, Forward * Time.deltaTime);
        Vector3 rot = transform.rotation.eulerAngles;
        rot.y = Mathf.Clamp(gameObject.transform.rotation.y, 0, 0);

    }

    public void Finish()
    {
        if (Forward == 0)
        {
            Boxes[count].transform.position = Vector3.Lerp(Boxes[count].transform.position, GroundTarget.transform.position, Time.deltaTime * 5);
            if (Boxes[count].transform.position.y <= 0.4f)
            {
                Boxes[count].transform.position = Vector3.Lerp(Boxes[count].transform.position, Target.transform.position, Time.deltaTime * 20);
            }     
            if (count == Counter)
            {
                animator.Play("Dance");
            }

        }


     

    }

    public void increase()
    {
        if (Forward == 0)
        {
            if (count <= Counter)
            {
                count++;
                Debug.Log(count);
            }
            
        }
       
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Finish")
        {

            Forward = 0;
            animator.Play("Kicking");
        }

        if (other.gameObject.tag == "Box")
        {

            Boxes[Counter - 1].gameObject.SetActive(true);
            Boxes[Counter - 1].gameObject.tag = "Collected";
            other.gameObject.SetActive(false);
            Counter++;

        }
    }
}
