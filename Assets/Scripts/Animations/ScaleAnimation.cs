using UnityEngine;
using DG.Tweening;
using System.Collections.Generic;

public class ScaleAnimation : MonoBehaviour
{
    public List<GameObject> objectsToIncrease;
    public List<GameObject> objectsToDecrease;
    [SerializeField] private float m_timeToScale;

    public void OpenAnimation()
    {
        if (objectsToIncrease.Count == 0)
            return;
        foreach (var item in objectsToIncrease)
        {
            item.gameObject.transform.localScale = Vector3.zero;
            item.SetActive(true);
            item.gameObject.transform.DOScale(Vector3.one, 0.5f).SetEase(Ease.Linear);
        }
    }

    public void CloseAnimation()
    {
        if (objectsToDecrease.Count == 0)
            return;
        foreach (var item in objectsToDecrease)
        {
            item.gameObject.transform.localScale = Vector3.one;
            item.gameObject.transform.DOScale(Vector3.zero, 0.5f).SetEase(Ease.Linear).OnComplete(SetItemsFalse).OnComplete(OpenAnimation);
        }
    }

    private void SetItemsFalse()
    {
        if (objectsToDecrease.Count == 0)
            return;
        foreach (var item in objectsToDecrease)
        {
            item.SetActive(false);
        }
    }
}
