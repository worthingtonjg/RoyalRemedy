using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Attempt : MonoBehaviour
{
    public GameObject Potion;
    public TMP_Text AttemptNumber;
    public TMP_Text Results;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Init(List<Color> guess, int fullyCorrect, int partiallyCorrect, int count)  
    {
        AttemptNumber.text = $"Attempt: {count} of 12"; 

        Potion.GetComponent<SolutionContainerUI>().SetGuess(guess);
        Results.text = $"Fully cured:\r\n     {fullyCorrect}\r\n\r\nPartially cured:\r\n     {partiallyCorrect}"; 
    }
}
