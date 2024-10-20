using System;

namespace Question2
{
    public class Program
    {
        public static bool PalindromeChecker(string inputString, string trashSymbolString)
        {
            if (inputString == null || trashSymbolString == null)
            {
                return false;
            }

            // Convert trashSymbolString to a HashSet for O(1) lookups
            HashSet<char> trashSymbols = new HashSet<char>(trashSymbolString.ToLower());

            //Check if is palindrome by using two pointer technique
            int left = 0;
            int right = inputString.Length - 1;

            while (left < right)
            {
                //Move left pointer forward if current character is in trashSymbolString
                if (trashSymbols.Contains(Char.ToLower(inputString[left])))
                {
                    left++;
                    continue;
                }

                //Move right pointer backwards if current character is in trashSymbolString
                if (trashSymbols.Contains(Char.ToLower(inputString[right])))
                {
                    right--;
                    continue;
                }

                //Compare the current characters, ignoring case
                if (Char.ToLower(inputString[left]) != Char.ToLower(inputString[right]))
                {
                    return false; //Not a palindrome
                }

                left++;
                right--;
            }

            return true;
        }


        public static void Main()
        {
            try
            {
                Console.Write("InputString: ");
                string? inputString = Console.ReadLine();

                Console.Write("TrashSymbolString: ");
                string? trashSymbolString = Console.ReadLine();

                bool result = PalindromeChecker(inputString, trashSymbolString);
                Console.WriteLine("Is the input string a palindrome? " + result);
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
            }
        }

    }
}

