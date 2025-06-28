using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HudManager : MonoBehaviour
{

    public static HudManager Instance { get; private set; }

    public int points = 0000000;

    public Text pointsText;
    public Text pointsTextDead;

    public GameObject deadScreen;
    public GameObject aliveUI;
    
    public List<Sprite> heartsImage;
    public List<GameObject> heartsList;

    EntityStats playerStats;

    private void Awake()
    {
        // If there is an instance, and it's not me, delete myself.

        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        playerStats = GameObject.FindGameObjectWithTag("Player").GetComponent<EntityStats>();
        Time.timeScale = 1;
    }

    // Update is called once per frame
    void Update()
    {
        pointsText.text = points.ToString();
        pointsTextDead.text = points.ToString();
    }

    public void Restart()
    {
        SceneManager.LoadScene("SampleScene");
    }
    public void updateLifeUi()
    {
        if (playerStats.hp >= 10)
        {
            heartsList[0].GetComponent<Image>().sprite = heartsImage[0];
            heartsList[1].GetComponent<Image>().sprite = heartsImage[0];
        }
        else if (playerStats.hp == 9)
        {
            heartsList[0].GetComponent<Image>().sprite = heartsImage[1];
            heartsList[1].GetComponent<Image>().sprite = heartsImage[0];
        }
        else if (playerStats.hp == 8)
        {
            heartsList[0].GetComponent<Image>().sprite = heartsImage[2];
            heartsList[1].GetComponent<Image>().sprite = heartsImage[0];
            heartsList[2].GetComponent<Image>().sprite = heartsImage[0];
        }
        else if (playerStats.hp == 7)
        {
            heartsList[0].GetComponent<Image>().sprite = heartsImage[2];
            heartsList[1].GetComponent<Image>().sprite = heartsImage[1];
            heartsList[2].GetComponent<Image>().sprite = heartsImage[0];
        }
        else if (playerStats.hp == 6)
        {
            heartsList[1].GetComponent<Image>().sprite = heartsImage[2];
            heartsList[2].GetComponent<Image>().sprite = heartsImage[0];
            heartsList[3].GetComponent<Image>().sprite = heartsImage[0];
        }
        else if (playerStats.hp == 5)
        {
            heartsList[1].GetComponent<Image>().sprite = heartsImage[2];
            heartsList[2].GetComponent<Image>().sprite = heartsImage[1];
            heartsList[3].GetComponent<Image>().sprite = heartsImage[0];
        }
        else if (playerStats.hp == 4)
        {
            heartsList[2].GetComponent<Image>().sprite = heartsImage[2];
            heartsList[3].GetComponent<Image>().sprite = heartsImage[0];
            heartsList[4].GetComponent<Image>().sprite = heartsImage[0];
        }
        else if (playerStats.hp == 3)
        {
            heartsList[2].GetComponent<Image>().sprite = heartsImage[2];
            heartsList[3].GetComponent<Image>().sprite = heartsImage[1];
            heartsList[4].GetComponent<Image>().sprite = heartsImage[0];
        }
        else if (playerStats.hp == 2)
        {
            heartsList[3].GetComponent<Image>().sprite = heartsImage[2];
            heartsList[4].GetComponent<Image>().sprite = heartsImage[0];
        }
        else if (playerStats.hp == 1)
        {
            heartsList[3].GetComponent<Image>().sprite = heartsImage[2];
            heartsList[4].GetComponent<Image>().sprite = heartsImage[1];
        }
        else if (playerStats.hp <= 0)
        {
            heartsList[4].GetComponent<Image>().sprite = heartsImage[2];
        }
    }

}
