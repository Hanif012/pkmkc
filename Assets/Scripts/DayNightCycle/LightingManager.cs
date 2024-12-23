using UnityEngine;

[ExecuteAlways]
public class LightingManager : MonoBehaviour
{
    // Scene References
    [SerializeField] private Light DirectionalLight;
    [SerializeField] private LightingPreset Preset;

    // Variables
    [SerializeField, Range(0, 24)] private float TimeOfDay;
    [SerializeField] private float maxIntensity = 1.0f; // Maximum intensity during the day
    [SerializeField] private float minIntensity = 0.2f; // Minimum intensity during the night
    [SerializeField] private float transitionStartTime = 18f; // Time when transition starts
    [SerializeField] private float transitionEndTime = 6f; // Time when transition ends (next day)

    private void Update()
    {
        if (Preset == null)
            return;

        if (Application.isPlaying)
        {
            // Simulate passing time
            TimeOfDay += Time.deltaTime * 0.05f;
            TimeOfDay %= 24; // Modulus to ensure always between 0-24
            UpdateLighting(TimeOfDay / 24f);
        }
        else
        {
            UpdateLighting(TimeOfDay / 24f);
        }
    }

    private void UpdateLighting(float timePercent)
    {
        // Set ambient and fog
        RenderSettings.ambientLight = Preset.AmbientColor.Evaluate(timePercent);
        RenderSettings.fogColor = Preset.FogColor.Evaluate(timePercent);

        // Adjust Directional Light properties
        if (DirectionalLight != null)
        {
            DirectionalLight.color = Preset.DirectionalColor.Evaluate(timePercent);
            DirectionalLight.transform.localRotation = Quaternion.Euler(new Vector3((timePercent * 360f) - 90f, 170f, 0));

            // Calculate light intensity transition
            float intensity = CalculateLightIntensity(TimeOfDay);
            DirectionalLight.intensity = intensity;
        }
    }

    private float CalculateLightIntensity(float currentTime)
    {
        // Transition logic: smoothly interpolate intensity based on time
        if (currentTime >= transitionStartTime || currentTime < transitionEndTime)
        {
            // Nighttime transition
            float t = (currentTime >= transitionStartTime)
                ? (currentTime - transitionStartTime) / (24f - transitionStartTime + transitionEndTime)
                : currentTime / transitionEndTime;

            return Mathf.Lerp(maxIntensity, minIntensity, t);
        }
        else if (currentTime >= transitionEndTime && currentTime < 12f)
        {
            // Daytime transition
            float t = (currentTime - transitionEndTime) / (12f - transitionEndTime);
            return Mathf.Lerp(minIntensity, maxIntensity, t);
        }

        // Full daytime intensity
        return maxIntensity;
    }

    // Try to find a directional light to use if we haven't set one
    private void OnValidate()
    {
        if (DirectionalLight != null)
            return;

        // Search for lighting tab sun
        if (RenderSettings.sun != null)
        {
            DirectionalLight = RenderSettings.sun;
        }
        // Search scene for light that fits criteria (directional)
        else
        {
            Light[] lights = GameObject.FindObjectsOfType<Light>();
            foreach (Light light in lights)
            {
                if (light.type == LightType.Directional)
                {
                    DirectionalLight = light;
                    return;
                }
            }
        }
    }
}
