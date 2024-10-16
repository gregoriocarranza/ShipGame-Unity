using UnityEngine;

[CreateAssetMenu(fileName = "CelestialBodyData", menuName = "Celestial Bodies/Celestial Body Data")]
public class CelestialBodyData : ScriptableObject
{
    // Identificación y parámetros de órbita
    public string bodyName;                         // Nombre del planeta/luna
    public bool hasOrbit = true;                    // Si tiene órbita o no
    public float rotationSpeed;                     // Velocidad de rotación (grados/segundo)
    public float orbitSpeed;                        // Velocidad de la órbita (grados/segundo)
    public float semiMajorAxis;                     // Semi-eje mayor de la órbita (en unidades)
    public float semiMinorAxis;                     // Semi-eje menor de la órbita (en unidades)
    public Color orbitLineColor = Color.white;      // Color de la línea de la órbita

    // Dimensiones del cuerpo celeste
    public float radius;                            // Radio del planeta/luna (en unidades)
    public float mass;                              // Masa del cuerpo celeste (en kg o cualquier unidad)

    // Opcionales para más detalle
    public Vector3 scale = Vector3.one;             // Escala para representar el cuerpo celeste (usualmente en (1, 1, 1))
    public Vector3 positionOffset = Vector3.zero;   // Offset para ajustar la posición inicial en el sistema
}
