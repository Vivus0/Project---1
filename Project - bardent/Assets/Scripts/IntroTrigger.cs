using System.Collections;
using UnityEngine;

public class IntroTrigger : MonoBehaviour
{
    public Animator introAnimator;  // Назначьте эту переменную в инспекторе
    public PlayerController playerController;  // Ссылка на ваш PlayerController

    private void Start()
    {
        StartCoroutine(PlayIntroAnimation());  // Запуск корутины для воспроизведения вступительной анимации
    }

    IEnumerator PlayIntroAnimation()
    {
        // Воспроизведение вступительной анимации
        introAnimator.SetTrigger("PlayIntro");  // Предполагается, что у вас есть триггер анимации "PlayIntro"

        // Ожидание завершения вступительной анимации
        yield return new WaitForSeconds(3f);  // Замените на реальную длительность вашей вступительной анимации

        // Включение управления персонажем после завершения вступительной анимации
        playerController.EnablePlayerControl();
    }
}
