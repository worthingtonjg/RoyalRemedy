using Unity.VisualScripting;
using UnityEngine;

public class potionContainer : MonoBehaviour
{
    private GameObject potionController;
    public GameObject potionLiquid;
    private Color potionColor;

    public AudioClip clip;
    public AudioSource audioSource;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        potionController = GameObject.Find("potionController");
        potionColor = potionLiquid.GetComponent<SpriteRenderer>().color;
    }

    void OnMouseDown()
    {
        potionController.GetComponent<potionController>().SetSelectedPotion(this.gameObject, potionColor);
        Debug.Log("Potion Clicked");
        //audioSource.PlayOneShot(clip);
    }
}
