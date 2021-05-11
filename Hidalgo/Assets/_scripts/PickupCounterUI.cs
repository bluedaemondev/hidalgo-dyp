using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Image = UnityEngine.UI.Image;

public class PickupCounterUI : MonoBehaviour
{
    public List<Sprite> spriteStates;
    [SerializeField] private int currentIndex;
    [SerializeField] private Image image;
    public GameObject timer;

    public bool IsCompleted()
    {
        if (currentIndex >= spriteStates.Count)
            return true;
        else
            return false;
    }

    private void Awake()
    {
        if (!image)
            image = GetComponent<Image>();
    }
    private void Start()
    {
        PickupsScapeGameManager.instance.onPickupItem.AddListener(this.StepNextSprite);
    }

    public void StepNextSprite(string idPickup = "default") // perdon por esto pero necesito el evento :( -juan
    {
        if (currentIndex < spriteStates.Count - 1)
            currentIndex++;

        timer.GetComponent<TimerCountdown>().ResetTimer();

        image.sprite = spriteStates[currentIndex];
        CheckEndList();
    }

    void CheckEndList()
    {
        if (currentIndex >= spriteStates.Count)
        {
            GameSceneManagerPickupsLevel.instance.SetPickupsCompletedState();
            this.enabled = false;
        }
    }
}
