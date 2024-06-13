using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIScrollRectInfiniteListItem : MonoBehaviour
{
    public Text TxtName;
    public Text TxtImage;
    public void Init(ScrollData _scrollData)
    {
        TxtName.text = _scrollData.Content;
        TxtImage.text = _scrollData.Image_Name;
    }
}
