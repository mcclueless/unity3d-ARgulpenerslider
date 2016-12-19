using UnityEngine;
using System.Collections;
using System.Xml;
using UnityEngine.UI;
public class RSSCheck : MonoBehaviour
{
	//Feed URL to grab XML
	public string url = "http://www.nu.nl/rss/Algemeen";
	//Text UI Component Reference
	public Text textNode;
	//Button to change text
	public Button textSwapButton;
	//All description nodes
	public string[] descriptions;
	//currently selected node
	public int currentTextNode = 0;
	void Awake()
	{
		//remove all onClick and add a change to text
		textSwapButton.onClick.RemoveAllListeners();
		textSwapButton.onClick.AddListener(() => { ChangeText(); });
	}
	void Start()
	{
		StartCoroutine(News());
	}
	public void ChangeText()
	{
		//Check if larger than array, if so, revert to first elem
		if ((currentTextNode + 1) <= (descriptions.Length - 1))
			currentTextNode += 1;
		else
			currentTextNode = 0;
		//change and display text
		textNode.text = descriptions[currentTextNode];
	}
	public IEnumerator News()
	{
		//Create WWW Object via URL
		WWW www = new WWW(url);
		yield return www;
		//wait until done
		if (www.isDone)
		{
			//create instance of XML Document
			XmlDocument xmlDoc = new XmlDocument();
			//Load XML from WWW String (Entirety of XML)
			xmlDoc.LoadXml(www.text);
			//get all item nodes and push to list
			XmlNodeList xmlNodeList = xmlDoc.SelectNodes("rss/channel/item");
			//descriptions length equal to node list
			descriptions = new string[xmlNodeList.Count];
			//grab each description and associate to array
			for (int i = 0; i < xmlNodeList.Count; i++)
				descriptions[i] = xmlNodeList[i].SelectSingleNode("description").InnerText;
			//if anything grabbed appropriately, assign first text to currentnode
			if (descriptions.Length > 0)
				textNode.text = descriptions[currentTextNode];
			//Quick Notification Stating completion
			Debug.Log("Complete");
		}
	}
}
// using UnityEngine;
// using System.Collections;
// using System.Xml;
// using UnityEngine.UI;
// public class RSSCheck : MonoBehaviour
// {
//	public string url = "http://www.nu.nl/rss/Algemeen";
//	public Text textNode;
//	void Start()
//	{
//		StartCoroutine(News());
//	}
//	public IEnumerator News()
//	{
//		WWW www = new WWW(url);
//		yield return www;
//		if (www.isDone)
//		{
//			XmlDocument xmlDoc = new XmlDocument();
//			xmlDoc.LoadXml(www.text);
//			XmlNodeList xmlNodeList = xmlDoc.SelectNodes("rss/channel/item/title");
//			foreach (XmlNode xmlNode in xmlNodeList)
//				if (textNode != null)
//				{
//					textNode.text = xmlNode.InnerText;
//					break;
//				}
//			Debug.Log("Complete");
//		}
//	}
//}