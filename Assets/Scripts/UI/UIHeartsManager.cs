using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHeartsManager : MonoBehaviour
{
    [SerializeField] private Image[] hearts;
    [SerializeField] private Sprite fullHeart;
    [SerializeField] private Sprite threeQuartersHeart;
    [SerializeField] private Sprite halfHeart;
    [SerializeField] private Sprite quarterHeart;
    [SerializeField] private Sprite emptyHeart;

    private int playerCurrentHealth;

    [Header("Listening on")]
    [SerializeField] private IntEventChannelSO updateHearts;

    // Start is called before the first frame update
    void Start()
    {
        InitHearts();
    }

    private void OnEnable()
    {
        updateHearts.OnEventRaised += UpdateHearts;
    }

    private void OnDisable()
    {
        updateHearts.OnEventRaised -= UpdateHearts;
    }

    public void InitHearts()
    {
        for(int i = 0; i < hearts.Length; i++)
        {
            hearts[i].gameObject.SetActive(true);
            hearts[i].sprite = fullHeart;
        }
    }

    public void DisableHearts()
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            hearts[i].gameObject.SetActive(false);
            hearts[i].sprite = fullHeart;
        }
    }

    public void UpdateHearts(int health)
    {
        playerCurrentHealth = health;


        for(int i = 0; i < hearts.Length; i++)
        {
            //Subtract the amoutn of health leading up to this heart. Then, what's left over tells us what state this heart should be in.
            //We clamp it since we know any value <= 0 would mean an empty heart, and any greater than 4 would be more than a full heart.
            //We multiply "i" by 4 because we are working with quarters and need to make the math match our health.
            int remainderHealth = Mathf.Clamp(playerCurrentHealth - (i * 4), 0, 4);

            switch(remainderHealth)
            {
                case 0:
                    hearts[i].sprite = emptyHeart;
                    break;
                case 1:
                    hearts[i].sprite = quarterHeart;
                    break;
                case 2:
                    hearts[i].sprite = halfHeart;
                    break;
                case 3:
                    hearts[i].sprite = threeQuartersHeart;
                    break;
                case 4:
                    hearts[i].sprite = fullHeart;
                    break;
            }
            
        }
    }
    
}
