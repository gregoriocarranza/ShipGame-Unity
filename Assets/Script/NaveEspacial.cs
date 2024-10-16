using UnityEngine;

public class NaveEspacial : MonoBehaviour
{
    public string ShipName = "SpaceShip";
    public float velocidad = 10.0f;
    public float rotacion = 100.0f;
    public float cabeceo = 5f;
    public float alabeo = 5f;
    public float cabeceoMaximo = 45.0f;
    public float alabeoMaximo = 45.0f;
    private Rigidbody rb;
    private float cabeceoActual = 0.0f;
    private float alabeoActual = 0.0f;

    private bool pause = false;

    private Vector3 posicionInicial;
    private Quaternion rotacionInicial;

    public bool naveIniciada = false;
    public Vector3 movimiento;
    public bool movimientoHabilitado = false;
    public GUIStyle style = new GUIStyle();
    private Vector3 playerDirection;
    private Vector3 velocidadGuardada;
    private Vector3 velocidadAngularGuardada;

    private void OnEnable()
    {
        UIManager.Pause += OnPause;
    }

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            Debug.LogError("Rigidbody no asignado en la nave: " + gameObject.name);
        }

        rb.useGravity = false;
        rb.isKinematic = false;

        posicionInicial = transform.position;
        rotacionInicial = transform.rotation;
    }

    void FixedUpdate()
    {
        if (!pause && movimientoHabilitado)
        {
            if (!naveIniciada && Input.GetKeyDown(KeyCode.Space))
            {
                naveIniciada = true;
            }

            if (naveIniciada)
            {
                float moveHorizontal = Input.GetAxis("Horizontal");
                float moveVertical = Input.GetAxis("Vertical");

                movimiento = transform.forward * moveVertical * velocidad * Time.deltaTime;
                movimiento += transform.right * moveHorizontal * velocidad * Time.deltaTime;
                rb.AddForce(movimiento, ForceMode.Acceleration);

                float rotacionY = 0.0f;
                if (Input.GetKey(KeyCode.E))
                {
                    rotacionY = 1.0f;
                }
                else if (Input.GetKey(KeyCode.Q))
                {
                    rotacionY = -1.0f;
                }

                rb.AddTorque(Vector3.up * rotacionY * rotacion * Time.deltaTime, ForceMode.Acceleration);

                float cabeceoDeseado = Input.GetAxis("Mouse Y") * cabeceo * Time.deltaTime;
                cabeceoActual = Mathf.Clamp(cabeceoActual + cabeceoDeseado, -cabeceoMaximo, cabeceoMaximo);
                Quaternion rotacionCabeceo = Quaternion.Euler(cabeceoActual, 0, 0);

                float alabeoDeseado = Input.GetAxis("Mouse X") * alabeo * Time.deltaTime;
                alabeoActual = Mathf.Clamp(alabeoActual + alabeoDeseado, -alabeoMaximo, alabeoMaximo);
                Quaternion rotacionAlabeo = Quaternion.Euler(0, 0, -alabeoActual);

                Quaternion rotacionFinal = rotacionAlabeo * rotacionCabeceo * transform.rotation;
                transform.rotation = rotacionFinal;

                if (Input.GetKeyDown(KeyCode.R))
                {
                    ResetNave();
                }
            }
        }
        else if (pause)
        {
            // Guardar las velocidades antes de pausar
            velocidadGuardada = rb.velocity;
            velocidadAngularGuardada = rb.angularVelocity;

            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            rb.isKinematic = true;
        }
    }

    void OnGUI()
    {
        if (!movimientoHabilitado)
        {
            Vector3 screenPos = Camera.main.WorldToScreenPoint(rb.position);

            style.normal.textColor = Color.white;
            style.fontSize = 14;

            Vector2 textSize = style.CalcSize(new GUIContent(ShipName));
            float padding = 10.0f;

            GUI.Label(
                new Rect(screenPos.x + padding, Screen.height - screenPos.y + padding, textSize.x, textSize.y),
                ShipName,
                style
            );
            GUI.Label(new Rect(10, 10, 200, 20), "Velocidad: " + rb.velocity.ToString("F2"), style);
        }
    }

    void OnDrawGizmos()
    {
        if (rb != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(rb.position + new Vector3(0, 0.5f, 0), rb.position + new Vector3(0, 0.5f, 0) + rb.velocity);
        }
    }

    void ResetNave()
    {
        transform.position = posicionInicial;
        transform.rotation = rotacionInicial;
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        cabeceoActual = 0f;
        alabeoActual = 0f;
        naveIniciada = false;
    }

    public void OnPause(bool pauses)
    {
        pause = pauses;
        if (!pause)
        {
            rb.isKinematic = false;
            rb.velocity = velocidadGuardada;
            rb.angularVelocity = velocidadAngularGuardada;
        }
    }

    private void OnDisable()
    {
        UIManager.Pause -= OnPause;
    }
}
