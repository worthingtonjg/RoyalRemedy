using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LogbookController : MonoBehaviour
{
    public Color[] Solution = {Color.red, Color.blue, Color.yellow, Color.green};

    public GameObject LogBook;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        var logbookComponent = LogBook.GetComponent<Logbook>();

        logbookComponent.MakeGuess(GetRandomColors(4), 0, 0);
        logbookComponent.MakeGuess(GetRandomColors(4), 0, 2);
        logbookComponent.MakeGuess(GetRandomColors(4), 2, 0);
        logbookComponent.MakeGuess(GetRandomColors(4), 1, 3);
    }

    private List<Color> GetRandomColors(int count)
    {
        var result = new List<Color> {Color.red, Color.blue, Color.yellow, Color.green, Color.magenta, Color.cyan};
        return result.OrderBy(x => Random.value).Take(count).ToList();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
