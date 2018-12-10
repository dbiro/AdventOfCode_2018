using System;
using System.Collections.Generic;
using System.IO;

namespace AdventOfCode.Day08
{
    class Node
    {
        public List<Node> Children { get; }
        public List<int> Metadata { get; }

        public Node()
        {
            Children = new List<Node>();
            Metadata = new List<int>();
        }
    }
    class Tree
    {
        public Node Root { get; }

        public Tree(Node root)
        {
            Root = root;
        }
    }

    //static void ReadInput()
    //{
    //    string input = File.ReadAllText("input.txt");
    //    string[] inputNumbers = input.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
    //    for (int i = 0; i < inputNumbers.Length;)
    //    {
    //        int childrenCount = int.Parse(inputNumbers[i]);
    //        int metadataCount = int.Parse(inputNumbers[i]);

    //        Node node = new Node();
    //    }
    //}    

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }
    }
}
