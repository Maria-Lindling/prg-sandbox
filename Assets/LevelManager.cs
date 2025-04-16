using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;

    [SerializeField]
    private GameObject _loaderCanvasObject;

    [SerializeField]
    private Image _progressBar;

    private float _targetFillAmount;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        } else {
            Destroy(gameObject);
        }
    }

    public async void LoadScene(string sceneName)
    {
        _targetFillAmount = 0.0f;
        _progressBar.fillAmount = 0.0f;

        var scene = SceneManager.LoadSceneAsync(sceneName);

        scene.allowSceneActivation = false;

        _loaderCanvasObject.SetActive(true);

        do
        {
            // artificial wait timer
            await Task.Delay(100);
            _targetFillAmount = scene.progress;
            FillUpdate();
        } while (scene.progress < 0.9f);

        scene.allowSceneActivation = true;

        _loaderCanvasObject.SetActive(false);
    }

    private void FillUpdate()
    {
        _progressBar.fillAmount = Mathf.MoveTowards(_progressBar.fillAmount, _targetFillAmount, 3 * Time.deltaTime);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
