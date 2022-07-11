//Lisa Fröhlich Gabra, ER, P6, "HEL: The Human Ecosystem Laboratory"

//Providing a reference to a created list of strings

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class ListReference
{
    public bool UseConstant = true;
    public List<string> ConstantContent;
    public ListObject Variable;

    public List<string> Content
    {
        get { return UseConstant ? ConstantContent : Variable.Content; }
    }
}
