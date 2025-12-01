using UnityEngine;
using Cysharp.Threading.Tasks;
using System; 

//[Serializable] 
public class GlitchEffect : MonoBehaviour, IGlitch
{
   //public abstract float _duration {get; set;}
   protected bool _isGlitchActive = false;

   public virtual void Apply(Gun gun) {}

    protected async UniTask ChangeValue<T>(Func<T> getter, Action<T> setter, T temporaryValue, float duration)
    {
        if (_isGlitchActive) return;

        _isGlitchActive = true; 
        
        T originalValue = getter(); // Получаем исходное значение

         setter(temporaryValue); // Устанавливаем временное значение

         await UniTask.Delay(TimeSpan.FromSeconds(duration), ignoreTimeScale: false);
        
         setter(originalValue); // Восстанавливаем исходное значение
         _isGlitchActive = false; 
    }
   

}
