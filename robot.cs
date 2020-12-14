using System;
using System.IO;

namespace robot{
    class Program{
        static void Main(string[] args){
            //Checking whether the File given exists
            if(!File.Exists(args[0])){
                Console.WriteLine("Please pass the file with commands as an arguement");
            }else{
                //reading file line by line
                using (StreamReader reader = File.OpenText(args[0])){
                    string line;
                    while ((line = reader.ReadLine()) != null){
                        if(!process(line)) Console.WriteLine("Invalid Command: "+line);
                    }
                }
            }

        }

        static bool process(string line){
            //checking each possible command
            if(line.Contains("PLACE")){
                //get the arguements from the place command
                string formatted_line = line.Substring(6);
                string[] arguements = formatted_line.Split(',');
                return true;
            }else{
                switch(line){
                    case "MOVE":
                        return true;
                    case "LEFT":
                    case "RIGHT":
                        //LEFT and RIGHT will likely use the same method
                        return true;
                    case "REPORT":
                        Console.WriteLine("reporting");
                        return true;
                    default:
                        Console.WriteLine("Invalid Command");
                        return false;
                }
            }
        }

        public class Robot{
            int xpos;
            int ypos;
            string heading;

            public Robot(){
                this.xpos = -1;
                this.ypos = -1;
                this.heading = "NONE";
            }

            public Robot(int x, int y, string heading){
                this.xpos = x;
                this.ypos = y;
                this.heading = heading;
                Console.WriteLine("Placing Robot");
            }

            public int[] GetCoords(){
                return new int[] {this.xpos,this.ypos};
            }

            public string GetHeading(){
                return this.heading;
            }

            public string GetInfo(){
                string x = this.xpos.ToString();
                string y = this.ypos.ToString();
                return "("+x+","+y+") facing "+this.heading;
            }
        }


    }

}
