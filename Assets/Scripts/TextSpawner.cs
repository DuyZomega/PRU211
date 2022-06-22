using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class TextSpawner : MonoBehaviour
{
    public static bool GameIsPause = false;

    [SerializeField]
    private GameObject

            pauseMenuUI,
            optionMenu;

    [SerializeField]




    private GameObject mainMenu;

    public string[] pool =
    {
        "apple",
        "color",
        "cat",
        "dog",
        "ant",
        "ship",
        "plane",
        "fish",
        "orange",
        "key",
        "house",
        "river",
        "earth",
        "money",
        "monkey"
    };
    public GameObject prefab;
    public float spawnInterval = 2;
    private float _counter;
    
    void Start()
    {
        // if (!pool.Any())
        // {
        //     throw new Exception("Pool is empty");
        // }

        _counter = 0;
    }

    void Update()
    {
        _counter += Time.deltaTime;
        Debug.Log(_counter);

        if (_counter >= spawnInterval)
        {
            _counter = 0;
            Debug.Log("Spawning square...");
            Spawn();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {

            if (GameIsPause)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    private GameObject Spawn()
    {
        var go = Instantiate(prefab);

        // Get a random spawn position
        var goSize = go.GetComponent<SpriteRenderer>().bounds.size;
        var spawnY = Screen.height + goSize.y;
        var spawnX = Random.Range(goSize.x, Screen.width - goSize.x);
        go.transform.position = Camera.main!.ScreenToWorldPoint(new Vector3(spawnX, spawnY, -Camera.main.transform.position.z));

        // Assign random text to object
        var textMesh = go.GetComponentInChildren<TextMeshPro>();
        textMesh.text = pool[Random.Range(0, pool.Length)];

        // Associate this object with the text
        gameObject.GetComponent<Controller>().Register(textMesh.text, go);
        
        return go;
    }

    public void Resume()
    {
        Cursor.lockState = CursorLockMode.None;
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPause = false;

    }

    void Pause()
    {

        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPause = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void Options()
    {
        if (GameIsPause)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            Time.timeScale = 0f;
            GameIsPause = true;

        }
        else
        {
            Resume();
        }
    }
}
