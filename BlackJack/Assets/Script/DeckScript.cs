using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckScript : MonoBehaviour
{
    public Sprite[] CardSprites;
    
    int[] cardValues = new int[53];
    int currentIndex = 0;


    void Start()
    {
        GetCardValues();
    }

    
    void GetCardValues()
    {
        int num = 0;
        for (int i = 0; i < CardSprites.Length; i++)
        {
            num = i;
            num %= 13;
            if (num > 10 || num == 0)
            {
                num = 10;
            }
            cardValues[i] = num++;
        }
       
    }

    public void Shuffle()
    {

        for (int i = CardSprites.Length - 1; i > 0; --i)
        {
            int j = Mathf.FloorToInt(Random.Range(0.0f, 1.0f) * CardSprites.Length - 1) + 1;
            Sprite face = CardSprites[i];
            CardSprites[i] = CardSprites[j];
            CardSprites[j] = face;

            int value = cardValues[i];
            cardValues[i] = cardValues[j];
            cardValues[j] = value;
        }
        currentIndex = 1;
    }

    public int DealCard(CardScript CardScript)
    {
        
        CardScript.SetSprite(CardSprites[currentIndex]);
        CardScript.SetValue(cardValues[currentIndex]);
        currentIndex++;
       
        return CardScript.GetValueOfCard();
        
    }

    public Sprite GetCardBack()
    {
        return CardSprites[0];
    }
}
