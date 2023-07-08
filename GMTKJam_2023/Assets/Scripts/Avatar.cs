using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Avatar : MonoBehaviour
{
    [SerializeField] MeshRenderer meshRenderer;
    [SerializeField] Transform headParent;
    [SerializeField] AvatarCharacteristics avatarCharacteristics;

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
        meshRenderer.material = avatarCharacteristics.GetAvatarMaterial(color);
    }

    public void AssignHead(HeadType headToAssign)
    {
        head = headToAssign;

        foreach (Transform child in headParent)
            Destroy(child.gameObject);

        GameObject prefab = avatarCharacteristics.GetAvatarHead(head);
        Instantiate(prefab, headParent);
    }
}
