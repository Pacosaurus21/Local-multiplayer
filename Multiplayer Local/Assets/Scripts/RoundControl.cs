using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RoundControl : MonoBehaviour
{
    public TextMeshProUGUI roundsText;
    public int rounds;
    
    void Update()
    {
        roundsText.text = rounds.ToString();
    }
}
