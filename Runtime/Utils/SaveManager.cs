using System;
using System.IO;
using UnityEngine;
using Newtonsoft.Json;

namespace Gamekit2D.Runtime.Utils
{
    public static class SaveManager
    {
        private static readonly string saveFolder = Application.persistentDataPath + "/gameData";
        private const string key = "b14ca5898a4e4133bbce2ea2315a1916"; //private key used for AES encryption
        
        /// <summary>
        /// Loads and returns a SaveProfile from the Saves Folder. Uses the given profile name to retrieve the file.
        /// The file is automatically decrypted and turned into a SaveProfile object
        /// </summary>
        /// <param name="profileName">The name of the profile being loaded</param>
        /// <returns>new SaveProfile Object</returns>
        public static SaveProfile<T> Load<T>(string profileName) where T : SaveProfileData
        {
            //check if file exists
            if (!File.Exists($"{saveFolder}/{profileName}"))
                throw new Exception($"Save file: {profileName} not found. Make sure you've saved the file before accessing it, and the profileName is correct.");
            
            // Read the entire file and save its contents.
            var fileContents = File.ReadAllText($"{saveFolder}/{profileName}"); //encrypted
            var outString = AesOperation.DecryptString(key, fileContents); //decrypted
            // Deserialize the JSON data 
            return JsonConvert.DeserializeObject<SaveProfile<T>>(outString);
        }

        /// <summary>
        /// Saves a SaveProfile to the Saves folder in encrypted JSON format
        /// </summary>
        /// <param name="save">SaveProfile being saved</param>
        /// <param name="overwrite">Should data which already exists be overwritten? Previous data will be lost forever!</param>
        public static void Save<T>(SaveProfile<T> save, bool overwrite = false) where T : SaveProfileData
        {
            if (!overwrite && File.Exists($"{saveFolder}/{save.name}"))
                throw new Exception($"Save file: {save.name} already exists, please use a different save profile name.");
            
            //Serialize the object into JSON and save string.
            var jsonString = JsonConvert.SerializeObject(save, Formatting.Indented, new JsonSerializerSettings{ReferenceLoopHandling = ReferenceLoopHandling.Ignore});
            //encrypt the plain text string using Aes and our symmetric key
            var outString = AesOperation.EncryptString(key, jsonString);
            // Write JSON to file.
            if (!Directory.Exists(saveFolder)) //create the saves folder if we don't already have it!
                Directory.CreateDirectory(saveFolder);
            //write the encrypted text into the file
            File.WriteAllText($"{saveFolder}/{save.name}", outString);
            
            Debug.Log($"Successfully saved data into: {saveFolder}/{save.name}");
        }
        
        /// <summary>
        /// Deletes a given save from the saves folder
        /// </summary>
        /// <param name="profileName">the name of the save file being deleted</param>
        public static void DeleteSave(string profileName)
        {
            //check if the file exists
            if (!File.Exists($"{saveFolder}/{profileName}"))
                throw new Exception($"Save file: {profileName} not found. Make sure you've saved the file before accessing it, and the profileName is correct.");
            //remove the file
            File.Delete($"{saveFolder}/{profileName}");
        }
    }
}