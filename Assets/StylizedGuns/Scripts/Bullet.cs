using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float bulletSpeed = 5.0f;
    public GameObject explotion;
    GameObject lastExplotion;
    private Rigidbody rb;

    private void Update()
    {
        gameObject.transform.Translate(Vector3.forward * bulletSpeed * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            return;
        }

        Destroy(gameObject,5);
    }
}
