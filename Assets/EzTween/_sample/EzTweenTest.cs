using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EzTweenTest : MonoBehaviour
{
    [SerializeField] EzEaseType ezEaseType = EzEaseType.Linear;
    [SerializeField] Transform targetTrans = null;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Act_RandomPosition()
    {
        float to = Random.Range(-5f, 5f);
        EzTween.TweenAct(this, ezEaseType, targetTrans.localPosition.x, to, 1, (v) => {
            targetTrans.localPosition = new Vector3(v, 0, 0);
        }, () => {
            Debug.Log("Complete_Act_RandomPosition");
        });
    }

    void Act_RandomScale() {
        float to = Random.Range(1f, 5f);
        EzTween.TweenAct(targetTrans, ezEaseType, targetTrans.localScale.x, to, 1, (v) => {
            targetTrans.localScale = Vector3.one * v;
        }, () => {
            Debug.Log("Complete_Act_RandomScale");
        });
    }
    
    void Act_ToScale() {
        Vector3 to = Vector3.one * Random.Range(1f, 5f);
        EzTween.TweenAct(targetTrans, ezEaseType, targetTrans.localScale, to, 1, (v) => {
            targetTrans.localScale = v;
        }, () => {
            Debug.Log("Complete_Act_ToScale");
        });
        //EzTween.TweenAct(this.transform, ezEaseType, this.transform.localScale.x, toScale, 1, (v) => {
        //    this.transform.localScale = Vector3.one * v;
        //}, () => {
        //    Debug.Log("Complete_Act_RandomScale");
        //});
    }

    void Act_RandomRot() {
        //Quaternion to = Quaternion.LookRotation(Random.onUnitSphere);
        Quaternion to = Random.rotationUniform;
        EzTween.TweenAct(this, ezEaseType, targetTrans.rotation, to, 1, (v) => {
            targetTrans.rotation = v;
        }, () => {
            Debug.Log("Complete_Act_RandomRot");
        });
    }

    [SerializeField] AnimationCurve animationCurve = null;
    void Act_RandomScaleAnimCurve() {
        Vector3 to = Vector3.one * Random.Range(1f, 5f);
        EzTween.TweenAct(targetTrans, animationCurve, targetTrans.localScale, to, 1, (v) => {
            targetTrans.localScale = v;
        }, () => {
            Debug.Log("Complete_Act_Act_RandomScaleAnimCurve");
        });
    }

    void Act_RandomColor() {
        Color to = Random.ColorHSV();
        Renderer _renderer = targetTrans.GetComponent<Renderer>();
        EzTween.TweenAct(this, ezEaseType, _renderer.material.color, to, 1, (v) => {
            _renderer.material.color = v;
        }, () => {
            Debug.Log("Complete_Act_RandomColor");
        });
    }

    IEnumerator Act_Chain() {
        float to = Random.Range(-5f, 5f);
        yield return EzTween.TweenAct(this, ezEaseType, targetTrans.localPosition.x, to, 1, (v) => {
            targetTrans.localPosition = new Vector3(v, 0, 0);
        }, () => {
            Debug.Log("Complete_Act_Chain_pos");
        });

        yield return new WaitForSeconds(1.0f); // delay

        float to2 = Random.Range(1f, 5f);
        yield return EzTween.TweenAct(targetTrans, ezEaseType, targetTrans.localScale.x, to2, 1, (v) => {
            targetTrans.localScale = Vector3.one * v;
        }, () => {
            Debug.Log("Complete_Act_Chain_scale");
        });
    }

    void Act_Chain2() {
        float to = Random.Range(-5f, 5f);
        EzTween.TweenAct(this, ezEaseType, targetTrans.localPosition.x, to, 1, (v) => {
            targetTrans.localPosition = new Vector3(v, 0, 0);
        }, () => {
            Debug.Log("Complete_Act_Chain_pos");
            float to2 = Random.Range(1f, 5f);
            EzTween.TweenAct(targetTrans, ezEaseType, targetTrans.localScale.x, to2, 1, (v) => {
                targetTrans.localScale = Vector3.one * v;
            }, () => {
                Debug.Log("Complete_Act_Chain_scale");
                //
                EzTween.TweenAct(this, ezEaseType, targetTrans.localPosition.x, Random.Range(-5f, 5f), 1, (v) => {
                    targetTrans.localPosition = new Vector3(v, 0, 0);
                }, () => {
                    Debug.Log("Complete_Act_Chain_pos");
                });
            });
        });
    }

    [SerializeField] Rect drawRect = new Rect(10,10,200,200);
    private void OnGUI() {
        GUILayout.BeginArea(drawRect);
        if (GUILayout.Button("Act_RandomPosition")) {
            Act_RandomPosition();
        }
        if (GUILayout.Button("Act_RandomScale")) {
            Act_RandomScale();
        }
        if (GUILayout.Button("Act_RandomRot")) {
            Act_RandomRot();
        }
        //if (GUILayout.Button("Act_ToScale")) {
        //    Act_ToScale();
        //}
        if (GUILayout.Button("Act_RandomScaleAnimCurve")) {
            Act_RandomScaleAnimCurve();
        }
        if (GUILayout.Button("Act_RandomColor")) {
            Act_RandomColor();
        }
        if (GUILayout.Button("parallel: Act_RandomPosition / Act_RandomScale")) {
            Act_RandomPosition();
            Act_RandomScale();
        }

        if (GUILayout.Button("chain: Act_Chain")) {
            StartCoroutine(Act_Chain());
        }
        if (GUILayout.Button("chain: Act_Chain2")) {
            Act_Chain2();
        }
        GUILayout.EndArea();
    }
}
