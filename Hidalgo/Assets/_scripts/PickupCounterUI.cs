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

    private bool triggeredHasRocinante;
    public HorseUIContainer uiRocinante;


    public bool IsCompleted()
    {
        if (currentIndex >= spriteStates.Count - 1)
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
        if (uiRocinante == null)
        {
            uiRocinante = FindObjectOfType<HorseUIContainer>();
        }
    }

    public void StepNextSprite(string idPickup = "default") // perdon por esto pero necesito el evento :( -juan
    {
        if (!triggeredHasRocinante && idPickup == "0")
        {
            triggeredHasRocinante = true;
        }

        if (currentIndex < spriteStates.Count && triggeredHasRocinante && idPickup != "0")
            currentIndex++;

        timer.GetComponent<TimerCountdown>().ResetTimer();

        if (currentIndex < spriteStates.Count && idPickup != "0")
            image.sprite = spriteStates[currentIndex];
        
        CheckEndList();
    }

    void CheckEndList()
    {
        if (currentIndex >= spriteStates.Count)
        {
            PickupsScapeGameManager.instance.SetPickupsCompletedState();
            this.enabled = false;
        }
    }
}
