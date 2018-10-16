using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Threading;

//attach script to image
//can process files where a number in the filename is increased for each frame
public class makeGif : MonoBehaviour
{
    public Image UnityImage;
    public string directory = @"";
    public string filenameBeforeNum = "";
    public string filenameAfterNum = "";
    public int firstNumber = 1;
    public int lastNumber = 15;
    // Use this for initialization
    void Start()
    {
        for (int i = firstNumber; i <= lastNumber; i++)
        {
            //UnityImage = Resources.Load<Image>(directory + filenameBeforeNum + i.ToString() + filenameAfterNum);
            //UnityImage.sprite = Resources.Load<Sprite>("spaceToilet" + i.ToString());
            Thread.Sleep(70);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
