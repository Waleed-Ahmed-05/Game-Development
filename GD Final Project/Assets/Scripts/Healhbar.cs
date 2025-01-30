using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour
{
    [SerializeField] private Image Health;
    private Camera _camera;

    public void UpdateHealhBar(float maxHealth, float currentHealth)
    {
        Health.fillAmount = currentHealth / maxHealth;
    }
    private void Start()
    {
        _camera = Camera.main;
    }
    void Update()
    {
        transform.rotation = Quaternion.LookRotation(transform.position - _camera.transform.position);
    }
}
