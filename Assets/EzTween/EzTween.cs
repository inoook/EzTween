using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum EzEaseType
{
    Linear,
    SineIn, SineOut, SineInOut,
    QuadIn, QuadOut, QuadInOut,
    CubicIn, CubicOut, CubicInOut,
    QuartIn, QuartOut, QuartInOut,
    ExpIn, ExpOut, ExpInOut,
    CircIn, CircOut, CircInOut,
    ElasticIn, ElasticOut, ElasticInOut,
    BackIn, BackOut, BackInOut,
    BounceIn, BounceOut, BounceInOut,
    AnimationCurve
}

//public static class Extension
//{
//    public static float Lerp(this float me, float to, float t) {
//        return Mathf.Lerp(me, to, t);
//    }
//    public static Color Lerp(this Color me, Color to, float t) {
//        return Color.Lerp(me, to, t);
//    }
//    public static Vector3 Lerp(this Vector3 me, Vector3 to, float t) {
//        return Vector3.Lerp(me, to, t);
//    }
//    public static Vector2 Lerp(this Vector2 me, Vector2 to, float t) {
//        return Vector2.Lerp(me, to, t);
//    }
//    public static Quaternion Lerp(this Quaternion me, Quaternion to, float t) {
//        return Quaternion.Lerp(me, to, t);
//    }
//}

//public interface ITweenValue<T>
//{
//    T Lerp(T to, float v);
//}

public class EzTween : MonoBehaviour
{

    static EzTween INSTANCE {
        get {
            if (_INSTANCE == null) {
                GameObject go = new GameObject("EzTween");
                _INSTANCE = go.AddComponent<EzTween>();
            }
            return _INSTANCE;
        }
    }

    static EzTween _INSTANCE;

    #region API
    Dictionary<Object, Coroutine> tweenDic = null;

    public static void Cancel(Object monoBehaviour) {
        if (INSTANCE.tweenDic.ContainsKey(monoBehaviour)) {
            Coroutine coroutine = INSTANCE.tweenDic[monoBehaviour];
            INSTANCE._Cancel(coroutine);
        }
        INSTANCE.tweenDic.Remove(monoBehaviour);
    }

    public static void CancelAll() {
        foreach (var m in INSTANCE.tweenDic) {
            if (INSTANCE.tweenDic.ContainsKey(m.Key)) {
                Coroutine coroutine = m.Value;
                INSTANCE._Cancel(coroutine);
            }
        }
        INSTANCE.tweenDic.Clear();
    }


