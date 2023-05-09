using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Windows;
using Random = UnityEngine.Random;
using Sequence = DG.Tweening.Sequence;

public class Selfie : MonoBehaviour
{
    public Camera selfieCamera;
    public GameObject phone;
    public GameObject hands;

    public Image snapShot;
    public Button shotButton;
    private Texture2D texture2D;
    public RenderTexture renderTexture;
    public Image camFrame;

    public Sprite SnapShotSprite;

    public Material renderMat;

    private void Start()
    {
        
    }

    void Update()
    {
        if (GameManager.Instance.gameStades == GameStades.Selfie)
        {
            phone.SetActive(true);
            hands.SetActive(true);
            Sequence sequence = DOTween.Sequence();
            sequence.Append(phone.transform.DOMove(new Vector3(6.21500015f, 1.39999998f, -2.22399998f), .5f));
            sequence.Join(phone.transform.DORotateQuaternion(new Quaternion(0.270598054f,-0.65328151f,0.270598054f,0.65328151f), .5f));
            //sequence.Append(_camera.transform.DOMove(new Vector3(3.60800004f, 2.10800004f, 0.337000012f), .5f));
            //sequence.Join(_camera.transform.DORotate(new Vector3(35f,145,0),.5f));
        }

        if (GameManager.Instance.gameStades != GameStades.Selfie)
        {
            phone.SetActive(false);
            hands.SetActive(false);
        }
    }
    
    /*public Texture2D RenderCameraToFile(string imageName)
    {
        RenderTexture rt = new RenderTexture(512, 512, 24, RenderTextureFormat.ARGB32, RenderTextureReadWrite.sRGB);
        RenderTexture oldRT = selfieCamera.targetTexture;
        selfieCamera.targetTexture = rt;
        selfieCamera.Render();
        selfieCamera.targetTexture = oldRT;
        RenderTexture.active = rt;
        Texture2D tex = new Texture2D(rt.width, rt.height, TextureFormat.RGB24, false);
        tex.ReadPixels(new Rect(0, 0, rt.width, rt.height), 0, 0);
        RenderTexture.active = null;
        byte[] bytes = tex.EncodeToJPG();
        string path = Application.persistentDataPath + "/" + imageName + ".jpg";
        System.IO.File.WriteAllBytes(path, bytes);
        tex.Apply();
        return tex;
        //AssetDatabase.ImportAsset(path);
    }
    public Texture2D LoadImage(string imageName)
    {
        string path = Application.persistentDataPath + "/" + imageName + ".jpg";
        if (System.IO.File.Exists(path))
        {
            Texture2D tex = new Texture2D(512, 512, TextureFormat.RGB24, false);
            byte[] bytes = System.IO.File.ReadAllBytes(path);
            tex.LoadImage(bytes);
            tex.Apply();
            return tex;
        }
        return null;
    }

    public void SaveRenderTexture()
    {
        RenderTexture.active = renderTexture;

        Texture2D texture = new Texture2D(renderTexture.width, renderTexture.height);
        texture.ReadPixels(new Rect(0, 0, renderTexture.width, renderTexture.height), 0, 0);
        texture.Apply();

        byte[] bytes = texture.EncodeToPNG();
        Destroy(texture);

        File.WriteAllBytes("C:GitHub/Date.io-Core/Assets/_Assets.png", bytes);

        RenderTexture.active = null;
    }*/

    public void ExportPhoto()
    {
        byte[] bytes = ToTexture2D(renderTexture).EncodeToPNG();
        var dirPath = Application.persistentDataPath + "/ExportPhoto";
        if (!System.IO.Directory.Exists(dirPath))
        {
            System.IO.Directory.CreateDirectory(dirPath);
        }
        System.IO.File.WriteAllBytes(dirPath + "/Photo_" + Random.Range(0,1000000) + ".png", bytes);
        Debug.Log(bytes.Length / 1024 + "kb was saved as: " + dirPath);
    }

    Texture2D ToTexture2D(RenderTexture rTex)
    {
        texture2D = new Texture2D(2560, 4860, TextureFormat.RGB24, false);
        RenderTexture.active = rTex;
        texture2D.ReadPixels(new Rect(0,0, rTex.width,rTex.height),0,0);
        texture2D.Apply();
        return texture2D;
    }
    public void Lens()
    {
        Debug.Log("vdfv dfvbdfv");
        //RenderCameraToFile("ekran");
        //LoadImage("ekran");
        ExportPhoto();
        snapShot.gameObject.SetActive(true);
        SnapShotSprite = Sprite.Create(texture2D, new Rect(0, 0, texture2D.width, texture2D.height), Vector2.zero);
        snapShot.sprite = SnapShotSprite;
        Sequence sequence = DOTween.Sequence();
        sequence.Append(snapShot.gameObject.transform.DOScale(Vector3.one * 0.09f, .3f));
        sequence.Append(snapShot.gameObject.transform.DOScale(Vector3.zero, .3f).SetDelay(.1f))
            .OnComplete(() =>
            {
                renderMat.mainTexture = null;
                renderMat.color = Color.magenta;
                shotButton.gameObject.SetActive(false);
                camFrame.gameObject.SetActive(false);
            });
        sequence.Append(snapShot.transform.DOScale(Vector3.one * 0.05f, .3f));
        sequence.Join(snapShot.rectTransform.DOLocalMove(new Vector3(2,0.184f,.03f), .3f));
    }
}
