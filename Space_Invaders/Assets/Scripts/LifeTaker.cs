using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]


public class LifeTaker : MonoBehaviour
{

  public int takeDamage = 1;
  public bool skipinvincible = false;
  public List<string> tags;

  private void OnTriggerEnter2D(Collider2D collision)
  {
    bool tagExist = false;

    foreach (string tag in tags)
    {
      if (collision.CompareTag(tag))
      {
        tagExist = true;
        break;
      }
    }

    if (tagExist)
    {
      collision.GetComponent<LifeController>()?.damage(takeDamage, skipinvincible);
    }

  }
  // Start is called once before the first execution of Update after the MonoBehaviour is created
  void Start()
  {

  }

  // Update is called once per frame
  void Update()
  {

  }
}
