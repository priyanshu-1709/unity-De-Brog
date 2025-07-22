using Kineractive;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zoomer : MonoBehaviour
{
    [SerializeField] protected Camera _camera;
    [SerializeField] protected float _zoomStep = 5f;
    [SerializeField] protected float _zoomLerpSpeed = 10f;
    [SerializeField] protected float _minZoom = 1f;
    [SerializeField] protected float _maxZoom = 120f;
    [SerializeField] protected UnityEventThreeFloats OnZoomChange;

    protected float _defaultZoom;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        if (_camera == null)
            _camera = Camera.main;

        _defaultZoom = _camera.fieldOfView;
    }

    public virtual void ZoomIn()
    {
        float newFov = _camera.fieldOfView - _zoomStep;

        StopAllCoroutines();
        StartCoroutine(IZoomIn(newFov));
    }

    public virtual void ZoomOut()
    {
        float newFov = _camera.fieldOfView + _zoomStep;

        StopAllCoroutines();
        StartCoroutine(IZoomOut(newFov));
    }

    public virtual void ResetZoom()
    {
        
        if(_camera.fieldOfView > _defaultZoom)
        {
            float newFov = _camera.fieldOfView - _zoomStep;
            StopAllCoroutines();
            StartCoroutine(IZoomIn(newFov));
        }
        else if (_camera.fieldOfView < _defaultZoom)
        {
            float newFov = _camera.fieldOfView + _zoomStep;
            StopAllCoroutines();
            StartCoroutine(IZoomOut(newFov));

        }
    }

    public virtual void SetZoom(float zoomLevel)
    {
        if (_camera.fieldOfView > zoomLevel)
        {
            StopAllCoroutines();
            StartCoroutine(IZoomIn(zoomLevel));
        }
        else if (_camera.fieldOfView < zoomLevel)
        {
            StopAllCoroutines();
            StartCoroutine(IZoomOut(zoomLevel));

        }
    }

    protected virtual IEnumerator IZoomIn(float newFov)
    {
        while (Mathf.Abs(_camera.fieldOfView - newFov) > 0.1f)
        {
            _camera.fieldOfView = Mathf.Lerp(_camera.fieldOfView, newFov, Time.deltaTime * _zoomLerpSpeed);


            if (_camera.fieldOfView < _minZoom)
                     _camera.fieldOfView = _minZoom;
            
            SendZoomInfo();

            yield return null;

        }

        

    }


    protected virtual IEnumerator IZoomOut(float newFov)
    {
        while (Mathf.Abs(_camera.fieldOfView - newFov) > 0.1f)
        {
            _camera.fieldOfView = Mathf.Lerp(_camera.fieldOfView, newFov, Time.deltaTime * _zoomLerpSpeed);

            if (_camera.fieldOfView > _maxZoom)
                    _camera.fieldOfView = _maxZoom;

            SendZoomInfo();

            yield return null;
        }

        

 
    }

    protected virtual void SendZoomInfo()
    {
        OnZoomChange.Invoke(_minZoom, _maxZoom, _camera.fieldOfView);
    }

          
}
