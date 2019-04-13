using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace EfCore
{
    public class ProductNode : BaseEntity
    {
        // Injected to lazy load the parent nodes
        private readonly DbContext _dbContext;

        public ProductNode Parent { get; private set; }
        public ProductLevel Level { get; private set;  }

        private readonly ICollection<ProductNode> _nodes;
        public IEnumerable<ProductNode> Nodes => _nodes;

        private readonly ICollection<BaseSection> _sections;
        public IEnumerable<BaseSection> Sections => _sections;

        public ProductNode(DbContext dbContext)
        {
            // For lazy loading on demand.
            // This is an anti-pattern, I could not find a better way
            _dbContext = dbContext;  
        }

        public ProductNode(ProductLevel level)
        {
            Level     = level;
            _nodes    = new List<ProductNode>();
            _sections = new List<BaseSection>();
        }

        public void AddSection(BaseSection section)
        {
            if (section == null)
                throw new ArgumentNullException(nameof(section));

            if (_sections.Any(n => n.GetType() == section.GetType()))
                throw new Exception("Only unique section types are allowed");

            _sections.Add(section);
        }

        public void AddNode(ProductNode node)
        {
            if (node == null 
                || node == this 
                || node == Parent)
                   throw new ArgumentException("Node cannot be null, the same reference or the same as its parent", nameof(node));

            if (_nodes.Any(n => n.GetType() == node.GetType()))
                throw new Exception("Only unique section types are allowed");

            node.Parent = this;
            _nodes.Add(node);
        }

        public TResult Get<TResult>(bool traverseUp = true) where TResult : BaseSection
        {
            if (traverseUp)
                return TraverseNode<TResult>(this, _dbContext);
            else
                return GetSectionByType<TResult>();
        }

        // I made this method static because I kept onto causing issues for myself by referencing
        // the wrong's node (this) properties instead of the one that is currently being traversed.
        private static TResult TraverseNode<TResult>(ProductNode node, DbContext dbContext) where TResult : BaseSection
        {
            int iteration = 0;
            while (iteration < 5) // Protected against bad data
            {
                if (node.Sections == null)
                    throw new Exception("Not correct loaded");

                var section = node.GetSectionByType<TResult>();

                if (!section.UseParent
                    || section.OverrideRule == OverrideRule.NeverInherits
                    || section.OverrideRule == OverrideRule.Top)
                {
                    return section;
                }

                if (node.Parent == null)
                {
                    // Attempt to lazy load the parent node and its sections
                    dbContext.Entry(node)
                        .Reference(e => e.Parent)
                        .Query()
                        .Include(e => e.Sections)
                        .Load();
                }

                node = node.Parent ?? throw new Exception("Data problem.  Parent is null");
                iteration++;
            }

            throw new Exception("Maximum product node traversal exceeded");
        }

        private TResult GetSectionByType<TResult>() where TResult : BaseSection
        {
            var section = (TResult) Sections.SingleOrDefault(n => n is TResult);

            if (section == null)
                throw new Exception($"Could not locate {typeof(TResult)} on Node Id={Id}, Level={Level}");
            return section;
        }
    }
}