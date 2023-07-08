using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Avatar : MonoBehaviour
{
    private string avatarName;
    private ColorType color;
    private HeadType head;

    public string AvatarName => avatarName;
    public ColorType Color => color;
    public HeadType Head => head;

    public void AssignName(string nameToAssign)
    {
        avatarName = nameToAssign;
    }

    public void AssignColor(ColorType colorToAssign)
    {
        color = colorToAssign;
    }

    public void AssignHead(HeadType headToAssign)
    {
        head = headToAssign;
    }
}
