//Lisa Fröhlich Gabra, ER, P6, "HEL: The Human Ecosystem Laboratory"

//Providing a reference to a created bool variable

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class BoolReference
{
    public bool UseConstant = true;
    public bool ConstantValue;
    public BoolObject Variable;

    public bool Value
    {
        get { return UseConstant ? ConstantValue : Variable.Value; }
    }
}
