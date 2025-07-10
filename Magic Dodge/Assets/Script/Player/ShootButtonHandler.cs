using UnityEngine;
using UnityEngine.EventSystems; // Necessário para as interfaces de evento

public class ShootButtonHandler : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private bool isPressed = false;

    void Update()
    {
        // Se o botão estiver pressionado, tenta atirar
        if (isPressed)
        {
            // Chama a função de tiro no singleton do PlayerShooting
            PlayerShooting.instance.TryToShoot();
        }
    }

    // Esta função é chamada quando o botão é pressionado
    public void OnPointerDown(PointerEventData eventData)
    {
        isPressed = true;
    }

    // Esta função é chamada quando o botão é solto
    public void OnPointerUp(PointerEventData eventData)
    {
        isPressed = false;
    }
}