using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Agentes : MonoBehaviour
{

    [SerializeField] public float speed;

    [SerializeField] public bool pilla;

    [SerializeField] private int color; //0 = rojo; 1 = verde; 2 = azul; 3 = amarillo;

    [SerializeField] private int complemento; //4 = sombrero; 5 = afro; 6 = coleta; 7 = cuernos;

    [SerializeField] public int score;

    [SerializeField] public string name_agent;

    [SerializeField] public bool pillado;

    [SerializeField] protected float timer_perseguido;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdatePillar()
    {
        if (GameObject.FindWithTag("ControllerGame").GetComponent<GameController>())
        {
            GameController gc = GameObject.FindWithTag("ControllerGame").GetComponent<GameController>();

            if(pillado)
                ResetPilladoState();

            if (gc.predator_element == color || gc.predator_element == complemento)
            {
                timer_perseguido = 5;
                pilla = true;
                speed = 3.5f;
            }

            else
            {
                pilla = false;
                speed = 3f;
            }
            
            if (this.GetComponent<Enemigo>())
            {
                this.GetComponent<Enemigo>().target = null;

                this.GetComponent<Enemigo>().jugadores_proximos.Clear();
            }
        }
    }

    public void UpdateTimerPerseguido()
    {
        if(!pillado)
        {
            timer_perseguido -= Time.deltaTime;
            if (timer_perseguido <= 0)
            {
                timer_perseguido = 5;
                score++;
            }
        }
    }

    public void PilladoState()
    {
        pillado = true;
        timer_perseguido = 5;
        speed = 0;

        if(this.GetComponent<Enemigo>())
        {
            this.GetComponent<Enemigo>().target = null;
        }

        //Poner animación de pillado
    }

    public void ResetPilladoState()
    {
        pillado = false;

        //Animación se levanta
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.gameObject.tag == "Character")
        {

            if (this.pilla)
            {

                if (!collision.collider.gameObject.GetComponent<Agentes>().pilla && !collision.collider.gameObject.GetComponent<Agentes>().pillado)
                {

                    this.score++;

                    collision.collider.gameObject.GetComponent<Agentes>().PilladoState();
                }
            }

            /*else
            {
                if (collision.collider.gameObject.GetComponent<Agentes>().pilla)
                {
                    this.PilladoState();
                }
            }*/
        }
    }
}
