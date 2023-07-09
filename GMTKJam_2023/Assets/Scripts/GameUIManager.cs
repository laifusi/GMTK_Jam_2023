using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameUIManager : MonoBehaviour
{

    [SerializeField] public TMP_Text TimerPersecutor, GameTimer, PointsInfo, RolPlayerInfo;
    [SerializeField] public Sprite red, blue, green, yellow, afro, hat, bunny, punky;
    [SerializeField] public Image image_persecutor, RolPlayerImage;
    [SerializeField] public TMP_Text[] text_array;
    [SerializeField] public GameObject tabla_ranking;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateTimerGameInfo(string minutes, string seconds)
    {
        string strTimerGame = minutes + ": " + seconds;
        GameTimer.text = strTimerGame;
    }

    public void UpdateSpritePersecutor(int persecutor)
    {
        switch(persecutor)
        {
            case 0:
                image_persecutor.sprite = red;
                break;
            case 1:
                image_persecutor.sprite = green;
                break;
            case 2:
                image_persecutor.sprite = blue;
                break;
            case 3:
                image_persecutor.sprite = yellow;
                break;
            case 4:
                image_persecutor.sprite = hat;
                break;
            case 5:
                image_persecutor.sprite = afro;
                break;
            case 6:
                image_persecutor.sprite = punky;
                break;
            case 7:
                image_persecutor.sprite = bunny;
                break;
        }
    }

    public void UpdtateTimerTag(string seconds)
    {
        string strTimerTag = seconds + " s";
        TimerPersecutor.text = strTimerTag;
    }

    public void UpdatePointsInfo(string score)
    {
        string strPointsInfo = "POINTS: " + score;
        PointsInfo.text = strPointsInfo;
    }

    public void UpdateRolPlayerInfo(string rol)
    {
        switch(rol)
        {
            case "PURSUED":
                RolPlayerImage.color = Color.green;
                break;

            case "CHASER":
                RolPlayerImage.color = Color.red;
                break;

            case "CAUGHT":
                RolPlayerImage.color = Color.yellow;
                break;
        }
        string strRolPlayer = rol;
        RolPlayerInfo.text = strRolPlayer;
    }

    public void FillRanking(List<Agentes> winners)
    {
        if(!tabla_ranking.activeSelf)
        {
            tabla_ranking.SetActive(true);
        }

        int i = 0;
        int posicion = 1;
        int last_score = winners[0].score;
        foreach(Agentes w in winners)
        {
            if (last_score > w.score )
                posicion++;
            text_array[i].text = posicion + "º";
            i++;
            text_array[i].text = w.name_agent;
            if(w.GetComponent<Jugador>())
            {
                text_array[i].color = Color.yellow;
            }

            i++;
            text_array[i].text = w.score.ToString();
            last_score = w.score;
            i++;


        }
    }
}
