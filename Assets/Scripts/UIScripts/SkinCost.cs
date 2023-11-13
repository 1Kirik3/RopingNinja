using TMPro;
using UnityEngine;

public class SkinCost : MonoBehaviour
{
    [SerializeField] private Skin m_skin;
    private string _costText;

    private void Awake()
    {
        _costText = GetComponent<TMP_Text>().text;
    }

    void Start()
    {
        CostToString();
    }

    private void CostToString()
        => _costText = m_skin.skinCost.ToString();

}