    // TODO: generic を使ってもっとシンプルにできないか？
    // extend
    // EaseType
    public static Coroutine TweenAct(Object keyObject, EzEaseType easeType, float from, float to, float time, System.Action<float> updateAction, System.Action completeAction = null) {
        return TweenBaseAct(keyObject, from, to, time, updateAction, completeAction, easeType);
    }
    public static Coroutine TweenAct(Object keyObject, EzEaseType easeType, Vector3 from, Vector3 to, float time, System.Action<Vector3> updateAction, System.Action completeAction = null) {
        //System.Action<float> act = (v) => {
        //    Vector3 p = Vector3.LerpUnclamped(from, to, v);
        //    updateAction(p);
        //};
        //if (from.Equals(to)) {
        //    completeAction?.Invoke();
        //    return null;
        //}
        Coroutine co = TweenBaseAct(keyObject, 0, 1, time, (v) => {
            Vector3 p = Vector3.LerpUnclamped(from, to, v);
            updateAction(p);
        }, completeAction, easeType, null);
        return co;
    }
    public static Coroutine TweenAct(Object keyObject, EzEaseType easeType, Vector2 from, Vector2 to, float time, System.Action<Vector2> updateAction, System.Action completeAction = null) {
        Coroutine co = TweenBaseAct(keyObject, 0, 1, time, (v) => {
            Vector2 p = Vector2.LerpUnclamped(from, to, v);
            updateAction(p);
        }, completeAction, easeType, null);
        return co;
    }
    public static Coroutine TweenAct(Object keyObject, EzEaseType easeType, Color from, Color to, float time, System.Action<Color> updateAction, System.Action completeAction = null) {
        Coroutine co = TweenBaseAct(keyObject, 0, 1, time, (v) => {
            Color p = Color.LerpUnclamped(from, to, v);
            updateAction(p);
        }, completeAction, easeType, null);
        return co;
    }
    public static Coroutine TweenAct(Object keyObject, EzEaseType easeType, Color32 from, Color32 to, float time, System.Action<Color32> updateAction, System.Action completeAction = null) {
        Coroutine co = TweenBaseAct(keyObject, 0, 1, time, (v) => {
            Color32 p = Color32.LerpUnclamped(from, to, v);
            updateAction(p);
        }, completeAction, easeType, null);
        return co;
    }
    public static Coroutine TweenAct(Object keyObject, EzEaseType easeType, Quaternion from, Quaternion to, float time, System.Action<Quaternion> updateAction, System.Action completeAction = null) {
        Coroutine co = TweenBaseAct(keyObject, 0, 1, time, (v) => {
            Quaternion p = Quaternion.LerpUnclamped(from, to, v);
            updateAction(p);
        }, completeAction, easeType, null);
        return co;
    }
    //
    // AnimationCurve
    public static Coroutine TweenAct(Object keyObject, AnimationCurve animationCurve, float from, float to, float time, System.Action<float> updateAction, System.Action completeAction = null) {
        return TweenBaseAct(keyObject, from, to, time, updateAction, completeAction, EzEaseType.AnimationCurve, animationCurve);
    }
    public static Coroutine TweenAct(Object keyObject, AnimationCurve animationCurve, Vector3 from, Vector3 to, float time, System.Action<Vector3> updateAction, System.Action completeAction = null) {
        Coroutine co = TweenBaseAct(keyObject, 0, 1, time, (v) => {
            Vector3 p = Vector3.LerpUnclamped(from, to, v);
            updateAction(p);
        }, completeAction, EzEaseType.AnimationCurve, animationCurve);
        return co;
    }
    public static Coroutine TweenAct(Object keyObject, AnimationCurve animationCurve, Vector2 from, Vector2 to, float time, System.Action<Vector2> updateAction, System.Action completeAction = null) {
        Coroutine co = TweenBaseAct(keyObject, 0, 1, time, (v) => {
            Vector2 p = Vector2.LerpUnclamped(from, to, v);
            updateAction(p);
        }, completeAction, EzEaseType.AnimationCurve, animationCurve);
        return co;
    }
    public static Coroutine TweenAct(Object keyObject, AnimationCurve animationCurve, Color from, Color to, float time, System.Action<Color> updateAction, System.Action completeAction = null) {
        Coroutine co = TweenBaseAct(keyObject, 0, 1, time, (v) => {
            Color p = Color.LerpUnclamped(from, to, v);
            updateAction(p);
        }, completeAction, EzEaseType.AnimationCurve, animationCurve);
        return co;
    }
    public static Coroutine TweenAct(Object keyObject, AnimationCurve animationCurve, Color32 from, Color32 to, float time, System.Action<Color32> updateAction, System.Action completeAction = null) {
        Coroutine co = TweenBaseAct(keyObject, 0, 1, time, (v) => {
            Color32 p = Color32.LerpUnclamped(from, to, v);
            updateAction(p);
        }, completeAction, EzEaseType.AnimationCurve, animationCurve);
        return co;
    }
    public static Coroutine TweenAct(Object keyObject, AnimationCurve animationCurve, Quaternion from, Quaternion to, float time, System.Action<Quaternion> updateAction, System.Action completeAction = null) {
        Coroutine co = TweenBaseAct(keyObject, 0, 1, time, (v) => {
            Quaternion p = Quaternion.LerpUnclamped(from, to, v);
            updateAction(p);
        }, completeAction, EzEaseType.AnimationCurve, animationCurve);
        return co;
    }
    //
    //public static Coroutine TweenAct<T>(Object keyObject, EzEaseType easeType, T from, T to, float time, System.Action<T> updateAction, System.Action completeAction = null) where T: ITweenValue<T> {
    //    Coroutine co = TweenAct(keyObject, easeType, 0, 1, time, (v) => {
    //        T p = from.Lerp(to, v);
    //        updateAction(p);
    //    }, completeAction);
    //    return co;
    //}
    #endregion

