using UnityEngine;
using DG.Tweening;

public class EnemuAnimation : MonoBehaviour
{
    [Header("Configura��o do Tremor")]
    // A for�a do tremor. Qu�o longe ele pode se mover do ponto original.
    public float shakeStrength = 0.1f;

    // A dura��o de cada "tremida".
    public float shakeDuration = 1f;

    // Qu�o "irregular" � o tremor. Valores mais altos s�o mais irregulares.
    public int shakeVibrato = 10;

    void Start()
    {
        // Inicia a anima��o de tremor.
        StartShakeAnimation();
    }

    void StartShakeAnimation()
    {
        // transform.DOShakePosition() � a fun��o para criar o tremor.
        // O primeiro par�metro � a dura��o de cada ciclo de tremor.
        // O segundo � a for�a (dist�ncia m�xima do centro).
        // O terceiro � a "vibra��o" (quantas vezes ele muda de dire��o).
        transform.DOShakePosition(shakeDuration, shakeStrength, shakeVibrato)
            // .SetLoops(-1) faz com que, ao terminar um ciclo de tremor, ele comece outro imediatamente.
            .SetLoops(-1)
            // .SetEase(Ease.Linear) mant�m a intensidade do tremor constante.
            .SetEase(Ease.Linear);
    }
}
