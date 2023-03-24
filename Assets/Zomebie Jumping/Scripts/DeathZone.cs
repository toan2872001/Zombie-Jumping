using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathZone : MonoBehaviour
{
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag(GameTag.Player.ToString()))
        {
            Destroy(collision.gameObject);
            if (GameManager.Ins)
            {
                GameManager.Ins.state = GameState.Gameover;
            }
            if(GuiManager.Ins || GuiManager.Ins.gameoverDiaglog)
            {
                GuiManager.Ins.gameoverDiaglog.Show(true);
            }
            if (AudioController.Ins)
            {
                AudioController.Ins.PlaySound(AudioController.Ins.gameover);
            }

        }

        if (collision.CompareTag(GameTag.Platform.ToString()))
        {
            Destroy(collision.gameObject);
        }
    }
}
