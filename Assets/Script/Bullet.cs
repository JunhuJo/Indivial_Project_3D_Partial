using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float bulletSpeed = 10.0f;
    
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
