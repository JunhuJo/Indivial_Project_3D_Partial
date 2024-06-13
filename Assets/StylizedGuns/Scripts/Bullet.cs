using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float bulletSpeed = 5.0f;
    public GameObject explotion;
    GameObject lastExplotion;
    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        // 총알의 초기 속도를 설정합니다.
        rb.velocity = transform.forward * bulletSpeed;

        // Rigidbody의 회전을 고정합니다.
        rb.angularVelocity = Vector3.zero;
    }


    private void Update()
    {
        Vector3 forword = new Vector3(-62.077f, -3.681f, 3.478f);
        gameObject.transform.Translate(forword * bulletSpeed* Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            return;
        }

        //GameObject lastExplotion = Instantiate(explotion, transform.position, transform.rotation);
        
        Destroy(gameObject,5);
        
    }

}
