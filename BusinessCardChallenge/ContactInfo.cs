using System;
using System.Linq;

namespace BusinessCardChallenge
{
    public class ContactInfo
    {
        private string name;
        private string phoneNumber;
        private string email;

        public ContactInfo(string document)
        {
            //We first split the document into separate lines, making it easier to grab individual info from it
            string[] docSplit = document.Split('\n');
            phoneNumber = findPhone(docSplit);
            string[] nameAndEmail = findNameAndEmail(docSplit);
            name = nameAndEmail[0];
            email = nameAndEmail[1];
        }

        public string getName()
        {
            return name;
        }

        public string getPhoneNumber()
        {
            return phoneNumber;
        }

        public string getEmailAddress()
        {
            return email;
        }

        //Helper function for the constructor, locates the phone number
        private string findPhone(string[] doc)
        {
            //Iterate through each line to search for the phone #
            foreach (string line in doc)
            {
                //Check if the line has at least one number in it AND is not the fax
                if (!(line.Contains("Fax")) && line.Any(char.IsDigit))
                {
                    //Check if the line has 10-11 numbers. If so, it's the phone #
                    //While counting, we construct a string of the numbers
                    string constructedNumber = "";
                    foreach (char c in line)
                    {
                        if (char.IsDigit(c))
                            constructedNumber += c;
                    }

                    if (constructedNumber.Length == 10 || constructedNumber.Length == 11)
                        return constructedNumber;
                }
            }

            //If it's not found in the given document, throw an error
            throw new Exception("Phone number not found");
        }

        //Helper function for the constructor, locates the email address then uses it to find the name
        private string[] findNameAndEmail(string[] doc)
        {
            string[] nameAndEmail = new string[2];
            //Iterate through each line to search for the email
            foreach (string line in doc)
            {
                //Check if '@' is located on the line. If so, it's the email
                if (line.Contains("@"))
                {
                    //Split the line into individual words, in case the line has 'Email: ' before the email
                    string[] lineSplit = line.Split(' ');
                    //Return the last word in the line, which will be the email
                    nameAndEmail[1] = lineSplit[lineSplit.Length-1];
                }
            }
            if (nameAndEmail[1] == "")
            {
                throw new Exception("Email address not found");
            }
            //Now we search to find the line with the greatest similarity to the beginning
            //of the email address (before the @), found by seeing which line has the
            //longest common substring
            string emailBeginning = nameAndEmail[1].Split('@')[0];
            int foundNameIndex = -1; //The line on which the string with the most in common resides
            int similarity = 0; //The length of the longest common substring found
            for(int i = 0; i < doc.Length; i++)
            {
                //Make sure the line isn't the email itself
                if (!doc[i].Contains("@"))
                {
                    //If the line contains the new longest substring
                    if (largestSubstringLength(emailBeginning, doc[i]) > similarity)
                    {
                        similarity = largestSubstringLength(emailBeginning, doc[i]);
                        foundNameIndex = i;
                    }
                }
            }
            nameAndEmail[0] = doc[foundNameIndex];

            if (foundNameIndex == -1)
            {
                throw new Exception("Name not found");
            }
            //Return the array containing both the name and email
            return nameAndEmail;
        }

        
        //Helper for finding the name. Finds the length of the longest possible substring
        //between both inputs
        private int largestSubstringLength(string emailBeginning, string line)
        {
            //Start by creating a 2d array with the dimensions of both strings
            int[,] table = new int[emailBeginning.Length, line.Length];
            int longest = 0;
            for (int i = 0; i < emailBeginning.Length; i++)
            {
                for (int j = 0; j < line.Length; j++)
                {
                    //If a match between two characters is found, update the table
                    if (emailBeginning[i] == line[j])
                    {
                        //If this is the beginning of either word, set the table entry to 1
                        if (i == 0 || j == 0)
                            table[i, j] = 1;
                        //If it's not, then if the characters before it in both strings are the
                        //same, we can increase the size of the substring by 1
                        else
                            table[i, j] = 1 + table[i - 1, j - 1];

                        if (table[i, j] > longest)
                            longest = table[i, j];
                    }
                    else
                        table[i, j] = 0;
                }
            }

            return longest;
        }
    }
}