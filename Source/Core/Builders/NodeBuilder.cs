using Automav2.Source.Definitions.Instructions;
using Automav2.Source.Definitions.Instructions.Blocks;
using static Automav2.Source.Core.IO.AutomaIO;

namespace Automav2.Source.Core.Builders
{
    internal class NodeBuilder
    {
        private Instruction Node{ get; set; } = null;
        private bool isDebug { get; } = false;

        public NodeBuilder(bool isdebug = false) => isDebug= isdebug;


        public async Task<int> AddNode(Instruction node,bool inBlock=false)
        {
            if (Node is null)
            {
                Node = node;
                return 0;

            }

            int traverse = 0;

            while (Node.Next != null)
            {

                if (isDebug)
                {
                    await Write($"Nodes traversed {traverse}",2);
                }

                Node = Node.Next;
                traverse++;
            }

            if(inBlock)
            {
                if(Node.Next is Block block)
                {
                    Instruction? CurrentRoute = block.Route;

                    if(CurrentRoute is null)
                    {
                        CurrentRoute = node;
                        return 0;
                    }
                    
                    while(CurrentRoute.Next is not null)
                    {
                        CurrentRoute = CurrentRoute.Next;
                    }

                    CurrentRoute.Next = node;
                    return 0;
                }
            }
            else
            {
                Node.Next = node;
            }

            return 0;
        }

        public Instruction GetNode() => Node;

    }
}
