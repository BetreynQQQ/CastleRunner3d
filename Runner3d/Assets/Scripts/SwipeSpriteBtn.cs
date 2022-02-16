using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwipeSpriteBtn : MonoBehaviour
{
    public Sprite pauseSprite;
    public Sprite playSprite;
    private bool paused;
    


    private void OnMouseDown()
    {
        if (paused)
        {
            GetComponent<Image>().sprite = playSprite;
            paused = false;
        }            
        else
        {
            GetComponent<Image>().sprite = pauseSprite;
            paused = true;
        }
            
    }
   
}
