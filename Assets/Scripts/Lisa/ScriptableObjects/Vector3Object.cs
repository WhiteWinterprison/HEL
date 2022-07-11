//Lisa Fröhlich Gabra, ER, P6, "HEL: The Human Ecosystem Laboratory"

//Providing a vector 3 as a scriptable object

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Variable/Vector3")]
public class Vector3Object : ScriptableObject
{
    public Vector3 Value;
}
