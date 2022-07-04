//Lisa Fröhlich Gabra, ER, P6, "HEL: The Human Ecosystem Laboratory"

//Providing a reference to a created string variable

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class StringReference
{
    public bool UseConstant = true;
    public string ConstantLetters;
    public StringObject Variable;

    public string Value
    {
        get { return UseConstant ? ConstantLetters : Variable.Letters; }
    }
}
