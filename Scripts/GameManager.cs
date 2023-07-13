using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
//using UnityEditor.Experimental.GraphView;

public class GameManager : MonoBehaviour
{
    [SerializeField] private AudioSource starCollectSound;
    public int Score;
    public float Distance;
    public static GameManager Instance;
    private Text TextOfScore;
    private Text TextOfDistance;
    private Transform transformOfPlayer;
    public float playerSpeed;
    private float after=0;
    private float before=0;
    public static bool isGameStarted;
    public int blackStars;

   


    private Mision[] missoes;

    private void Awake()
    {
        if (Instance == null)
        {
            Debug.Log("entrei");
            Instance = this;
            blackStars = 0;
        }
        else if (Instance != this) // remove the semicolon here
        {
            Debug.Log("me destrui");
            Destroy(gameObject);
        }

        missoes = new Mision[2];

        for(int i = 0; i < missoes.Length; i++)
        {
            GameObject novaMissao = new GameObject("Mission "+ i);
            novaMissao.transform.SetParent(transform);
            int RandomType = Random.Range(0, 2);
            if(i== 0)
            {
                missoes[0] = novaMissao.AddComponent<SingleRun>();
                missoes[0].Criado();
            }
            else if(i== 1)
            {
                missoes[1] = novaMissao.AddComponent<StarsSingleRun>();
                missoes[1].Criado();
            }/*
            else if(RandomType == 2)
            {
                missoes[i] = novaMissao.AddComponent<SingleRun>();
                missoes[i].Criado();
            }*/
        }

        DontDestroyOnLoad(gameObject);
    }
    // Start is called before the first frame update
    void Start()
    {
        isGameStarted = false;
        //Distance = 1;
        
        
        if (transformOfPlayer == null)
        {
            GameObject otherSceneObject = GameObject.Find("PLAYER");
            if (otherSceneObject != null)
            {
                transformOfPlayer = otherSceneObject.GetComponentInChildren<Transform>();
            }
        }
    }

    public void ChangeScore()
    {
        if (TextOfScore == null)
        {
            GameObject otherSceneObject = GameObject.Find("ScoreText");
            if (otherSceneObject != null)
            {
                TextOfScore = otherSceneObject.GetComponentInChildren<Text>();
            }
        }
        
        Score++;
        TextOfScore.text = "Stars: " + Score;
        //
    }
    public void playStarSound()
    {
        starCollectSound.Play();
    }
   public void ChangeDistance()
    {
        if (TextOfDistance == null)
        {
            Debug.Log("Estou procurando text of distance");
            GameObject otherSceneObject = GameObject.Find("DistanceText");
            if (otherSceneObject != null)
            {
                Debug.Log("encontrei text of distance");
                TextOfDistance = otherSceneObject.GetComponentInChildren<Text>();
            }
        }
        if (TextOfDistance == null)
        {
            Debug.Log("ESTOU NULOOOO!");
        }
        //if(isGameStarted)
        //{
            TextOfDistance.text = "Distance: " + (int)++Distance;
        //}
        
    }
    // Update is called once per frame
    void Update()
    {
        //verificar se a cena é do game IF
        if (Input.anyKey && !Input.GetMouseButton(0) && !Input.GetMouseButtonUp(0) && !Input.GetMouseButtonDown(0))
        {
            isGameStarted = true;
        }

    }

    public static void StartGame()
    {
        //Distance = 0;
        //Score = 0;
        Instance.StartMissao();
        Instance.Distance = 0;
        Instance.Score = 0;
        isGameStarted = false;
        SceneManager.LoadScene("Game");
    }

    public void Menu()
    {
        isGameStarted=false;
        //Distance = 0;
        //Score = 0;
        Invoke("callMenu",1f);
        
    }

    public void gerarMissao(int i)
    {
        Destroy(missoes[i].gameObject);
        GameObject novaMissao = new GameObject("Mission " + i);
        novaMissao.transform.SetParent(transform);
        //int RandomType = Random.Range(0, 2);
        if (i==0)
        {
           
                missoes[0] = novaMissao.AddComponent<SingleRun>();
                missoes[0].Criado();
           
            

            

            
            // didnt solve// missoes[i].progresso = 0;//line trying to solve bug, if doesnt solve delete
        }
        else if (i==1)
        {
           
                missoes[1] = novaMissao.AddComponent<StarsSingleRun>();
                missoes[1].Criado();
            
        }
        /*else if (RandomType == 2)
        {
            missoes[i] = novaMissao.AddComponent<SingleRun>();
            missoes[i].Criado();
            //didnt solve // missoes[i].progresso = 0;//line trying to solve bug, if doesnt solve delete
        }*/
        FindObjectOfType<Menu>().SetMissao(i);
    }

    public void callMenu()
    {
        //GameManager.Instance.Distance = 0;//trying to solve the reward bug, if doesnt solve delete
        SceneManager.LoadScene("Menu");
    }

    public Mision GetMissao(int index)
    {
        return missoes[index];
    }

    public void StartMissao()
    {
        for(int i = 0; i < 2; i++)
        {
            missoes[i].InicieCorrida(); 
        }
    }


}
