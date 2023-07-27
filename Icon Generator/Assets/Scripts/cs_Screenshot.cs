using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public enum FileType
{

    png,
    jpg,
    gif,
    tiff

}

public class cs_Screenshot : MonoBehaviour
{

    public Camera iconCamera;

    [SerializeField] private FileType fileType = FileType.png;
    [SerializeField] private string outputFolder = "Icons";
    [SerializeField] private string outputFileName = "Icon_";

    public int iconWidth = 512;
    public int iconHeight = 512;

    public GameObject[] iconObjs;

    public Projector iconSilhouetteProjector;
    public bool silhouette = false;

    public Color silhouetteColor;



    // Start is called before the first frame update
    void Start()
    {

        if (outputFolder == "")
        {

            outputFolder = "Icons";

        }

        for (int i = 0; i < iconObjs.Length; i++)
        {

            iconObjs[i].SetActive(false);

        }

        if (iconWidth <= 0)
        {

            iconWidth = 512;

        }

        if (iconHeight <= 0)
        {

            iconHeight = 512;

        }

        if (iconCamera == null)
        {

            iconCamera = GetComponent<Camera>();

        }

        if (silhouette)
        {

            if (iconSilhouetteProjector == null)
            {

                iconSilhouetteProjector = iconCamera.GetComponent<Projector>();

            }

            iconSilhouetteProjector.enabled = true;

            iconSilhouetteProjector.material.color = silhouetteColor;



        }

        StartCoroutine(Capture());

    }

    IEnumerator Capture()
    {

        for (int i = 0; i < iconObjs.Length; i++)
        {

            iconObjs[i].SetActive(true);

            yield return new WaitForSeconds(0.4f);

            string folderPath = "Assets/" + outputFolder + "/"; // the path of your project folder

            if (!System.IO.Directory.Exists(folderPath))// if this path does not exist yet
            {
                System.IO.Directory.CreateDirectory(folderPath);  // it will get created
            }

            // puts the current time right into the screenshot name  // put youre favorite data format here
            var screenshotName = outputFileName + iconObjs[i].name + "." + fileType;




            Texture2D iconTexture = new Texture2D(iconWidth, iconHeight, TextureFormat.ARGB32, false);

            RenderTexture iconRenderTexture = new RenderTexture(iconTexture.width, iconTexture.height, 24);

            RenderTexture camRenderTexture = iconCamera.targetTexture;

            iconCamera.targetTexture = iconRenderTexture;

            iconCamera.Render();

            iconCamera.targetTexture = camRenderTexture;
            RenderTexture.active = iconRenderTexture;

            //discussions.unity.com/t/how-to-save-a-texture2d-into-a-png/184699/3



            iconTexture.ReadPixels(new Rect(0, 0, iconTexture.width, iconTexture.height), 0, 0);



            iconTexture.Apply();

            // System.IO.File.WriteAllBytes(Application.dataPath + "/../screenshot_" + ssn.ToString() + "_" + Random.Range(0, 1024).ToString() + ".png", pngShot);


            byte[] bytes = iconTexture.EncodeToPNG();
            System.IO.File.WriteAllBytes(folderPath + screenshotName, bytes);


            Debug.Log(folderPath + screenshotName); // You get instant feedback in the console

            yield return new WaitForSeconds(0.4f);

            iconObjs[i].SetActive(false);


        }

        yield return null;

    }


}


