using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using Vuforia;

public class TrackableList : MonoBehaviour
{
    [SerializeField] private string _firstTargetTrackableName;
    [SerializeField] private string _secondTargetTrackableName;
    [SerializeField] private float _distanceLimit;
    [SerializeField] private TextMeshPro _textMeshToChange;
    [SerializeField] private GameObject _disableText1;
    [SerializeField] private GameObject _disableText2;
    StateManager sm;
    private void Awake()
    {
        // Get the Vuforia StateManager
        sm = TrackerManager.Instance.GetStateManager();
        StartCoroutine(UpdateCoroutine());
    }

    IEnumerator UpdateCoroutine()
    {
        while(true)
        {
            yield return new WaitForSeconds(1);

            IEnumerable<TrackableBehaviour> activeTrackables = sm.GetActiveTrackableBehaviours();

            if (activeTrackables != null && activeTrackables.Count() == 2)
            {
                TrackableBehaviour firstTrackable = activeTrackables.FirstOrDefault(x => x.TrackableName == _firstTargetTrackableName);
                TrackableBehaviour secondTrackable = activeTrackables.FirstOrDefault(x => x.TrackableName == _secondTargetTrackableName);

                float distance = Vector3.Distance(firstTrackable.transform.position, secondTrackable.transform.position);
                Debug.Log(distance);
                if (distance < _distanceLimit)
                {
                    firstTrackable.transform.GetChild(0).gameObject.SetActive(false);
                    secondTrackable.transform.GetChild(0).gameObject.SetActive(false);

                    firstTrackable.transform.GetChild(1).gameObject.SetActive(true);
                    _disableText1.SetActive(false);
                    _disableText2.SetActive(false);
                    _textMeshToChange.text = "CO2";
                }
                else
                {
                    firstTrackable.transform.GetChild(0).gameObject.SetActive(true);
                    secondTrackable.transform.GetChild(0).gameObject.SetActive(true);

                    firstTrackable.transform.GetChild(1).gameObject.SetActive(false);
                    _disableText1.SetActive(true);
                    _disableText2.SetActive(true);
                    _textMeshToChange.text = "C";
                }
            }
        }
        
    }
}
