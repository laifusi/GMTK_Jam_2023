using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jugador : Agentes
{
    [SerializeField] private float horizontalMove;
    [SerializeField] private float verticalMove;
    [SerializeField] private CharacterController player;

    [SerializeField] public GameUIManager ui_info;

    void Start()
    {
        player = GetComponent<CharacterController>();
        timer_perseguido = 5;
        score = 0;
        pillado = false;

        ui_info = GameObject.FindWithTag("ControllerGame").GetComponent<GameUIManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!pilla)
        {
            UpdateTimerPerseguido();
        }

        horizontalMove = Input.GetAxis("Horizontal");
        verticalMove = Input.GetAxis("Vertical");

        player.Move(new Vector3(horizontalMove, 0, verticalMove) * speed * Time.deltaTime);
    }
}
