using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [SerializeField] private Rigidbody rb;
    [SerializeField] private GameObject objectToBeSpawned;

    public Transform[] wayPoints;
    Transform targetPosition;
    Animator animator;

    int index;
    bool key = true;
    float distance = 0.1f;
    

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

            animator.SetBool("isRunning", false);

            if (StackManager.instance.picked.Count >1)
            {
                Destroy(StackManager.instance.picked[StackManager.instance.picked.Count - 1]);
                StackManager.instance.picked.RemoveAt(StackManager.instance.picked.Count - 1);
            }    

            if (key)
            {
                Vector3 position = new Vector3(transform.position.x - 0.5f, transform.position.y, transform.position.z);    
                Instantiate(objectToBeSpawned, position, Quaternion.identity);
            }
            else if (!key)
            {
                Vector3 position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
                Instantiate(objectToBeSpawned, position, Quaternion.Euler(0f,90f,0f));
            }
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

        if (Vector3.Distance(transform.position, targetPosition.position)<0.1f)
        {
            index++;
            targetPosition = wayPoints[index];
            key = false;
        }

        if(index == 2)
        {
            rb.MovePosition(new Vector3(0f, 0f, 0f));
            animator.SetBool("isRunning", false);
            key = true;
        }
    }

    public void Finish()
    {
        for (int i = 0; i < StackManager.pickCount; i++)
        {          
            if(StackManager.pickCount == 0)
            {
                return;
            }
            Vector3 position = new Vector3(transform.position.x - distance, transform.position.y, transform.position.z);
            distance += 0.1f;
            Instantiate(objectToBeSpawned, position, Quaternion.identity);

            if (StackManager.instance.picked.Count > 1)
            {
                Destroy(StackManager.instance.picked[StackManager.instance.picked.Count - 1]);
                StackManager.instance.picked.RemoveAt(StackManager.instance.picked.Count - 1);
            }
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
            
            if (StackManager.pickCount > 10)
            {
                animator.SetBool("isHappy", true);
                Finish();               
            }

            else
            {
                animator.SetBool("isSad", true);
                Finish();
            }

            StartCoroutine(DelayedAnim());

        }
    }

    IEnumerator DelayedAnim()
    {
        yield return new WaitForSecondsRealtime(3f);
        UIManager.nextScene = true;
    }
    

}
