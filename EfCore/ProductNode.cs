using System;
using System.Collections.Generic;
using System.Linq;

namespace EfCore
{
    public class ProductNode : BaseEntity
    {
        public virtual ProductNode Parent { get; set; }
        public virtual  ProductLevel Level { get; set; }

        public virtual ICollection<ProductNode> Nodes { get; } 
        public virtual ICollection<BaseSection> Sections { get; }

        public ProductNode()
        {
            Nodes  = new List<ProductNode>();
            Sections = new List<BaseSection>();
        }

        public void AddNode(ProductNode node)
        {
            if (Nodes.Any(n => n.GetType() == node.GetType()))
            {
                throw new Exception("Only unique section types are allowed");
            }

            node.Parent = this;
            Nodes.Add(node);
        }

        public TResult Get<TResult>(bool effectiveSection = false) where TResult : BaseSection
        {
            if (effectiveSection == false)
            {
                var section = (TResult)Sections.Single(n => n is TResult);
                return section;
            }
            else
            {
                var section = (TResult)Sections.Single(n => n is TResult);

                while (true)
                {
                    if (section.OverrideRule == OverrideRule.NeverInherits
                        || section.OverrideRule == OverrideRule.Top)
                        return section;

                    if (!section.UseParent)
                        return section;

                    if (Parent == null)
                        return section;

                    section = (TResult)Parent.Sections.Single(n => n is TResult);
                }
            }
        }
    }
}