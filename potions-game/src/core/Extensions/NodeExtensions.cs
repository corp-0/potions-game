using System.Linq;
using Godot;

namespace PotionsGame.Core.Extensions
{
    public static class NodeExtensions
    {
        /// <summary>
        /// Tries to get a child node using only the type as identifier. Don't use this if you have multiple nodes of the same type as child of
        /// the parent node.
        /// </summary>
        /// <param name="node"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T GetNodeByTypeOrNull<T>(this Node node) where T : Node
        {
            var children = node.GetChildren();
            return children.OfType<T>().Select(child => child as T).FirstOrDefault();
        }
    }
}
