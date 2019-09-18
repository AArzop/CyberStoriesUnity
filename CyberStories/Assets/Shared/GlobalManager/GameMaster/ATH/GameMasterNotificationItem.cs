using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameMasterNotificationItem : MonoBehaviour
{
    public Text notificationText;
    public string message;
    public float lifeTime = 2f;
    public float slideTime = 1f;

    public Color standardColor;
    public Color WarningColor;
    public Color MailColor;
    public Image headerBackground;

    private GameMasterNotification manager;
    private CanvasGroup grp;
    private float currentLifeTime = 0f;
    private bool isDead = false;
    public bool isSlidding = false;

    public enum NotificationType
    {
        Standard,
        Warning,
        Mail
    };

    void Start()
    {
        grp = GetComponent<CanvasGroup>();
        manager = GameObject.Find("GameMasterNotifiacation")?.GetComponent<GameMasterNotification>();
        if (manager == null)
            Destroy(gameObject);

        notificationText.text = message;
    }

    IEnumerator Disappear()
    {
        for (float f = 1f; f >= 0f; f -= 0.1f)
        {
            grp.alpha = f;
            yield return new WaitForSeconds(0.05f);
        }

        isDead = true;
        yield return null;
    }

    IEnumerator SlideUpCoroutine()
    {
        RectTransform rect = GetComponent<RectTransform>();
        if (rect != null)
        {
            isSlidding = true;
            float y = transform.position.y;
            float newY = y + rect.rect.height + 0.05f;

            int loop = 100;
            float loopTime = slideTime / loop;

            float delta = (newY - y) / loop;
            for (float f = y; f <= newY; f += delta)
            {
                transform.position = new Vector3(transform.position.x, f, transform.position.z);
                yield return new WaitForSeconds(loopTime);
            }
        }

        isSlidding = false;
        yield return null;
    }

    public void SlideUp()
    {
        StartCoroutine(SlideUpCoroutine());
    }

    void Update()
    {
        if (!isDead)
        {
            currentLifeTime += Time.deltaTime;
            if (currentLifeTime >= lifeTime)
                StartCoroutine(Disappear());
        }
        else
        {
            StopAllCoroutines();
            manager.RemoveNotification(this);
            Destroy(gameObject);
        }
    }

    public void SetHeaderColor(NotificationType type)
    {
        switch (type)
        {
            case NotificationType.Standard:
                headerBackground.color = standardColor;
                break;

            case NotificationType.Warning:
                headerBackground.color = WarningColor;
                break;

            case NotificationType.Mail:
                headerBackground.color = MailColor;
                break;

            default:
                break;
        }
    }
}
