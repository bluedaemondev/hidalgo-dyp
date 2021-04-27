using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Image = UnityEngine.UI.Image;

public class PickupCounterUI : MonoBehaviour
{
    public List<Sprite> spriteStates;
    [SerializeField] private int currentIndex;
    [SerializeField] private Image image;

    private void Awake()
    {
        if (!image)
            image = GetComponent<Image>();
    }

    public void StepNextSprite()
    {
        if(currentIndex < spriteStates.Count - 1)
            currentIndex++;

        image.sprite = spriteStates[currentIndex];
        CheckEndList();
    }

    void CheckEndList()
    {
        if (currentIndex >= spriteStates.Count) { 
            GameSceneManagerPickupsLevel.instance.SetPickupsCompletedState();
            this.enabled = false;
        }
    }
}
