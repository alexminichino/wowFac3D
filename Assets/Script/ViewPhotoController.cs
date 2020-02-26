using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class ViewPhotoController : MonoBehaviour
{
    [SerializeField]
    public GameObject canvas;
    public Sprite placeholderSprite;
    string[] files = null;
    int currentImage = 0;

    // Use this for initialization
    void Start()
    {
        updateFiles();
        currentImage = currentImage = files.Length - 1;
        if (files.Length > 0)
        {
            
            showImage();
        }
        else
        {
            showPlaceholder();
        }
    }

    private void updateFiles()
    {
        files = Directory.GetFiles(Application.persistentDataPath + "/", "*.png");
    }

    void showImage()
    {
        string pathToFile = files[currentImage];
        Texture2D texture = GetImage(pathToFile);
        Sprite sp = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height),
            new Vector2(0.5f, 0.5f));
        canvas.GetComponent<Image>().sprite = sp;
    }

    Texture2D GetImage(string path)
    {
        Texture2D texture = null;
        byte[] fileBytes;
        if (File.Exists(path))
        {
            fileBytes = File.ReadAllBytes(path);
            texture = new Texture2D(2, 2, TextureFormat.RGB24, false);
            texture.LoadImage(fileBytes);
        }
        return texture;
    }

    public void nextImage()
    {
        if (files.Length > 0)
        {
            currentImage+=1;
            if(currentImage > files.Length - 1)
            {
                currentImage = 0;
            }
            showImage();
        }
    }

    public void PreviousImage()
    {
        if (files.Length > 0)
        {
            decrementIndex();
            showImage();
        }
    }

    public void sharePhoto()
    {
        StartCoroutine(shareScreenshot(files[currentImage]));
    }

    private IEnumerator shareScreenshot(string destination)
    {
        yield return new WaitForEndOfFrame();
        new NativeShare().AddFile(destination).SetSubject("Prova la nostra fantastica app di realtà aumentata!").SetText("Prova la nostra app!").Share();
    }

    private void showPlaceholder()
    {
        canvas.GetComponent<Image>().sprite = placeholderSprite;
        foreach (Button b in canvas.GetComponentsInChildren<Button>())
        {
            if (b.name != "Back")
            {
                b.interactable = false;
            }
        }
    }

    private void decrementIndex()
    {
        currentImage -= 1;
        if (currentImage < 0)
        {
            currentImage = files.Length - 1;
        }
    }

    public void deletePhoto()
    {
        updateFiles();
        if (files.Length > 0)
        {
            FileInfo info = new FileInfo(files[currentImage]);
            info.Delete();
            updateFiles();          
            if (files.Length > 0)
            {
                decrementIndex();
                showImage();               
            }
            else
            {
                showPlaceholder();                
            }
        }      
    }
}
