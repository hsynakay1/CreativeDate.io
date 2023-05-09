using System;
using System.Collections;
using System.Collections.Generic;
using PaintIn3D;
using UnityEngine;

public enum PainterStade
{
    Marker,
    Brush,
    Sticker,
    Null
}
public class PainterObectController : MonoBehaviour
{
    public static PainterObectController Instance;
    
    public PainterStade painterStade;
    private RaycastHit _hit;
    private Ray _ray;

    public GameObject brush;
    public GameObject marker;

    public GameObject paint;
    //public GameObject paintableObject;

    private Camera _cam;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        _cam = Camera.main;
    }

    void Update()
    {
        if (GameManager.Instance.gameStades != GameStades.Draw)
        {
            painterStade = PainterStade.Null;
           brush.SetActive(false);
           marker.SetActive(false);
           GameManager.Instance.paint.SetActive(false);
        }
        _ray = _cam.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(_ray,out _hit,1000f))
        {
            transform.position = Vector3.Lerp(transform.position, _hit.point, 100f * Time.deltaTime);
        }
        
        if (painterStade == PainterStade.Brush)
        {
            brush.SetActive(true);
            marker.SetActive(false);
            paint.SetActive(GameManager.Instance.changeControl);
            
            
        }
        else if (painterStade == PainterStade.Marker)
        {
            marker.SetActive(true);
            brush.SetActive(false);
            paint.SetActive(GameManager.Instance.changeControl);
                
        }
        else if (painterStade == PainterStade.Null) 
        {
            marker.SetActive(false);
            brush.SetActive(false);
            paint.SetActive(GameManager.Instance.changeControl);
        }
    }
}