    #region API shortcut
    // よく使うtweenを使いやすくするため
    // transform
    public static Coroutine TweenPosition(Transform transform, EzEaseType easeType, Vector3 to, float time, System.Action completeAction = null) {
        Vector3 from = transform.position;
        return TweenAct(transform, easeType, from, to, time, (v) => {
            transform.position = v;
        }, completeAction);
    }
    public static Coroutine TweenLocalPosition(Transform transform, EzEaseType easeType, Vector3 to, float time, System.Action completeAction = null) {
        Vector3 from = transform.localPosition;
        return TweenAct(transform, easeType, from, to, time, (v) => {
            transform.localPosition = v;
        }, completeAction);
    }
    public static Coroutine TweenRotation(Transform transform, EzEaseType easeType, Quaternion to, float time, System.Action completeAction = null) {
        Quaternion from = transform.rotation;
        return TweenAct(transform, easeType, from, to, time, (v) => {
            transform.rotation = v;
        }, completeAction);
    }
    public static Coroutine TweenLocalRotation(Transform transform, EzEaseType easeType, Quaternion to, float time, System.Action completeAction = null) {
        Quaternion from = transform.localRotation;
        return TweenAct(transform, easeType, from, to, time, (v) => {
            transform.localRotation = v;
        }, completeAction);
    }
    public static Coroutine TweenScale(Transform transform, EzEaseType easeType, Vector3 to, float time, System.Action completeAction = null) {
        Vector3 from = transform.localScale;
        return TweenAct(transform, easeType, from, to, time, (v) => {
            transform.localScale = v;
        }, completeAction);
    }
    // renderer
    public static Coroutine TweenRendererColor(Renderer renderer, EzEaseType easeType, Color to, float time, System.Action completeAction = null) {
        Color from = renderer.material.color;
        return TweenAct(renderer, easeType, from, to, time, (v) => {
            renderer.material.color = v;
        }, completeAction);
    }
    // material
    public static Coroutine TweenMaterial(Material material, string floatParamName, EzEaseType easeType, float to, float time, System.Action completeAction = null) {
        float from = material.GetFloat(floatParamName);
        return TweenAct(material, easeType, from, to, time, (v) => {
            material.SetFloat(floatParamName, v);
        }, completeAction);
    }
    // ui
    public static Coroutine TweenUiColor(MaskableGraphic uiElement, EzEaseType easeType, Color to, float time, System.Action completeAction = null) {
        Color from = uiElement.color;
        return TweenAct(uiElement, easeType, from, to, time, (v) => {
            uiElement.color = v;
        }, completeAction);
    }
    public static Coroutine TweenCanvasGroupAlpha(CanvasGroup canvasGroup, EzEaseType easeType, float to, float time, System.Action completeAction = null) {
        float from = canvasGroup.alpha;
        return TweenAct(canvasGroup, easeType, from, to, time, (v) => {
            canvasGroup.alpha = v;
        }, completeAction);
    }
    //
    #endregion

    static Coroutine TweenBaseAct(Object keyObject, float from, float to, float time, System.Action<float> updateAction, System.Action completeAction = null, EzEaseType easeType = EzEaseType.Linear, AnimationCurve curve = null) {
        Cancel(keyObject);
        Coroutine co = INSTANCE._StartTween(from, to, time, updateAction, () => {
            // complete
            INSTANCE.tweenDic.Remove(keyObject);
            completeAction?.Invoke();
        }, easeType, curve);
        INSTANCE.tweenDic.Add(keyObject, co);

        return co;
    }

