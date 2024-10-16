using System;
using System.IO;
using System.Collections.Generic;
using System.Text.RegularExpressions;

class EFIRandomizer
{
    static void Main()
    {

        Console.Title = "EFI Randomizer || 3xx ";
        string filePath = "Startup.nsh";
        string[] patterns =
        {
            "AMIDEEFIx64.efi /BS",
            "AMIDEEFIx64.efi /SM",
            "AMIDEEFIx64.efi /SS",
            "AMIDEEFIx64.efi /CS"
        };


        string fileContent = File.ReadAllText(filePath);

        HashSet<string> usedStrings = new HashSet<string>();


        foreach (var pattern in patterns)
        {

            string regexPattern = $"{Regex.Escape(pattern)}\\s+.*";
            string newString;


            do
            {
              newString = GenerateRandomString(15); 
            } while (usedStrings.Contains(newString));

            usedStrings.Add(newString);


            fileContent = System.Text.RegularExpressions.Regex.Replace(fileContent, regexPattern, $"{pattern} {newString}");
        }


        File.WriteAllText(filePath, fileContent);

        Console.WriteLine("The file has been modified successfully.");
    }

    static string GenerateRandomString(int length)
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        char[] stringChars = new char[length];
        Random random = new Random();

        for (int i = 0; i < length; i++)
        {
            stringChars[i] = chars[random.Next(chars.Length)];
        }

        return new string(stringChars);
    }
}
