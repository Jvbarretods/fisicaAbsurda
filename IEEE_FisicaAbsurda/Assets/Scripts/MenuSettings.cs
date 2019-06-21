using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuSettings : MonoBehaviour
{
    public GameObject menuInicial; //Irá armazenar o subMenu "Menu Inicial"
    public GameObject menuConfig; //Irá armazenar o subMenu "Configurações"

    [Space(20)] //Cria um espaço entre as variávei na interface do Unity. É apenas estético.
    public TMPro.TMP_Dropdown resolucoes; //Irá armazenar o Dropdown de Resoluções
    public Toggle modoJanela;
    public Scrollbar barraVolume;

    private Resolution[] resolucoesSuportadas; //Irá armazenar o Array das resoluções suportadas pelo monitor
    private int modoJanelaAtivo; //Caso o modo janela fique ativo o valor será 1 senão será 0.


    //====================BOTÕES====================
    public void Jogar()
    {
        MudarOrdem();
        this.gameObject.SetActive(false);
    }

    public void Configuracao(bool ativo)
    {
        menuInicial.SetActive(!ativo);
        menuConfig.SetActive(ativo);        
    }

    public void Sair()
    {
        Application.Quit();
    }

    public void Salvar()
    {
        SalvarPreferencias(); //Salvas as alterações
        Configuracao(false); //Voltará para o Menu Inicial
    }
    //==============================================

    private void Awake()
    {
        ChecarResolucoes();
        ResgatarPreferencias();
        MudarOrdem(); //Gera a ordem aleatória das salas
        menuConfig.SetActive(false); //Deixa o Menu de Configurações desativado na inicialização do jogo
    }

    private void MudarOrdem() //Altera a ordem do surgimento das salas e dos Tiros
    {
        RandomSelection rs = gameObject.AddComponent<RandomSelection>() as RandomSelection; //Instanciar a classe "RandomSelection" em um objeto "RandomSelection" chamado rs
        rs.ListaRandomSemRepeticao(RandomSelection.ordemSalas, 4);//Cria a ordem em que as salas irão aparecer
        rs.ListaRandomSemRepeticao(RandomSelection.ordemTiros, 4);//Cria a ordem em que as tiros irão aparecer
    }
    
    private void ChecarResolucoes()
    {
        resolucoesSuportadas = Screen.resolutions; //Armazena as resoluçõoes suportadas pelo monitor

        //======================Inversor da Resolução Suportada========================
        Resolution[] inversor;
        inversor = Screen.resolutions;
        int contador = resolucoesSuportadas.Length - 1;

        for (int i = 0; i < resolucoesSuportadas.Length; i++) // Tem como objetivo inverter o vetor de Resoluções Suportadas. Motivo: estética kkkk
        {
            resolucoesSuportadas[i].width = inversor[contador].width;
            resolucoesSuportadas[i].height = inversor[contador].height;
            contador--;
        }
        //=============================================================================

        resolucoes.options.Clear(); //Limpa as opções do Dropdown

        for (int i = 0; i < resolucoesSuportadas.Length; i++) //Repete até que todas as resoluções sejam adicionadas no Dropdown
        {
            resolucoes.options.Add(new TMPro.TMP_Dropdown.OptionData() { text = resolucoesSuportadas[i].width + "X" + resolucoesSuportadas[i].height }); //Adiciona as resoluções suportadas no Dropdown TMP
        }
    }

    private void SalvarPreferencias()
    {
        if (modoJanela.isOn)
        {
            modoJanelaAtivo = 1;
        }
        else
        {
            modoJanelaAtivo = 0;
        }
        PlayerPrefs.SetInt("Resolucao", resolucoes.value); //Armazena o valor da resolução, para que possa ser resgatada quando o jogo for aberto novamente
        PlayerPrefs.SetInt("ModoJanela", modoJanelaAtivo); //Armazena o valor do modo janela
        PlayerPrefs.SetFloat("Volume", barraVolume.value); //Armazena o valor da barra de volume

        Debug.Log("resolucoes.value == " + resolucoes.value);
        Debug.Log("resolucoes.value == " + PlayerPrefs.GetInt("Resolucao"));

        Screen.SetResolution(resolucoesSuportadas[resolucoes.value].width, resolucoesSuportadas[resolucoes.value].height, !modoJanela.isOn); //Altera a Resolução da tela e o modo janela
    }

    private void ResgatarPreferencias()
    {
        if (PlayerPrefs.GetInt("ModoJanela") == 1) //Caso o save do ModoJanela seja 1
        {
            modoJanelaAtivo = 1; //tornará o modoJanelaAtivo = 1;
            modoJanela.isOn = true; //E manterá o Toggle do modoJanela Verdadeiro 
        }
        else
        {
            modoJanelaAtivo = 0; //tornará o modoJanelaAtivo = 0;
            modoJanela.isOn = false; //E manterá o Toggle do modoJanela Falso
        }
        barraVolume.value = PlayerPrefs.GetFloat("Volume"); //Tornará o valor da barra de volume igual o que foi salvo
        resolucoes.value = PlayerPrefs.GetInt("Resolucao");
        Debug.Log("resolucoes.value == " + PlayerPrefs.GetInt("Resolucao"));
        Screen.SetResolution(resolucoesSuportadas[PlayerPrefs.GetInt("Resolucao")].width, resolucoesSuportadas[PlayerPrefs.GetInt("Resolucao")].height, !modoJanela.isOn); //Altera a resolução da tela utilizando os dados que foram salvos.
    }

    void Update()
    {
        AudioListener.volume = barraVolume.value; //Altera o volume do jogo
    }
}
