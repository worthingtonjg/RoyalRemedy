using System.Collections.Generic;
using UnityEngine;

public class Pager : MonoBehaviour
{
    public List<GameObject> pages;
    public int currentPage = 0;
    
    public void NextPage()
    {

        pages[currentPage].SetActive(false);

        if(currentPage < pages.Count - 1) 
        {
            currentPage = (currentPage + 1) % pages.Count;
            pages[currentPage].SetActive(true);
        }
    }
    
}
