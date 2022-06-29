//Lisa Fröhlich Gabra, ER, P6, "HEL: The Human Ecosystem Laboratory"

//Providing a reference to a created vector 3 variable

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class Vector3Reference
{
    public bool UseConstant = true;
    public Vector3 ConstantValue;
    public Vector3Object Variable;

    public Vector3 Value
    {
        get { return UseConstant ? ConstantValue : Variable.Value; }
    }
}
