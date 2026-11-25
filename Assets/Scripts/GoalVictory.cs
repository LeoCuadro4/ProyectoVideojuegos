using UnityEngine;

public class GoalVictory : MonoBehaviour
{
    public bool HasWon { get; private set; }

    private Collider2D goalCollider;
    private GUIStyle victoryStyle;

    private void Awake()
    {
        goalCollider = GetComponent<Collider2D>();
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (HasWon)
            return;

        PlayerMovement player = other.GetComponentInParent<PlayerMovement>();
        if (player == null || goalCollider == null || !goalCollider.OverlapPoint(player.transform.position))
            return;

        HasWon = true;
    }

    private void OnGUI()
    {
        if (!HasWon)
            return;

        if (victoryStyle == null)
        {
            victoryStyle = new GUIStyle(GUI.skin.label);
            victoryStyle.alignment = TextAnchor.MiddleCenter;
            victoryStyle.fontSize = Mathf.Clamp(Screen.height / 20, 24, 56);
            victoryStyle.fontStyle = FontStyle.Bold;
            victoryStyle.normal.textColor = Color.white;
        }

        float width = Mathf.Min(520f, Screen.width - 32f);
        Rect banner = new Rect((Screen.width - width) * 0.5f, Screen.height * 0.1f, width, 88f);
        GUI.Box(banner, GUIContent.none);
        GUI.Label(banner, "¡Nivel completado!", victoryStyle);
    }
}
