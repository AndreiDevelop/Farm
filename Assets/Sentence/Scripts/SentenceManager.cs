using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
//[ExecuteInEditMode]
public class SentenceManager : MonoBehaviour
{
    [SerializeField] private Animator _scrollAnimator;
    [SerializeField] private Material _defaultWordMaterial;
    [SerializeField] private Vector3 _defaultWordScale = new Vector3(2, 2, 2);
    [SerializeField] private TextMeshProUGUI _resultSentencePanel;

    [Header("Sentences")]
    [SerializeField] private Sentence _sentencePrefab;
    [SerializeField] private string[] _sentencesTemplate;
    [SerializeField] private List<Sentence> _sentencesList = new List<Sentence>();

    private string _sentenceName = "Sentence ";
    private int _currentSentenceIndex = 0;

    private void OnEnable()
    {
        ShowFirstSentence();
    }

    public void ShowFirstSentence()
    {
        _scrollAnimator.SetTrigger("show");
        ShowCurrentSentence();
    }

    public void HideAllSentences()
    {
        _scrollAnimator.SetTrigger("idle");
        HideCurrentSentence();
        _currentSentenceIndex = 0;
        foreach (var sentence in _sentencesList)
        {
            sentence.Hide();
        }     
    }

    private void ShowNextSentence()
    {
        if (_sentencesList != null && _sentencesList.Count > 0)
        {
            if (_currentSentenceIndex < _sentencesList.Count-1)
            {
                HideCurrentSentence();
                _currentSentenceIndex++;
                ShowCurrentSentence();
            }
            else
            {
                HideCurrentSentence();
                _currentSentenceIndex = 0;
            }
        }
    }

    private void ShowCurrentSentence()
    {
        UpdateWordResultPanel(string.Empty);
        _sentencesList[_currentSentenceIndex].Show();
        _sentencesList[_currentSentenceIndex].OnSuccessComplete += ShowNextSentence;
        _sentencesList[_currentSentenceIndex].OnClickWord += UpdateWordResultPanel;
    }

    private void HideCurrentSentence()
    {
        _sentencesList[_currentSentenceIndex].OnSuccessComplete -= ShowNextSentence;
        _sentencesList[_currentSentenceIndex].OnClickWord -= UpdateWordResultPanel;
        _sentencesList[_currentSentenceIndex].Hide();       
    }

    private void UpdateWordResultPanel(string word)
    {
        if(string.IsNullOrEmpty(word))
        {
            _resultSentencePanel.text = string.Empty;
        }
        else
        {
            _resultSentencePanel.text += " " + word;
        }       
    }

    #region Editor
    private void ResetSentencesEditor()
    {
        foreach (var sentence in _sentencesList)
        {
            sentence.gameObject.SetActive(false);
        }
        _sentencesList[0].gameObject.SetActive(true);
    }
    public void GenerateSentencesEditor()
    {
        Debug.Log("Generate Sentences");
        for (int i = 0; i < _sentencesTemplate.Length; i++)
        {
            AddSentenceEditor(i);
        }
    }

    public void ClearSentencesEditor()
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

    private async void AddSentenceEditor(int index)
    {
        Sentence sentence = Instantiate(_sentencePrefab, transform);
        sentence.InitializeEditor(_sentencesTemplate[index], _defaultWordMaterial);
        sentence.name = _sentenceName + index;

        _sentencesList.Add(sentence);

        await Task.Delay(1000);

        sentence.transform.localScale = _defaultWordScale;
    }
    #endregion
}
