using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

public class Sentence: MonoBehaviour
{
    [HideInInspector] public event Action OnSuccessComplete;
    [HideInInspector] public event Action<string> OnClickWord;

    [SerializeField] private Word _wordPrefab;
    [SerializeField] private List<Word> _wordsOrder = new List<Word>();
    private int _currentWordIndex = 0;

    private void Awake()
    {
        Initialize();
    }

    public void Initialize()
    {
        foreach (var word in _wordsOrder)
        {
           word.OnClick += TapOnWord;   
        }
    }

    private async void TapOnWord(Word word)
    {
        if (word.WordString.Equals(_wordsOrder[_currentWordIndex].WordString))
        {
            OnClickWord?.Invoke(word.WordString);
            word.WordIsCorrect();

            _currentWordIndex++;

            if (_currentWordIndex == _wordsOrder.Count)
            {
                await Task.Delay(2000);
                Debug.Log("OnSuccessComplete");
                OnSuccessComplete?.Invoke();
            }
        }
        else
        {
            OnClickWord?.Invoke(string.Empty);
            ResetSentence();
        }
    }

    public void ResetSentence()
    {      
        for (int i = 0; i < _wordsOrder.Count; i++)
        {
            _wordsOrder[i].ResetWord();
        }
        _currentWordIndex = 0;
    }

    public void Show()
    {
        SetActive(true);
    }

    public void Hide()
    {
        ResetSentence();
        SetActive(false);
    }

    private void SetActive(bool isActive)
    {
        gameObject.SetActive(isActive);
    }

    #region Editor
    public void InitializeEditor(string wordsText, Material wordMaterial)
    {
        if (!string.IsNullOrEmpty(wordsText))
        {
            var punctuation = wordsText.Where(Char.IsPunctuation).Distinct().ToArray();
            var words = wordsText.Split().Select(x => x.Trim(punctuation)).ToList();

            for (int i = 0; i < words.Count; i++)
            {
                int j = i;

                Word wordComponent = Instantiate(_wordPrefab, transform);
                wordComponent.Initialize(words[j], wordMaterial);
                wordComponent.name = "Word " + j;

                wordComponent.OnClick += TapOnWord;

                _wordsOrder.Add(wordComponent);
            }
        }
    }
    #endregion
}
