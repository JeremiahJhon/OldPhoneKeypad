using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class OldPhoneKeyPad
{
    // A static list that represents the keypad mapping (keys -> characters)
    static List<Key> keypad;
    static char ReturnKey = '*';
    static char SendKey = '#';
    static char PauseKey = ' ';

    public static void Main(string[] args)
    {
        // Initialize the keypad with mappings for each button
        keypad = new List<Key>
        {
            new Key { Label = "1", Values = new string[] { "&", "'", "(" } },
            new Key { Label = "2", Values = new string[] { "A", "B", "C" } },
            new Key { Label = "3", Values = new string[] { "D", "E", "F" } },
            new Key { Label = "4", Values = new string[] { "G", "H", "I" } },
            new Key { Label = "5", Values = new string[] { "J", "K", "L" } },
            new Key { Label = "6", Values = new string[] { "M", "N", "O" } },
            new Key { Label = "7", Values = new string[] { "P", "Q", "R", "S" } },
            new Key { Label = "8", Values = new string[] { "T", "U", "V" } },
            new Key { Label = "9", Values = new string[] { "W", "X", "Y", "Z" } },
            new Key { Label = "0", Values = new string[] { " " } } // 0 maps to a space
        };

        if(args.Length > 0){
            Console.WriteLine($"Input: {args[0]}");
            Console.WriteLine($"{args[0]} = " + OldPhonePad(args[0])); 
        }else{
            // Test cases for OldPhonePad
            Console.WriteLine("33# = " + OldPhonePad("33#"));                 // Expected: E
            Console.WriteLine("227*# = " + OldPhonePad("227*#"));               // Expected: B
            Console.WriteLine("4433555 555666# = " + OldPhonePad("4433555 555666#"));     // Expected: HELLO
            Console.WriteLine("96667775553# = " + OldPhonePad("96667775553#"));// WORLD
            Console.WriteLine("8 88777444666*664# = " + OldPhonePad("8 88777444666*664#"));  // Expected: TURING
            Console.WriteLine("# = " + OldPhonePad("#"));           // "" (empty, immediate end)
            Console.WriteLine("2*# = " + OldPhonePad("2*#"));         // "" (typed 2, then erased)
            Console.WriteLine("0 0 0# = " + OldPhonePad("0 0 0#"));      // "   " (multiple spaces)
            Console.WriteLine("2222# = " + OldPhonePad("2222#"));       // A (loop back to index 0)
            Console.WriteLine("777*777# = " + OldPhonePad("777*777#"));    // R   (first 777 removed)
            Console.WriteLine("4433555*5# = " + OldPhonePad("4433555*5#"));  // HEJ (L removed, replaced with K)
        }
    }

    // Decodes input string into a final word/message
    public static String OldPhonePad(string input)
    {
        string result = string.Empty;

        // Encode the raw input (group repeated key presses, handle *, space, #)
        string encoded = EncodePattern(input);

        // Go through encoded string two characters at a time
        for (int i = 0; i < encoded.Length; i += 2)
        {
            // Stop decoding if end character is reached
            if (encoded[i] == SendKey) return result;

            // Find the key mapping for the current digit
            string[] key = keypad
                .Where(p => p.Label == encoded[i].ToString())
                .FirstOrDefault(new Key())
                .Values;

            int characterCount = Convert.ToInt32(encoded[i + 1].ToString());
            int index = (characterCount - 1) % key.Length;

            // Append the corresponding letter
            result += key[index];
        }
        return result;
    }

    // I want to keep this method as "Public" for re-usability
    // Converts raw input (like "4433555 555666#") into a simplified pattern
    // Example: "33#" → "32#" (digit + number of times pressed)
    public static string EncodePattern(string input)
    {
        if (string.IsNullOrEmpty(input))
            return string.Empty;

        var result = new StringBuilder();
        char? lastKey = null;   // Track previous pressed key
        int pressCount = 0;     // Count consecutive presses

        foreach (char c in input)
        {
            if (c == PauseKey) // Space: finalize current key sequence
            {
                if (lastKey.HasValue)
                    result.Append(pressCount);

                lastKey = null;
                pressCount = 0;
                continue;
            }

            if (c == ReturnKey) // Backspace: remove last key + count safely
            {
                result.Append(pressCount);
                if (result.Length >= 2)
                    result.Remove(result.Length - 2, 2);
                lastKey = null;
                pressCount = 0;
                continue;
            }

            if (c == SendKey) // End of input
            {
                break;
            }

            // Same key pressed consecutively
            if (lastKey.HasValue && lastKey.Value == c)
            {
                pressCount++;
            }
            else
            {
                // Different key pressed: finalize previous key
                if (lastKey.HasValue)
                    result.Append(pressCount);

                // Append the current key if it's not special
                if (c != PauseKey && c != ReturnKey)
                    result.Append(c);

                lastKey = c;
                pressCount = 1;
            }
        }

        // Append the final key count if any
        if (lastKey.HasValue)
            result.Append(pressCount);

        return result.ToString();
    }

}

// Represents a keypad button with label (digit) and possible values (letters/symbols)
public class Key
{
    public string Label { get; set; }
    public string[] Values { get; set; }

    public Key() { }
}

