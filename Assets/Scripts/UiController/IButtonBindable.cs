using UnityEngine.Events;

namespace UiController
{
    public interface IButtonBindable
    {
        string GetPath();
        void AddListener(UnityAction action);
    }
}