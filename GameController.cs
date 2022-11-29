using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameController : MonoBehaviour
{
    [SerializeField] Transform parent;
    public GameObject prefab;
    
    private float random;

    GameObject obstacle;

    public GameObject restartButton;

    public GameObject player;
    public GameObject[] canvasStartPage;

    [SerializeField] private MonoBehaviour playerController;

    public Text scoreTxt;

    public int _direction = -1;
    public float speed = 4f;

    float score = 0f;
    int seconds = 0;
    int maxScore = 0;

    Coroutine _coroutine;
    bool alive;
    private void Start()
    {
        alive = false;
        scoreTxt.text = "<size=32px>BEST:</size>" +
            PlayerPrefs.GetInt("score") + "\n<size=28px>NOW:0</size>";
        playerController.enabled = false;

    }

    void Update()
    {
        StartGame();
        MoveObst();
        Lose();
    }

    private IEnumerator Spawn()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position,
            transform.TransformDirection(Vector3.forward), out hit, 100))
        {
            
        }
        else
        {
            random = Random.Range(2, 4);
            for (int i = 1; i <= random; i++)
            {
                float checkRand = -11;
            label: float rand = Random.Range(-10f, 10f);
                rand = Mathf.Round(rand);
                if (rand != checkRand)
                {
                    checkRand = rand;
                }
                else
                {
                    goto label;
                }

                var pos = new Vector3(10.2f, 1, rand);

                obstacle = Instantiate(prefab, pos, Quaternion.identity, parent);
                
                Destroy(obstacle, 6);
            }
        }
        yield return null;
    }
    public void StartGame()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            playerController.enabled = true;
            alive = true;
            foreach (GameObject obj in canvasStartPage)
            {
                Destroy(obj);
            }

        }
        if (alive == true)
        {
            _coroutine = StartCoroutine(Spawn());
            Score();
        }
    }
    public void Score()
    {
        score += Time.deltaTime;
        seconds = (int)(score % 60);
    
        if(seconds >= PlayerPrefs.GetInt("score", maxScore))
        {
            maxScore = seconds;
            PlayerPrefs.SetInt("score", maxScore);
        }
        
        scoreTxt.text = "<size=32px>BEST:</size>" +
            PlayerPrefs.GetInt("score") + "\n<size=28px>NOW:</size>" + seconds;
    }

    public void MoveObst()
    {
        parent.transform.Translate(_direction * speed * Time.deltaTime, 0, 0);
    }
    public void Lose()
    {
        if (player.transform.position.y < -1)
        {
            StopCoroutine(_coroutine);
            alive = false;
            restartButton.SetActive(true);
            playerController.enabled = false;
            scoreTxt.text = "<size=32px>BEST:</size>" +
                PlayerPrefs.GetInt("score") + "\n<size=28px>NOW:</size>" + seconds;

        }
    }

}
