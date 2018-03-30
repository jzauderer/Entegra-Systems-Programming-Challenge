using System;

namespace BusinessCardChallenge
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            //Instantiate the parser itself
            BusinessCardParser parser = new BusinessCardParser();
            while (true)
            {
                Console.WriteLine("Please input the text from a business card:");
        
                string line;
                string document = "";
                //Keep reading from the console until you've read all lines
                while ((line = Console.ReadLine()) != "")
                {
                    document += line + '\n';
                }
                //Parse the contact info
                ContactInfo currentContactInfo = parser.getContactInfo(document);
                //Put the contact together into a document to write to the console
                string output = "Name: " + currentContactInfo.getName();
                output += "\nPhone: " + currentContactInfo.getPhoneNumber();
                output += "\nEmail: " + currentContactInfo.getEmailAddress();
                Console.Write(output + "\n");
                //Ask if the user would like to try another card
                Console.WriteLine("Would you like to try another card? Enter Y for yes, N for no (Case Sensitive)\n");
                string response = Console.ReadLine();
                do
                {
                    if (response == "N")
                    {
                        return;
                    }
                    else if (response != "Y")
                    {
                        Console.WriteLine("Response not recognized. Please enter either Y or N\n");
                        response = Console.ReadLine();
                    }
                } while (response != "Y" && response != "N");
            }
        }
    }
}