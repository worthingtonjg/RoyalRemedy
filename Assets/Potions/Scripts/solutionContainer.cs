using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class solutionContainer : MonoBehaviour
{
    public GameObject[] solutions;
    public List<Color> solutionColors;
    private GameObject potionController;
    private Color potionColor;

    public AudioClip pourClip;
    public AudioClip clinkClip;
    public AudioSource audioSource;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        potionController = GameObject.Find("potionController");
    }

    void OnMouseDown()
    {
        if(potionController.GetComponent<potionController>().selectedPotion == null)
        {
            if(solutionColors.Count > 0)
            {
                solutions.Last(s => s.GetComponent<SpriteRenderer>().color != Color.clear).GetComponent<SpriteRenderer>().color = Color.clear;
                solutionColors.RemoveAt(solutionColors.Count - 1);
                //audioSource.PlayOneShot(clinkClip);
                return;
            }
        }

        Debug.Log("Solution Clicked");
        potionColor=potionController.GetComponent<potionController>().selectedPotionColor;
        foreach (GameObject solution in solutions)
        {
            if ((solution.GetComponent<SpriteRenderer>().color == Color.clear) && (potionController.GetComponent<potionController>().selectedPotionColor != Color.clear))
            {
                solution.GetComponent<SpriteRenderer>().color = potionColor;
                solutionColors.Add(potionColor);
                potionController.GetComponent<potionController>().ClearSelectedPotion();
                Debug.Log("Potion Added");
                //audioSource.PlayOneShot(pourClip);
            }
            else
            {
                Debug.Log("Potion Not Added");
            }
        }

        // Bottle is full
        if(solutionColors.Count == 4)
        {
            potionController.GetComponent<potionController>().guessCheck(solutionColors);
            EmptySolution();
        }
    }

    void EmptySolution()
    {
        foreach (GameObject solution in solutions)
        {
            solution.GetComponent<SpriteRenderer>().color = Color.clear;
        }

        solutionColors.Clear();
    }
}
