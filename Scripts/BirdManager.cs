using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class BirdManager : MonoBehaviour
{
    [SerializeField] public GameObject[] birds;
    [SerializeField] GameObject infiniteBird;
    [SerializeField] public uint birdNumber = 0;
    [SerializeField] public GameObject activeBird;
    [SerializeField] CinemachineTargetGroup targetGroup;
    [SerializeField] bool infiniteBirds = false;

    Bird activebirdScript;

    FollowGameObject leaves;

    Vector3 startPosition;


    public static BirdManager Instance { get; private set; }

    private void Awake()
    {
        Instance = this;

        activeBird = birds[0];
        activebirdScript = activeBird.GetComponent<Bird>();
        startPosition = activeBird.transform.position;

        targetGroup = FindObjectOfType<CinemachineTargetGroup>();
        targetGroup.AddMember(activeBird.transform, 2, 1);
        if (GameObject.Find("Leaves") != null)
        {
            leaves = GameObject.Find("Leaves").GetComponent<FollowGameObject>();
            leaves.NewBird();
        }

    }

    void OnEnable()
    {
        targetGroup = FindObjectOfType<CinemachineTargetGroup>();
    }




    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && activebirdScript.fired && activebirdScript.abilityActivated)
        {
            MakeNewBird();

        }
    }

    void MakeNewBird()
    {

        if (birds.Length > birdNumber + 1)
        {
            if (!infiniteBirds)
            {
                birdNumber++;
                targetGroup.RemoveMember(activeBird.transform);
                activeBird = Instantiate(birds[birdNumber], startPosition, Quaternion.identity);
                targetGroup.AddMember(activeBird.transform, 2, 4);
                activebirdScript = activeBird.GetComponent<Bird>();
                if (leaves != null)
                {
                    leaves.NewBird();

                }
                Catapult.Instance.OnNewBird();

            }
            else
            {
                birds[0] = infiniteBird;
                targetGroup.RemoveMember(activeBird.transform);
                activeBird = Instantiate(birds[0], startPosition, Quaternion.identity);
                targetGroup.AddMember(activeBird.transform, 2, 4);
                activebirdScript = activeBird.GetComponent<Bird>();
                if (leaves != null)
                {
                    leaves.NewBird();

                }
                Catapult.Instance.OnNewBird();

            }



        }


    }


}
