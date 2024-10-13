using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CameraPathVisuals
{
    public Color pathColor = Color.green;
    public Color inactivePathColor = Color.gray;
    public Color frustrumColor = Color.white;
    public Color handleColor = Color.yellow;
}

public enum EnumCurveType
{
    EaseInAndOut,
    Linear,
    Custom
}

public enum LoopCompletionAction
{
    Continue,
    Stop
}

[System.Serializable]
public class CameraPoint
{
    public Vector3 position;
    public Quaternion rotation;
    [Range(1, 179)] public float fieldOfView = 60;
    public Vector3 handleprev = Vector3.back;
    public Vector3 handlenext = Vector3.forward;
    public EnumCurveType curveTypeRotation = EnumCurveType.EaseInAndOut;
    public AnimationCurve rotationCurve = AnimationCurve.EaseInOut(0, 0, 1, 1);
    public EnumCurveType curveTypePosition = EnumCurveType.Linear;
    public AnimationCurve positionCurve = AnimationCurve.Linear(0, 0, 1, 1);
    public EnumCurveType curveTypeFOV = EnumCurveType.Linear;
    public AnimationCurve fovCurve = AnimationCurve.Linear(0, 0, 1, 1);
    public bool chained = true;

    public CameraPoint(Vector3 pos, Quaternion rot, float fov)
    {
        position = pos;
        rotation = rot;
        fieldOfView = fov;
    }
}

public class CameraPath : MonoBehaviour
{
    [Header("Camera Settings")]
    public bool useMainCamera = true;
    public Camera selectedCamera;
    public bool lookAtTarget = false;
    public Transform target;

    [Header("Playback Settings")]
    public bool playOnAwake = false;
    public float playOnAwakeTime = 10;
    public bool looped = false;
    public LoopCompletionAction afterLoop = LoopCompletionAction.Continue;

    [Header("Visualization Settings")]
    public CameraPathVisuals visual;
    public bool alwaysShow = true;

    [Header("Camera Points")]
    public List<CameraPoint> points = new List<CameraPoint>();

    private int currentWaypointIndex;
    private float currentTimeInWaypoint;
    private float timePerSegment;
    private bool paused = false;
    private bool playing = false;

    void Start()
    {
        InitializeCamera();
        ValidatePointsCurves();

        if (playOnAwake)
        {
            PlayPath(playOnAwakeTime);
        }
    }

    private void InitializeCamera()
    {
        if (Camera.main == null)
        {
            Debug.LogError("There is no main camera in the scene!");
        }

        if (useMainCamera)
        {
            selectedCamera = Camera.main;
        }
        else if (selectedCamera == null)
        {
            selectedCamera = Camera.main;
            Debug.LogError("No camera selected for following path, defaulting to main camera");
        }

        if (lookAtTarget && target == null)
        {
            lookAtTarget = false;
            Debug.LogError("No target selected to look at, defaulting to normal rotation");
        }
    }

    private void ValidatePointsCurves()
    {
        foreach (var point in points)
        {
            point.rotationCurve = GetCurve(point.curveTypeRotation);
            point.positionCurve = GetCurve(point.curveTypePosition);
        }
    }

    private AnimationCurve GetCurve(EnumCurveType curveType)
    {
        switch (curveType)
        {
            case EnumCurveType.EaseInAndOut:
                return AnimationCurve.EaseInOut(0, 0, 1, 1);
            case EnumCurveType.Linear:
                return AnimationCurve.Linear(0, 0, 1, 1);
            default:
                return AnimationCurve.Linear(0, 0, 1, 1);
        }
    }

    public void PlayPath(float time)
    {
        if (time <= 0) time = 0.001f;
        paused = false;
        playing = true;
        StopAllCoroutines();
        StartCoroutine(FollowPath(time));
    }

    public void StopPath()
    {
        playing = false;
        paused = false;
        StopAllCoroutines();
    }

    public void PausePath()
    {
        paused = true;
        playing = false;
    }

    public void ResumePath()
    {
        if (paused)
        {
            playing = true;
            paused = false;
        }
    }

    public bool IsPaused() => paused;
    public bool IsPlaying() => playing;
    public int GetCurrentWayPoint() => currentWaypointIndex;
    public float GetCurrentTimeInWaypoint() => currentTimeInWaypoint;
    public void SetCurrentWayPoint(int value) => currentWaypointIndex = value;
    public void SetCurrentTimeInWaypoint(float value) => currentTimeInWaypoint = value;

