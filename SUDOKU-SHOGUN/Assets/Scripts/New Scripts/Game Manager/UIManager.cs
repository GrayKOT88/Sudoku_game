using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace NewScripts
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private TMP_Text _levelText;
        [SerializeField] private Slider slider;

        private GameObject[] backgrounds;
        private int index;

        public void InitializeUI(int level)
        {
            _levelText.text += " " + level.ToString();
            ImageSelect();
            StartVolume();
        }

        private void ImageSelect()
        {
            index = PlayerPrefs.GetInt("SelectBackgrounds");
            backgrounds = new GameObject[transform.childCount];
            for (int i = 0; i < transform.childCount; i++)
            {
                backgrounds[i] = transform.GetChild(i).gameObject;
            }
            foreach (GameObject go in backgrounds)
            {
                go.SetActive(false);
            }
            if (backgrounds[index])
            {
                backgrounds[index].SetActive(true);
            }
        }

        public void SelectLeft()
        {
            backgrounds[index].SetActive(false);
            index--;
            if (index < 0)
            {
                index = backgrounds.Length - 1;
            }
            backgrounds[index].SetActive(true);
        }

        public void SelectRight()
        {
            backgrounds[index].SetActive(false);
            index++;
            if (index == backgrounds.Length)
            {
                index = 0;
            }
            backgrounds[index].SetActive(true);
        }

        private void StartVolume()
        {
            if (!PlayerPrefs.HasKey("volume"))
            {
                slider.value = 1;
            }
            else
            {
                slider.value = PlayerPrefs.GetFloat("volume");
            }
        }

        public void SaveBackground()
        {
            PlayerPrefs.SetInt("SelectBackgrounds", index);
        }

        public void SaveSliderVolume()
        {
            PlayerPrefs.SetFloat("volume", slider.value);
        }
    }
}