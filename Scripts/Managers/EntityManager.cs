using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityManager : MonoBehaviour
{
    public Pig[] pigs;
    public Bird[] birds;

    void OnEnable()
    {
        pigs = FindObjectsOfType<Pig>();
    }


}
