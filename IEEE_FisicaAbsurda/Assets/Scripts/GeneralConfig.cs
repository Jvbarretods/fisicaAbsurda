using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralConfig : MonoBehaviour
{
    public bool menuAtivo; // Será mantido temporariamente, apenas para facilitar no desenvolvimento do jogo.
    [Space(20)]
    public GameObject menuPrincipal;
    public GameObject pause;
    

    private void Awake()
    {
        menuPrincipal.SetActive(true);
        menuPrincipal.SetActive(menuAtivo);// Será mantido temporariamente, apenas para facilitar no desenvolvimento do jogo.
    }
}
