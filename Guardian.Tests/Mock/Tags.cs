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
            ID = 1,
            Text = "File",
            State = new State()
        };

        public static Tag PublicTag = new Tag() {
            ID = 2,
            Text = "Public",
            State = new State()
        };

    }
}
