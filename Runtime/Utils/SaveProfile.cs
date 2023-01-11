using System;
using UnityEngine;

namespace Gamekit2D.Runtime.Utils
{
    [Serializable]
    public sealed class SaveProfile<T> where T : SaveProfileData
    {
        public string name;
        public T saveData { get; private set; }

        private SaveProfile() { } //default constructor - used by JSON converter
        
        /// <summary>
        /// Creates a new SaveProfile with the given name.
        /// </summary>
        /// <param name="name">string name and filename of the save profile</param>
        /// <param name="saveData">T data to be saved into this profile</param>
        public SaveProfile(string name, T saveData)
        {
            this.name = name;
            this.saveData = saveData;
        }
    }

    public abstract record SaveProfileData { }
}