    public System.Func<float, float> GetEaseAction(EzEaseType type) {

        if (type == EzEaseType.SineIn) {
            return (v) => Easing.SineIn(v);
        }
        else if (type == EzEaseType.SineOut) {
            return (v) => Easing.SineOut(v);
        }
        else if (type == EzEaseType.SineInOut) {
            return (v) => Easing.SineInOut(v);
        }
        else if (type == EzEaseType.QuadIn) {
            return (v) => Easing.QuadIn(v);
        }
        else if (type == EzEaseType.QuadOut) {
            return (v) => Easing.QuadOut(v);
        }
        else if (type == EzEaseType.QuadInOut) {
            return (v) => Easing.QuadInOut(v);
        }
        else if (type == EzEaseType.CubicIn) {
            return (v) => Easing.CubicIn(v);
        }
        else if (type == EzEaseType.CubicOut) {
            return (v) => Easing.CubicOut(v);
        }
        else if (type == EzEaseType.CubicInOut) {
            return (v) => Easing.CubicInOut(v);
        }
        else if (type == EzEaseType.QuartIn) {
            return (v) => Easing.QuartIn(v);
        }
        else if (type == EzEaseType.QuartOut) {
            return (v) => Easing.QuartOut(v);
        }
        else if (type == EzEaseType.QuartInOut) {
            return (v) => Easing.QuartInOut(v);
        }
        else if (type == EzEaseType.ExpIn) {
            return (v) => Easing.ExpIn(v);
        }
        else if (type == EzEaseType.ExpOut) {
            return (v) => Easing.ExpOut(v);
        }
        else if (type == EzEaseType.ExpInOut) {
            return (v) => Easing.ExpInOut(v);
        }
        else if (type == EzEaseType.CircIn) {
            return (v) => Easing.CircIn(v);
        }
        else if (type == EzEaseType.CircOut) {
            return (v) => Easing.CircOut(v);
        }
        else if (type == EzEaseType.CircInOut) {
            return (v) => Easing.CircInOut(v);
        }
        else if (type == EzEaseType.ElasticIn) {
            return (v) => Easing.ElasticIn(v);
        }
        else if (type == EzEaseType.ElasticOut) {
            return (v) => Easing.ElasticOut(v);
        }
        else if (type == EzEaseType.ElasticInOut) {
            return (v) => Easing.ElasticInOut(v);
        }
        else if (type == EzEaseType.BackIn) {
            return (v) => Easing.BackIn(v);
        }
        else if (type == EzEaseType.BackOut) {
            return (v) => Easing.BackOut(v);
        }
        else if (type == EzEaseType.BackInOut) {
            return (v) => Easing.BackInOut(v);
        }
        else if (type == EzEaseType.BounceIn) {
            return (v) => Easing.BounceIn(v);
        }
        else if (type == EzEaseType.BounceOut) {
            return (v) => Easing.BounceOut(v);
        }
        else if (type == EzEaseType.BounceInOut) {
            return (v) => Easing.BounceInOut(v);
        }
        else {
            return (v) => Easing.Linear(v);
        }
    }

    //
    void Awake() {
        _INSTANCE = this;
        tweenDic = new Dictionary<Object, Coroutine>();
    }

    void _Cancel() {
        StopAllCoroutines();
    }
    void _Cancel(Coroutine routine) {
        StopCoroutine(routine);
    }

    private void Update() {
        if (Application.isEditor) {
            this.gameObject.name = "EzTween (" + (tweenDic.Count) + ")";
        }
    }

    #region tween
    Coroutine _StartTween(float from, float to, float time, System.Action<float> updateAction, System.Action completeAction = null, EzEaseType easeType = EzEaseType.Linear, AnimationCurve curve = null) {
        return StartCoroutine(Tween(from, to, time, updateAction, completeAction, easeType, curve));
    }

    IEnumerator Tween(float from, float to, float time, System.Action<float> updateAction, System.Action completeAction = null, EzEaseType easeType = EzEaseType.Linear, AnimationCurve curve = null) {
        float t = 0;
        float d = to - from;
        if (d != 0) {

            System.Func<float, float> easeAct = GetEaseAction(easeType);
            while (t < time) {
                t += Time.deltaTime;
                float per = t / time;
                per = Mathf.Clamp01(per);
                if (curve != null) {
                    per = curve.Evaluate(per);
                }
                else {
                    per = easeAct(per);
                }

                updateAction(from + d * per);
                yield return null;
            }
            updateAction(to);
        }
        else {
            yield return null;
        }
        completeAction?.Invoke();
    }
    #endregion
}
