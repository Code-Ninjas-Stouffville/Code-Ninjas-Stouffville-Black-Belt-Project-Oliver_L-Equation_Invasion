﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuizManager : MonoBehaviour
{
    public List<QuestionAndAnswers> QnA;
    public GameObject[] options;
    public int currentQuestion;

    public Text questionText;
    public GameObject questionpanel;
    public bool rewardspanel = false;

    void SetAnswers()
    {
        for (int i = 0; i < options.Length; i++)
        {
            options[i].GetComponent<AnswerScript>().isCorrect = false;
            options[i].transform.GetChild(0).GetComponent<Text>().text = QnA[currentQuestion].Answers[i];
            if (QnA[currentQuestion].CorrectAnswer == i + 1)
            {
                options[i].GetComponent<AnswerScript>().isCorrect = true;
            }
        }
    }

    public void pressedButton()
    {
        questionpanel.SetActive(true);
        generateQuestion();
    }

    public void correct()
    {
        QnA.RemoveAt(currentQuestion);
        questionpanel.SetActive(false);
        rewardspanel = true;
    }

    
    void generateQuestion()
    {
        if (QnA.Count > 0)
        {

            currentQuestion = Random.Range(0, QnA.Count);

            questionText.text = QnA[currentQuestion].Question;

            SetAnswers();

            QnA.RemoveAt(currentQuestion);
        }
        else
        {
            Debug.Log("Out of Questions");
        }
    }
}
