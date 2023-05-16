using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace BugsBunny.Utilities.Timer
{
    [CustomEditor(typeof(TimersManager))]
    public class TimersManagerEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            TimersManager manager = target as TimersManager;
            EditorGUILayout.LabelField("Timers Running: " + manager.TimersCountInternal(), EditorStyles.boldLabel);
        }

    }
    /// <summary>
    /// A utility that manages Timers being ran. 
    /// Of course you can create and run your own timer but then you will have to Update it manually.
    /// </summary>
    public class TimersManager : MonoBehaviour
    {
        #region Hidden Members

        private List<Timer> timers = new List<Timer>();

        private static TimersManager instance;
        private static TimersManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new GameObject("~Timer").AddComponent<TimersManager>();
                    instance.timers = new List<Timer>();
                    DontDestroyOnLoad(instance.gameObject);
                }
                return instance;
            }
        }
        private void Update()
        {
            for (int i = timers.Count - 1; i >= 0; i--)
            {
                timers[i].Tick(Time.deltaTime);
                if (timers[i].IsFinished())
                {
                    timers.RemoveAt(i);
                }
            }
        }
        private Timer AddInternal(Timer timer)
        {
            timers.Add(timer);
            return timer;
        }
        public int TimersCountInternal()
        {
            return timers.Count;
        }
        #endregion

        #region Exposed Members
        /// <summary>
        /// Adds a new timer to the execution list
        /// </summary>
        /// <param name="timer"></param>
        public static Timer Add(Timer timer)
        {
            return Instance.AddInternal(timer);
        }
        public static int TimersCount()
        {
            return instance == null ? 0 : Instance.TimersCountInternal();
        }
        #endregion
    }
}
