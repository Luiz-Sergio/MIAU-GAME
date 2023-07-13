using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class Mision : MonoBehaviour
{
    public int maximo;
    public int progresso;
    public int recompensa;
    public int progressoAtual;
    public int tipoDeMissao;

    public abstract void Criado();
    public abstract string DescricaoDaMissao();
    public abstract void InicieCorrida();
    public abstract void Update();
    public abstract bool MissaoCompleta();
}

public class SingleRun : Mision
{
    public override void Criado()
    {
        tipoDeMissao = 0;
        int[] maxValues = { 60, 80, 100, 120 };
        int randomMaxValue = Random.Range(0, maxValues.Length);
        int[] rewards = { 5, 10, 15, 20 };
        recompensa = rewards[randomMaxValue];
        maximo = maxValues[randomMaxValue];
        progresso = 0;
    }

    public override string DescricaoDaMissao()
    {
        return "Corra " + maximo + "m em uma corrida";
    }

    public override void InicieCorrida()
    {
        progresso = 0;
    }

    public override void Update()
    {
        //if (player == null)
         //   return;

        progresso = (int)GameManager.Instance.Distance;
        Debug.Log("PROGRESSO>>>: " + progresso);
    }

    public override bool MissaoCompleta()
    {
        if (( progresso) >= maximo)
        {
            progresso = 0;
            return true;
        }
        else
        {
            return false;
        }

    }
         
}

public class TotalMeters : Mision
{
    public override void Criado()
    {
        tipoDeMissao = 1;
        int[] maxValues = { 120, 140, 180, 200 };
        int randomMaxValue = Random.Range(0, maxValues.Length);
        int[] rewards = { 20, 30, 40, 50 };
        recompensa = rewards[randomMaxValue];
        maximo = maxValues[randomMaxValue];
        progresso = 0;
        progressoAtual = 0;
    }

    public override string DescricaoDaMissao()
    {
        return "Corra " + maximo + "m no total";
    }

    public override void InicieCorrida()
    {
        progresso += progressoAtual;
    }

    public override void Update()
    {
  
        progressoAtual = (int)GameManager.Instance.Distance;
        Debug.Log("PROGRESSO: " + progresso);
    }

    public override bool MissaoCompleta()
    {
        if ((progresso + progressoAtual) >= maximo)
        {
            progresso = 0;
            progressoAtual = 0;
            return true;
        }
        else
        {
            return false;
        }

    }
}

public class StarsSingleRun : Mision
{
    public override void Criado()
    {
        tipoDeMissao = 2;
        int[] maxValues = { 60, 100, 120, 180, 220 };
        int randomMaxValue = Random.Range(0, maxValues.Length);
        int[] rewards = { 5, 10, 15, 20, 25 };
        recompensa = rewards[randomMaxValue];
        maximo = maxValues[randomMaxValue];
        progresso = 0;
    }

    public override string DescricaoDaMissao()
    {
        return "Colete " + maximo + " estrelas";
    }

    public override void InicieCorrida()
    {
        progresso = 0;
    }

    public override void Update()
    {

        progresso = GameManager.Instance.Score;
        Debug.Log("PROGRESSO: " + progresso);
    }
    public override bool MissaoCompleta()
    {
        if ((progresso + progressoAtual) >= maximo)
        {
            return true;
        }
        else
        {
            return false;
        }

    }
}
