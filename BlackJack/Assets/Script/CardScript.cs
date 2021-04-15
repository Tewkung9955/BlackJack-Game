using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardScript : MonoBehaviour
{
    public int value = 0;

    public int GetValueOfCard()
    {
        return value;
    }
    
    public void SetValue(int NewValue)
    {
        value = NewValue;
    } 
    public string GetSpriteName()
    {
        return GetComponent<SpriteRenderer>().sprite.name;
    }
    public void SetSprite(Sprite NewSprite)
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = NewSprite;
    }

    public void ResetCard()
    {
        Sprite back = GameObject.Find("Deck").GetComponent<DeckScript>().GetCardBack();
        gameObject.GetComponent<SpriteRenderer>().sprite = back;
        value = 0;
    }
}
