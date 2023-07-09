using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemigo : Agentes
{

    [SerializeField] private NavMeshAgent agent = null;


    
    public Comportamiento_Perseguidor cp;

    [SerializeField] public Transform target = null;

    [SerializeField] public List<GameObject> jugadores_proximos;

    //Elementos presa
    [SerializeField] private float displacementDist = 7f;

    [SerializeField] private float angle_away;


    // Start is called before the first frame update
    void Start()
    {
        timer_perseguido = 5;
        score = 0;
        pillado = false;
        angle_away = Random.Range(0, 359);
        agent = GetComponent<NavMeshAgent>();
        agent.speed = speed;

    }

    // Update is called once per frame
    void Update()
    {
        if (!pilla)
        {
            UpdateTimerPerseguido();
            ComportamientoPresa();
        }

        else
        {
            ComportamientoDepredador();
        }
    }





    private void ComportamientoDepredador()
    {
        if (target)
        {
            agent.speed = speed;
            CompareJugadoresProximas();
            MoveToTarget();
        }
        else
        {
            float distancia_agente = 99999f;
            if(jugadores_proximos.Count > 0)
            {
                CompareJugadoresProximas();
            }

            else
            {

                target = null;

                GameObject[] jugadores = GameObject.FindGameObjectsWithTag("Character");

                if (jugadores.Length > 0)
                {
                    foreach (GameObject jugador in jugadores)
                    {
                        if (!jugador.GetComponent<Agentes>().pilla && !jugador.GetComponent<Agentes>().pillado)
                        {
                            float dist_actual = CalculoDistancia(jugador.transform);

                            if (dist_actual < distancia_agente)
                            {
                                agent.speed = speed;
                                target = jugador.transform;
                                distancia_agente = dist_actual;
                            }
                        }
                    }

                    if(target != null)
                        AddPresaProxima(target.gameObject);
                    else
                    {
                        agent.isStopped = true;
                        agent.speed = 0;
                    }
                }

                else
                {
                    agent.speed = 0;
                    target = null;
                }
            }
        }
    }

    private void MoveToTarget()
    {
        if(target.gameObject.GetComponent<Agentes>().pillado)
        {
            RemovePresaProxima(target.gameObject);
            target = null;
        }
        else
        {
            agent.SetDestination(target.position);
            agent.isStopped = false;
        }
    }

    private float CalculoDistancia(Transform t_agent)
    {
        return Vector3.Distance(this.gameObject.transform.position, t_agent.position);
    }

    public void AddPresaProxima(GameObject new_prey)
    {
        jugadores_proximos.Add(new_prey);
    }

    public void RemovePresaProxima(GameObject prey_rem)
    {
        jugadores_proximos.Remove(prey_rem);
    }

    public void CompareJugadoresProximas()
    {
        float dist_menor = 9999f;
        GameObject new_target = null;
        foreach(GameObject presa in jugadores_proximos)
        {
            float dist_actual = CalculoDistancia(presa.transform);
            Debug.Log("Comparamos la distancia de " + dist_actual + " con la distancia original " + dist_menor);
            if (dist_actual < dist_menor)
            {
                dist_menor = dist_actual;
                new_target = presa;
            }
        }

        if(new_target != null)
        {
            target = new_target.transform;
        }
    }





    private void ComportamientoPresa()
    {
        if(!this.pillado)
        {

            if (target)
            {
                Debug.Log(this.name_agent + " huye de " + target.gameObject.GetComponent<Agentes>().name_agent);

                GameObject target_anterior = target.gameObject;

                CompareJugadoresProximas();

                GameObject target_actual = target.gameObject;

                if (target_anterior.GetComponent<Agentes>().name_agent != target_actual.GetComponent<Agentes>().name_agent)
                    angle_away = Random.Range(0, 359);

                Vector3 normDir = (target.position - transform.position).normalized;

                normDir = Quaternion.AngleAxis(angle_away, Vector3.up) * normDir;

                MoveToPpos(transform.position - (normDir * displacementDist));
            }


            else
            {

                float distancia_agente = 99999f;
                if (jugadores_proximos.Count > 0)
                {
                    CompareJugadoresProximas();
                }

                else
                {
                    target = null;

                    GameObject[] jugadores = GameObject.FindGameObjectsWithTag("Character");

                    if (jugadores.Length > 0)
                    {
                        foreach (GameObject jugador in jugadores)
                        {
                            if (jugador.GetComponent<Agentes>().pilla)
                            {
                                float dist_actual = CalculoDistancia(jugador.transform);

                                if (dist_actual < distancia_agente)
                                {
                                    agent.speed = speed;
                                    target = jugador.transform;
                                    distancia_agente = dist_actual;
                                }
                            }
                        }

                        if (target != null)
                            AddPresaProxima(target.gameObject);
                        else
                        {
                            agent.isStopped = true;
                            agent.speed = 0;
                        }
                    }

                    else
                    {
                        agent.speed = 0;
                        target = null;
                    }
                }
            }
        }

        else
        {
            agent.speed = 0;
            agent.isStopped = true;
            jugadores_proximos.Clear();
        }
    }

    private void MoveToPpos(Vector3 pos)
    {
        Debug.Log("Se mueve a la posición");

        agent.SetDestination(pos);
        agent.isStopped = false;
    }
}
