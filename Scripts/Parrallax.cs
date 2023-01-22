using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Parrallax : MonoBehaviour
{
    [Range(0, 1)][SerializeField] float parallaxMultiplierX, parallaxMultiplierY;
    [SerializeField] bool repeatX;
    [SerializeField] bool repeatY;

    [SerializeField] Transform customTargetTransform;

    Transform cameraTransform;
    Vector3 lastcameraPosition;
    float textureUnitSizeX, textureUnitSizeY;

    private void Start()
    {

        cameraTransform = Camera.main.transform;
        if (customTargetTransform != null)
        {
            cameraTransform = customTargetTransform;
        }

        lastcameraPosition = cameraTransform.position;
        Sprite sprite = GetComponent<SpriteRenderer>().sprite;
        Texture2D texure = sprite.texture;
        textureUnitSizeX = texure.width / sprite.pixelsPerUnit;
        textureUnitSizeY = texure.height / sprite.pixelsPerUnit;
    }

    private void LateUpdate()
    {
        Vector3 deltaMovement = cameraTransform.position - lastcameraPosition;
        transform.position += new Vector3(deltaMovement.x * parallaxMultiplierX, deltaMovement.y * parallaxMultiplierY);
        lastcameraPosition = cameraTransform.position;

        if (Mathf.Abs(cameraTransform.position.x - transform.position.x) >= textureUnitSizeX)
        {
            float offsetPositionX = 0;
            float offsetPositionY = 0;
            if (repeatX)
            {
                offsetPositionX = (cameraTransform.position.x - transform.position.x) % textureUnitSizeX;
            }
            if (repeatY)
            {
                offsetPositionY = (cameraTransform.position.y - transform.position.y) % textureUnitSizeY;
            }

            transform.position = new Vector3(cameraTransform.position.x + offsetPositionX, transform.position.y + offsetPositionY);
        }
    }
}
