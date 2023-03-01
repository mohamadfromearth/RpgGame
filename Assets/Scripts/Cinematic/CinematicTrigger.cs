using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Playables;


namespace RPG.Cinematics
{
    public class CinematicTrigger : MonoBehaviour
    {

        private bool isFirstTrigger = true;
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == "Player" && isFirstTrigger)
            {
                isFirstTrigger = false;
                GetComponent<PlayableDirector>().Play();  
            }
          
        }
    }
}