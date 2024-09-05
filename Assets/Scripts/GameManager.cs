using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

    public MonsterSpawner monsterSpawner;
    public MonsterDataLoader monsterDataLoader;
    public GameObject player;

    public GameObject startBtn;
    public GameObject restartBtn;

    private bool isGameActive = false;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        InitializeGame();

    }

    void InitializeGame()
    {
        isGameActive = false;

        monsterDataLoader.LoadMonsterData();

        monsterSpawner.InitializeSpawner(monsterDataLoader.Monster);
    }

    public void StartGame()
    {
        isGameActive = true;
        startBtn.SetActive(false);
        restartBtn.SetActive(true);

        monsterSpawner.StartSpawning();
    }

    public void RestartGame()
    {
        startBtn.SetActive(true);
        restartBtn.SetActive(false);
        InitializeGame();
    }
}
