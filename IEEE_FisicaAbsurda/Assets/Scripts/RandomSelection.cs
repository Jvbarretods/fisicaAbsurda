using System.Collections.Generic;
using UnityEngine;

public class RandomSelection : MonoBehaviour
{
    static public List<int> ordemSalas = new List<int>(); //Lista que irá armazenar a ordem das Salas
    private List<int> ordemTiros = new List<int>(); //Lista que irá armazenar a ordem dos Tiros

    public void ListaRandomSemRepeticao(List<int> lista, int numLimite) //Todos os números entre 1 e o "numLimite" serão salvos aleatóriamente na Lista indicada no parâmetro
    {
        for (int i = 0; i < numLimite; i++)
        {
        gerar:
            int n = ObterNumeroAleatorio(numLimite+1);

            if (ExisteNumero(lista, n))
                goto gerar;
            else
            {
                lista.Add(n);
                Debug.Log("Número gerado: " + n.ToString());
            }
        }
    }

    private bool ExisteNumero(List<int> lista, int numero) //Compara o número com os elementos da lista, caso o número já esteja na lista ele retorna Verdadeiro
    {
        for (int i = 0; i < lista.Count; i++)
        {
            if (lista[i] == numero)
                return true;
        }

        return false;
    }

    private int ObterNumeroAleatorio(int i)
    {
        System.Random randNum = new System.Random();
        return randNum.Next(1, i); //Escolhe aleatoriamente números de 1 até "i"
    }

    protected void LimparLista(List<int> lista)//Removerá todos os elementos da Lista indicada no parâmetro 
    {
        for(int i = lista.Count; i > 0 ; i--)
        {
            lista.RemoveAt(0);
            Debug.Log("Tamanho: " + lista.Count);
        }
    }
}
