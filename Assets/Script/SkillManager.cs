using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private GameObject Darkness_Bullet;
    [SerializeField] private GameObject SkillB;
    [SerializeField] private GameObject SkillC;


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            animator.SetTrigger("isCutting");

        }
    }

   

}
