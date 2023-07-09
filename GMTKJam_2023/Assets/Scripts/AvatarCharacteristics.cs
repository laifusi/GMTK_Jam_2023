using System;
using UnityEngine;

[CreateAssetMenu(fileName = "new AvatarCharacteristics", menuName = "AvatarCharacteristics")]
public class AvatarCharacteristics : ScriptableObject
{
    [SerializeField] ColorMaterial[] colorMaterials;
    [SerializeField] HeadTypePrefab[] headTypePrefabs;

    public Material GetAvatarMaterial(ColorType color)
    {
        foreach (ColorMaterial colorMat in colorMaterials)
        {
            if (colorMat.Color == color)
            {
                return colorMat.Material;
            }
        }
        return null;
    }

    public GameObject GetAvatarHead(HeadType head)
    {
        foreach (HeadTypePrefab headPref in headTypePrefabs)
        {
            if (headPref.Head == head)
            {
                return headPref.Prefab;
            }
        }
        return null;
    }
}

public enum ColorType
{
    red, blue, green, yellow
}

public enum HeadType
{
    hat, afro, ponytail, horns
}

[Serializable]
public struct ColorMaterial
{
    [SerializeField] ColorType color;
    [SerializeField] Material material;

    public ColorType Color => color;
    public Material Material => material;
}

[Serializable]
public struct HeadTypePrefab
{
    [SerializeField] HeadType head;
    [SerializeField] GameObject prefab;

    public HeadType Head => head;
    public GameObject Prefab => prefab;
}
