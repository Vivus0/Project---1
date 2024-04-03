using System.Collections;
using UnityEngine;

public class IntroTrigger : MonoBehaviour
{
    public Animator introAnimator;  // ��������� ��� ���������� � ����������
    public PlayerController playerController;  // ������ �� ��� PlayerController

    private void Start()
    {
        StartCoroutine(PlayIntroAnimation());  // ������ �������� ��� ��������������� ������������� ��������
    }

    IEnumerator PlayIntroAnimation()
    {
        // ��������������� ������������� ��������
        introAnimator.SetTrigger("PlayIntro");  // ��������������, ��� � ��� ���� ������� �������� "PlayIntro"

        // �������� ���������� ������������� ��������
        yield return new WaitForSeconds(3f);  // �������� �� �������� ������������ ����� ������������� ��������

        // ��������� ���������� ���������� ����� ���������� ������������� ��������
        playerController.EnablePlayerControl();
    }
}
