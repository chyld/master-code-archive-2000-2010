using System;
using System.Collections.Generic;

namespace VikingOne.Common
{
    public class AstNode
    {
        public AstNodeType NodeType { get; set; }
        public AstNode Parent { get; set; }
        public List<AstNode> Children { get; set; }
        public Dictionary<AstNodeKey, dynamic> Attributes { get; set; }

        public AstNode(AstNodeType nodeType)
        {
            NodeType = nodeType;
            Parent = null;
            Children = new List<AstNode>();
            Attributes = new Dictionary<AstNodeKey, dynamic>();
        }

        public AstNode AddChild(AstNode child)
        {
            child.Parent = this;
            Children.Add(child);
            return child;
        }
    }
}
