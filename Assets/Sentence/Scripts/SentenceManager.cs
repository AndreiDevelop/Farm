using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class SentenceManager : MonoBehaviour
{
    [SerializeField] private Animator _scrollAnimator;
    [SerializeField] private Material _defaultWordMaterial;
    [SerializeField] private Vector3 _defaultWordScale = new Vector3(2, 2, 2);

    [Header("Sentences")]
    [SerializeField] private Sentence _sentencePrefab;
    [SerializeField] private string[] _sentencesTemplate;
    [SerializeField] private List<Sentence> _sentencesList = new List<Sentence>();

    private string _sentenceName = "Sentence ";

    public void GenerateSentences()
    {
        Debug.Log("Generate Sentences");
        for(int i = 0; i < _sentencesTemplate.Length; i++)
        {
            AddSentence(i);
        }
    }

    public void ClearSentences()
    {
        Debug.Log("Clear Sentences");
        int childCount = transform.childCount;      
        for (int i = childCount - 1; i >= 0; i--)
        {
            GameObject sentenceObj = transform.GetChild(i).gameObject;

            if (sentenceObj.name.Contains(_sentenceName))
            {
                GameObject.DestroyImmediate(transform.GetChild(i).gameObject);
            }               
        }
        _sentencesList.Clear();
    }

    private async void AddSentence(int index)
    {
        Sentence sentence = Instantiate(_sentencePrefab, transform);
        sentence.Initialize(_sentencesTemplate[index], _defaultWordMaterial);
        sentence.name = _sentenceName + index;

        _sentencesList.Add(sentence);

        await Task.Delay(1000);

        sentence.transform.localScale = _defaultWordScale;
    }

    public void ShowSentence()
    {
        _scrollAnimator.SetTrigger("show");
    }

    public void HideSentence()
    {
        _scrollAnimator.SetTrigger("idle");
        foreach(var sentence in _sentencesList)
        {
            sentence.ResetSentence();
        }
        
    }
}
