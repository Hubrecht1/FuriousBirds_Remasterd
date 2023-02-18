using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class BirdPickerScript : MonoBehaviour
{
    [SerializeField] RectTransform defaultMenuTransform;
    [SerializeField] RectTransform backgroundBirdPicker;

    [SerializeField] GameObject firstBirdButton;
    List<GameObject> birds = new List<GameObject>();
    GameObject[] birdImages;
    uint currentBirdNumber = 0;

    void Start()
    {
        for (int i = 0; i < BirdManager.Instance.birds.Length; i++)
        {
            birds.Add(BirdManager.Instance.birds[i]);
        }

        birdImages = new GameObject[birds.Count];

        firstBirdButton.GetComponent<Image>().sprite = birds[0].GetComponent<SpriteRenderer>().sprite;

        for (int i = 1; i < birds.Count; i++)
        {
            birdImages[i] = Instantiate(firstBirdButton, transform);
            birdImages[i].name = "BirdButton " + i;
            Image birdimage = birdImages[i].GetComponent<Image>();
            Button birdbutton = birdImages[i].GetComponent<Button>();
            birdimage.sprite = birds[i].GetComponent<SpriteRenderer>().sprite;
            //birdImages[i].transform.position = new Vector3(transform.position.x, firstBirdButton.transform.position.y - i * (birdimage.sprite.texture.height / birdimage.sprite.pixelsPerUnit), 0);
            birdImages[i].transform.position = new Vector3(transform.position.x, firstBirdButton.transform.position.y - i * defaultMenuTransform.rect.height, 0);
            backgroundBirdPicker.offsetMin = new Vector2(backgroundBirdPicker.offsetMin.x, -defaultMenuTransform.rect.height * (birds.Count - 1));
        }

    }

    private void Update()
    {
        if (BirdManager.Instance.birdNumber != currentBirdNumber)
        {
            removeBirdImage(currentBirdNumber);
            currentBirdNumber = BirdManager.Instance.birdNumber;

        }


    }

    void removeBirdImage(uint birdImageNumber)
    {
        if (birdImageNumber == 0)
        {
            firstBirdButton.SetActive(false);

        }
        else
        {
            birdImages[birdImageNumber].SetActive(false);

        }
        for (int i = 1; i < birds.Count; i++)
        {
            birdImages[i].transform.position = new Vector2(transform.position.x, firstBirdButton.transform.position.y - ((i - currentBirdNumber - 1) * defaultMenuTransform.rect.height));
        }
        backgroundBirdPicker.offsetMin = new Vector2(backgroundBirdPicker.offsetMin.x, -defaultMenuTransform.rect.height * (birds.Count - birdImageNumber - 2));
    }





}
