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

    [SerializeField] AvatarCharacteristics avatarCharacteristics;
    [SerializeField] Transform headParent;
    [SerializeField] MeshRenderer meshRenderer;

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

    public void AssignColor(int color)
    {
        this.color = color;
        switch(color)
        {
            case 0:
                meshRenderer.material = avatarCharacteristics.GetAvatarMaterial(ColorType.red);
                break;
            case 1:
                meshRenderer.material = avatarCharacteristics.GetAvatarMaterial(ColorType.green);
                break;
            case 2:
                meshRenderer.material = avatarCharacteristics.GetAvatarMaterial(ColorType.blue);
                break;
            case 3:
                meshRenderer.material = avatarCharacteristics.GetAvatarMaterial(ColorType.yellow);
                break;
        }
    }

    public void AssignComplemento(int head)
    {
        complemento = head;

        foreach (Transform child in headParent)
            Destroy(child.gameObject);

        GameObject prefab;
        switch (head)
        {
            case 4:
                prefab = avatarCharacteristics.GetAvatarHead(HeadType.hat);
                break;
            case 5:
                prefab = avatarCharacteristics.GetAvatarHead(HeadType.afro);
                break;
            case 6:
                prefab = avatarCharacteristics.GetAvatarHead(HeadType.ponytail);
                break;
            case 7:
                prefab = avatarCharacteristics.GetAvatarHead(HeadType.horns);
                break;
            default:
                prefab = avatarCharacteristics.GetAvatarHead(HeadType.hat);
                break;
        }
        
        Instantiate(prefab, headParent);
    }
}
