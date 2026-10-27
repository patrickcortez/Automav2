using Automav2.Source.Definitions;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace Automav2.Source.Core
{
    internal class Lexer
    {
        private string FilePath { get;  }
        private bool IsDebug { get; }

        private string[] Keywords =
        {
            "Write","Read","Run","If","Elif","Else","Let","Func"
        };

        public Lexer(string filePath, bool isdebug = false)
        {
            if (!Path.Exists(filePath))
            {
                throw new FileNotFoundException($"File: {filePath} doesn't exist");
            }

            FilePath = filePath;
            IsDebug= isdebug;
        }

        private async Task<string[]?> ReadLines()
        {
            StreamReader Reader = new StreamReader(FilePath);
            List<string> ReadLine = new();
            string? line = "";

            

            while((line= await Reader.ReadLineAsync()) != null)
            {

                if (line.StartsWith('#') || String.IsNullOrWhiteSpace(line))
                {
                    continue;
                }


                if (line.Contains('#'))
                {
                    line=line.Remove(line.IndexOf('#'), line.Length - (line.IndexOf('#') + 1));
                }
                
                ReadLine.Add(line);
            }

            return (ReadLine.Count>0)?ReadLine.ToArray():null;
        }

        private async Task<Tokens[]> Tokenize()
        {
            List<Tokens> Toks = new();
            bool inString = false,hasLetter=false;
            string[]? Lines = await ReadLines();
            StringBuilder CurrentString = new(), 
                CurrentIdentifier = new();

            int CurrentLine = 0;


            if(Lines is null)
            {
                throw new Exception("Source code cannot be empty!");
            }

            foreach (string line in Lines)
            {
                for (int i = 0; i < Lines.Length; i++)
                {


                    char current = line[i];
                    char next = ((i+1) < line.Length)? line[i+1] : '\0';

                    if (current is '"')
                    {
                        inString = !inString;

                        Toks.Add(new(TokenType.Token_String,CurrentLine, CurrentString.ToString()));
                        CurrentString.Clear();

                        continue;
                    }

                    if (inString)
                    {
                        CurrentString.Append(current);
                        continue;
                    }
                    
                    
                    if(!inString && (char.IsLetterOrDigit(next) || next is '_' )) // Identifier, int or keyword handler
                    {
                        CurrentIdentifier.Append(current);
                        hasLetter = char.IsLetter(current);

                    }
                    else
                    {
                        CurrentIdentifier.Append(current);
                        hasLetter = char.IsLetter(current);

                        string identifier = CurrentIdentifier.ToString();

                        if (hasLetter)
                        {

                            if (Keywords.Contains(identifier)) // keyword 
                            {
                                Toks.Add(new(TokenType.Token_Keyword, CurrentLine, identifier));
                            }else if(bool.TryParse(identifier.ToLower(),out _)) // boolean
                            {
                                Toks.Add(new(TokenType.Token_Boolean, CurrentLine, identifier.ToLower()));
                            }
                            else // identifier
                            {
                                Toks.Add(new(TokenType.Token_Identifier, CurrentLine, identifier));
                            }

                            CurrentIdentifier.Clear();
                            hasLetter = !hasLetter;
                        }
                        else // integer
                        {
                            Toks.Add(new(TokenType.Token_Int, CurrentLine, identifier));
                            CurrentIdentifier.Clear();
                        }

                        continue;
                    }

                    if(current is '=')
                    {

                    }

                    switch (current) // special characters
                    {
                        case '{':
                            Toks.Add(new(TokenType.Token_LBrace, CurrentLine));
                            break;
                        case '}':
                            Toks.Add(new(TokenType.Token_RBrace, CurrentLine));
                            break;
                        case '(':
                            Toks.Add(new(TokenType.Token_LParen, CurrentLine));
                            break;
                        case ')':
                            Toks.Add(new(TokenType.Token_RParen, CurrentLine));
                            break;
                        case ';':
                            Toks.Add(new(TokenType.Token_SemiColon, CurrentLine));
                            break;
                        case ',':
                            Toks.Add(new(TokenType.Token_Comma, CurrentLine));
                            break;
                        case '+':
                            Toks.Add(new(TokenType.Token_Add, CurrentLine));
                            break;
                        case '-':
                            Toks.Add(new(TokenType.Token_Minus, CurrentLine));
                            break;
                        case '*':
                            Toks.Add(new(TokenType.Token_Multiply, CurrentLine));
                            break;
                        case '\\':
                            Toks.Add(new(TokenType.Token_Divide, CurrentLine));
                            break;
                        case '=':

                            if(next is '=')
                            {
                                i += 1;
                                Toks.Add(new(TokenType.Token_EqualTo, CurrentLine));
                                break;
                            }

                            Toks.Add(new(TokenType.Token_Equal, CurrentLine));
                            break;
                        case '>':

                            if (next is '=')
                            {
                                i += 1;
                                Toks.Add(new(TokenType.Token_GTE, CurrentLine));
                                break;
                            }

                            Toks.Add(new(TokenType.Token_GreaterThan, CurrentLine));
                             break;
                        case '<':

                            if(next is '=')
                            {
                                i += 1;
                                Toks.Add(new(TokenType.Token_LTE, CurrentLine));
                                break;
                            }

                            Toks.Add(new(TokenType.Token_LessThan, CurrentLine));
                            break;
                        case '!':

                            if(next is '=')
                            {
                                i += 1;
                                Toks.Add(new(TokenType.Token_NotEqualTo, CurrentLine));
                                break;
                            }
                            Toks.Add(new(TokenType.Token_Not, CurrentLine));
                        break;
                        default:
                            throw new Exception($"Unknown Character: {current} at Instruction Line: {CurrentLine}");
                    }



                }
                CurrentLine++;
            }

            

            return Toks.ToArray();
        }

        public async Task<int> Start()
        {
            try
            {
                Tokens[] Toks = await Tokenize();

                return 0; // replace w/ Parser
            }catch(Exception ex)
            {
                await IO.AutomaIO.Write(ex);
                return 1;
            }
        }

    }
}
