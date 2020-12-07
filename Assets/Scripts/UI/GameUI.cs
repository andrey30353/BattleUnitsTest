using TMPro;
using UnityEngine;

public class GameUI : MonoBehaviour
{
    [SerializeField] private Game _game;

    [SerializeField] private TMP_Text _zombiesCountText;
    [SerializeField] private TMP_Text _humansCountText;

    private string _startZombiesText;
    private string _startHumansText;

    private void Start()
    {
        _startZombiesText = _zombiesCountText.text;
        _startHumansText = _humansCountText.text;

        UpdateUI();
    }

    private void OnEnable()
    {
        _game.OnUnitCountChangeEvent += UpdateUI;
    }

    private void OnDisable()
    {
        _game.OnUnitCountChangeEvent -= UpdateUI;
    }

    private void UpdateUI()
    {
        _zombiesCountText.text = _startZombiesText + _game.ZombiesCount.ToString();
        _humansCountText.text = _startHumansText + _game.HumansCount.ToString();
    }
}
