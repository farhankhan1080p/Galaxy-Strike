using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] GameObject playerDestroyingVFX;
    private void OnTriggerEnter(Collider other) {
        Instantiate(playerDestroyingVFX, transform.position, Quaternion.identity);
        gameObject.transform.GetChild(0).gameObject.SetActive(false);
        StartCoroutine(LoadSceneAtTime("SampleScene", 1.5f));
        // Destroy(this.gameObject);
    }

    private IEnumerator LoadSceneAtTime(string name, float time)
    {
        yield return new WaitForSeconds(time);
        SceneManager.LoadScene(name);
    }
}
