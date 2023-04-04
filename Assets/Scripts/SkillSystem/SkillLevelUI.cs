using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace SkillSystem
{
    public class SkillLevelUI : MonoBehaviour
    {
        [SerializeField] private Slider experienceBar;
        [SerializeField] private TextMeshProUGUI skillPoints;
        [SerializeField] private TextMeshProUGUI currentHealth, nextHealth;
        [SerializeField] private TextMeshProUGUI currentSpeed, nextSpeed;
        [SerializeField] private Button increaseHealth, increaseSpeed;
        private SkillLevelManager _skillLevelManager;

        [Inject]
        private void Construct(SkillLevelManager skillLevelManager)
        {
            _skillLevelManager = skillLevelManager;
        }

        private void Awake()
        {
            increaseHealth.onClick.AddListener(() => { _skillLevelManager.IncreaseHealth(); });
            increaseSpeed.onClick.AddListener(() => { _skillLevelManager.IncreaseSpeed(); });

            _skillLevelManager.OnGetExperience += UpdateExperience;
            _skillLevelManager.OnSkillPointAmountChanged += UpdateSkillPoints;
            _skillLevelManager.OnHealthAmountChanged += UpdateHealth;
            _skillLevelManager.OnSpeedAmountChanged += UpdateSpeed;
        }

        private void UpdateExperience(float current, float forLevelUp)
        {
            experienceBar.value = current;
            experienceBar.maxValue = forLevelUp;
        }

        private void UpdateSkillPoints(int points)
        {
            skillPoints.text = points.ToString();
        }

        private void UpdateHealth(float current, float next)
        {
            currentHealth.text = current.ToString("#.##");
            nextHealth.text = next.ToString("#.##");
        }

        private void UpdateSpeed(float current, float next)
        {
            currentSpeed.text = current.ToString("#.##");
            nextSpeed.text = next.ToString("#.##");
        }

        private void OnDestroy()
        {
            _skillLevelManager.OnGetExperience -= UpdateExperience;
            _skillLevelManager.OnSkillPointAmountChanged -= UpdateSkillPoints;
            _skillLevelManager.OnHealthAmountChanged -= UpdateHealth;
            _skillLevelManager.OnSpeedAmountChanged -= UpdateSpeed;
        }
    }
}