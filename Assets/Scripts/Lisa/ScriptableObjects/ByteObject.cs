//Lisa Fröhlich Gabra, ER, P6, "HEL: The Human Ecosystem Laboratory"

//Providing a byte as a scriptable object

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Variable/Byte")]
public class ByteObject : ScriptableObject
{
    public byte Value;
}
