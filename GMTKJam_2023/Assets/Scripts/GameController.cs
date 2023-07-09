using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private bool Game_finished;

    [SerializeField] private float Timer_Game;
    private int minutes, seconds;

    [SerializeField] private float Timer_Cambio_Rol;

    [SerializeField] public int predator_element; //0 = rojo; 1 = verde; 2 = azul; 3 = amarillo; 4 = sombrero; 5 = afro; 6 = coleta; 7 = cuernos;

    //[SerializeField] private GameObject[] jugadores;
    [SerializeField] private Agentes prefabPersonaje;

    [SerializeField] private Jugador playerCharacter;
    [SerializeField] int totalExtraAvatars;

    [SerializeField] float planeX, planeZ;

    List<Agentes> jugadores = new List<Agentes>();

    // Start is called before the first frame update
    void Start()
    {
        InitializePlayer();
        InitializeEnemies();

        Game_finished = false;
        Timer_Game = 240;
        Timer_Cambio_Rol = 30;
        predator_element = UpdatePredator();
        /*if (GameObject.FindGameObjectWithTag("Character"))
        {
            jugadores = GameObject.FindGameObjectsWithTag("Character");
        }*/
        UpdateStatePlayers();
    }

    private void InitializePlayer()
    {
        playerCharacter.AssignColor(TranslateColor(PlayerPrefs.GetString("AvatarColor")));
        playerCharacter.AssignComplemento(TranslateHead(PlayerPrefs.GetString("AvatarHead")));
    }

    private void InitializeEnemies()
    {
        float avatarsPerType = totalExtraAvatars / 4;
        int colorId = 0;
        int headId = 4;
        int colorCount = 0;
        for (int i = 0; i < totalExtraAvatars; i++)
        {
            float randomX = Random.Range(-planeX, planeX);
            float randomZ = Random.Range(-planeZ, planeZ);
            Agentes avatar = Instantiate(prefabPersonaje, new Vector3(randomX, 2, randomZ), Quaternion.identity);
            colorCount++;

            if(colorCount >= avatarsPerType && colorId < 3)
            {
                colorId++;
                colorCount = 0;
            }

            avatar.AssignColor(colorId);
            avatar.AssignComplemento(headId);
            jugadores.Add(avatar);

            if(headId >= 7)
            {
                headId = 4;
            }
            else
            {
                headId++;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(!Game_finished)
        {
            UpdateTimerGame();
            UpdateTimerTag();
        }
    }

    public int UpdatePredator()
    {
        int predator_actual = predator_element;
        int nuevo_predator = Random.Range(0, 7);
        if (predator_actual == nuevo_predator)
        {
            switch (Random.Range(0, 1))
            {
                case 0:
                    nuevo_predator = predator_actual++;
                    break;

                case 1:
                    nuevo_predator = predator_actual--;
                    break;
            }
        }
        return nuevo_predator;
    }

    void UpdateTimerGame()
    {
        Timer_Game -= Time.deltaTime;

        if (Timer_Game <= 0)
        {
            Timer_Game = 0;
            Game_finished = true;
        }
        
        minutes = (int)(Timer_Game / 60f);

        seconds = (int)(Timer_Game - minutes * 60f);

        
    }

    void UpdateTimerTag()
    {
        Timer_Cambio_Rol -= Time.deltaTime;

        if (Timer_Cambio_Rol <= 0)
        {
            Timer_Cambio_Rol = Random.Range(10f, 25f);
            predator_element = UpdatePredator();
            UpdateStatePlayers();
        }
    }

    void UpdateStatePlayers()
    {
        foreach(Agentes jugador in jugadores)
        {
            jugador.UpdatePillar();
        }
    }

    int TranslateColor(string colorName)
    {
        switch(colorName)
        {
            case "red":
                return 0;
            case "green":
                return 1;
            case "blue":
                return 2;
            case "yellow":
                return 3;
        }

        return 0;
    }

    int TranslateHead(string headName)
    {
        switch(headName)
        {
            case "hat":
                return 4;
            case "afro":
                return 5;
            case "ponytail":
                return 6;
            case "horns":
                return 7;
        }

        return 0;
    }

}
