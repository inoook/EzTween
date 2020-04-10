using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EzTweenCanvasTest : MonoBehaviour
{

    [SerializeField] EzEaseType ezEaseType = EzEaseType.Linear;
    [SerializeField] RectTransform targetRectTrans = null;
    [SerializeField] Image image = null;


    void Act_RandomPosition() {
        float time = Random.Range(0.5f, 2f);
        Vector2 to = Random.insideUnitSphere * Random.Range(0, 400f);
        EzTween.TweenAct(targetRectTrans, ezEaseType, targetRectTrans.anchoredPosition, to, time, (v) => {
            targetRectTrans.anchoredPosition = v;
        }, () => {
            Debug.Log("Complete_Act_RandomPositionA");
        });
    }

    void Act_RandomSizeDelta() {
        float time = Random.Range(0.5f, 2f);
        Vector2 to = Vector2.one * Random.Range(0, 300f);
        EzTween.TweenAct(targetRectTrans, ezEaseType, targetRectTrans.sizeDelta, to, time, (v) => {
            targetRectTrans.sizeDelta = v;
        }, () => {
            Debug.Log("Complete_Act_RandomPositionA");
        });
    }

    void Act_RandomColor() {
        float time = Random.Range(0.5f, 2f);
        Color to = Random.ColorHSV();
        EzTween.TweenAct(image, ezEaseType, image.color, to, time, (v) => {
            image.color = v;
        }, () => {
            Debug.Log("Complete_Act_RandomPositionA");
        });
    }

    void Cancel() {
        EzTween.Cancel(targetRectTrans);
        EzTween.Cancel(image);
    }
    void CancelAll() {
        EzTween.CancelAll();
    }

    [SerializeField] Rect drawRect = new Rect(10, 10, 200, 200);
    private void OnGUI() {
        GUILayout.BeginArea(drawRect);
        if (GUILayout.Button("Act_RandomPosition")) {
            Act_RandomPosition();
        }
        if (GUILayout.Button("Act_RandomSizeDelta")) {
            Act_RandomSizeDelta();
        }
        if (GUILayout.Button("Act_RandomColor")) {
            Act_RandomColor();
        }
        if (GUILayout.Button("Cancel")) {
            Cancel();
        }
        if (GUILayout.Button("CancelAll")) {
            CancelAll();
        }
        GUILayout.EndArea();
    }
}
