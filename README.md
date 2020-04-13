# EzTween
simple tween for Unity using coroutine.
coroutine を使用したシンプルなUnity用のtween。

~~~cs
float time = Random.Range(0.5f, 2f);
Vector3 to = Random.insideUnitSphere * Random.Range(0, 5f);
EzTween.TweenLocalPosition(targetTrans, EzEaseType.Linear, to, time, () => {
    Debug.Log("Complete");
});
~~~

~~~cs
Color to = Random.ColorHSV();
Renderer renderer = targetTransA.GetComponent<Renderer>();
EzTween.TweenRendererColor(renderer, EzEaseType.Linear, to, time, () => {
  Debug.Log("Complete");
});
~~~

~~~cs
IEnumerator Act_Chain() {
    Vector3 to1 = Random.insideUnitSphere * Random.Range(0, 5f);
    yield return EzTween.TweenLocalPosition(targetTrans, EzEaseType.Linear, to1, 1);

    yield return new WaitForSeconds(1.0f); // delay

    Vector3 to2 = Vector3.one * Random.Range(1f, 5f);
    yield return EzTween.TweenScale(targetTrans, EzEaseType.Linear, to2, 1);

    Debug.Log("complete");
}
//
StartCoroutine(Act_Chain());
~~~
