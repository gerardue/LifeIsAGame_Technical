using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

public class TestParabolicMovement : MonoBehaviour
{

    public AnimationCurve easy; 

    public float time;
    protected float anim;

    private CancellationTokenSource cts;

    //private void Update()
    //{
    //    anim += Time.deltaTime;

    //    anim %= time; 
    //    Debug.Log(anim + " anim");
    //    Debug.Log(anim /time + " anim time");

    //    transform.position = MathParabolic.Parabola(Vector3.zero, Vector3.forward * 10, 6f, anim/time);
    //}

    private void Awake()
    {
        cts = new CancellationTokenSource();

    }

    [ContextMenu("Parabolic")]
    public void Parabolic()
    {
        RunParabolic().WrapErrors();    
    }

    private void OnTriggerEnter(Collider other)
    {
        cts.Cancel(); 
        cts = new CancellationTokenSource();
    }

    private async Task RunParabolic()
    {
        float t = 0; 
        while(t < time - 0.01f && !cts.IsCancellationRequested)
        {
            t += Time.deltaTime;


            anim += Time.deltaTime;

            anim %= time;
            Debug.Log(easy.Evaluate(anim/time) + " bu");

            transform.position = MathParabolic.Parabola(Vector3.zero, Vector3.forward * 10, 6f, anim / time, easy);
            await Task.Yield();
        }
    }
}
