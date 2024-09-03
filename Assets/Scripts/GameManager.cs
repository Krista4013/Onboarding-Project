using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

    public Scroller scroller;
    public MonsterSpawner monsterSpawner;
    public MonsterDataLoader monsterDataLoader;
    public GameObject player;

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
        player.SetActive(false);

        scroller.enabled = false;

        monsterDataLoader.LoadMonsterData();

        monsterSpawner.InitializeSpawner(monsterDataLoader.Monster);
    }

    public void StartGame()
    {
        isGameActive = true;
        player.SetActive(true);

        scroller.enabled = true;

        monsterSpawner.StartSpawning();
    }

    public void EndGame()
    {
        isGameActive = false;

        scroller.enabled = false;

        monsterSpawner.StopSpawning();
    }

    public void GameOver()
    {
        EndGame();
    }

    public void RestartGame()
    {
        InitializeGame();
        StartGame();
    }
}
