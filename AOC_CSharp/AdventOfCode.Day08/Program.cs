using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Linq;

namespace AdventOfCode.Day08
{
    class Node
    {
        private readonly int childrenCount;
        private readonly int metadataCount;

        private readonly List<Node> children;
        private readonly List<int> metadata;
                
        private Node(int childrenCount, int metadataCount)
        {
            children = new List<Node>(childrenCount);
            metadata = new List<int>(metadataCount);

            this.childrenCount = childrenCount;
            this.metadataCount = metadataCount;
        }

        private void LoadChildren(ref int i, int[] input)
        {
            for (int j = 0; j < childrenCount; j++)
            {
                Node child = Load(ref i, input);
                children.Add(child);
            }
        }

        private void LoadMetadata(ref int i, int[] input)
        {
            int iterateTo = i + metadataCount;
            while (i < iterateTo)
            {
                metadata.Add(input[i]);
                i++;
            }
        }

        public int SumMetada()
        {
            int sum = metadata.Sum();
            foreach (var child in children)
            {
                sum += child.SumMetada();
            }
            return sum;
        }

        public int Value()
        {            
            if (childrenCount == 0)
            {
                return SumMetada();
            }
            else
            {
                int value = 0;
                foreach (var m in metadata)
                {
                    int i = m - 1;
                    if (i < childrenCount)
                    {
                        value += children[i].Value();
                    }
                }
                return value;
            }
        }

        public static Node Load(ref int i, int[] input)
        {
            Node node = new Node(input[i], input[i + 1]);

            i += 2;

            if (node.childrenCount == 0)
            {
                node.LoadMetadata(ref i, input);
            }
            else
            {
                node.LoadChildren(ref i, input);
                node.LoadMetadata(ref i, input);
            }
            return node;
        }
    }
    class Tree
    {
        public Node Root { get; private set; }

        private Tree() { }

        public int SumMetada()
        {
            return Root.SumMetada();
        }

        public int Value()
        {
            return Root.Value();
        }

        public static Tree Load(int[] input)
        {
            int i = 0;
            Tree tree = new Tree();
            tree.Root = Node.Load(ref i, input);
            return tree;
        }
    }

    class Program
    {
        static ImmutableList<int> ReadInput()
        {
            string input = File.ReadAllText("input.txt");
            return ImmutableList.Create(input
                .Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries)
                .Select(number => int.Parse(number))
                .ToArray());
        }

        static void Main(string[] args)
        {
            ImmutableList<int> inputNumbers = ReadInput();
            Tree tree = Tree.Load(inputNumbers.ToArray());
            Console.WriteLine(tree.SumMetada());
            Console.WriteLine(tree.Value());
        }
    }
}
