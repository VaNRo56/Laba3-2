using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Newtonsoft.Json;

class TextManager
{
    private string text;

    public TextManager(string initialText)
    {
        this.text = initialText;
    }

    public string GetText
    {
        get =>  text;
        
    }
    public void AddSentence(string sentence)
    {
        text += " " + sentence;
    }


    public void RemoveSentence(string sentence)
    {
        text = text.Replace(sentence, "");
    }


    public void InsertSentence(int index, string sentence)
    {
        string[] sentences = text.Split('.');
        if (index >= 0 && index < sentences.Length)
        {
            sentences[index] = sentence;
            text = string.Join(".", sentences);
        }
    }


    public int CountLetters
    {
        get { return text.Length; }
    }


    public int CountWords
    {
        get { return text.Split(new char[] { ' ', '.', ',', '!', '?' }, StringSplitOptions.RemoveEmptyEntries).Length; }
    }


    public int CountSentences
    {
        get { return text.Split(new char[] { '.' }, StringSplitOptions.RemoveEmptyEntries).Length; }
    }


    public string GetSentence(int index)
    {
        string[] sentences = text.Split('.');
        if (index >= 0 && index < sentences.Length)
        {
            return sentences[index];
        }
        return null;
    }

    public string LongestSentence()
    {
        string[] sentences = text.Split('.');
        string longest = "";
        foreach (string sentence in sentences)
        {
            if (sentence.Length > longest.Length)
            {
                longest = sentence;
            }
        }
        return longest;
    }


    public string ShortestSentence()
    {
        string[] sentences = text.Split('.');
        string shortest = sentences[0];
        foreach (string sentence in sentences)
        {
            if (sentence.Length < shortest.Length && sentence.Length > 0)
            {
                shortest = sentence;
            }
        }
        return shortest;
    }

    public bool ContainsSentence(string sentence)
    {
        return text.Contains(sentence);
    }
    public bool EqualSentence(string sentence,string sentence1)
    {
        return sentence.Equals(sentence1, StringComparison.OrdinalIgnoreCase);
    }

    public void SerializeToJson(string filePath)
    {
        string json = JsonConvert.SerializeObject(this);
        File.WriteAllText(filePath, json);
    }


    public static TextManager DeserializeFromJson(string filePath)
    {
        string json = File.ReadAllText(filePath);
        return JsonConvert.DeserializeObject<TextManager>(json);
    }

 

}

class Program
{
    static void Main(string[] args)
    {
        Console.OutputEncoding = Encoding.UTF8;
        TextManager textManager = new TextManager("Wassup Bro,Nice How do u do? I just wanna tell u that u were invited to the party on Sunday.How do u do?.");
        Console.WriteLine("Оригінальний текст:");
        Console.WriteLine(textManager.GetText);
        Console.WriteLine("Порівняння речень: " + textManager.EqualSentence(textManager.GetText, "Wassup Bro,Nice How do u do? I just wanna tell u that u were invited to the party on Sunday.How do u do?."));
        textManager.AddSentence("Regards");
        Console.WriteLine("\nТекст після додавання речення:");
        Console.WriteLine(textManager.GetText);

        textManager.RemoveSentence("Regards");
        Console.WriteLine("\nТекст після видалення речення:");
        Console.WriteLine(textManager.GetText);

        textManager.InsertSentence(1, "How do u do?");
        Console.WriteLine("\nТекст після вставки речення:");
        Console.WriteLine(textManager.GetText);

        Console.WriteLine("\nКількість букв у тексті: " + textManager.CountLetters);
        Console.WriteLine("Кількість слів у тексті: " + textManager.CountWords);
        Console.WriteLine("Кількість речень у тексті: " + textManager.CountSentences);

        Console.WriteLine("\nРечення під номером 1: " + textManager.GetSentence(1));
        Console.WriteLine("Найдовше речення: " + textManager.LongestSentence());
        Console.WriteLine("Найкоротше речення: " + textManager.ShortestSentence());
        

        Console.WriteLine("\nТекст містить речення 'Wassup Bro': " + textManager.ContainsSentence("Wassup Bro"));


        string filePath = "text_manager.json";
        textManager.SerializeToJson(filePath);
        TextManager deserializedTextManager = TextManager.DeserializeFromJson(filePath);
        Console.WriteLine("\nDES successful");
        Console.WriteLine(deserializedTextManager.GetText);
    }
}








//Wassup Bro,Nice How do u do? I just wanna tell u that u were invited to the party on Sunday.How do u do?.
// Wassup Bro, How do u do? I just wanna tell u that u were invited to the party on Sunday.Nice too meet u