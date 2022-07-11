//Lisa Fröhlich Gabra, ER, P6, "HEL: The Human Ecosystem Laboratory"

//Providing a reference to a created byte variable

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class ByteReference
{
    public bool UseConstant = true;
    public byte ConstantValue;
    public ByteObject Variable;

    public byte Value
    {
        get { return UseConstant ? ConstantValue : Variable.Value; }
    }
}
