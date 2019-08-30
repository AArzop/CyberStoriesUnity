using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace LevelChangerController
{
    public class LevelChanger : MonoBehaviour
    {
        // For scene transition
        [FormerlySerializedAs("animator")] public Animator Animator;

        [FormerlySerializedAs("fillImage")] public Image FillImage;

        [FormerlySerializedAs("sceneToLoad")] public string SceneToLoad;

        public void ChangeScene()
        {
            FillImage.fillAmount = 0f;
            Animator.SetTrigger("FadeOut");
        }

        public void OnFadeComplete()
        {
            StartCoroutine(LoadScene());
        }

        private IEnumerator LoadScene()
        {
            yield return new WaitForSeconds(0.25f);
            // Begin to load the Scene you specify
            AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(SceneToLoad);
            // Let the Scene activate until you allow it to
            asyncOperation.allowSceneActivation = true;
            // When the load is still in progress, output the Text and progress bar
            while (!asyncOperation.isDone)
            {
                // Output the current progress
                FillImage.fillAmount = asyncOperation.progress / 0.9f;
                yield return null;
            }
        }
    }
}