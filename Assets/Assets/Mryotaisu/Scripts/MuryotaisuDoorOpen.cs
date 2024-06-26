using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MuryotaisuDoor
{  
    public class MuryotaisuDoorOpen : MonoBehaviour
    {
        private Animator animator;

        private bool InDoor;

        private bool gateOpen = false;

        // Start is called before the first frame update
        void Start()
        {
            animator = GetComponent<Animator>();
        }

        // Update is called once per frame
        void Update()
        {
            GateOpenKey();
        }

        void GateOpenKey()
        {
            if (Input.GetKeyDown(KeyCode.T) && !gateOpen)
            {
                gateOpen = true;
            }
            else if(Input.GetKeyDown(KeyCode.T) && gateOpen)
            {
                gateOpen =false;
            }

            GateOpen();
        }

        void GateOpen()
        {
            if (gateOpen)
            {
                animator.SetBool("doorOpenFlag", true);
            }
            else if (!gateOpen)
            {
                animator.SetBool("doorOpenFlag", false);
            }

            if (InDoor)
            {
                GetComponent<Renderer>().material.color = Color.red;
                animator.SetBool("doorOpenFlag", true);
            }

        }

        void OnTriggerStay(Collider other)
        {
            if (other.gameObject.tag == "Player")
            {
                InDoor = true;
            }
        }

        void OnTriggerExit(Collider other)
        {
            if (other.gameObject.tag == "Player")
            {
                GetComponent<Renderer>().material.color = Color.yellow;
                InDoor = false;
            }
        }

    }
}