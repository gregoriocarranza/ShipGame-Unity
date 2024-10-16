using UnityEngine;

public class PlanetBehavior : MonoBehaviour
{
    public CelestialBodyData celestialData;  // Referencia al ScriptableObject
    public Transform target;
    private LineRenderer lineRenderer;
    public float lineWidth = 0.01f;

    private bool pause = false;
    private float currentAngle = 0;

    private void OnEnable()
    {
        UIManager.Pause += OnPause;
    }

    private void Awake()
    {
        if (celestialData == null)
        {
            Debug.LogError("No se ha asignado CelestialBodyData a " + gameObject.name);
            return;
        }

        // Configurar las propiedades del planeta/luna desde el ScriptableObject
        ApplyCelestialData();

        // Ajustar escala basada en el radio y la escala general del cuerpo
        transform.localScale = celestialData.scale * celestialData.radius;

        // Ajustar la posición inicial usando el offset y el target
        if (target != null)
        {
            transform.position = target.position + celestialData.positionOffset;
        }
        else
        {
            Debug.LogWarning("No se ha asignado un target para " + celestialData.bodyName);
        }
    }

    private void Start()
    {
        if (celestialData.hasOrbit && target != null)
        {
            // Configurar LineRenderer para la órbita solo si el objeto tiene órbita
            lineRenderer = gameObject.AddComponent<LineRenderer>();
            lineRenderer.positionCount = 50;
            lineRenderer.startWidth = lineWidth;
            lineRenderer.endWidth = lineWidth;

            Material newMaterial = new Material(Shader.Find("Unlit/Color"));
            newMaterial.color = celestialData.orbitLineColor;
            lineRenderer.material = newMaterial;

            // Calcular la órbita inicial
            CalculateOrbitPath();
        }
    }

    private void ApplyCelestialData()
    {
        // Cambiar el nombre del GameObject según los datos
        gameObject.name = celestialData.bodyName;
    }

    private void FixedUpdate()
    {
        if (!pause)
        {
            // Rotar el objeto en torno a su propio eje
            transform.Rotate(0, celestialData.rotationSpeed * Time.deltaTime, 0);

            // Si el objeto tiene órbita, actualiza su posición en la órbita
            if (celestialData.hasOrbit && target != null)
            {
                UpdateOrbitPosition();
            }
        }
    }

    private void CalculateOrbitPath()
    {
        // Calcular la trayectoria de la órbita solo si el objeto tiene órbita
        for (int i = 0; i < lineRenderer.positionCount; i++)
        {
            float angle = i * Mathf.PI * 2f / lineRenderer.positionCount;
            float x = celestialData.semiMajorAxis * Mathf.Cos(angle);
            float z = celestialData.semiMinorAxis * Mathf.Sin(angle);
            lineRenderer.SetPosition(i, new Vector3(x, 0f, z) + target.position);
        }
    }

    private void UpdateOrbitPosition()
    {
        currentAngle += celestialData.orbitSpeed * Time.deltaTime;
        float x = celestialData.semiMajorAxis * Mathf.Cos(currentAngle);
        float z = celestialData.semiMinorAxis * Mathf.Sin(currentAngle);
        transform.position = new Vector3(x, 0, z) + target.position;
    }

    public void OnPause(bool pauses)
    {
        pause = pauses;
    }

    private void OnDisable()
    {
        UIManager.Pause -= OnPause;
    }
}
