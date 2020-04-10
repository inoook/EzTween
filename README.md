# EzTween
simple tween for Unity.
~~~cs
float timeA = Random.Range(0.5f, 2f);
Vector3 toA = Random.insideUnitSphere * Random.Range(0, 5f);
EzTween.TweenLocalPosition(targetTransA, ezEaseType, toA, timeA, () => {
    Debug.Log("Complete_Act_RandomPositionA");
});
  
Color toA = Random.ColorHSV();
Renderer rendererA = targetTransA.GetComponent<Renderer>();
EzTween.TweenRendererColor(rendererA, ezEaseType, toA, timeA, () => {
  Debug.Log("Complete_Act_RandomColorA");
});
~~~
