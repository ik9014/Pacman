using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DirectorController : MonoBehaviour
{
    public PacmanController pacmancontroller;
    public Text scoreText;

    public AudioSource munch_1;
    public AudioSource munch_2;
    public AudioSource Siren;
    int currentMunch = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = pacmancontroller.score.ToString();
        
    }

    public void sound_eatCookie(PacmanController pacmanController)
    {
        if (currentMunch == 0)
        {
            munch_1.Play();
            currentMunch = 1;
        }
        else if (currentMunch == 1)
        {
            munch_2.Play();
            currentMunch = 0;
        }
    }
}
