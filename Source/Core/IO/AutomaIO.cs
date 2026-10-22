using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Text.Unicode;

namespace Automav2.Source.Core.IO
{


    internal static class AutomaIO
    {
        private static string FilterString(string input)
        {
            return Regex.Replace(input, @"\\(.)", match =>
            {
                return match.Groups[1].Value switch
                {
                    "n" => "\n", // newline
                    "t" => "\t", // horizonal tab
                    "\\" => "\\", // backslash
                    "\"" => "\"", // qoute
                    "'" => "'", // single character
                    "e" => "\e", // escape code
                    "0" => "\0", // null
                    "a" => "\a", // alert
                    "r" => "\r", // carriage return
                    "b" => "\b", // backspace
                    "f" => "\f", // form feed
                    "v" => "\v", // vertical tab
                    _ => match.Value
                };
            });
        }


        public static async Task<string>? Read(uint BufferSize= 4128) // temporary
        {
            byte[] buffer = new byte[BufferSize];

            Stream Input = Console.OpenStandardInput();
            int BytesRead = 0;
            StringBuilder ContextBuilder = new StringBuilder();

            while((BytesRead = Input.Read(buffer,0,buffer.Length)) > 0)
            {
                string chunk = Encoding.UTF8.GetString(buffer, 0, BytesRead);

                
                if (chunk.Contains("\r\n")) 
                { 
                    chunk=chunk.Remove(chunk.IndexOf("\r\n")); // Clear out CRLF
                    ContextBuilder.Append(chunk);
                    break; 
                } 
                else
                {
                    ContextBuilder.Append(chunk);
                }
            }
            Input.Close();
            return ContextBuilder.ToString();
        }

        public static async Task Write(string Content,byte ErrorLevel=0)
        {
            Stream Output = Console.OpenStandardOutput(Content.Length);

            

            string ErrorFilter = ErrorLevel switch
            {
                0 => Content, // Normal Output
                1 => "\e[1;31m[ERROR] \e[0m" + Content, // Error
                2 => "\e[1;93m[DEBUG] \e[0m" + Content, // Debug
                _ => Content

            };

            string filtered = FilterString(ErrorFilter);

            byte[] EncodedSentence = Encoding.UTF8.GetBytes(filtered);

            Output.Write(EncodedSentence);

            Output.Close();
        }

        public static async Task Write(Exception ex, byte ErrorLevel = 1) // Write overload for exceptions
        {
            int padding = 200;
            Stream Output = Console.OpenStandardOutput(ex.Message.Length + ex.Source.Length + ex.Message.Length + padding);

            string Content = @$"
An Exception has Occured:
====[Message]
{ex.Message}
====[Source]
{ex.Source}
====[StackTrace]
{ex.StackTrace}
====
";

            string ErrorFilter = ErrorLevel switch
            {
                0 => Content, // Normal Output
                1 => "\e[1;31m[ERROR] \e[0m" + Content, // Error
                2 => "\e[1;93m[DEBUG] \e[0m" + Content, // Debug

            };

            string filtered = FilterString(ErrorFilter);

            byte[] EncodedSentence = Encoding.UTF8.GetBytes(filtered);

            Output.Write(EncodedSentence);
            Output.Close();
        }


    }


}
