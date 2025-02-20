using System;
using System.Collections.Generic;
using TMPro;
using Unity.Profiling;
using UnityEngine;
using UnityEngine.SceneManagement;

public class potionController : MonoBehaviour
{
    public GameObject solutionContainer;
    public GameObject logBook;
    public GameObject glowContainer;
    public GameObject[] potionContainers;
    public List<String> potionBottleOptions;
    public List<Vector2> potionLiquidOffsets;
    private GameObject childObject;
    public Color selectedPotionColor;
    public GameObject selectedPotion;
    public int antidoteCountLimit = 4;
    public List<Color> solution;  // This is the solution we are trying to guess

    public GameObject GameOverPanel;
    public GameObject WinPanel;
    public GameObject EndTurnPanel;
    public TMP_Text EndturnText;

    private int bottleSelector;
    private List<Color> availableColors;
    private int attempts = 0;
    private int maxAttempts = 12;
    private int countFull;

    private List<string> responsesNoneCorrect = new List<string> {
        "This tastes like feet and despair! Are you trying to finish the job?",
        "Are you sure this is an antidote? It feels more like a death sentence.",
        "Do I look more poisoned to you, or is it just me?",
        "I’m starting to think you’re in on this, Alchemist!",
        "This potion was so bad, I saw my ancestors for a moment.",
        "You’re really bad at this, aren’t you?",
        "You wouldn’t happen to have a will-writing potion, would you?",
        "It’s fine. We’ll just tell the people the poison is ‘fashionable.’",
        "My poisoner must be laughing their royal boots off right now.",
        "I feel worse! HOW is that possible?"
    };

    private List<string> responsesOnlyPartials = new List<string> {
        "I feel... slightly better? Or is that just placebo?",
        "It’s a start, I suppose. But I still feel like a soggy crumpet.",
        "You’re close, but 'close' doesn’t save kings, does it?",
        "Hmm. Half the kingdom will mourn if I die. The other half might applaud.",
        "A little better! Maybe try... less frog guts next time?",
        "This is progress, but so is a snail crossing a moat.",
        "If I live, I’ll knight you. If I die, I’ll haunt you.",
        "Can you make this less... lethal next time?",
        "I feel like a partially poisoned king. Improvement, I guess?",
        "Getting warmer! Or is that just the fever?"
    };

    private List<string> responsesSomeFull = new List<string> {
        "This tastes... odd, but I think it's working! Keep going, Alchemist!",
        "Better, better! I feel like I could almost sit up without groaning.",
        "Ah, progress! Though I still feel like a poisoned pigeon.",
        "You’ve nailed part of it! The rest? Still trying to kill me.",
        "I feel halfway to better. Or halfway to dead. Hard to tell.",
        "Hmm, this antidote is getting there. But hurry! I’m not immortal!",
        "That was close! If only it didn’t still burn like dragon’s fire!",
        "Half a cure is better than none, I suppose. Unless you're me!",
        "I feel alive in my left arm now! The rest of me? Still debating.",
        "Keep it up! If the poison doesn’t kill me, maybe your optimism will!"
    };

    private List<string> responsesAllFull = new List<string> {
        "That’s it! I feel like I could joust a dragon!",
        "You’ve done it! The poison is gone, and so is my headache!",
        "Amazing! I knew I kept you around for a reason.",
        "You’ve saved the king! Drinks are on me. Or they would be if I trusted anyone to make them.",
        "I’ll rename the castle after you! 'Alchemist’s Palace' has a nice ring.",
        "Ah, sweet relief. Let’s punish the double agent, shall we?",
        "I knew you could do it. Mostly. Well, a little.",
        "You’re the hero of the realm! I’ll tell the bards to be kind in your ballad.",
        "You saved me! And to think I doubted you when you said, 'Trust me, I got this.'",
        "Good work! Now, go mix me a celebratory drink. On second thought... maybe not."
    };

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Debug.Log("Started");
        selectedPotionColor = Color.clear;

        availableColors = new List<Color> 
        {
            new Color(1,0,0,1), // red
            new Color(0,1,0,1), // green
            new Color(0,0,1,1), // blue
            new Color(1,1,0,1), // yellow
            new Color(1,0,1,1), // magenta
            new Color(0,1,1,1) // cyan
        };

