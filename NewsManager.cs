using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Net;

public class NewsManager : MonoBehaviour
{
   public Text newsText; // Text UI element to display the news

   private void Start()
   {
       StartCoroutine(GetNews()); // Start the coroutine to get the news data
   }

   private IEnumerator GetNews()
   {
       // Set up the request to the JSON URL
       string url = "https://raw.githubusercontent.com/ngmisl/mwgnews/main/news.json";
       var request = WebRequest.Create(url);
       request.Method = "GET";
       request.ContentType = "application/json";
       var response = request.GetResponse();

       // Read the response and store it in a string
       using (var streamReader = new System.IO.StreamReader(response.GetResponseStream()))
       {
           string jsonResponse = streamReader.ReadToEnd();

           // Deserialize the JSON string into a News object
           News news = JsonUtility.FromJson<News>(jsonResponse);

           // Update the news text with the titles and authors of all the news items
           newsText.text = "";
           foreach (NewsItem item in news.items)
           {
               newsText.text += item.date + "\n" + item.title;
           }
       }
    Debug.Log(newsText.text);

       yield return null;
   }
}

// Class to hold the news data
[System.Serializable]
public class News
{
   public NewsItem[] items;
}

[System.Serializable]
public class NewsItem
{
   public string title;
   public string author;
   public string date;
}