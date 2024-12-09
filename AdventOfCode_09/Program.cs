using AdventOfCode_Helper;
using System.Text;
using System.Text.RegularExpressions;

namespace AdventOfCode_09
{
    public class FileBlock
    {
        public int ID { get; set; }
        public int Length { get; set; }
        //public int Position { get; set; }
        public string Display { get; set; } = ".";

        public override string ToString()
        {
            return $"{ID}";
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            string data = Helper.ReadFileString(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "puzzle.txt"));


            List<FileBlock> blocks = [];
            int counter = 0;
            for (int d = 0; d < data.Length; d++)
            {
                if (d % 2 == 0)
                {
                    FileBlock file = new FileBlock
                    {
                        ID = counter,
                        Display = $"{counter}",
                        Length = data[d] - '0'
                    };

                    for (int l = 0; l < file.Length; l++)
                    {
                        blocks.Add(file);
                    }
                    counter++;
                }
                else
                {
                    FileBlock file = new FileBlock
                    {
                        ID = -1,
                        Length = data[d] - '0'
                    };

                    for (int l = 0; l < file.Length; l++)
                    {
                        blocks.Add(file);
                    }
                }
            }

            //foreach (FileBlock file in blocks)
            //{
            //    Console.Write(file.Display);
            //}


            int i = 0;
            int j = blocks.Count - 1;
            while (i < j)
            {
                //Console.SetCursorPosition(0, 0);
                //for (int l = 0; l < blocks.Count; l++)
                //{
                //    Console.Write(blocks[l].Display);
                //}

                i = blocks.FindIndex(i, j - i, x => x.ID == -1);
                if (blocks[j].ID != -1)
                {
                    FileBlock tmp = blocks[i];
                    blocks[i] = blocks[j];
                    blocks[j] = tmp;


                    j--;
                    i++;
                    continue;
                }

                j--;


                ;
            }

            long sum = 0;
            for (int l = 0; l < blocks.Count; l++)
            {
                if (blocks[l].ID != -1)
                {
                    sum += l * blocks[l].ID;
                }
            }


            //PART 2
            Console.Clear();

            blocks = [];
            counter = 0;
            for (int d = 0; d < data.Length; d++)
            {
                if (d % 2 == 0)
                {
                    FileBlock file = new FileBlock
                    {
                        ID = counter,
                        Display = $"{counter}",
                        Length = data[d] - '0'
                    };

                    for (int l = 0; l < file.Length; l++)
                    {
                        blocks.Add(file);
                    }
                    counter++;
                }
                else
                {
                    FileBlock file = new FileBlock
                    {
                        ID = -1,
                        Length = data[d] - '0'
                    };

                    for (int l = 0; l < file.Length; l++)
                    {
                        blocks.Add(file);
                    }
                }
            }





            i = 0;
            j = blocks.Count - 1;
            while (j >= 0)
            {
                //Console.SetCursorPosition(0, 0);
                //for (int l = 0; l < blocks.Count; l++)
                //{
                //    Console.Write(blocks[l].Display);
                //}

                if (blocks[j].ID != -1)
                {
                    int index = blocks.FindIndex(0, j, x => x.ID == -1 && x.Length >= blocks[j].Length);
                    if (index >= 0)
                    {
                        i = index;
                        int length = blocks[j].Length;
                        blocks[i].Length -= length;
                        for (int u = 0; u < length; u++)
                        {
                            FileBlock tmp = blocks[i + u];
                            blocks[i + u] = blocks[j - u];
                            blocks[j - u] = tmp;
                        }

                        j -= length;
                        i += length;
                        continue;
                    }
                }

                j -= blocks[j].Length;
                

                ;
            }

            //Console.SetCursorPosition(0, 0);
            //for (int l = 0; l < blocks.Count; l++)
            //{
            //    Console.Write(blocks[l].Display);
            //}

            sum = 0;
            for (int l = 0; l < blocks.Count; l++)
            {
                if (blocks[l].ID != -1)
                {
                    sum += l * blocks[l].ID;
                }
            }

            ;

        }
    }
}
