using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace LevelChangerController
{
    public class LevelChanger : MonoBehaviour
    {
        // For scene transition
        public Animator animator;

        public Image fillImage;

        public string sceneToLoad;

        public void ChangeScene()
        {
            fillImage.fillAmount = 0f;
            animator.SetTrigger("FadeOut");
        }

        public void OnFadeComplete()
        {
            StartCoroutine(LoadScene());
        }

        IEnumerator LoadScene()
        {
            yield return new WaitForSeconds(0.25f);
            // Begin to load the Scene you specify
            AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneToLoad);
            // Let the Scene activate until you allow it to
            asyncOperation.allowSceneActivation = true;
            // When the load is still in progress, output the Text and progress bar
            while (!asyncOperation.isDone)
            {
                // Output the current progress
                fillImage.fillAmount = asyncOperation.progress / 0.9f;
                yield return null;
            }
        }
    }
}
