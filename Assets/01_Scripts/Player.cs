using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement; // Necesario para recargar la escena

public class Player : MonoBehaviour
{
    public float moveSpeed = 4f;
    public float jumpForce = 7f;
    public float dashSpeed = 15f;
    public float dashDuration = 0.2f;
    public float dashCooldown = 1f;

    public Rigidbody2D rb;
    public int maxJumps = 2;
    private int jumpCount;
    private bool isDashing = false;
    private bool canDash = true;
    private bool isGrounded = false;

    private float lastDashTime = -10f;

    // Variables para la interacci�n con el diamante y el muro
    public GameObject diamante;  // El diamante que el jugador debe agarrar
    public GameObject muroBloqueado;  // El muro que se desbloquear� al agarrar el diamante
    public bool tieneDiamante = false;  // Verifica si el jugador tiene el diamante

    void Start()
    {
        jumpCount = 0;
    }

    void Update()
    {
        if (!isDashing)
        {
            Jump();
            Movement();
        }
        Dash();

        // Interacci�n con el diamante cuando el jugador presiona la tecla 'E'
        if (Input.GetKeyDown(KeyCode.E) && !tieneDiamante)
        {
            AgarrarDiamante();
        }
    }

    void Jump()
    {
        if (jumpCount < maxJumps)
        {
            if (Input.GetKeyDown(KeyCode.Z))
            {
                rb.velocity = new Vector2(rb.velocity.x, 0);
                rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
                jumpCount++;
                isGrounded = false;
            }
        }
    }

    void Movement()
    {
        float x = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(x * moveSpeed, rb.velocity.y);
    }

    void Dash()
    {
        if (Input.GetKeyDown(KeyCode.C) && Time.time >= lastDashTime + dashCooldown && !isDashing && canDash)
        {
            StartCoroutine(PerformDash());
        }
    }

    IEnumerator PerformDash()
    {
        isDashing = true;
        canDash = isGrounded ? true : false;
        lastDashTime = Time.time;
        float originalGravity = rb.gravityScale;
        rb.gravityScale = 0;

        float dashDirection = Input.GetAxisRaw("Horizontal");
        if (dashDirection == 0)
        {
            dashDirection = transform.localScale.x > 0 ? 1 : -1;
        }

        rb.velocity = new Vector2(dashDirection * dashSpeed, 0);
        yield return new WaitForSeconds(dashDuration);

        rb.gravityScale = originalGravity;
        isDashing = false;

        if (!isGrounded)
        {
            canDash = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            jumpCount = 0;
            canDash = true;
            isGrounded = true;
        }

        // Detectar colisi�n con el obst�culo
        if (collision.gameObject.CompareTag("Obstaculo"))
        {
            Destroy(gameObject); // Destruir el jugador
            SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Recargar la escena actual
        }
    }

    // M�todo para agarrar el diamante
    private void AgarrarDiamante()
    {
        tieneDiamante = true;
        diamante.SetActive(false); // Desactiva el diamante cuando el jugador lo agarra
        Debug.Log("Diamante agarrado");

        // Desbloquear el muro
        if (muroBloqueado != null)
        {
            muroBloqueado.SetActive(false); // El muro desaparece
            Debug.Log("Muro desbloqueado y desaparecido");
        }
    }
}
