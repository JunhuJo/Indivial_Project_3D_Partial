using UnityEngine;

public class TurretRotate : MonoBehaviour
{
    [SerializeField] private GameObject TurretHead;
    [SerializeField] private float rotateSpeed = 5;

    private void Update()
    {
        TurretHead.transform.Rotate(Vector3.down * rotateSpeed *  Time.deltaTime) ;
    }
}
