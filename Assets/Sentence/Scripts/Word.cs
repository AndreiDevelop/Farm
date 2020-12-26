using System;
using System.Threading.Tasks;
using UnityEngine;

[ExecuteInEditMode]
public class Word : MonoBehaviour
{
    [HideInInspector] public Action<string> OnClick;
    [SerializeField] private SimpleHelvetica _simpleHelvetica;
    [SerializeField] private string _word;
    [SerializeField] private Animator _animator;
    [SerializeField] private Collider _collider;
    [SerializeField] private Transform _bodyTransform;

    [Header("Materials")]
    [SerializeField] private Material _wordWrongMaterial;
    [SerializeField] private Material _wordDefaultMaterial;
    [SerializeField] private Material _wordCorrectMaterial;

    private Vector3 _startPosition;

    private void Awake()
    {
        _startPosition = transform.position;
    }

    public void Initialize(string word, Material wordMaterial)
    {
        if (_simpleHelvetica != null && !string.IsNullOrEmpty(word))
        {
            _wordDefaultMaterial = wordMaterial;
            ChangeMaterial(wordMaterial);
            _word = word;
            _simpleHelvetica.Text = word;
            _simpleHelvetica.GenerateText();
        }
    }


    public void WordIsCorrect()
    {
        _collider.enabled = false;
        //transform.position = _destination.position;
        _bodyTransform.localScale = new Vector3(1f, 1f, 0.05f);
        ChangeMaterial(_wordCorrectMaterial);
    }

    public void ResetWord()
    {
        _collider.enabled = true;
        transform.position = _startPosition;
        _bodyTransform.localScale = new Vector3(1f, 1f, 1f);
        ChangeMaterial(_wordDefaultMaterial);
    }

    protected virtual void OnMouseDown()
    {
        OnClick?.Invoke(_word);
        _animator.SetTrigger("TapDown");
    }

    protected virtual void OnMouseUp()
    {
        _animator.SetTrigger("TapUp");
    }

    private void ChangeMaterial(Material material)
    {
        _simpleHelvetica.MeshRenderer.material = material;
        _simpleHelvetica.ApplyMeshRenderer();
    }
}
