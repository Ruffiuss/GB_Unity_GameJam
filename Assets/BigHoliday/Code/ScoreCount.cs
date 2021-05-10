using UnityEngine;
using UnityEngine.UI;
using System;

namespace BigHoliday
{
    public class ScoreCount : IUpdatable
    {
        #region Fields

        private Text _text;

        #endregion


        #region Properties

        public int CurrentScore { get; private set; }

        #endregion


        #region ClassLifeCycles

        public ScoreCount(Text text)
        {
            _text = text;
        }

        #endregion


        #region Methods

        public void Update(float deltaTime)
        {
            _text.text = $"Score: {CurrentScore}";
        }

        public void AddScore(int value)
        {
            CurrentScore += value;
        }

        #endregion
    }
}