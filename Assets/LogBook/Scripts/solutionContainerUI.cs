using System.Collections.Generic;
using UnityEngine;

public class SolutionContainerUI : MonoBehaviour
{
    public GameObject[] solutions;

    public void SetGuess(List<Color> guess)
    {
        int index = 0;
        foreach(var g in guess)
        {
            solutions[index].GetComponent<UnityEngine.UI.Image>().color = g;
            index++;
        }
    }
}
