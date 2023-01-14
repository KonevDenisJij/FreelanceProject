using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace FirstTask
{
    internal static class UserData
    {
        public static string FirstName => _firstName;
        private static string _firstName;

        public static string LastName => _lastName;
        private static string _lastName;
        public static string MiddleName => _middleName;
        private static string _middleName;
        public static DateTime BirthDate => _birthDate;
        private static DateTime _birthDate;

        public static List<string> Words => _words.Distinct().ToList();
        private static List<string> _words = new List<string>();

        public static bool SetUserData(string lastName, string firstName, string middleName, DateTime birthDate)
        {
            if (lastName != string.Empty && firstName != string.Empty && middleName != string.Empty && birthDate != DateTime.Today)
            {
                _lastName = lastName.Trim();
                _firstName = firstName.Trim();
                _middleName = middleName.Trim();
                _birthDate = birthDate.Date;

                return true;
            }

            Debug.WriteLine("Wrong user data!");
            return false;
        }

        public static string AddWord(string word)
        {
            var checkResult = CheckWord(word);
            if (checkResult == string.Empty)
            {
                for (int i = 0; i < Settings.GetInstance().SettingsData.RepeatsNumber; i++)
                {
                    _words.Add(word);
                }
            }

            return checkResult;
        }

        public static string GetRandomWord()
        {

            var rand = new Random();
            var nextWordNumber = rand.Next(_words.Count);
            var nextWord = _words[nextWordNumber];
            _words.RemoveAt(nextWordNumber);

            return nextWord;
        }

        private static string CheckWord(string word)
        {
            if (word == string.Empty)
            {
                return "Введите слово";
            }

            if (Words.Count() >= Settings.GetInstance().SettingsData.EventsNumber)
            {
                return "Слишком много слов";
            }

            return string.Empty;
        }
    }
}
