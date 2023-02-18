using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Sprites;
using UnityEngine.U2D.Animation;
using UnityEngine.XR;



public class Catapult : MonoBehaviour
{
    public static Catapult Instance { get; private set; }

    Bird currentBird;
    [SerializeField] SpriteSkin Bones1, Bones2;
    [SerializeField] LineRenderer blackLine;

    float[] originalRotations1;
    float[] originalRotations2;
    float step;

    private void Awake()
    {
        Instance = this;

    }

    private void Start()
    {
        originalRotations1 = new float[Bones1.boneTransforms.Length];
        originalRotations2 = new float[Bones2.boneTransforms.Length];

        currentBird = BirdManager.Instance.activeBird.GetComponent<Bird>();

        for (int i = 0; i < Bones1.boneTransforms.Length; i++)
        {
            originalRotations1[i] = Bones1.boneTransforms[i].localEulerAngles.z;

        }
        for (int i = 0; i < Bones2.boneTransforms.Length; i++)
        {
            originalRotations2[i] = Bones2.boneTransforms[i].localEulerAngles.z;

        }

    }

    public void OnNewBird()
    {
        currentBird = BirdManager.Instance.activeBird.GetComponent<Bird>();

    }


    void Update()
    {
        if (!currentBird.fired)
        {
            float birdDistance = (currentBird._startPosition.x - currentBird.transform.position.x);

            blackLine.SetPosition(0, Bones1.boneTransforms[6].transform.position);
            blackLine.SetPosition(1, currentBird.transform.position);
            blackLine.SetPosition(2, Bones2.boneTransforms[3].transform.position);

            for (int i = 0; i < Bones1.boneTransforms.Length; i++)
            {
                Bones1.boneTransforms[i].localEulerAngles = new Vector3(0, 0, originalRotations1[i] + birdDistance);

            }
            for (int i = 0; i < Bones2.boneTransforms.Length; i++)
            {
                Bones2.boneTransforms[i].localEulerAngles = new Vector3(0, 0, originalRotations2[i] + birdDistance);

            }



        }
        else
        {
            blackLine.SetPosition(0, Bones1.boneTransforms[6].transform.position);
            blackLine.SetPosition(1, Bones1.boneTransforms[6].transform.position);
            blackLine.SetPosition(2, Bones2.boneTransforms[3].transform.position);

            for (int i = 0; i < Bones1.boneTransforms.Length; i++)
            {

                Bones1.boneTransforms[i].localEulerAngles = originalRotations1[i] * Vector3.forward;

            }
            for (int i = 0; i < Bones2.boneTransforms.Length; i++)
            {

                Bones2.boneTransforms[i].localEulerAngles = originalRotations2[i] * Vector3.forward;

            }

        }

    }

}
