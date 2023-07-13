using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Menu : MonoBehaviour
{
    public Text TextoDasEstrelas;
    public Text[] descricao, recompensa, progresso;
    public GameObject[] botaoDeRecompensa;
    // Start is called before the first frame update
    void Start()
    {
        TextoDasEstrelas.text = GameManager.Instance.blackStars.ToString();
        SetMissao(0);
        SetMissao(1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame()
    {
        GameManager.StartGame();
    }

    public void SetMissao(int i)
    {
       
            Mision missao = GameManager.Instance.GetMissao(i);
            descricao[i].text = missao.DescricaoDaMissao();
            recompensa[i].text = "Premio: " + missao.recompensa;
            if (missao.progresso + missao.progressoAtual > 0)
            {
                progresso[i].text = missao.progresso + missao.progressoAtual  + " / " + missao.maximo;
            }
            else
            {
                progresso[i].text = missao.progresso + missao.progressoAtual + " / " + missao.maximo;
            }
            
            if (missao.MissaoCompleta())
            {
                botaoDeRecompensa[i].SetActive(true);
            }
        
    }

    public void GetRecompensa(int i)
    {
        GameManager.Instance.blackStars += GameManager.Instance.GetMissao(i).recompensa;
        AtualizeEstrelas(GameManager.Instance.blackStars);
        botaoDeRecompensa[i].SetActive(false);
        GameManager.Instance.gerarMissao(i); 
      
    }

    public void AtualizeEstrelas(int estrelas)
    {
        TextoDasEstrelas.text = estrelas.ToString();
    }

    public void EndGame()
    {
        Application.Quit();
    }

}
