// PlayerMovement.cs
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Movimentação")]
    public float moveSpeed = 5f;
    private Rigidbody2D rb;
    private Vector2 moveInput;
    public Vector2 lastMoveDirection { get; private set; }

    [Header("Referências do Joystick")]
    public Joystick joystick; // Arraste o componente Joystick aqui no Inspector

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0;
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
    }

    void Start()
    {
        lastMoveDirection = Vector2.right;
    }

    void Update()
    {
        // Usa os eixos do joystick em vez do Input.GetAxisRaw
        // O joystick já retorna um valor entre -1 e 1, então funciona perfeitamente
        float moveX = joystick.Horizontal;
        float moveY = joystick.Vertical;
        moveInput = new Vector2(moveX, moveY); // A saída do joystick já é um vetor, não precisa normalizar aqui

        // Se o jogador estiver se movendo, atualiza a última direção
        if (moveInput != Vector2.zero)
        {
            lastMoveDirection = moveInput.normalized;
        }
    }

    void FixedUpdate()
    {
        // O moveInput do joystick pode não ser normalizado, então normalizamos aqui
        // para garantir velocidade constante em todas as direções.
        rb.linearVelocity = moveInput.normalized * moveSpeed;
    }
}