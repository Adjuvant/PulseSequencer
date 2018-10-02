using UnityEngine;
using System.Collections;
using Entitas;

namespace ButtonBehaviours 
{
    public class CycleMaterialColour : AbstractListenerBehaviour, IButtonListener
    {
        [SerializeField] private Color[] _colors;

        [SerializeField] private Renderer _renderer;

        [SerializeField] private int _currentColorIdx;		

		private void OnEnable()
        {
            _currentColorIdx = 0;
            CycleColor();
            if (_renderer == null) _renderer = GetComponent<Renderer>();
        }

        private void CycleColor()
        {
            if (_colors == null || _colors.Length == 0 || _renderer == null)
            {
                return;
            }

            _renderer.material.color = _colors[_currentColorIdx];
            _currentColorIdx = (_currentColorIdx + 1) % _colors.Length;
        }

        private void CycleColor(int value)
        {
            if (_colors == null || _colors.Length == 0 || _renderer == null)
            {
                return;
            }
            _currentColorIdx = value;
            _renderer.material.color = _colors[_currentColorIdx];
        }

        public void ButtonChanged(ButtonState value)
        {
            CycleColor((int)value);
        }

        public override void RegisterListeners(IEntity entity)
        {
            var e = (GameEntity)entity;
            e.AddButtonListener(this);
        }
    }
}
