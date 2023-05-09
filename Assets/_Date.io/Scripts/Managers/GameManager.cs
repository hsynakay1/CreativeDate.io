using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public enum GameStades
{
    Questions,
    Answer,
    Tap,
    Draw,
    Selfie,
    Survey
}
public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameStades gameStades;
    public GameObject paint;

    public bool changeControl;
    private void Awake()
    {
        Instance = this;
        gameStades = GameStades.Questions;
    }

    private void Start()
    {
        changeControl = true;
        gameStades = GameStades.Questions;
    }

    private void Update()
    {
        if (gameStades == GameStades.Questions)
        {
            PainterObectController.Instance.gameObject.SetActive(false);
            ButtonController.Instance.tabToDrawButton.enabled = false;
            ButtonController.Instance.tabToDrawButton.image.enabled = false;
        }

        if (gameStades == GameStades.Draw)
        {
            PainterObectController.Instance.gameObject.SetActive(true);
        }

        if (gameStades == GameStades.Tap && Input.GetMouseButton(0))
        {
            Camera.main.transform.DORotateQuaternion(new Quaternion(0.212631077f,-0.674379766f,0.212631077f,0.674379766f),.5f);
            ButtonController.Instance.tabToDrawButton.enabled = true;
            ButtonController.Instance.tabToDrawButton.image.enabled = true;
        }
        
        
    }
}
