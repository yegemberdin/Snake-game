using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake
{
    class State
    {
            private int index;
            public int Index
            {
                get { return index; }
                set
                {
                    int maxVal = Folder.GetFileSystemInfos().Length;
                    if (value >= 0 && value < maxVal)
                    {
                        index = value;
                    }
                }
            }
            public DirectoryInfo Folder { get; set; }
        public void ShowFolderContent()
        {
            Console.Clear();
            FileSystemInfo[] list = Folder.GetFileSystemInfos();


            for (int i = 0; i < list.Length; ++i)
            {
                if (Index == i)
                {

                    Console.ForegroundColor = ConsoleColor.Yellow;
                }
                if (list[i] is DirectoryInfo)
                {

                    Console.Write("[+] " + list[i]);
                }
                else
                {
                    Console.Write(list[i]);

                }

                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine();
            }

        }

    }
}
