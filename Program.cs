using System;
namespace URLEncoder
{
    class Program
    {  
        public static string[,] bannedCharacters={{" ", "%20"}, {"<", "%3C"}, {">", "%3E"}, {"#", "%23"}, {"\"", "%22"}, 
            {";", "%3B"}, {"/", "%2F"}, {"?", "%3F"}, {":", "%3A"}, {"@", "%40"}, 
            {"&", "%26"}, {"=", "%3D"}, {"+", "%2B"}, {"$", "%24"}, {",", "%2C"}, 
            {"{", "%7B"}, {"}", "%7D"}, {"|", "%7C"}, {"\\", "%5C"}, {"^", "%5E"}, 
            {"[", "%5B"}, {"]", "%5D"}, {"`", "%60"}};
        public static int leng=bannedCharacters.GetLength(0);
        static void Main(string[] args)
            {
                URL_Encoder URL=new URL_Encoder();
                Console.WriteLine(URL.output);
                Console.ReadLine();// I use the external console in VSCode as I have been getting errors when using the internal so I put a readline statement just to read output.
            }

        class URL_Encoder{
            public string project;
            public string activity;
            public string output;
            public URL_Encoder(){
                    project=GetProject();
                    activity=GetActivity();
                    output=makeOutput(project,activity);
            }
            static Boolean isValidInput(string input){
                foreach(char character in input){
                    if(character==';'||character=='/'||character=='?'||character==':'||character=='@'||character=='&'||character=='='||character=='+'||character=='$'||character==','){
                        return false;
                    }
                }
                return true;
            }
            static string GetActivity(){
                string input="";
                do{   
                    Console.WriteLine("Enter activity name:");
                    input = Console.ReadLine();
                    if (isValidInput(input)) return input;
                    Console.Write("The input contains invalid characters.");
                } while (true);
            }
            static string GetProject(){
                Console.WriteLine("Enter project name:");
                string input="";
                do{   
                    input = Console.ReadLine();
                    if (isValidInput(input)) return input;
                    Console.Write("The input contains invalid characters.");
                } while (true);
            }
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
            static string makeOutput(string project1, string activity1){
                string projectE=Encode(project1);
                string activityE=Encode(activity1);
                return $"https://companyserver.com/content/{projectE}/files/{activityE}/{activityE}Report.pdf";
            }
        }    
    }
}
