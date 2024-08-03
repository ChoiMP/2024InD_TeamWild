using UnityEngine;
using UnityEngine.UI;

public class StageManager : MonoBehaviour
{
    public Button stage1Button;
    public Button stage2Button;
    public Button stage3Button;

    private const string Stage1ClearedKey = "Stage1Cleared";
    private const string Stage2ClearedKey = "Stage2Cleared";
    private const string Stage3ClearedKey = "Stage3Cleared";

    private void Start()
    {
        // Load the saved stage clear states
        bool isStage1Cleared = PlayerPrefs.GetInt(Stage1ClearedKey, 0) == 1;
        bool isStage2Cleared = PlayerPrefs.GetInt(Stage2ClearedKey, 0) == 1;
        bool isStage3Cleared = PlayerPrefs.GetInt(Stage3ClearedKey, 0) == 1;

        // Set up buttons based on saved states
        UpdateButtonStates(isStage1Cleared, isStage2Cleared, isStage3Cleared);

        // Add button listeners
        stage1Button.onClick.AddListener(() => ClearStage(1));
        stage2Button.onClick.AddListener(() => ClearStage(2));
        stage3Button.onClick.AddListener(() => ClearStage(3));
    }

    private void UpdateButtonStates(bool isStage1Cleared, bool isStage2Cleared, bool isStage3Cleared)
    {
        stage1Button.interactable = true; // Stage 1 is always interactable
        stage2Button.interactable = isStage1Cleared; // Stage 2 is interactable only if Stage 1 is cleared
        stage3Button.interactable = isStage2Cleared; // Stage 3 is interactable only if Stage 2 is cleared
    }

    public void ClearStage(int stage)
    {
        switch (stage)
        {
            case 1:
                PlayerPrefs.SetInt(Stage1ClearedKey, 1);
                break;
            case 2:
                PlayerPrefs.SetInt(Stage2ClearedKey, 1);
                break;
            case 3:
                PlayerPrefs.SetInt(Stage3ClearedKey, 1);
                break;
        }
        PlayerPrefs.Save(); // Save the changes to PlayerPrefs

        // Update button states after clearing a stage
        UpdateButtonStates(
            PlayerPrefs.GetInt(Stage1ClearedKey, 0) == 1,
            PlayerPrefs.GetInt(Stage2ClearedKey, 0) == 1,
            PlayerPrefs.GetInt(Stage3ClearedKey, 0) == 1
        );
    }
}
