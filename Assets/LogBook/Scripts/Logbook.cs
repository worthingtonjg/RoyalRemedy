using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Logbook : MonoBehaviour
{
    public GameObject Pages;

    public ScrollRect ScrollRect;
    public float scrollAmount = 10f; 

    public GameObject AttemptPrefab;

    private List<Potion> _solution = new List<Potion>();

    private List<GameObject> _attempts = new List<GameObject>();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Init(List<Potion> solution)
    {
        _solution = solution;
        
        ClearAttempts();
    }       

    private void ClearAttempts()
    {
        foreach (var attempt in _attempts)
        {
            Destroy(attempt);
        }

        _attempts.Clear();
    }

    public void MakeGuess(List<Color> guess, int fullyCorrect, int partiallyCorrect)
    {
        var attemptInstance = Instantiate(AttemptPrefab);
        attemptInstance.transform.parent = Pages.transform;
        
        _attempts.Add(attemptInstance);
        
        var attemptComponent = attemptInstance.GetComponent<Attempt>();
     
        attemptComponent.Init(guess, fullyCorrect, partiallyCorrect, _attempts.Count);  

        StartCoroutine(ScrollToBottom());
    }

    IEnumerator ScrollToBottom()
    {
        yield return new WaitForSeconds(0.1f);
        ScrollRect.verticalNormalizedPosition = 0f;
    }

}
