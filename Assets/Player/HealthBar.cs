using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public int maxHealth;
    public int currentHealth;
    public int blockWidth;
    public int numBlocks;

    public Sprite midHealthBar;
    public Sprite lowHealthBar;
    public AudioClip clip;

    private Image[] blockImages;

    void Start()
    {
        blockImages = new Image[numBlocks];
        for (int i = 0; i < numBlocks; i++)
        {
            GameObject blockObject = new GameObject("Block " + i);
            blockObject.transform.SetParent(transform, false);
            blockImages[i] = blockObject.AddComponent<Image>();
            blockImages[i].sprite = GetComponent<Image>().sprite;
            blockImages[i].rectTransform.anchorMin = new Vector2(0, 0);
            blockImages[i].rectTransform.anchorMax = new Vector2(0, 1);
            blockImages[i].rectTransform.pivot = new Vector2(0, 0.5f);
            blockImages[i].rectTransform.sizeDelta = new Vector2(blockWidth, 0);
            blockImages[i].rectTransform.anchoredPosition = new Vector2(i * blockWidth, 0);
        }
    }

    void Update()
    {
        int numVisibleBlocks = Mathf.CeilToInt((float)currentHealth / maxHealth * numBlocks);
        for (int i = 0; i < numBlocks; i++)
        {
            if (i < numVisibleBlocks)
            {
                blockImages[i].gameObject.SetActive(true);
                blockImages[i].color = Color.white;
            }
            else
            {
                blockImages[i].gameObject.SetActive(false);
            }
        }
    }

    public void UpdateHealth(int health)
    {
        if (((float)currentHealth / maxHealth) < .5f)
        {
            GetComponent<Image>().sprite = midHealthBar;
            UpdateBlocks();
        }
        if (((float)currentHealth / maxHealth) < .25f)
        {
            GetComponent<Image>().sprite = lowHealthBar;
            UpdateBlocks();
        }
        currentHealth = health;
        if (currentHealth == 0)
        {
            GameObject.Find("SoundFX").GetComponent<AudioSource>().PlayOneShot(clip);
            AudioManager audioManager = FindObjectOfType<AudioManager>();
            audioManager.ChangeTrack(0, .5f);
            Destroy(GameObject.Find("Player"));
        }
    }

    private void UpdateBlocks()
    {
        for (int i = 0; i < numBlocks; i++)
        {
            blockImages[i].sprite = GetComponent<Image>().sprite;
        }
    }
}
