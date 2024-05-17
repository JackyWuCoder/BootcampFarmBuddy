using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SeedsBuyUIElement : MonoBehaviour
{
    [SerializeField] TMP_Text _txtPlantName;
    [SerializeField] TMP_Text _txtSeedAmount;
    [SerializeField] Image _imgSeed;

    [SerializeField] private Button _btnBuy;

    public void SetElement(string plantName, float seedAmount, Sprite sprite)
    {
        _txtPlantName.text = plantName;
        _txtSeedAmount.text = $"${seedAmount}";
        _imgSeed.sprite = sprite;
    }

    public Button GetButton() { return _btnBuy; }
}
