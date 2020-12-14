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
                Robot rb = new Robot();
                using (StreamReader reader = File.OpenText(args[0])){
                    string line;
                    while ((line = reader.ReadLine()) != null){
                        rb = process(rb,line);
                    }
                }
            }

        }

        static Robot process(Robot rbt, string line){
            //checking each possible command
            if(line.Contains("PLACE")){
                //get the arguements from the place command
                string formatted_line = line.Substring(6);
                string[] arguements = formatted_line.Split(',');
                rbt = new Robot(Int32.Parse(arguements[0]),Int32.Parse(arguements[1]),arguements[2]);
                Console.WriteLine(arguements[0]+","+arguements[1]+","+arguements[2]);
                return rbt;
            }else{
                if(!rbt.BeenPlaced()){
                    return rbt;
                }
                switch(line){
                    case "MOVE":
                        rbt.Move();
                        return rbt;
                    case "LEFT":
                    case "RIGHT":
                        rbt.SetHead(line);
                        return rbt;
                    case "REPORT":
                        Console.WriteLine(rbt.GetInfo());
                        return rbt;
                    default:
                        Console.WriteLine("Invalid Command");
                        return rbt;
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
                if(!BeenPlaced()) return "Robot has not been placed";
                string x = this.xpos.ToString();
                string y = this.ypos.ToString();
                return "("+x+","+y+") facing "+this.heading;
            }

            public bool BeenPlaced(){
                return (this.xpos != -1 && this.ypos != -1);
            }

            public bool IsValid(int x, int y){
                if(x<5 && y<5 && x>-1 && y>-1) return true;
                else{
                    Console.WriteLine("Saving the Robot from destruction...");
                    return false;
                }
            }

            public void SetHead(string s){
                string[] headings = new string[] {"NORTH", "EAST", "SOUTH", "WEST"};
                int indx = Array.IndexOf(headings,this.heading);
                if(s=="LEFT") indx = (indx+3)%4;
                else indx = (indx+1)%4;
                this.heading = headings[indx];
            }

            public bool Move(){
                int tempx = this.xpos;
                int tempy = this.ypos;
                switch(this.heading){
                    case "NORTH":
                        tempy = this.ypos+1;
                        break;
                    case "EAST":
                        tempx = this.xpos+1;
                        break;
                    case "WEST":
                        tempx = this.xpos-1;
                        break;
                    case "SOUTH":
                        tempy = this.ypos-1;
                        break;
                    default:
                        return false;
                }
                if(IsValid(tempx,tempy)){
                    this.xpos = tempx;
                    this.ypos = tempy;
                    return true;
                }
                return false;
            }
        }


    }

}
