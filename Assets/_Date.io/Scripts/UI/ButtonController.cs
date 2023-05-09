using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using PaintIn3D;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ButtonController : MonoBehaviour
{
    [Header("Acrylic")]
    [SerializeField] public Button acrylicButton;
    [SerializeField] public TextMeshProUGUI acrylicText;
    [SerializeField] public Image acrylicsImage;
    [SerializeField] public Outline acrylicsOutline;
    [SerializeField] public Image acrylicsPanel;
    
    [Header("Marker")]
    [SerializeField] public Button markersButton;
    [SerializeField] public TextMeshProUGUI markerText;
    [SerializeField] public Image markersImage;
    [SerializeField] public Outline markersOutline;
    [SerializeField] public Image markersPanel;
    
    [Header("Sticker")]
    [SerializeField] public Button stickerButton;
    [SerializeField] public TextMeshProUGUI stickerText;
    [SerializeField] public Image stickerImage;
    [SerializeField] public Outline stickerOutline;
    [SerializeField] public Image stickersPanel;

    [Header("Scripts")]
    [SerializeField] private P3dPaintSphere _paintSphere;
    [SerializeField] private GameObject PaintBoss;

    [Header("Canvas")] public Canvas generalCanvas;

    [Header("Tab To Draw")] public Button tabToDrawButton;
    public TextMeshProUGUI timer;

    [Header("Stage Text")] 
    public TextMeshProUGUI stageText;
    public int turn;

    [Header("Canvases")] 
    public Transform playerCanvas;
    public Transform aiCanvas;

    [Header("Color Material")] 
    public Material pencilMaterial;
    public Material brushMaterial;

    [Header("Stickers")] 
    public GameObject angryFace;
    public GameObject star;
    public GameObject heart;
    public GameObject smile;
    
    public static ButtonController Instance;
    private Vector3 _camStartPos;
    private Quaternion _camStartRot;

    private Vector3 _playerCanvasPos;
    private Vector3 _iaCanvasPos;
    private Quaternion _playerCanvasRot;
    private Quaternion _aiCanvasRot;

    public bool change;
    private Camera _cam;
    private void Awake()
    {
        Instance = this; 
        _cam = Camera.main;
        _camStartPos = _cam.transform.position;
        _camStartRot = _cam.transform.rotation;

        _playerCanvasPos = playerCanvas.transform.position;
        _playerCanvasRot = playerCanvas.transform.rotation;

        _iaCanvasPos = aiCanvas.transform.position;
        _aiCanvasRot = aiCanvas.transform.rotation;
    }

    void Start()
    {
        ResetButtons();
        generalCanvas.enabled = false;
        turn = 1;
        stageText.text = "turn: " + turn;
        
        FixStickers();
    }

    private void Update()
    {
        if (Timer.Instance.timer == 0 && turn < 5)
        {
            ChangeCanvasButton();
        }

        if (turn >= 5)
        {
            generalCanvas.enabled = false;
            GameManager.Instance.gameStades = GameStades.Selfie;
        }

        if (turn < 4)
        {
            stickerButton.interactable = false;
        }
        else
        {
            stickerButton.interactable = true;
        }

        if (stickerButton.interactable == false)
        {
            stickerImage.enabled = false;
        }
        else
        {
            stickerImage.enabled = true;
        }
        
    }

    public void Acrylics()
    {
        ResetButtons();
        acrylicButton.image.color = Color.white;
        acrylicsImage.color = Color.white;
        acrylicsOutline.enabled = true;
        acrylicText.color = Color.magenta;
        acrylicsPanel.gameObject.SetActive(true);
        GameManager.Instance.gameStades = GameStades.Draw;
        PainterObectController.Instance.painterStade = PainterStade.Null;
        _paintSphere.Radius = .02f;
    }

    public void Markers()
    {
        ResetButtons();
        markersButton.image.color = Color.white;
        markersImage.color = Color.white;
        markersOutline.enabled = true;
        markerText.color = Color.magenta;
        markersPanel.gameObject.SetActive(true);
        PainterObectController.Instance.painterStade = PainterStade.Null;
        _paintSphere.Radius = .005f;
    }

    public void Stickers()
    {
        ResetButtons();
        stickerButton.image.color = Color.white;
        stickerImage.color = Color.white;
        stickerOutline.enabled = true;
        stickerText.color = Color.magenta;
        stickersPanel.gameObject.SetActive(true);
        PainterObectController.Instance.painterStade = PainterStade.Null;
    }

    public void ResetButtons()
    {
        acrylicButton.image.color = Color.magenta;
        acrylicsImage.color = Color.magenta;
        acrylicsOutline.enabled = false;
        acrylicsPanel.gameObject.SetActive(false);
        acrylicText.color = Color.white;
        
        
        markersButton.image.color = Color.magenta;
        markersImage.color = Color.magenta;
        markersOutline.enabled = false;
        markersPanel.gameObject.SetActive(false);
        markerText.color = Color.white;
        
        stickerButton.image.color = Color.magenta;
        stickerImage.color = Color.magenta;
        stickerOutline.enabled = false;
        stickersPanel.gameObject.SetActive(false);
        stickerText.color = Color.white;
    }

    public void RedMarker()
    {
        PainterObectController.Instance.painterStade = PainterStade.Marker;
        _paintSphere.Color = Color.red;
        pencilMaterial.color = Color.red;
        FixStickers();
    }

    public void BlueMarker()
    {
        PainterObectController.Instance.painterStade = PainterStade.Marker;
        _paintSphere.Color = Color.blue;
        pencilMaterial.color = Color.blue;
        FixStickers();
    }

    public void GreenMarker()
    {
        PainterObectController.Instance.painterStade = PainterStade.Marker;
        _paintSphere.Color = Color.green;
        pencilMaterial.color = Color.green;
        FixStickers();
    }

    public void YellowMarker()
    {
        PainterObectController.Instance.painterStade = PainterStade.Marker;
        _paintSphere.Color = Color.yellow;
        pencilMaterial.color = Color.yellow;
        FixStickers();
    }

    public void RedAcrylicsRed()
    {
        PainterObectController.Instance.painterStade = PainterStade.Brush;
        _paintSphere.Color = Color.red;
        brushMaterial.color = Color.red;
        FixStickers();
    }
    
    public void RedAcrylicsBlue()
    {
        PainterObectController.Instance.painterStade = PainterStade.Brush;
        _paintSphere.Color = Color.blue;
        brushMaterial.color = Color.blue;
        FixStickers();
    }
    
    public void RedAcrylicsYellow()
    {
        PainterObectController.Instance.painterStade = PainterStade.Brush;
        _paintSphere.Color = Color.yellow;
        brushMaterial.color = Color.yellow;
        FixStickers();
    }
    
    public void RedAcrylicsGreen()
    {
        PainterObectController.Instance.painterStade = PainterStade.Brush;
        _paintSphere.Color = Color.green;
        brushMaterial.color = Color.green;
        FixStickers();
    }

    public void TapToDraw()
    {
        tabToDrawButton.gameObject.SetActive(false);
        _cam.gameObject.transform.DOMove(new Vector3(6, 1.3f, -2.2f), .5f);
        _cam.gameObject.transform.DORotate(new Vector3(70f, 270f, 0),.5f);
        GameManager.Instance.gameStades = GameStades.Draw;
        generalCanvas.enabled = true;
        timer.enabled = true;
    }

    public void AngryFaceSticker()
    {
        FixStickers();
        angryFace.SetActive(true);
    }

    public void SmileSticker()
    {
        FixStickers();
        smile.SetActive(true);
    }

    public void HearthSticker()
    {
        FixStickers();
        heart.SetActive(true);
    }

    public void StarSticker()
    {
        FixStickers();
        star.SetActive(true);
    }
    
    

    public void ChangeCanvasButton()
    {
        GameManager.Instance.changeControl = false;
        Timer.Instance.timer = 12;
        change = true;
        generalCanvas.enabled = false;
        Sequence sequence = DOTween.Sequence();
        sequence.Append(Camera.main.transform.DOMove(_camStartPos, .5f));
        sequence.Join(Camera.main.transform.DORotateQuaternion(_camStartRot, .5f));
        sequence.Append(playerCanvas.transform.DOMove(_iaCanvasPos, .5f));
        sequence.Join(playerCanvas.DOJump(_iaCanvasPos, .2f, 1,.5f));
        sequence.Join(playerCanvas.transform.DORotateQuaternion(_aiCanvasRot, .5f));
        sequence.Join(aiCanvas.transform.DOMove(_playerCanvasPos, .5f));
        sequence.Join(aiCanvas.transform.DORotateQuaternion(_playerCanvasRot, .5f));
        
        sequence.onComplete = () =>
        {
            generalCanvas.enabled = true;
            _iaCanvasPos = aiCanvas.position;
            _playerCanvasPos = playerCanvas.position;
            _aiCanvasRot = aiCanvas.rotation;
            _playerCanvasRot = playerCanvas.rotation;
            change = false;
            GameManager.Instance.changeControl = true;
        };
        turn++;
        stageText.text = "turn: " + turn;

        if (turn != 5)
        {
            sequence.Append(_cam.gameObject.transform.DOMove(new Vector3(6, 1.3f, -2.2f), .5f));
            sequence.Append(_cam.gameObject.transform.DORotate(new Vector3(75f, 270f, 0),.5f));
        }
    }

    public void FixStickers()
    {
        angryFace.SetActive(false);
        smile.SetActive(false);
        heart.SetActive(false);
        star.SetActive(false);
    }
}
