using UnityEngine;
using System.Collections.Generic;
public class CustomTag : MonoBehaviour
{
    public static string placementWall = "wall";
    public static string placementFloor = "floor";
    public static string[] placementTypes = new string[] { "floor", "wall" };
    public List<string> placements = new List<string>();
}
