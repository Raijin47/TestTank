using System.Collections;
using UnityEngine;

public class MuzzleBase : MonoBehaviour
{
    public static MuzzleBase Instance;

    [SerializeField] private ButtonBase _fireButton;
    [SerializeField] private RotateState _rotate;
    [SerializeField] private FireState _fire;

    private Coroutine _coroutine;
    public FireState FireState => _fire;

    private void Awake() => Instance = this;

    private void Start()
    {
        _fireButton.OnClick.AddListener(Fire);
        _rotate.Init();
        _fire.Init();
        _fire.OnEndFire += StartMovementProcess;
        StartMovementProcess();
    }

    private void Fire()
    {
        Release();
        _coroutine = StartCoroutine(_fire.FireProcess());
    }

    private void StartMovementProcess()
    {
        Release();
        _coroutine = StartCoroutine(_rotate.RotateProcess());
    }

    private void Release()
    {
        if(_coroutine != null)
        {
            StopCoroutine(_coroutine);
            _coroutine = null;
        }
    }
}