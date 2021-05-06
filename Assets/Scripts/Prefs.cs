using System.Collections.Generic;
using System;
using UnityEngine;

namespace Assets.Scripts
{
    /**
     * A wrapper class around PlayerPrefs to suit the specific needs of this game. 
     * Provides abstraction for keys and default values.
     */
    static class Prefs
    {
        public enum Property
        {
            Highscore,          // float
            LastScore,          // float
            MouseSensitivity    // float
        }

        private static readonly Dictionary<Property, string> propertyKeys = new Dictionary<Property, string>()
        {
            { Property.Highscore, "highscore" },
            { Property.LastScore, "last_score" },
            { Property.MouseSensitivity, "mouse_sensitivity" }
        };

        private static readonly Dictionary<Property, float> floatDefaults = new Dictionary<Property, float>()
        {
            { Property.Highscore, Int64.MaxValue },
            { Property.LastScore, Int64.MaxValue },
            { Property.MouseSensitivity, 0.5f }
        };

        private static readonly Dictionary<Property, int> intDefaults = new Dictionary<Property, int>()
        {
        };

        private static readonly Dictionary<Property, bool> boolDefaults = new Dictionary<Property, bool>()
        {
        };

        private static readonly Dictionary<Property, string> stringDefaults = new Dictionary<Property, string>()
        {
        };

        public static bool IsDefaultValue(Property prop)
        {
            if (floatDefaults.ContainsKey(prop))
            {
                return floatDefaults[prop] == GetFloat(prop);
            }
            if (intDefaults.ContainsKey(prop))
            {
                return intDefaults[prop] == GetInt(prop);
            }
            if (boolDefaults.ContainsKey(prop))
            {
                return boolDefaults[prop] == GetBool(prop);
            }
            if (stringDefaults.ContainsKey(prop))
            {
                return stringDefaults[prop] == GetString(prop);
            }
            return false;
        }

        static public int GetInt(Property prop)
        {
            string key = propertyKeys[prop];
            return PlayerPrefs.HasKey(key) ? PlayerPrefs.GetInt(key) : intDefaults[prop];
        }

        static public float GetFloat(Property prop)
        {
            string key = propertyKeys[prop];
            return PlayerPrefs.HasKey(key) ? PlayerPrefs.GetFloat(key) : floatDefaults[prop];
        }

        static public string GetString(Property prop)
        {
            string key = propertyKeys[prop];
            return PlayerPrefs.HasKey(key) ? PlayerPrefs.GetString(key) : stringDefaults[prop];
        }

        static private bool GetBool(Property prop)
        {
            string key = propertyKeys[prop];
            return PlayerPrefs.HasKey(key) ? PlayerPrefs.GetInt(key) == 1 : boolDefaults[prop];

        }

        static public void SetBool(Property prop, bool value)
        {
            string key = propertyKeys[prop];
            PlayerPrefs.SetInt(key, value ? 1 : 0);
        }

        static public void SetFloat(Property prop, float value)
        {
            string key = propertyKeys[prop];
            PlayerPrefs.SetFloat(key, value);
        }

        static public void SetInt(Property prop, int value)
        {
            string key = propertyKeys[prop];
            PlayerPrefs.SetInt(key, value);
        }

        static public void SetString(Property prop, string value)
        {
            string key = propertyKeys[prop];
            PlayerPrefs.SetString(key, value);
        }

        static public void IncrementInt(Property prop, int step)
        {
            SetInt(prop, GetInt(prop) + step);
        }

        static public void IncrementFloat (Property prop, float step)
        {
            SetFloat(prop, GetFloat(prop) + step);
        }

        static public void SetAllToDefault()
        {
            SetFloat(Property.Highscore, floatDefaults[Property.Highscore]);
            SetFloat(Property.LastScore, floatDefaults[Property.LastScore]);
        }
    }
}
