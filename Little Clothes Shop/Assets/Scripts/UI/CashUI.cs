using UnityEngine;
using UnityEngine.UI;

public class CashUI : MonoBehaviour
{
    public static CashUI Cash;

    // Components 
    [SerializeField] private Text cashText;

    // Prefab
    private GameObject CashFlowPrefab => GetCashFlowPrefab();
    private GameObject _cfPrefab;

    private GameObject GetCashFlowPrefab()
    {
        if (!_cfPrefab) _cfPrefab = Resources.Load<GameObject>("Prefabs/CashFlow");
        return _cfPrefab;
    }

    private void Start()
    {
        Cash = this;

        _displayedValue = Player.P.cash;
        cashText.text = _displayedValue.ToString("n2");
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        FlowingAnimation();
    }

    #region CashFlow

    ////Cash flowing out when spent (Not just vanishing)
    // Cash Flow Variables
    private float _displayedValue;
    private const float MinimumFlowValue = 5f;
    private const float TimeEachDisplayedUpdate = .05f;
    private float _currentTime = 0f;

    // Flowing Animation gradually changes the displayed cash amount until the actual value
    private void FlowingAnimation()
    {
        if (_currentTime <= 0)
        {
            UpdateCashAmount();
            _currentTime += TimeEachDisplayedUpdate;
        }

        _currentTime -= Time.fixedDeltaTime;
    }

    private void UpdateCashAmount()
    {
        if (Player.P.cash == _displayedValue)
            return; // if the displayed cash is already the player`s current cash amount, just ignore 

        if (Mathf.Abs(Player.P.cash - _displayedValue) < MinimumFlowValue)
        {
            _displayedValue =
                Player.P.cash; // If the difference is smaller then the minimum flow value, it just updates to the final value 
        }
        else
        {
            _displayedValue =
                Mathf.Lerp(_displayedValue, Player.P.cash, 0.2f); // Every tick the displayed value will get t% closer
        }

        cashText.text = _displayedValue.ToString("n2"); // Displays the current displayed value
    }

    public void CashFlow(float v)
    {
        Player.P.cash += v; // Updates the total cash the player currently have

        var cf = Instantiate(CashFlowPrefab, transform)
            .GetComponent<CashFlow>(); // Instantiates a cash flow number to display the amount received or spent

        cf.transform.localPosition = Vector3.zero;
        cf.Flow(v); // Triggers the flow animation with the value
    }

    #endregion
}