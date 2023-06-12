namespace angular_project;

public class MorseManager{
    LinkedList<DoubleObjects> morseDictionary;
    public MorseManager(){
        morseDictionary = new LinkedList<DoubleObjects>();
        morseDictionary.Add("00000", "A");
        morseDictionary.Add("00001", "B");
        morseDictionary.Add("00010", "C");
        morseDictionary.Add("00011", "D");
        morseDictionary.Add("00100", "E");
        morseDictionary.Add("00101", "F");
        morseDictionary.Add("00110", "G");
        morseDictionary.Add("00111", "H");
        morseDictionary.Add("01000", "I");
        morseDictionary.Add("01001", "J");
        morseDictionary.Add("01010", "K");
        morseDictionary.Add("01011", "L");
        morseDictionary.Add("01100", "M");
        morseDictionary.Add("01101", "N");
        morseDictionary.Add("01110", "O");
        morseDictionary.Add("01111", "P");
        morseDictionary.Add("10000", "Q");
        morseDictionary.Add("10001", "R");
        morseDictionary.Add("10010", "S");
        morseDictionary.Add("10011", "T");
        morseDictionary.Add("10100", "U");
        morseDictionary.Add("10101", "V");
        morseDictionary.Add("10110", "W");
        morseDictionary.Add("10111", "X");
        morseDictionary.Add("11000", "Y");
        morseDictionary.Add("11001", "Z");

    }
    public string MorseToAlphabet(string morseCode)
{
    string result = "";

    for (int i = 0; i < morseCode.Length; i += 5)
    {
        // Check if there are enough characters remaining to extract a 4-character Morse sequence
        if (i + 4 <= morseCode.Length)
        {
            string morseSequence = morseCode.Substring(i, 5);

            // Search for the corresponding alphabet in the Morse dictionary
            string charValue = morseDictionary.GetDataByKey(morseSequence);
            result += charValue;
        }
    }

    return result; // returns the alphabetical value
}



    public string alphabetToMorse(string word){
        string result="";
        for(int i=0; i<word.Length;i++){
        string letter = word[i].ToString();
        string morseSequence = morseDictionary.GetKeyByData(letter); //GETS THE SEQUENCE
        // If a matching Morse sequence is found, append it to the result
        if (!string.IsNullOrEmpty(morseSequence))
        {
            result += morseSequence;
        }
        else
        {
            // Handle the case where an unknown letter is encountered
            result += "?";
        }
        }

        return result; //returns the morse string
    }

}