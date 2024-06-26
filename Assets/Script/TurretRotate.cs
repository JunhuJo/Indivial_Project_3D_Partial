using UnityEngine;

public class TurretRotate : MonoBehaviour
{

    public GameObject TurretHead;
    public float rotateSpeed = 100;
    private MonsterAI monsterAI;

    private void Start()
    {
        monsterAI = GetComponent<MonsterAI>();
    }

    private void Update()
    {
        Rotate();
    }

    private void Rotate()
    {
      TurretHead.transform.Rotate(Vector3.down * rotateSpeed * Time.deltaTime);
        if (rotateSpeed == 0)
        {
            //LookAtTarget();
        }
    }

    //void LookAtTarget()
    //{
    //    Vector3 direction = (monsterAI.target.position - transform.position).normalized;
    //    Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
    //    transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    //}
}
