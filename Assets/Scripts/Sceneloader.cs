using UnityEngine;
using UnityEngine.SceneManagement;

public class Sceneloader : MonoBehaviour
{
    public string sceneToLoad;
    [SerializeField] private bool loadOnCollision;

    public void LoadSpecificScene()
    {
        SceneManager.LoadScene(sceneToLoad);
    }

    public void LoadLevel(int levelIndex)
    {
        SceneManager.LoadScene(levelIndex);
    }

    public void NextLevel()
    {
        int currentIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentIndex + 1);
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(loadOnCollision)
        {
            if (collision.CompareTag("Player"))
            {
                LoadSpecificScene();
            }
        }
    }
}