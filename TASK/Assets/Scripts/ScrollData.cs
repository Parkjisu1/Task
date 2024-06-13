using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class ScrollData 
{
    public string Image_Name { get; private set; }
    public string Content { get; private set; }

    public ScrollData(string _name, string _content)
    {
        Image_Name = _name;
        Content = _content;
    }
}
