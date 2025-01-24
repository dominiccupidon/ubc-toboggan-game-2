using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ViewScript : MonoBehaviour
{
    private TextMeshPro tmp;
    private Animator animator;
    private string correctValue = "SAND"; //Must be uppercase
    private int totalBlanks;
    private string currentVal = "";
    void Start()
    {
        tmp = transform.GetChild(0).gameObject.GetComponent<TextMeshPro>();
        animator = GetComponent<Animator>();
        totalBlanks = correctValue.Length;
        displayVal();
    }

    public void keyPressed(string key) 
    {
        if (key == "<")
        {
            if (currentVal.Length > 0) 
            {
                currentVal = currentVal.Remove(currentVal.Length - 1);
            }
        }
        else if (key == "_")
        {
            checkVal();
        }
        else 
        {
            if (currentVal.Length < totalBlanks) 
            {
                currentVal = currentVal + key;
            }
        }
        displayVal();
    }
    private void displayVal() 
    {
        tmp.text = appendBlanks(currentVal);
    }
    private string appendBlanks(string input) 
    {
        string output = "";
        if (input.Length > totalBlanks)
        {
            Debug.LogError("Too many inputs");
            return output;
        }
        else 
        {
            for (int i = input.Length; i < totalBlanks; i++) 
            {
                output = output + "_";
            }
            output = input + output;
            return output;
        }
    }
    private void checkVal() 
    {
        if (currentVal.Equals(correctValue))
        {
            GameObject.FindGameObjectWithTag("Triggered").GetComponent<Level4Door>().open();
        }
        else 
        {
            animator.Play("FlashWrong");
        }
    }

    public void clearVal() 
    {
        currentVal = "";
        displayVal();
    }

    public void colourRed() 
    { 
        tmp.color = Color.red;
    }
    
    public void colourGreen() 
    { 
        tmp.color = Color.green;
    }
}
