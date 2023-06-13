namespace angular_project.Models;
public class HuffmanNode
    {
        public int FrequencyParent{get;set;}
        public char Symbol { get; set; }
        public int Frequency { get; set; }
        public HuffmanNode? Left { get; set; }
        public HuffmanNode? Right { get; set; }
    }