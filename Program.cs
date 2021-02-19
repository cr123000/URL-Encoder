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
        /*static char[] makeReferenceArray(string[,] array){
            char[] reference='';
            for(int i=0;i<leng;i++){
                reference[i]=bannedCharacters[i,0][0];
            }
        }*/
        static void Main(string[] args)
            {
                URL_Encoder URL=new URL_Encoder();
                Console.WriteLine(URL.output);
            }

        class URL_Encoder{
            public string project;
            public string activity;
            public string output;
            public URL_Encoder(){
                    project=GetProject();
                    activity=GetActivity();
                    output=Encode(project,activity);
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
            static string Encode(string project1,string activity1){
                //"https://companyserver.com/content/{project1}/files/{activity1}/{activity1}Report.pdf";
                string URL="";
                string projectName=project1;
                string activityName=activity1;
                char[] projArray=project1.ToCharArray();
                char[] actArray=activity1.ToCharArray();
                int counter=0;
                URL+="https://companyserver.com/content/";
                Boolean x=true;
                foreach(char character in projArray){
                    for(int i=0;i<leng;i++){
                        if(character==bannedCharacters[i,0][0]){
                            URL+=bannedCharacters[i,1]; 
                            x=false;
                            break;
                        }
                    }
                    if(x) URL+=character;
                    counter++;
                }
                return URL;
            }
        }    
    }
}
