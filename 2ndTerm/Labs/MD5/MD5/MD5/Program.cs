using System;


public class Program
{
    public static void Main()
    {

        MD5.MD5 md = new MD5.MD5(); //rfc 1321 - MD5
     
        for(;;)
        { 
            md.Value = Console.ReadLine();
            Console.WriteLine( "MD5 Value =" + md.Value );
            Console.WriteLine("");
            
        }
    }
}


