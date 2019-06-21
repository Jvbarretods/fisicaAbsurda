using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomChange : RandomSelection
{
    public GameObject porta;
    public GameObject spawningLobby, spawningSala1, spawningSala2, spawningSala3, spawningSala4; //Recebem o Objeto que marcará a posição onde o player irá surgir no teletrasporte 

    static public int proximaSala = 0;
    static public int salasConcluidas = 0;

    static private int k = -1;

    public void TrocaDeSala(Collider2D player)//Realiza o teletrasporte para a próxima sala por meio da lista de ordem randômica
    {
        if(k == -1)
        {
            k += 1;
        }
        else if(k < 4)
        {
            proximaSala = ordemSalas[k]; //A "ordemSalas" retorna numeros entre 1 e 4, esses números corresponde as 4 salas do castelo que possuem dificuldade.   
            k += 1;
        }

        switch (proximaSala)
        {
            case 0:
                player.transform.position = spawningLobby.transform.position;
                break;
            case 1:
                player.transform.position = spawningSala1.transform.position;
                salasConcluidas += 1;
                break;
            case 2:
                player.transform.position = spawningSala2.transform.position;
                salasConcluidas += 1;
                break;
            case 3:
                player.transform.position = spawningSala3.transform.position;
                salasConcluidas += 1;
                break;
            case 4:
                player.transform.position = spawningSala4.transform.position;
                salasConcluidas += 1;
                break;
            default:
                Debug.Log("Sala Inválida");
                break;
        }

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        { 
            TrocaDeSala(other);
        }
    }
}
