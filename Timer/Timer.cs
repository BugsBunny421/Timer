using System;
using UnityEngine;
using System.Collections.Generic;

namespace BugsBunny.Utilities.Timer
{
    public class Timer
    {
        private Action onFinishAction = null;
        private List<Action<float>> onUpdateActions = null;
        private List<TimedCallback> timedCallbacks = null;

        private float totalTime = 0f;
        private float passedTime = 0f;

        private bool isFinished = false;
        private bool ownerIsSet = false;

        private GameObject owner = null;

        /// <summary>
        /// Creates a default Timed Action with time set to 1f, and no finish action.
        /// </summary>
        public Timer()
        {
            totalTime = 0f;
            passedTime = 0f;

            isFinished = false;

            onFinishAction = null;
            onUpdateActions = new List<Action<float>>();
            timedCallbacks = new List<TimedCallback>();
        }
        public Timer(float time, Action onFinishAction)
        {
            totalTime = time;
            passedTime = 0f;

            isFinished = false;
            ownerIsSet = false;

            this.onFinishAction = onFinishAction;
            onUpdateActions = new List<Action<float>>();
            timedCallbacks = new List<TimedCallback>();
        }
        /// <summary>
        /// Sets the owner of this timer, So when the owner is destroyed this timer is stopped automatically. 
        /// In a lot of situations it's better to set owner to avoid null reference exceptions.
        /// </summary>
        /// <param name="owner">The <see cref="GameObject"/> to set as owner of this timer.</param>
        /// <returns></returns>
        public Timer SetOwner(GameObject owner)
        {
            if (owner != null)
            {
                ownerIsSet = true;
                this.owner = owner;
            }
            else
            {
                Debug.LogError("Setting a null owner is not allowed.");
            }
            return this;
        }
        /// <summary>
        /// Changes the method which will be called when this timer finishes
        /// </summary>
        /// <param name="onfinishAction">The action which will be executed when this timer finishes</param>
        public Timer ChangeOnFinishAction(Action onfinishAction)
        {
            this.onFinishAction = onfinishAction;
            return this;
        }
        /// <summary>
        /// Adds an action which will be executed on each Update of this timer
        /// </summary>
        /// <param name="onUpdateAction"></param>
        public Timer AddOnUpdateAction(Action<float> onUpdateAction)
        {
            onUpdateActions.Add(onUpdateAction);
            return this;
        }
        /// <summary>
        /// Adds a callback to the timer which will be triggered after specified time
        /// </summary>
        /// <param name="callback">The Callback to add</param>
        public TimedCallback AddCallback(TimedCallback callback)
        {
            timedCallbacks.Add(callback);
            return callback;
        }
        /// <summary>
        /// <inheritdoc cref="AddCallback(TimedCallback)"/>
        /// </summary>
        public Timer AddCallback(float time, Action action)
        {
            TimedCallback callback = new TimedCallback(time, action);
            AddCallback(callback);
            return this;
        }
        /// <summary>
        /// How much time is remaining before this timer finishes?
        /// </summary>
        /// <returns>the remaining time before this timer finishes</returns>
        public float RemainingTime()
        {
            return totalTime - passedTime;
        }
        /// <summary>
        /// Is this timer finished
        /// </summary>
        /// <returns>True if the timer has finished, false otherwise</returns>
        public bool IsFinished()
        {
            return isFinished;
        }
        /// <summary>
        /// This method will stop further Ticking of this timer. 
        /// </summary>
        /// <remarks>
        /// Basically it just marks the timer as finished, 
        /// So that whenever you try to tick it again it will ignore the tick and show a warning.
        /// It's better to check <see cref="IsFinished"/> before ticking.
        /// </remarks>
        public void Stop()
        {
            isFinished = true;
        }
        /// <summary>
        /// Updates the timer
        /// </summary>
        /// <param name="deltaTime">How much delta time has passed since this timer was last updated?</param>
        public void Tick(float deltaTime)
        {
            if (IsFinished())
            {
                Debug.LogWarning("Timer already finished, It shouldn't be ticked again");
                return;
            }
            if (ownerIsSet && (owner == null))
            {
                Stop();
                return;
                // Will be automatically removed in the next iteration.
            }

            passedTime += deltaTime;
            foreach (var action in onUpdateActions)
            {
                try
                {
                    action?.Invoke(passedTime);
                }
                catch (Exception)
                {
                    throw;
                }
            }

            foreach (var callback in timedCallbacks)
            {
                callback?.TryInvoke(passedTime);
            }

            if (passedTime >= totalTime)
            {
                try
                {
                    onFinishAction?.Invoke();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
                finally
                {
                    isFinished = true;
                }
            }
        }
    }
}
