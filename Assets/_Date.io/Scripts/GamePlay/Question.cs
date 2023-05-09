using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Question : MonoBehaviour
{
    private Camera _camera;
    public int qId;

    [Header("Speaking Baloon")] 
    public TextMeshProUGUI questionMarkText;
    public TextMeshProUGUI button1;
    public TextMeshProUGUI button2;
    public TextMeshProUGUI button3;
    //public TextMeshProUGUI button4;
    public Canvas answerCanvas;
    public Canvas questionCanvas;
    public TextMeshProUGUI answerText;
    
    void Start()
    {
        _camera = Camera.main;
        qId = 0;
    }
    
    void Update()
    {
        if (GameManager.Instance.gameStades == GameStades.Questions)
        {
            _camera.transform.rotation = new Quaternion(0.122787789f,-0.696364284f,0.122787789f,0.696364284f);
            answerCanvas.enabled = false;
        }

        switch (qId)
        {
            case 0:
                questionMarkText.text = "Hmm, what should I ask her??";
                button1.text = "What's your favorite color?";
                button2.text = "Winter or Summer?";
                button3.text = "Sunset or Sunrise?";
                //button4.text = "Day or Night?";
                break;
        }

        if (GameManager.Instance.gameStades == GameStades.Answer)
        {
            answerCanvas.enabled = true;
        }
    }

    public void Button1()
    {
        int a = Random.Range(0, 4);
        
        switch (a)
        {
            case 0:
                ButtonEvent("Blue");
                break;
            case 1:
                ButtonEvent("Yellow");
                break;
            case 2:
                ButtonEvent("Green");
                break;
            case 3:
                ButtonEvent("Red"); 
                break;

        }
        
        GameManager.Instance.gameStades = GameStades.Tap;
    }

    public void Button2()
    {
        int b = Random.Range(0,2);

        switch (b)
        {
            case 0:
                ButtonEvent("Summer");
                break;
            case 1:
                ButtonEvent("Winter");
                break;
        }
        
        GameManager.Instance.gameStades = GameStades.Tap;
    }

    public void Button3()
    {
        int c = Random.Range(0, 2);
        
        switch (c)
        {
            case 0:
                ButtonEvent("Sunset");
                break;
            case 1:
                ButtonEvent("Sunrise");
                break;
        }
        ButtonEvent("Sunset");
        GameManager.Instance.gameStades = GameStades.Tap;
    }

    public void ButtonEvent(string text)
    {
        answerText.text = text;
        questionCanvas.enabled = false;
        answerCanvas.enabled = true;
        //_camera.transform.DORotate(Vector3.down * -10, .5f);
    }
}
