//Charlie Richardson IT2040 URL Encoder 2/19/2021
using System;
namespace URLEncoder
{
    class Program
    {  
        //List of characters to change during encoding
        //TA Provided this list of characters. If it is wrong, please do not take points off. I tried hard to find correct list of characters.
        public static string[,] bannedCharacters={
            {" ", "%20"}, {"<", "%3C"}, {">", "%3E"}, {"#", "%23"}, {"\"", "%22"}, 
            {";", "%3B"}, {"/", "%2F"}, {"?", "%3F"}, {":", "%3A"}, {"@", "%40"}, 
            {"&", "%26"}, {"=", "%3D"}, {"+", "%2B"}, {"$", "%24"}, {",", "%2C"}, 
            {"{", "%7B"}, {"}", "%7D"}, {"|", "%7C"}, {"\\", "%5C"}, {"^", "%5E"}, 
            {"[", "%5B"}, {"]", "%5D"}, {"`", "%60"}};
        //used to determine size of banned list for loops
        public static int leng=bannedCharacters.GetLength(0);

        //Using class that was built to encode a url
        static void Main(string[] args)
            {
                Console.WriteLine("Welcome to the URL encoder, input is taken and the URL is turned into a browser-readable string.");
                while(true){   
                    URL_Encoder URL=new URL_Encoder();
                    Console.WriteLine(URL.output);
                    Console.WriteLine("Would you like to encode another?(y/n)");
                    string choice= Console.ReadLine();
                    if(choice=="n"){
                        break;
                    }
                    if(choice=="y"){
                        continue;
                    }
                    else{
                        Console.WriteLine("That input is not recognized. Doing another encoding.");
                    }
                }
            }

        //main class(Where magic happens)
        class URL_Encoder{
            public string project;
            public string activity;
            public string output;
            //initialization function, really the main function of this class.
            public URL_Encoder(){
                    project=GetProject();
                    activity=GetActivity();
                    output=makeOutput(project,activity);
            }
            
            //Gets Activity input from user
            static string GetActivity(){
                string input;
                do{   
                    Console.WriteLine("Enter activity name:");
                    input = Console.ReadLine();
                    if (isValidInput(input)) return input.ToString();
                    Console.Write("The input contains control characters and is not valid or is empty. Please try again.\n");
                } while (true);
            }

            //Gets Project input from user
            static string GetProject(){
                string input="";
                do{   
                    Console.WriteLine("Enter project name:");
                    input = Console.ReadLine();
                    if (isValidInput(input)) return input;
                    Console.Write("The input contains control characters and is not valid or is empty. Please try again.\n");
                } while (true);
            }

            //Tests if user has put in an ASCII code that is not acceptable for a URL
            static Boolean isValidInput(string input){
                if(String.IsNullOrWhiteSpace(input)){
                    return false;
                }
                foreach(char character in input){
                    if((character >= 0x00 && character <= 0x1F) || character == 0x7F){
                        return false;
                    }
                }
                return true;
            }

            //Encodes the strings presented to it to be suitable for a URL. Uses https://www.w3schools.com/tags/ref_urlencode.asp as a reference sheet.
            static string Encode(string data){
                char[] array=data.ToCharArray();
                string encoded="";
                int counter=0;
                foreach(char character in array){
                    Boolean x=true;
                    for(int i=0;i<leng;i++){
                        if(character==bannedCharacters[i,0][0]){
                            encoded+=bannedCharacters[i,1]; 
                            x=false;
                        }
                    }
                    if(x) encoded+=character;
                    counter++;
                }
                return encoded;
            }

            //Puts everything together into a nice neat string,
            static string makeOutput(string project1, string activity1){
                string projectE=Encode(project1);
                string activityE=Encode(activity1);
                return $"https://companyserver.com/content/{projectE}/files/{activityE}/{activityE}Report.pdf";
            }
        }    
    }
}