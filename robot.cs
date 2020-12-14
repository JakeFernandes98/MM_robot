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
            /*
            params: Robot, Command to process
            */
            if(line.Contains("PLACE")){
                //get the arguements from the place command
                string formatted_line = line.Substring(6);
                string[] arguements = formatted_line.Split(',');
                rbt = new Robot(Int32.Parse(arguements[0]),Int32.Parse(arguements[1]),arguements[2]);
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
            /*
            class Robot:
            attributes:
                xpos - the x position of the Robot
                ypos - the y position of the Robot
                heading - the direction the robot is facing. Valid valued: NORTH,
                            EAST,SOUTH,WEST,NONE
            constructors:
                empty - places the robot outside of the board. Used in order to
                        create the initial object for use with static methods.
                parameters - used when PLACE command is issued.
            */
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
            /*
            returns: int array - the x and y position of the Robot
            1st element is the x position and the 2nd is the y.
            */
                return new int[] {this.xpos,this.ypos};
            }

            public string GetHeading(){
            //returns: string - the heading of the Robot
                return this.heading;
            }

            public string GetInfo(){
            /*
            returns: formatted string containing position and heading of robot
            */
                if(!BeenPlaced()) return "Robot has not been placed";
                string x = this.xpos.ToString();
                string y = this.ypos.ToString();
                return "("+x+","+y+") facing "+this.heading;
            }

            public bool BeenPlaced(){
            /*
            returns: boolean - whether robot has been placed
            checks whether robot has been placed based on logic of empty constructor
            */
                return (this.xpos != -1 && this.ypos != -1);
            }

            public bool IsValid(int x, int y){
            /*
            params: x and y coordinates
            returns: boolean - whether coords are in the board
            checks whether the coordinated given are within the board size
            */
                if(x<5 && y<5 && x>-1 && y>-1) return true;
                else{
                    Console.WriteLine("Saving the Robot from destruction...");
                    return false;
                }
            }

            public void SetHead(string s){
            /*
            changes the heading of the robot by using modular arithmetic. An
            array is created which contains the headings in clockwise order.
            If the robot turns left then it is moving anticlockwise (so must subtract
            from the index of the array). If right then the index must be incremented
            */
                string[] headings = new string[] {"NORTH", "EAST", "SOUTH", "WEST"};
                int indx = Array.IndexOf(headings,this.heading);
                if(s=="LEFT") indx = (indx+3)%4;
                else indx = (indx+1)%4;
                this.heading = headings[indx];
            }

            public bool Move(){
            /*
            calculates the theoretical position of the robot if it was to move,
            which are then checked. If it is within the board then the robot will
            be moved, otherwise the robot will remain still
            */
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
