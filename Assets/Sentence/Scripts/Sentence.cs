using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Sentence: MonoBehaviour
{
    [HideInInspector] public Action OnSuccessComplete;
    [SerializeField] private Word _wordPrefab;
    [SerializeField] private List<Word> _wordsOrder = new List<Word>();
    private int _currentWordIndex = 0;
    public void Initialize(string wordsText, Material wordMaterial)
    {
        if(!string.IsNullOrEmpty(wordsText))
        {
            var punctuation = wordsText.Where(Char.IsPunctuation).Distinct().ToArray();
            var words = wordsText.Split().Select(x => x.Trim(punctuation)).ToList();

            for (int i = 0; i < words.Count; i++)
            {
                int j = i;

                Word wordComponent = Instantiate(_wordPrefab, transform);
                wordComponent.Initialize(words[j], wordMaterial);
                wordComponent.name = "Word " + j;

                wordComponent.OnClick += (word) => TapOnWord(j, word);

                _wordsOrder.Add(wordComponent);
            }
        }          
    }

    private void TapOnWord(int index, string word)
    {
        if (index == _currentWordIndex)
        {
            _wordsOrder[_currentWordIndex].WordIsCorrect();

            _currentWordIndex++;

            if (_currentWordIndex == _wordsOrder.Count)
            {
                Debug.Log("OnSuccessComplete");
                OnSuccessComplete?.Invoke();
            }
        }
        else
        {
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
}
