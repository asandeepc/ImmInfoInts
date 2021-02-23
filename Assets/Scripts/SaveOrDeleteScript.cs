using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SaveOrDeleteScript : MonoBehaviour
{
    public RectTransform currentCard;
    public RectTransform content;
    public GameObject sideButtonSet;
    public GameObject newCard;

    public void Start()
    {
        content.sizeDelta = new Vector2(200, 0);
        content.DOSizeDelta(new Vector2(200, 140), 0.3f, false);
        currentCard.gameObject.GetComponent<ContentSizeFitter>().verticalFit = ContentSizeFitter.FitMode.PreferredSize;
    }

    public void SaveCard()
    {
        RemoveCardFromView();
        AddNewCardToView();
    }

    public void DeleteCard()
    {
        RemoveCardFromView();
        AddNewCardToView();
    }

    private void RemoveCardFromView()
    {
        sideButtonSet.SetActive(false);
        currentCard.gameObject.GetComponent<ContentSizeFitter>().verticalFit = ContentSizeFitter.FitMode.MinSize;
        content.DOSizeDelta(new Vector2(200, 0), 0.3f, false);
    }

    private void AddNewCardToView()
    {
        Instantiate(newCard, currentCard.transform.parent);
        StartCoroutine(WaitingDestroy());
    }

    IEnumerator WaitingDestroy()
    {
        yield return new WaitForSeconds(0.3f);
        Destroy(currentCard.gameObject);
    }
}
