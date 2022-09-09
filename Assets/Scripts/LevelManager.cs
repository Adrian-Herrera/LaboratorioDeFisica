using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using System.Threading.Tasks;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;
    [SerializeField] private GameObject _loaderCanvas;
    [SerializeField] private Image _progressBar;
    private float _target;
    // global variables
    public int temaId;
    public int quizId;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        Application.targetFrameRate = 30;
    }

    public async void LoadScene(string sceneName)
    {
        _target = 0;
        _progressBar.fillAmount = 0;
        var scene = SceneManager.LoadSceneAsync(sceneName);
        scene.allowSceneActivation = false;

        _loaderCanvas.SetActive(true);

        do
        {
            // await Task.Delay(100);
            _target = scene.progress;
        } while (scene.progress < 0.9f);
        scene.allowSceneActivation = true;
        await Task.Delay(1000);
        _loaderCanvas.SetActive(false);


    }
    public void chargeScene(string name)
    {
        SceneManager.LoadScene(name);
    }
    public void ChargeQuiz(int id)
    {
        quizId = id;
        LoadScene("Exercise");
    }

    private void Update()
    {
        // Debug.Log(_progressBar.fillAmount);
        _progressBar.fillAmount = Mathf.MoveTowards(_progressBar.fillAmount, _target, 3 * Time.deltaTime);
    }
}
