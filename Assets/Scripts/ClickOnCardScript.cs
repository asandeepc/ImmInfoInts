using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ClickOnCardScript : MonoBehaviour, IPointerClickHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public GameObject sideButtonSet;
    public RectTransform content;
    public bool sideButtonOut = false;
    public GameObject newCard;
    private Vector3 originalPosition;
    private Vector3 beginRayPosition;
    private GameObject sphere;
    private GameObject savedItems;
    private GameObject deletedItems;
    private bool dragActive = true;

    public void Start()
    {
        sphere = GameObject.FindGameObjectWithTag("Sphere");
        savedItems = GameObject.FindGameObjectWithTag("VRUISavedItems");
        deletedItems = GameObject.FindGameObjectWithTag("VRUIDeletedItems");
    }

    public void Update()
    {
        if(sphere == null)
        {
            sphere = GameObject.FindGameObjectWithTag("Sphere");
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (GameObject.FindGameObjectWithTag("SceneManager").GetComponent<ControlSettingsScript>().interactionType != 1)
        {
            originalPosition = transform.position;
            beginRayPosition = sphere.transform.position;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (GameObject.FindGameObjectWithTag("SceneManager").GetComponent<ControlSettingsScript>().interactionType == 2)
        {
            if (dragActive)
            {
                float delta_x = (sphere.transform.position - beginRayPosition).x;
                if (delta_x > 1)
                {
                    delta_x = 1;
                    dragActive = false;
                    transform.position = originalPosition;
                    DestroyAnimation();
                }
                else if (delta_x < -1)
                {
                    delta_x = -1;
                    dragActive = false;
                    transform.position = originalPosition;
                    DestroyAnimation();
                }
                transform.position = new Vector3(delta_x, 0, 0) + originalPosition;
            }
        }
        if(GameObject.FindGameObjectWithTag("SceneManager").GetComponent<ControlSettingsScript>().interactionType == 3)
        {
            if (dragActive)
            {
                transform.position = sphere.transform.position - beginRayPosition + originalPosition;
                if (Vector3.Distance(transform.position, savedItems.transform.position) <= 1.46f)
                {
                    dragActive = false;
                    DestroyAnimation();
                }
                if (Vector3.Distance(transform.position, deletedItems.transform.position) <= 1.46f)
                {
                    dragActive = false;
                    DestroyAnimation();
                }
            }
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (GameObject.FindGameObjectWithTag("SceneManager").GetComponent<ControlSettingsScript>().interactionType != 1)
        {
            transform.DOMove(originalPosition, Vector3.Distance(transform.position, originalPosition) * 0.1f);
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (GameObject.FindGameObjectWithTag("SceneManager").GetComponent<ControlSettingsScript>().interactionType == 1)
        {
            if (!sideButtonOut)
            {
                sideButtonSet.SetActive(true);
                sideButtonOut = true;
            }
            else
            {
                sideButtonSet.SetActive(false);
                sideButtonOut = false;
            }
        }
    }
    IEnumerator WaitingDestroy()
    {
        yield return new WaitForSeconds(0.3f);
        dragActive = true;
        Destroy(gameObject);
    }

    private void DestroyAnimation()
    {
        Instantiate(newCard, transform.parent);
        GetComponent<ContentSizeFitter>().verticalFit = ContentSizeFitter.FitMode.MinSize;
        content.DOSizeDelta(new Vector2(200, 0), 0.3f, false);
        StartCoroutine(WaitingDestroy());
    }
}
