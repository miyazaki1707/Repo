using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kurs
{
    public class Meme
    {
        public String title;

        public String category;

        public String filePath;

        public Meme(String title, String category, string filePath)
        {
            this.title = title;
            this.category = category;
            this.filePath = filePath;
        }

        public override string ToString()
        {
            return title;
        }
    }
}
