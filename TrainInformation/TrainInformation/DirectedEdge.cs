namespace TrainInformation
{
    internal class DirectedEdge
    {
        public DirectedEdge(char startVertex, char endVertex, int weight)
        {
            StartVertex = startVertex;
            EndVertex = endVertex;
            Weight = weight;
        }

        public char StartVertex { get; set; }

        public char EndVertex { get; set; }
        public int Weight { get; set; }
    }
}