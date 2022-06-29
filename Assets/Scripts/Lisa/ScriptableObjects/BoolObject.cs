//Lisa Fröhlich Gabra, ER, P6, "HEL: The Human Ecosystem Laboratory"

//Providing a boolean as a scriptable object

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Variable/Boolean")]
public class BoolObject : ScriptableObject
{
    public bool Value;
}
