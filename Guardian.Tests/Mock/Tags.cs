using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Guardian.Tests.Mock
{
    public static class Tags
    {
        public static Tag FileTag = new Tag() {
            Text = "File",
        };

        public static Tag PublicTag = new Tag() {
            Text = "Public",
        };

        public static Tag PrivateTag = new Tag()
        {
            Text = "Private",
        };
    }
}
