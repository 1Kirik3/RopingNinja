using UnityEngine.SceneManagement;
using UnityEngine;
using DG.Tweening;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] private int m_sceneToLoad;
    [Header("AnimationParametrs")]
    [SerializeField] private float m_endScale;
    [SerializeField] private float m_animationDuration;
    private Transform _objectTransform;

    private void Awake()
    {
        _objectTransform = GetComponent<Transform>();
    }

    public void OnPress()
    {
        var animationQueue = DOTween.Sequence();
        animationQueue.Append(transform.DOScale(m_endScale, m_animationDuration).SetEase(Ease.InOutSine));
        animationQueue.Append(transform.DOScale(_objectTransform.localScale, m_animationDuration).SetEase(Ease.InOutSine));
        animationQueue.OnComplete(LoadScene);
    }

    private void LoadScene()
    {
        
        GetComponentInParent<Transform>().DOKill();
        SceneManager.LoadScene(m_sceneToLoad);
    }
}
