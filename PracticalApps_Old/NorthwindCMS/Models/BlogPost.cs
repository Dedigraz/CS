using Piranha.AttributeBuilder;
using Piranha.Extend;
using Piranha.Models;

namespace NorthwindCMS.Models
{
    [PostType(Title = "Blog post")]
    public class BlogPost : Post<BlogPost>
    {
    }
}