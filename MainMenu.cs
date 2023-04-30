using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public InputField speedInput;
    public InputField beeInput;
    public Toggle naiveImplement;


   
    public void StartSimulation()
        {
        SceneManager.LoadScene("SampleScene");
        }

    public void EndSimulation()
        {
        SceneManager.LoadScene("MenuScene");
        }

    public void Quit()
        {
        Application.Quit();
        }

    public void EditSpeed()
        {

        string speedString = speedInput.text;

        float speed;
        try
            {
            speed = int.Parse(speedString);
            }
        catch (FormatException e)
            {
            speed = 3;
            }

        if (speed <= 0 || speed > 6) speed = 3;

        PlayerPrefs.SetFloat("speed", speed);
        
        }

    public void EditBees()
        {
        string beeString;
        if (beeInput.text == null) beeString = "20";
        else beeString = beeInput.text;

        int bee;
        try
            {
            bee = int.Parse(beeString);
            
            }
        catch (FormatException e)
            {
            bee = 20;
            }

        if (bee <= 1 || bee > 60) bee = 20;
        PlayerPrefs.SetInt("bee", bee);
        
        }

    public void EditSimType()
        {
        if (naiveImplement == null) PlayerPrefs.SetInt("naive", 0);
        else PlayerPrefs.SetInt("naive", naiveImplement.isOn ? 1:0);

       Debug.Log(PlayerPrefs.GetInt("naive"));
        }
    }