        // pick a random solution
        while(solution.Count < antidoteCountLimit) solution.Add(availableColors[UnityEngine.Random.Range(0, availableColors.Count)]);

        foreach (GameObject potion in potionContainers)
        {
            bottleSelector = UnityEngine.Random.Range(0,3);
            childObject = potion.transform.GetChild(0).gameObject;
            childObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Bottles/" + potionBottleOptions[bottleSelector]);
            childObject = potion.transform.GetChild(1).gameObject;
            childObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Liquids/" + potionBottleOptions[bottleSelector]);
            childObject.transform.localPosition = potionLiquidOffsets[bottleSelector];
        }
    }

    public void SetSelectedPotion(GameObject gameObject, Color color)
    {
        selectedPotion = gameObject;
        selectedPotionColor = color;

        selectedPotion.GetComponent<Bounce>().ResetPosition();
        selectedPotion.GetComponent<Bounce>().enabled = true;
        solutionContainer.GetComponent<Animator>().SetBool("Activated", true);
        glowContainer.SetActive(true);

        foreach (var potion in potionContainers)
        {
            if (potion != selectedPotion)
            {
                potion.GetComponent<Bounce>().enabled = false;
            }
        }
    }

    public void ClearSelectedPotion()
    {
        selectedPotion.GetComponent<Bounce>().ResetPosition();
        selectedPotion.GetComponent<Bounce>().enabled = false;
        solutionContainer.GetComponent<Animator>().SetBool("Activated", false);
        glowContainer.SetActive(false);

        selectedPotion = null;
        selectedPotionColor = Color.clear;
    }

    public void guessCheck(List<Color> solutionColors)
    {
        bool[] FullMatches = {false, false, false, false};
        bool[] PartialMatches = {false, false, false, false};
        countFull = 0;
        var countPartial = 0;

        var localSolution = solution;
        for(int i = 0; i < 4; i++)
        {
            Debug.Log("Checking Full Matches");
            if(localSolution[i] == solutionColors[i])
            {
                countFull++;
                FullMatches[i] = true;
                Debug.Log("Full Match");
            }
        }
        for(int i = 0; i< localSolution.Count; i++)
        {
            if (!FullMatches[i])
            {
                for(int j = 0; j < solutionColors.Count; j++)
                {
                    Debug.Log("Checking Partial Matches for " + localSolution[i]);
                    if(!FullMatches[j] && !PartialMatches[j] && i != j && localSolution[i] == solutionColors[j])
                    {
                        countPartial++;
                        PartialMatches[j] = true;
                        j = solutionColors.Count;
                        Debug.Log("Partial Match");
                    }
                }
            }
        }

        logBook.GetComponent<Logbook>().MakeGuess(solutionColors, countFull, countPartial);
        ++attempts;


        if(countFull == 4)
        {
            ShowWin();
            return;
        }

        if(attempts >= maxAttempts)
        {
            GameOver();
            return;
        }

        ShowEndTurnPanel(countFull, countPartial);
    }

    private void ShowWin()
    {
        WinPanel.SetActive(true);
    }

    private void ShowEndTurnPanel(int countFull, int countPartial)
    {
        if(countFull == 4)
        {
            EndturnText.text = responsesAllFull[UnityEngine.Random.Range(0, responsesAllFull.Count)];
        }
        else if(countPartial == 0 && countFull == 0)
        {
            EndturnText.text = responsesNoneCorrect[UnityEngine.Random.Range(0, responsesNoneCorrect.Count)];
        }
        else if(countPartial > 0 && countFull == 0)
        {
            EndturnText.text = responsesOnlyPartials[UnityEngine.Random.Range(0, responsesOnlyPartials.Count)];
        }
        else if(countFull > 0)
        {
            EndturnText.text = responsesSomeFull[UnityEngine.Random.Range(0, responsesSomeFull.Count)];
        }

        EndTurnPanel.SetActive(true);
    }

    private void GameOver()
    {
        GameOverPanel.SetActive(true);
    }

    public void NextTurn()
    {
        EndTurnPanel.SetActive(false);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
