using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UIElements;

public class AvatarCreationManager : MonoBehaviour
{
    [SerializeField] Avatar avatar;
    [SerializeField] TMP_Text colorText;
    [SerializeField] TMP_Text headText;
    [SerializeField] Button leftRotationButton;
    [SerializeField] float rotationSpeed = 0.5f;

    private bool rotationButtonPressed;
    private int rotationSide;

    private void Start()
    {
        RandomizeAvatar();
    }

    public void ChangeColor(bool next)
    {
        if(next)
        {
            switch(avatar.Color)
            {
                case ColorType.red:
                    avatar.AssignColor(ColorType.blue);
                    colorText.SetText("Blue");
                    break;
                case ColorType.blue:
                    avatar.AssignColor(ColorType.green);
                    colorText.SetText("Green");
                    break;
                case ColorType.green:
                    avatar.AssignColor(ColorType.yellow);
                    colorText.SetText("Yellow");
                    break;
                case ColorType.yellow:
                    avatar.AssignColor(ColorType.red);
                    colorText.SetText("Red");
                    break;
            }
        }
        else
        {
            switch (avatar.Color)
            {
                case ColorType.red:
                    avatar.AssignColor(ColorType.yellow);
                    colorText.SetText("Yellow");
                    break;
                case ColorType.blue:
                    avatar.AssignColor(ColorType.red);
                    colorText.SetText("Red");
                    break;
                case ColorType.green:
                    avatar.AssignColor(ColorType.blue);
                    colorText.SetText("Blue");
                    break;
                case ColorType.yellow:
                    avatar.AssignColor(ColorType.green);
                    colorText.SetText("Green");
                    break;
            }
        }
    }

    public void ChangeHead(bool next)
    {
        if(next)
        {
            switch (avatar.Head)
            {
                case HeadType.hat:
                    avatar.AssignHead(HeadType.afro);
                    headText.SetText("Afro");
                    break;
                case HeadType.afro:
                    avatar.AssignHead(HeadType.ponytail);
                    headText.SetText("Ponytail");
                    break;
                case HeadType.ponytail:
                    avatar.AssignHead(HeadType.horns);
                    headText.SetText("Horns");
                    break;
                case HeadType.horns:
                    avatar.AssignHead(HeadType.hat);
                    headText.SetText("Hat");
                    break;
            }
        }
        else
        {
            switch (avatar.Head)
            {
                case HeadType.hat:
                    avatar.AssignHead(HeadType.horns);
                    headText.SetText("Horns");
                    break;
                case HeadType.afro:
                    avatar.AssignHead(HeadType.hat);
                    headText.SetText("Hat");
                    break;
                case HeadType.ponytail:
                    avatar.AssignHead(HeadType.afro);
                    headText.SetText("Afro");
                    break;
                case HeadType.horns:
                    avatar.AssignHead(HeadType.ponytail);
                    headText.SetText("Ponytail");
                    break;
            }
        }
    }

    public void ChangeName(string newName)
    {
        avatar.AssignName(newName);
    }

    public void RandomizeAvatar()
    {
        int randomColorId = Random.Range(0, 4);
        int randomHeadId = Random.Range(0, 4);

        switch (randomColorId)
        {
            case 0:
                avatar.AssignColor(ColorType.red);
                colorText.SetText("Red");
                break;
            case 1:
                avatar.AssignColor(ColorType.blue);
                colorText.SetText("Blue");
                break;
            case 2:
                avatar.AssignColor(ColorType.green);
                colorText.SetText("Green");
                break;
            case 3:
                avatar.AssignColor(ColorType.yellow);
                colorText.SetText("Yellow");
                break;
        }

        switch (randomHeadId)
        {
            case 0:
                avatar.AssignHead(HeadType.hat);
                headText.SetText("Hat");
                break;
            case 1:
                avatar.AssignHead(HeadType.afro);
                headText.SetText("Afro");
                break;
            case 2:
                avatar.AssignHead(HeadType.ponytail);
                headText.SetText("Ponytail");
                break;
            case 3:
                avatar.AssignHead(HeadType.horns);
                headText.SetText("Horns");
                break;
        }
    }

    #region Avatar Rotation
    private void Update()
    {
        if (rotationButtonPressed)
        {
            RotateAvatar();
        }
    }

    public void InitializeRotation(int rotationSide)
    {
        rotationButtonPressed = true;
        this.rotationSide = rotationSide;
    }

    public void EndRotation()
    {
        rotationButtonPressed = false;
    }

    public void RotateAvatar()
    {
        avatar.transform.Rotate(Vector3.up, rotationSide*rotationSpeed);
    }
    #endregion
}
