using UnityEngine;
using UnityEngine.UI;

public class LoreImageController : MonoBehaviour
{
    [Header("Story Images")]
    [SerializeField] private Sprite[] storySprites;

    [Header("UI")]
    [SerializeField] private Image storyImage;

    private int currentIndex = 0;

    void Start()
    {
        ShowImage();
    }

    private void ShowImage()
    {
        storyImage.sprite = storySprites[currentIndex];
    }

    public void NextImage()
    {
        if (currentIndex < storySprites.Length - 1)
        {
            currentIndex++;
            ShowImage();
        }
    }

    public void PreviousImage()
    {
        if (currentIndex > 0)
        {
            currentIndex--;
            ShowImage();
        }
    }
}
