using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Planar.Interfaces;

namespace Planar
{
    public class GraphReader
    {
        public IGraph GetGraph() //ToDo redo prettier if enough time
        {
            IGraph graph = new Graph();

            OpenFileDialog openFileDialog = new OpenFileDialog(); //ToDo
            openFileDialog.RestoreDirectory = true;

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    Stream stream =  openFileDialog.OpenFile();

                    using (StreamReader fileStream = new StreamReader(stream))
                    {
                        string currString = fileStream.ReadLine();
                        string strNumber = Regex.Match(currString, @"(\d)+").Value;
                        int nodeCount = Int32.Parse(strNumber);
                        for (int i = 1; i <= nodeCount; i++)
                        {
                            INode node = new Node(i,Position.GetNullVector(2));
                            graph.Nodes.Add(node);
                        }

                        while (!fileStream.EndOfStream)
                        {
                            currString = fileStream.ReadLine();
                            MatchCollection twoVals = Regex.Matches(currString, @"(\d)+");
                            int firstNumber = Int32.Parse(twoVals[0].Value);
                            int secondNumber = Int32.Parse(twoVals[1].Value);

                            INode firstNode = graph.Nodes[firstNumber - 1];
                            INode secondNode = graph.Nodes[secondNumber - 1];
                            IEdge edge = new Edge(firstNode, secondNode);

                            firstNode.Edges.Add(edge);
                            secondNode.Edges.Add(edge);

                            graph.Edges.Add(edge);
                        }
                    }
                }
                catch (Exception e)
                {
                    MessageBox.Show(@"Could not read file: " + e.Message);
                }
            }

            return graph;
        }
    }
}