    public void RefreshTransform()
    {
        selectedCamera.transform.position = GetBezierPosition(currentWaypointIndex, currentTimeInWaypoint);
        selectedCamera.fieldOfView = GetLerpFOV(currentWaypointIndex, currentTimeInWaypoint);
        if (lookAtTarget)
        {
            Vector3 directionToTarget = (target.position - selectedCamera.transform.position).normalized;
            selectedCamera.transform.rotation = Quaternion.LookRotation(directionToTarget);
        }
        else
        {
            selectedCamera.transform.rotation = GetLerpRotation(currentWaypointIndex, currentTimeInWaypoint);
        }
    }

    private IEnumerator FollowPath(float time)
    {
        UpdateTimeInSeconds(time);
        currentWaypointIndex = 0;

        while (currentWaypointIndex < points.Count)
        {
            currentTimeInWaypoint = 0;
            while (currentTimeInWaypoint < 1)
            {
                if (!paused)
                {
                    currentTimeInWaypoint += Time.deltaTime / timePerSegment;
                    UpdateCameraTransform(currentWaypointIndex, currentTimeInWaypoint);
                }
                yield return null;
            }
            ++currentWaypointIndex;
            HandleLoop();
        }
        StopPath();
    }

    private void UpdateCameraTransform(int pointIndex, float time)
    {
        selectedCamera.transform.position = GetBezierPosition(pointIndex, time);
        selectedCamera.fieldOfView = GetLerpFOV(pointIndex, time);
        if (lookAtTarget)
        {
            Vector3 directionToTarget = (target.position - selectedCamera.transform.position).normalized;
            selectedCamera.transform.rotation = Quaternion.LookRotation(directionToTarget);
        }
        else
        {
            selectedCamera.transform.rotation = GetLerpRotation(pointIndex, time);
        }
    }

    private void HandleLoop()
    {
        if (currentWaypointIndex == points.Count - 1 && !looped) return;
        if (currentWaypointIndex == points.Count && afterLoop == LoopCompletionAction.Continue)
        {
            currentWaypointIndex = 0;
        }
    }

    public void UpdateTimeInSeconds(float seconds)
    {
        timePerSegment = seconds / ((looped) ? points.Count : points.Count - 1);
    }

    private int GetNextIndex(int index) => (index == points.Count - 1) ? 0 : index + 1;

    private Vector3 GetBezierPosition(int pointIndex, float time)
    {
        float t = points[pointIndex].positionCurve.Evaluate(time);
        int nextIndex = GetNextIndex(pointIndex);
        Vector3 p0 = points[pointIndex].position;
        Vector3 p1 = p0 + points[pointIndex].handlenext;
        Vector3 p2 = points[nextIndex].position + points[nextIndex].handleprev;
        Vector3 p3 = points[nextIndex].position;

        return Vector3.Lerp(
            Vector3.Lerp(Vector3.Lerp(p0, p1, t), Vector3.Lerp(p1, p2, t), t),
            Vector3.Lerp(Vector3.Lerp(p1, p2, t), Vector3.Lerp(p2, p3, t), t), t);
    }

    private Quaternion GetLerpRotation(int pointIndex, float time)
    {
        return Quaternion.LerpUnclamped(points[pointIndex].rotation, points[GetNextIndex(pointIndex)].rotation, points[pointIndex].rotationCurve.Evaluate(time));
    }

    private float GetLerpFOV(int pointIndex, float time)
    {
        return Mathf.Lerp(points[pointIndex].fieldOfView, points[GetNextIndex(pointIndex)].fieldOfView, points[pointIndex].positionCurve.Evaluate(time));
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        if (UnityEditor.Selection.activeGameObject == gameObject || alwaysShow)
        {
            DrawPath();
            DrawFrustums();
        }
    }

    private void DrawPath()
    {
        if (points.Count < 2) return;
        for (int i = 0; i < points.Count; i++)
        {
            int nextIndex = (i < points.Count - 1) ? i + 1 : (looped ? 0 : -1);
            if (nextIndex == -1) break;
            DrawBezier(points[i], points[nextIndex]);
        }
    }

    private void DrawBezier(CameraPoint pointA, CameraPoint pointB)
    {
        UnityEditor.Handles.DrawBezier(
            pointA.position, pointB.position,
            pointA.position + pointA.handlenext,
            pointB.position + pointB.handleprev,
            (UnityEditor.Selection.activeGameObject == gameObject) ? visual.pathColor : visual.inactivePathColor,
            null, 5);
    }

    private void DrawFrustums()
    {
        foreach (var point in points)
        {
            Gizmos.matrix = Matrix4x4.TRS(point.position, point.rotation, Vector3.one);
            Gizmos.color = visual.frustrumColor;
            Gizmos.DrawFrustum(Vector3.zero, 90f, 0.25f, 0.01f, 1.78f);
        }
        Gizmos.matrix = Matrix4x4.identity;
    }
#endif
}