using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ControlSettingsScript : MonoBehaviour
{
    public GameObject leftColumn;
    public GameObject middleColumn;
    public GameObject rightColumn;
    public int interactionType = 1;
    public GameObject SwipeHint;
    private int columnNumber = 1;

    public void Start()
    {
        leftColumn.SetActive(false);
        middleColumn.SetActive(false);
        rightColumn.SetActive(false);
        middleColumn.SetActive(true);
    }

    public void ChangeColumnNumberToOne(bool b)
    {
        if(columnNumber != 1)
        {
            leftColumn.SetActive(false);
            middleColumn.SetActive(true);
            rightColumn.SetActive(false);
            columnNumber = 1;
        }
    }

    public void ChangeColumnNumberToTwo(bool b)
    {
        if (columnNumber != 2)
        {
            leftColumn.SetActive(false);
            middleColumn.SetActive(true);
            rightColumn.SetActive(true);
            columnNumber = 2;
        }
    }

    public void ChangeColumnNumberToThree(bool b)
    {
        if (columnNumber != 3)
        {
            leftColumn.SetActive(true);
            middleColumn.SetActive(true);
            rightColumn.SetActive(true);
            columnNumber = 3;
        }
    }

    public void ChangeInteractionTypeToMenu(bool b)
    {
        if(interactionType != 1)
        {
            if(interactionType == 2)
            {
                SwipeHint.SetActive(false);
            }
            interactionType = 1;
        }
    }

    public void ChangeInteractionTypeToSwipe(bool b)
    {
        if (interactionType != 2)
        {
            if(interactionType == 1)
            {
                CancelAllSidePanel();
            }
            interactionType = 2;
            SwipeHint.SetActive(true);
        }
    }

    public void ChangeInteractionTypeToPlace(bool b)
    {
        if (interactionType != 3)
        {
            if (interactionType == 1)
            {
                CancelAllSidePanel();
            }
            if (interactionType == 2)
            {
                SwipeHint.SetActive(false);
            }
            interactionType = 3;
        }
    }

    private void CancelAllSidePanel()
    {
        for (int i = 0; i < rightColumn.transform.childCount; i++)
        {
            rightColumn.transform.GetChild(i).gameObject.GetComponent<ClickOnCardScript>().sideButtonSet.SetActive(false);
            rightColumn.transform.GetChild(i).gameObject.GetComponent<ClickOnCardScript>().sideButtonOut = false;
        }
        for (int i = 0; i < middleColumn.transform.childCount; i++)
        {
            middleColumn.transform.GetChild(i).gameObject.GetComponent<ClickOnCardScript>().sideButtonSet.SetActive(false);
            middleColumn.transform.GetChild(i).gameObject.GetComponent<ClickOnCardScript>().sideButtonOut = false;
        }
        for (int i = 0; i < leftColumn.transform.childCount; i++)
        {
            leftColumn.transform.GetChild(i).gameObject.GetComponent<ClickOnCardScript>().sideButtonSet.SetActive(false);
            leftColumn.transform.GetChild(i).gameObject.GetComponent<ClickOnCardScript>().sideButtonOut = false;
        }
    }
}