using System;
using UnityEngine;
using Zenject;

namespace SkillSystem
{
    public class SkillLevelManager : MonoBehaviour
    {
        private float _currentExperience;
        private float _experienceForLevelUp;
        private int _availableSkillPoints = 2;
        private PlayerStats _playerStats;
        private const float HealthModifier = 1.1f;
        private const float SpeedModifier = 1.05f;

        private const string ExperienceSave = "Experience";
        private const string ExperienceForLevelUpSave = "ExperienceForLevelUp";
        private const string AvailableSkillPointsSave = "AvailableSkillPoints";

        public event Action<float, float> OnGetExperience;
        public event Action<int> OnSkillPointAmountChanged;
        public event Action<float, float> OnHealthAmountChanged;
        public event Action<float, float> OnSpeedAmountChanged;

        [Inject]
        private void Construct(PlayerStats playerStats)
        {
            _playerStats = playerStats;
        }

        private void Awake()
        {
            Load();
        }

        private void Start()
        {
            OnGetExperience?.Invoke(_currentExperience, _experienceForLevelUp);
            OnSkillPointAmountChanged?.Invoke(_availableSkillPoints);
            OnHealthAmountChanged?.Invoke(_playerStats.GetMaxHealth(), _playerStats.GetMaxHealth() * HealthModifier);
            OnSpeedAmountChanged?.Invoke(_playerStats.GetSpeed(), _playerStats.GetSpeed() * SpeedModifier);
        }

        public void AddExperience(float amount)
        {
            _currentExperience += amount;
            TryLevelUp();
            Save();
        }

        private void TryLevelUp()
        {
            if (_currentExperience >= _experienceForLevelUp)
            {
                _availableSkillPoints++;
                _experienceForLevelUp *= 1.25f;
                Save();
            }
        }

        public void IncreaseHealth()
        {
            if (_availableSkillPoints > 0)
            {
                _availableSkillPoints--;
                OnSkillPointAmountChanged?.Invoke(_availableSkillPoints);
                _playerStats.SetMaxHealth(_playerStats.GetMaxHealth() * HealthModifier);
                OnHealthAmountChanged?.Invoke(_playerStats.GetMaxHealth(),
                    _playerStats.GetMaxHealth() * HealthModifier);
                Save();
            }
        }

        public void IncreaseSpeed()
        {
            if (_availableSkillPoints > 0)
            {
                _availableSkillPoints--;
                OnSkillPointAmountChanged?.Invoke(_availableSkillPoints);
                _playerStats.SetSpeed(_playerStats.GetSpeed() * 1.1f);
                OnSpeedAmountChanged?.Invoke(_playerStats.GetSpeed(), _playerStats.GetSpeed() * SpeedModifier);
                Save();
            }
        }

        private void Load()
        {
            var savedExperience = PlayerPrefs.GetFloat(ExperienceSave, 0);
            var savedExperienceToLevelUp = PlayerPrefs.GetFloat(ExperienceForLevelUpSave, 10);
            var savedSkillPoints = PlayerPrefs.GetInt(AvailableSkillPointsSave, 0);
            _currentExperience = savedExperience;
            _experienceForLevelUp = savedExperienceToLevelUp;
            _availableSkillPoints = savedSkillPoints;
        }

        private void Save()
        {
            PlayerPrefs.SetFloat(ExperienceSave, _currentExperience);
            PlayerPrefs.SetFloat(ExperienceForLevelUpSave, _experienceForLevelUp);
            PlayerPrefs.SetInt(AvailableSkillPointsSave, _availableSkillPoints);
            PlayerPrefs.Save();
        }

        private void OnApplicationFocus(bool hasFocus)
        {
            if (hasFocus == false)
            {
                Save();
            }
        }
    }
}