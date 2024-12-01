using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] Animator animator;

    IEnumerator LoadSceneAsync(string sceneName)
    {
        animator.SetTrigger("Start");
        yield return new WaitForSeconds(1);

        SceneManager.LoadSceneAsync(sceneName);

        Player.PlayerInstance.transform.position = new(0, -4.5f);
        animator.SetTrigger("End");
    }

    public void LoadScene(string sceneName)
    {
        StartCoroutine(LoadSceneAsync(sceneName));
    }
}
