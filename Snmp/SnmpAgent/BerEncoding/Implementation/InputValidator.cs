using System;
using SnmpAgent.BerEncoding.Interfaces;
using SnmpAgent.MibParsing.Models.MibParts;

namespace SnmpAgent.BerEncoding.Implementation
{
    public class InputValidator : IInputValidator
    {
        private Syntax receivedSyntax;
        private string inputToValidate;
        public bool CheckIfValueCompliesWithObjectSyntax(Syntax nodeSyntax, string value)
        {
            inputToValidate = value;
            receivedSyntax = nodeSyntax;
            var trimmedSyntaxName = nodeSyntax.Name.Replace(" ", "").ToLower();

            if (trimmedSyntaxName.Contains("integer"))
            {
                return ValidateInteger();
            }
            else if (trimmedSyntaxName.Contains("octetstring"))
            {
                return ValidateOctetString();
            }
            else if (trimmedSyntaxName.Contains("visiblestring"))
            {
                return ValidateVisibleString();
            }
            else if (trimmedSyntaxName.Contains("displaystring"))
            {
                return ValidateVisibleString();
            }
            else
            {
                return false;
            }

            
        }

        private bool ValidateVisibleString()
        {
            if (receivedSyntax.Min != null && receivedSyntax.Max != null)
            {
                var min = int.Parse(receivedSyntax.Min);
                var max = int.Parse(receivedSyntax.Max);
                var stringLength = inputToValidate.Length;

                if (stringLength >= min && max >= stringLength)
                {
                    return true;
                }
                else
                {
                    Console.WriteLine("Input is INVALID");
                    Console.WriteLine($"Tried to input '{inputToValidate}' to '{receivedSyntax.Name}' with MIN: '{receivedSyntax.Min}' and MAX '{receivedSyntax.Max}'");
                    return false;
                }
            }
            else
            {
                return true;
            }
        }

        private bool ValidateOctetString()
        {
            if (receivedSyntax.Min != null && receivedSyntax.Max != null)
            {
                var min = int.Parse(receivedSyntax.Min);
                var max = int.Parse(receivedSyntax.Max);
                var octetsCount = inputToValidate.Length / 2;

                if (octetsCount >= min && max >= octetsCount)
                {
                    return true;
                }
                else
                {
                    Console.WriteLine("Input is INVALID");
                    Console.WriteLine($"Tried to input '{inputToValidate}' to '{receivedSyntax.Name}' with MIN: '{receivedSyntax.Min}' and MAX '{receivedSyntax.Max}'");
                    return false;
                }
            }
            else
            {
                return true;
            }
        }

        private bool ValidateInteger()
        {
            if (receivedSyntax.Min != null && receivedSyntax.Max != null)
            {
                var min = int.Parse(receivedSyntax.Min);
                var max = int.Parse(receivedSyntax.Max);
                int parsedInput;
                var isPossibleToParse = int.TryParse(inputToValidate, out parsedInput);

                if (isPossibleToParse && parsedInput >= min && max >= parsedInput)
                {
                    return true;
                }
                else
                {
                    Console.WriteLine("Input is INVALID");
                    Console.WriteLine($"Tried to input '{inputToValidate}' to '{receivedSyntax.Name}' with MIN: '{receivedSyntax.Min}' and MAX '{receivedSyntax.Max}'");
                    return false;
                }

            }

            return true;
        }
    }
}