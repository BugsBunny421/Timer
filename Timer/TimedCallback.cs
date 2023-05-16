using System;

namespace BugsBunny.Utilities.Timer
{
    public class TimedCallback
    {
        public float time;
        public Action action;

        private bool isFired;

        public TimedCallback(float time, Action action)
        {
            this.time = time;
            this.action = action;
        }
        public void TryInvoke(float currentTime)
        {
            if (isFired)
            {
                return;
            }
            if (currentTime >= time)
            {
                try
                {
                    action?.Invoke();
                }
                catch (Exception)
                {
                    throw;
                }
                finally
                {
                    isFired = true;
                }
            }
        }
        /// <summary>
        /// Will not execute this callback. This can only be done before it has not been executed already.
        /// </summary>
        public void DontFire()
        {
            this.isFired = true;
        }
    }
}
