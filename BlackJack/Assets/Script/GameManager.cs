using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    // Game Buttons
    public Button dealBtn;
    public Button hitBtn;
    public Button standBtn;
    public Button betBtn;
    

    private int StandClick =0;

    public PlayerScript PlayerScript;
    public PlayerScript dealerScript;
    
    public Text standBtnText;
    public Text scoreText;
    public Text DealerScoreText;
    public Text betsText;
    public Text CashText;
    public Text MainText;
    public GameObject hidecard;    

    int pot = 0;

    void Start()
    {
        dealBtn.onClick.AddListener(() => dealClicked());
        hitBtn.onClick.AddListener(() => hitClicked());
        standBtn.onClick.AddListener(() => standClicked());
        betBtn.onClick.AddListener(() => Betclicked());
    }

    private void dealClicked()
    {

        PlayerScript.ResetHand();
        dealerScript.ResetHand();
 
        MainText.gameObject.SetActive(false);
        DealerScoreText.gameObject.SetActive(false);
       
        GameObject.Find("Deck").GetComponent<DeckScript>().Shuffle();
        
        PlayerScript.StartHand();
        dealerScript.StartHand();

        scoreText.text = "Hand: "+PlayerScript.handValue.ToString();
        DealerScoreText.text = "Hand: " + dealerScript.handValue.ToString();

        hidecard.GetComponent<Renderer>().enabled = true;

        dealBtn.gameObject.SetActive(false);
        hitBtn.gameObject.SetActive(true);
        standBtn.gameObject.SetActive(true);
        standBtnText.text = "Stand";

        pot = 40;
        betsText.text = "Bets: $"+pot.ToString();
        PlayerScript.AdjustMoney(-20);
        CashText.text = PlayerScript.GetMoney().ToString();


    }
    private void hitClicked()
    {  
        if (PlayerScript.cardIndex <= 10)
        {
            
            PlayerScript.GetCard();
            scoreText.text = "hand: " + PlayerScript.handValue.ToString();
            if(PlayerScript.handValue > 20)
            {
                roundOver();
            }

        }
    }
    private void standClicked()
    {
        StandClick++;
        if (StandClick > 1) roundOver();
        Hitdealer();
        standBtnText.text = "Call";
    }

    private void Hitdealer()
    {
        while (dealerScript.handValue < 16 && dealerScript.cardIndex < 10)
        {
            dealerScript.GetCard();
            DealerScoreText.text = "hand: "+dealerScript.handValue.ToString();
        }
    }

    void roundOver()
    {
        bool playerBust = PlayerScript.handValue > 21;
        bool dealerBust = dealerScript.handValue > 21;
        bool player21 = PlayerScript.handValue == 21;
        bool dealer21 = dealerScript.handValue == 21;
 
        if (StandClick < 2 && !playerBust && !dealerBust && !player21 && !dealer21) return;
        bool roundOver = true;

        if (playerBust && dealerBust)
        {
            MainText.text = "All Bust: Bets returned";
            PlayerScript.AdjustMoney(pot / 2);
        }
        else if (playerBust || (!dealerBust && dealerScript.handValue > PlayerScript.handValue))
        {
            MainText.text = "Dealer wins!";
        }
        else if (dealerBust || PlayerScript.handValue > dealerScript.handValue)
        {
            MainText.text = "You win!";
            PlayerScript.AdjustMoney(pot);
        }
        else if (PlayerScript.handValue == dealerScript.handValue)
        {
            MainText.text = "Push: Bets returned";
            PlayerScript.AdjustMoney(pot / 2);
        }
        else
        {
            roundOver = false;
        }
        if (roundOver)
        {
            hitBtn.gameObject.SetActive(false);
            standBtn.gameObject.SetActive(false);
            dealBtn.gameObject.SetActive(true);
            MainText.gameObject.SetActive(true);
            DealerScoreText.gameObject.SetActive(true);
            hidecard.GetComponent<Renderer>().enabled = false;
            CashText.text = "$" + PlayerScript.GetMoney().ToString();
            StandClick = 0;
        }
    }

    void Betclicked()
    {
        Text newBet = betBtn.GetComponentInChildren(typeof(Text)) as Text;
        int intBet = int.Parse(newBet.text.ToString().Remove(0, 1));
        PlayerScript.AdjustMoney(-intBet);
        CashText.text = "$" + PlayerScript.GetMoney().ToString();
        pot += (intBet * 2);
        betsText.text = "Bets: $" + pot.ToString();
    }

}
