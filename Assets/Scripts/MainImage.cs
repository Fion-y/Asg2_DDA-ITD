using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainImage : MonoBehaviour
{
    [SerializeField] private GameObject card_bg;

    // check if image is yes, will hide it 
        
    public void OnMouseDown()
    {
        if (card_bg.activeSelf)
        { 
            card_bg.SetActive(false);
        } 
    }


    // to change the images

    private int _spriteId;

    public int spriteId { get { return _spriteId; } } 

    public void ChangeSprite(int id, Sprite image)
    {
        _spriteId = id;
        GetComponent<SpriteRenderer>().sprite = image;
    }
}
