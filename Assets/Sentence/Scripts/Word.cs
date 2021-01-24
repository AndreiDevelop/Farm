using System;
using System.Threading.Tasks;
using UnityEngine;

//[ExecuteInEditMode]
public class Word : MonoBehaviour
{
    [HideInInspector] public event Action<Word> OnClick;
    [SerializeField] private SimpleHelvetica _simpleHelvetica;
    [SerializeField] private Animator _animator;
    [SerializeField] private Transform _bodyTransform;
    [SerializeField] private Collider _collider;

    [SerializeField] private string _word;
    public string WordString => _word;

    [Header("Materials")]
    [SerializeField] private Material _wordWrongMaterial;
    [SerializeField] private Material _wordDefaultMaterial;
    [SerializeField] private Material _wordCorrectMaterial;

    private void Awake()
    {
        //FitColliderToChildren();
        //RemoveCollidersFromChildren();
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
        _bodyTransform.gameObject.SetActive(false);
        ChangeMaterial(_wordCorrectMaterial);
    }

    public void ResetWord()
    {
        _bodyTransform.gameObject.SetActive(true);
        ChangeMaterial(_wordDefaultMaterial);
    }

    protected virtual void OnMouseDown()
    {
        OnClick?.Invoke(this);
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

    #region Editor
    void FitColliderToChildren()
    {
        bool hasBounds = false;
        Bounds bounds = new Bounds(Vector3.zero, Vector3.zero);
        int childCount = transform.GetChild(0).GetChild(0).childCount;
        for (int i = 1; i < childCount; ++i)
        {
            Renderer childRenderer = transform.GetChild(0).GetChild(0).transform.GetChild(i).GetComponent<Renderer>();
            if (childRenderer != null)
            {
                if (hasBounds)
                {
                    bounds.Encapsulate(childRenderer.bounds);
                }
                else
                {
                    bounds = childRenderer.bounds;
                    hasBounds = true;
                }
            }
        }

        Vector3 boundsSize = bounds.size * 5f;
        Vector3 boundsCentre = (bounds.center - transform.position) * 5;
        BoxCollider collider = (BoxCollider)_collider;
        collider.center = new Vector3(boundsCentre.x, boundsCentre.y, boundsCentre.z * (-1));
        collider.size = new Vector3(boundsSize.x, boundsSize.z, boundsSize.y);
    }

    void RemoveCollidersFromChildren()
    {
        int childCount = transform.GetChild(0).GetChild(0).childCount;
        for (int i = 1; i < childCount; ++i)
        {
            Collider childCollider = transform.GetChild(0).GetChild(0).transform.GetChild(i).GetComponent<Collider>();
            DestroyImmediate(childCollider);
        }
    }
    #endregion
}
