using UnityEngine;
using UnityEngine.EventSystems; // Necess�rio para as interfaces de evento

public class ShootButtonHandler : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private bool isPressed = false;

    void Update()
    {
        // Se o bot�o estiver pressionado, tenta atirar
        if (isPressed)
        {
            // Chama a fun��o de tiro no singleton do PlayerShooting
            PlayerShooting.instance.TryToShoot();
        }
    }

    // Esta fun��o � chamada quando o bot�o � pressionado
    public void OnPointerDown(PointerEventData eventData)
    {
        isPressed = true;
    }

    // Esta fun��o � chamada quando o bot�o � solto
    public void OnPointerUp(PointerEventData eventData)
    {
        isPressed = false;
    }
}