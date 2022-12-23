use reqwest::blocking::Client;
use serde::{Deserialize, Serialize};

#[derive(Debug, Deserialize, Serialize)]
struct NewsItem {
    title: String,
    author: String,
    date: String,
}

#[derive(Debug, Deserialize, Serialize)]
struct NewsSystem {
    items: Vec<NewsItem>,
}

fn main() -> Result<(), Box<dyn std::error::Error>> {
    let client = Client::new();

    // Query the JSON data via the URL
    let response = client
        .get("https://raw.githubusercontent.com/ngmisl/rust-news/main/news.json")
        .send()?
        .text()?;

    // Deserialize the JSON data into a Data struct;
    let news_system: NewsSystem = serde_json::from_str(&response)?;

    // You can now access the data in the NewsSystem struct
    println!("{:?}", news_system);

    Ok(())
}
