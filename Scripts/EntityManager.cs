using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EntityManager : MonoBehaviour
{
    public Pig[] pigs;


    void OnEnable()
    {
        pigs = FindObjectsOfType<Pig>();



    }


}
