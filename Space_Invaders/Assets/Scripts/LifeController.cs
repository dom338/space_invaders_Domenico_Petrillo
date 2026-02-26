using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LifeController : MonoBehaviour
{

    public int life;
    public float invincibleTime;
    public bool invincible;
    public enum kill_consequence { none, destroyObject, reloadScene }
    public kill_consequence killConsequence;

    public void damage(int takeDamage = 1, bool skipinvincible = false)
    {
        if (!invincible || skipinvincible)
        {
            life -= takeDamage;
            invincible = true;
            StartCoroutine(ResetInvincible_Corutine());

            if (life <= 0)
            {
                life = 0;
                kill();
            }

        }

    }

    public void kill()
    {
        switch (killConsequence)
        {
            case kill_consequence.destroyObject:
                Destroy(gameObject);
                break;
            case kill_consequence.reloadScene:
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                break;
        }


    }

    IEnumerator ResetInvincible_Corutine()
    {

        yield return new WaitForSeconds(invincibleTime);

        invincible = false;
    }
}
