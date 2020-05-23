using UnityEngine;
using System.Collections;

/// <summary>
/// Creating instance of sounds from code with no effort
/// </summary>
public class SoundEffectsHelper : MonoBehaviour
{

  /// <summary>
  /// Singleton
  /// </summary>
  public static SoundEffectsHelper Instance;

  public AudioClip playerJumpSound;
  public AudioClip playerWalkSound;
  public AudioClip playerDamageSound;
  public AudioClip playerAttackSound;
  public AudioClip shotSound;
  public AudioClip fallingEnemySound;


  void Awake()
  {
    // Register the singleton
    if (Instance != null)
    {
      Debug.LogError("Multiple instances of SoundEffectsHelper!");
    }
    Instance = this;
  }

  public void MakePlayerAttackSound()
  {
    MakeSound(playerAttackSound);
  }

  public void MakeShotSound()
  {
    MakeSound(shotSound);
  }

  public void MakePlayerJumpSound()
  {
    MakeSound(playerJumpSound);
  }

  public void MakePlayerWalkSound()
  {
    MakeSound(playerWalkSound);
  }

  public void MakePlayerDamageSound()
  {
    MakeSound(playerDamageSound);
  }

  public void MakeFallingEnemySound()
  {
    MakeSound(fallingEnemySound);
  }

  /// <summary>
  /// Play a given sound
  /// </summary>
  /// <param name="originalClip"></param>
  private void MakeSound(AudioClip originalClip)
  {
    // As it is not 3D audio clip, position doesn't matter.
    AudioSource.PlayClipAtPoint(originalClip, transform.position);
  }
}