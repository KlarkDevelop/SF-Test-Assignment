using UnityEngine;
using TMPro;

public abstract class Counter : MonoBehaviour
{
    private TMP_Text label;
    private void Awake()
    {
        Init();
    }

    protected virtual void Init()
    {
        label = GetComponent<TMP_Text>();
    }

    protected void UpdateCounter(int value)
    {
        label.text = value.ToString();
    }

    protected void UpdateCounter()
    {
        label.text = (int.Parse(label.text) + 1).ToString();
    }
}
