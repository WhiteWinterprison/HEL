//Lisa Fröhlich Gabra, ER, P6, "HEL: The Human Ecosystem Laboratory"

//Providing a list of strings as a scriptable object

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Variable/List<String>")]
public class ListObject : ScriptableObject
{
    public List<string> Content;
}
