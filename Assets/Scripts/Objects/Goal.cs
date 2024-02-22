using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Goal : MonoBehaviour
{

    [SerializeField] private string nextScene;

    private GameManager gameManager;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerMovement>() != null)
        {
            StartCoroutine(NextScene());
        }
    }

    private IEnumerator NextScene()
    {
        if (nextScene.Equals(""))
        {
            Debug.LogWarning("No Next Scene");
        }
        else if(gameManager.CanFinishLevel())
        {
            yield return new WaitForSecondsRealtime(.5f);
            SceneManager.LoadScene(nextScene);
        }
    }
}
