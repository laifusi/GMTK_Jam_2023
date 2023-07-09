using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Comportamiento_Perseguidor : MonoBehaviour
{
    public Enemigo enemy;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Character")
        {
            if(enemy.pilla)
            {
                if (!other.gameObject.GetComponent<Agentes>().pilla && !other.gameObject.GetComponent<Agentes>().pillado)
                {
                    bool iguales = false;
                    foreach (GameObject jugador in enemy.jugadores_proximos)
                    {
                        if (jugador.GetComponent<Agentes>().name_agent == other.gameObject.GetComponent<Agentes>().name_agent)
                        {
                            iguales = true;
                        }
                    }

                    if (!iguales)
                        enemy.AddPresaProxima(other.gameObject);
                }
            }
            else
            {
                if (other.gameObject.GetComponent<Agentes>().pilla)
                {
                    bool iguales = false;
                    foreach (GameObject jugador in enemy.jugadores_proximos)
                    {
                        if (jugador.GetComponent<Agentes>().name_agent == other.gameObject.GetComponent<Agentes>().name_agent)
                        {
                            iguales = true;
                        }
                    }

                    if (!iguales)
                        enemy.AddPresaProxima(other.gameObject);
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Character")
        {
            if(enemy.pilla)
            {
                if (!other.gameObject.GetComponent<Agentes>().pilla && !other.gameObject.GetComponent<Agentes>().pillado)
                {
                    if (enemy.jugadores_proximos.Count > 1)
                    {
                        enemy.jugadores_proximos.Remove(other.gameObject);
                    }
                }
            }
            else
            {
                if (other.gameObject.GetComponent<Agentes>().pilla)
                {
                    if (enemy.jugadores_proximos.Count > 1)
                    {
                        enemy.jugadores_proximos.Remove(other.gameObject);
                    }
                }
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Character")
        {
            if(enemy.pilla)
            {
                if (!other.gameObject.GetComponent<Agentes>().pilla && !other.gameObject.GetComponent<Agentes>().pillado)
                {
                    bool iguales = false;
                    foreach (GameObject jugador in enemy.jugadores_proximos)
                    {
                        if (jugador.GetComponent<Agentes>().name_agent == other.gameObject.GetComponent<Agentes>().name_agent)
                        {
                            iguales = true;
                        }
                    }

                    if (!iguales)
                        enemy.AddPresaProxima(other.gameObject);
                }
            }
            else
            {
                if (other.gameObject.GetComponent<Agentes>().pilla)
                {
                    bool iguales = false;
                    foreach (GameObject jugador in enemy.jugadores_proximos)
                    {
                        if (jugador.GetComponent<Agentes>().name_agent == other.gameObject.GetComponent<Agentes>().name_agent)
                        {
                            iguales = true;
                        }
                    }

                    if (!iguales)
                        enemy.AddPresaProxima(other.gameObject);
                }
            }
        }
    }
}
