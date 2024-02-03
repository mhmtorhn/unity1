using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinBanks : MonoBehaviour
{
    public int bank;

    public static CoinBanks instance;

    public Text bankText;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    void Start()
    {
        bankText.text = "x " + bank.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Money(int coinCollected)
    {
        bank += coinCollected;
        bankText.text = "x " + bank.ToString();
    }

}
