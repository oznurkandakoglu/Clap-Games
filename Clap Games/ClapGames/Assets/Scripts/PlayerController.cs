using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [SerializeField] private Rigidbody rb;
    [SerializeField] private GameObject objectToBeSpawned;

    public Transform[] wayPoints;
    int index;
    bool key = true;
    Transform targetPosition;


    Animator animator;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        targetPosition = wayPoints[0];
    }

    void FixedUpdate()
    {

        Movement();
        
        if (Input.GetButton("Jump") && StackManager.pickCount > 0 && StackManager.instance.picked != null)
        {
            
            rb.velocity += new Vector3(0f, .5f, 0f);
            StackManager.pickCount--;
            animator.SetBool("isJumping", true);

                
            GameObject deleted = StackManager.instance.picked[StackManager.instance.picked.Count - 1];
            StackManager.instance.picked.RemoveAt(StackManager.instance.picked.Count - 1);
            Destroy(deleted);
          
            Vector3 position = new Vector3(transform.position.x-0.5f, transform.position.y, transform.position.z);
            Instantiate(objectToBeSpawned, position, Quaternion.identity);
                    }
        else
        {
            animator.SetBool("isJumping", false);
        }

  
    }

    void Movement()
    {
         if(transform.position != targetPosition.position)
         {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition.position, 3f * Time.deltaTime);
            transform.LookAt(targetPosition.position);

        }
        if (transform.position == targetPosition.position)
        {
            index++;
            targetPosition = wayPoints[index];
        }
        if(index == 2)
        {
            rb.MovePosition(new Vector3(0f, 0f, 0f));
            animator.SetBool("isRunning", false);
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Obstacle"))
        {
            UIManager.gameOver = true;
        }

        if (other.CompareTag("Finish"))
        {
            UIManager.nextScene = true;
        }
    }

    

}
